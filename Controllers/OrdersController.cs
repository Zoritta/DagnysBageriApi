
// using DagnysBageriApi.Data;
// using DagnysBageriApi.Models.ResponseModels;
// using Microsoft.AspNetCore.Mvc;
// using DagnysBageriApi.Entities;
// using DagnysBageriApi.Models.RequestModels;
// using Microsoft.EntityFrameworkCore;

// namespace DagnysBageriApi.Controllers
// {
//     [ApiController]
//     [Route("api/[controller]")]
//     public class OrdersController : ControllerBase
//     {
//         private readonly DataContext _context;

//         public OrdersController(DataContext context)
//         {
//             _context = context;
//         }

//         [HttpPost]
//         public async Task<ActionResult<OrderResponseModel>> AddOrder([FromBody] CreateOrderRequestModel request)
//         {
//             if (request == null || !request.OrderItems.Any())
//             {
//                 return BadRequest(new { success = false, message = "Invalid order data." });
//             }

//             var customer = await _context.Customers.FirstOrDefaultAsync(c => c.CustomerId == request.CustomerId);
//             if (customer == null)
//             {
//                 return NotFound(new { success = false, message = "Customer not found." });
//             }

//             var order = new Order
//             {
//                 OrderDate = DateTime.Now,
//                 OrderNumber = Guid.NewGuid().ToString(),
//                 CustomerId = customer.CustomerId,
//                 Customer = customer
//             };

//             foreach (var item in request.OrderItems)
//             {
//                 var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == item.ProductId);
//                 if (product == null)
//                 {
//                     return NotFound(new { success = false, message = $"Product with ID {item.ProductId} not found." });
//                 }

//                 order.OrderItems.Add(new OrderItem
//                 {
//                     ProductId = product.ProductId,
//                     Product = product,
//                     Quantity = item.Quantity,
//                     Price = product.Price
//                 });
//             }

//             _context.Orders.Add(order);
//             await _context.SaveChangesAsync();

//             var orderResponseModel = new OrderResponseModel
//             {
//                 OrderId = order.OrderId,
//                 OrderNumber = order.OrderNumber,
//                 OrderDate = order.OrderDate,
//                 StoreName = customer.StoreName,
//                 ContactPerson = customer.ContactPerson,
//                 Email = customer.Email,
//                 OrderItems = order.OrderItems.Select(oi => new OrderItemResponse
//                 {
//                     ProductName = oi.Product.Name,
//                     Quantity = oi.Quantity,
//                     Price = oi.Price,
//                     TotalPrice = oi.TotalPrice
//                 }).ToList(),
//                 TotalOrderPrice = order.OrderItems.Sum(oi => oi.TotalPrice)
//             };

//             return CreatedAtAction(nameof(GetOrdersByCustomer), new { orderId = orderResponseModel.OrderId }, orderResponseModel);
//         }

//         [HttpGet("by-customer/{storeName}")]
//         public async Task<ActionResult<IEnumerable<OrderResponseModel>>> GetOrdersByCustomer(string storeName)
//         {
//             var normalizedStoreName = storeName.Trim().Replace(" ", "").ToLower();

//             var orders = await _context.Orders
//                 .Include(o => o.Customer)
//                 .Include(o => o.OrderItems)
//                     .ThenInclude(oi => oi.Product)
//                 .Where(o => o.Customer.StoreName.Replace(" ", "").ToLower() == normalizedStoreName)
//                 .ToListAsync();

//             if (!orders.Any())
//             {
//                 return NotFound(new { success = false, message = "No orders found for this store." });
//             }

//             var orderResponseModels = orders.Select(order => new OrderResponseModel
//             {
//                 OrderId = order.OrderId,
//                 OrderNumber = order.OrderNumber,
//                 OrderDate = order.OrderDate,
//                 StoreName = order.Customer.StoreName,
//                 ContactPerson = order.Customer.ContactPerson,
//                 Email = order.Customer.Email,
//                 OrderItems = order.OrderItems.Select(oi => new OrderItemResponse
//                 {
//                     ProductName = oi.Product.Name,
//                     Quantity = oi.Quantity,
//                     Price = oi.Price,
//                     TotalPrice = oi.TotalPrice
//                 }).ToList(),
//                 TotalOrderPrice = order.OrderItems.Sum(oi => oi.TotalPrice)
//             }).ToList();

//             return Ok(orderResponseModels);
//         }

//         [HttpGet]
//         public async Task<ActionResult<IEnumerable<OrderResponseModel>>> GetOrders()
//         {
//             var orders = await _context.Orders
//                 .Include(o => o.Customer)
//                 .Include(o => o.OrderItems)
//                     .ThenInclude(oi => oi.Product)
//                 .ToListAsync();

//             var orderResponseModels = orders.Select(order => new OrderResponseModel
//             {
//                 OrderId = order.OrderId,
//                 OrderNumber = order.OrderNumber,
//                 OrderDate = order.OrderDate,
//                 StoreName = order.Customer.StoreName,
//                 ContactPerson = order.Customer.ContactPerson,
//                 Email = order.Customer.Email,
//                 OrderItems = order.OrderItems.Select(oi => new OrderItemResponse
//                 {
//                     ProductName = oi.Product.Name,
//                     Quantity = oi.Quantity,
//                     Price = oi.Price,
//                     TotalPrice = oi.TotalPrice
//                 }).ToList(),
//                 TotalOrderPrice = order.OrderItems.Sum(oi => oi.TotalPrice)
//             }).ToList();

//             return Ok(orderResponseModels);
//         }

//         [HttpGet("search")]
//         public async Task<ActionResult<IEnumerable<OrderResponseModel>>> SearchOrders([FromQuery] string orderNumber, [FromQuery] DateTime? orderDate)
//         {
//             IQueryable<Order> query = _context.Orders
//                 .Include(o => o.Customer)
//                 .Include(o => o.OrderItems)
//                     .ThenInclude(oi => oi.Product);

//             if (!string.IsNullOrEmpty(orderNumber))
//             {
//                 query = query.Where(o => o.OrderNumber.Contains(orderNumber));
//             }

//             if (orderDate.HasValue)
//             {
//                 query = query.Where(o => o.OrderDate.Date == orderDate.Value.Date);
//             }

//             var orders = await query.ToListAsync();

//             var orderResponsModels = orders.Select(order => new OrderResponseModel
//             {
//                 OrderId = order.OrderId,
//                 OrderNumber = order.OrderNumber,
//                 OrderDate = order.OrderDate,
//                 StoreName = order.Customer.StoreName,
//                 ContactPerson = order.Customer.ContactPerson,
//                 Email = order.Customer.Email,
//                 OrderItems = order.OrderItems.Select(oi => new OrderItemResponse
//                 {
//                     ProductName = oi.Product.Name,
//                     Quantity = oi.Quantity,
//                     Price = oi.Price,
//                     TotalPrice = oi.TotalPrice
//                 }).ToList(),
//                 TotalOrderPrice = order.OrderItems.Sum(oi => oi.TotalPrice)
//             }).ToList();

//             return Ok(orderResponsModels);
//         }
//     }
// }