using Microsoft.AspNetCore.Mvc;
using PackageSize.Domain;
using PackageSize.Web.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PackageSize.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly OrderContext _context;

        public OrderController(OrderContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            // A customer can order 1 or multiple items.
            if (order.OrderItems.Count() == 0)
            {
                return BadRequest();
            }

            try
            {
                _context.Add(order);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetOrder), new { orderID = order.OrderID }, order);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("{orderID}")]
        public async Task<ActionResult<Order>> GetOrder(int orderID)
        {
            var order = await _context.FindAsync<Order>(orderID);

            if (order == null)
            {
                return NotFound();
            }
            
            // Get Order.OrderItems
            await _context.Entry(order).Collection(o => o.OrderItems).LoadAsync();

            return order;
        }
    }
}
