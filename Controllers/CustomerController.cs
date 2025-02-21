using DagnysBageriApi.Data;
using DagnysBageriApi.Entities;
using DagnysBageriApi.Models.RequestModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DagnysBageriApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController(DataContext context) : ControllerBase
    {
        private readonly DataContext _context = context;

        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            var customers = await _context.Customers.ToListAsync();
            return Ok(new { success = true, customers });
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetCustomer(int customerId)
        {
            var customer = await _context.Customers
                .Include(c => c.Orders) // Include order history
                .FirstOrDefaultAsync(c => c.CustomerId == customerId);

            if (customer == null)
                return NotFound(new { success = false, message = $"Customer with ID '{customerId}' not found." });

            return Ok(new { success = true, customer });
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] AddCustomerRequestModel request)
        {
            if (await _context.Customers.AnyAsync(c => c.Email == request.Email))
            {
                return BadRequest(new { success = false, message = $"A customer with email '{request.Email}' already exists!" });
            }

            var customer = new Customer
            {
                StoreName = request.StoreName,
                ContactPerson = request.ContactPerson,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                DeliveryAddress = request.DeliveryAddress,
                InvoiceAddress = request.InvoiceAddress
            };

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCustomer), new { customerId = customer.CustomerId }, new { success = true, message = "Customer created successfully!", customer });
        }


        [HttpPatch("{customerId}")]
        public async Task<IActionResult> UpdateCustomer(int customerId, [FromBody] UpdateCustomerRequestModel request)
        {
            var customer = await _context.Customers.FindAsync(customerId);
            if (customer == null)
                return NotFound(new { success = false, message = $"Customer with ID '{customerId}' not found." });

            customer.StoreName = request.StoreName;
            customer.ContactPerson = request.ContactPerson;
            customer.PhoneNumber = request.PhoneNumber;
            customer.Email = request.Email;
            customer.DeliveryAddress = request.DeliveryAddress;
            customer.InvoiceAddress = request.InvoiceAddress;

            await _context.SaveChangesAsync();
            return Ok(new { success = true, message = "Customer updated successfully!" });
        }

        [HttpDelete("{customerId}")]
        public async Task<IActionResult> DeleteCustomer(int customerId)
        {
            var customer = await _context.Customers.FindAsync(customerId);
            if (customer == null)
                return NotFound(new { success = false, message = $"Customer with ID '{customerId}' not found." });

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return Ok(new { success = true, message = "Customer deleted successfully!" });
        }

        [HttpGet("name/{customerName}")]
        public async Task<IActionResult> GetCustomerByName(string customerName)
        {
            customerName = customerName.Trim().ToLower();

            var customer = await _context.Customers
                .Include(c => c.Orders)
                    .ThenInclude(o => o.OrderItems)
                        .ThenInclude(oi => oi.Product)  
                .FirstOrDefaultAsync(c => c.StoreName.ToLower() == customerName);

            if (customer == null)
            {
                return NotFound(new { success = false, message = $"Customer '{customerName}' not found!" });
            }

            var customerDetails = new
            {
                customer.CustomerId,
                customer.StoreName,
                customer.ContactPerson,
                customer.PhoneNumber,
                customer.Email,
                customer.DeliveryAddress,
                customer.InvoiceAddress,
                OrderHistory = customer.Orders.Select(order => new
                {
                    order.OrderId,
                    order.OrderDate,
                    order.OrderNumber,
                    OrderItems = order.OrderItems.Select(orderItem => new
                    {
                        ProductName = orderItem.Product.Name,
                        orderItem.Quantity,
                        UnitPrice = orderItem.Price,
                        TotalPrice = orderItem.TotalPrice
                    }).ToList()
                }).ToList()
            };

            return Ok(new { success = true, customer = customerDetails });
        }
    }
}

