using Microsoft.AspNetCore.Mvc;
using PackageSize.Domain;
using PackageSize.Web.Models;
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
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrder), new { orderID = order.OrderID }, order);
        }

        [HttpGet("{orderID}")]
        public async Task<ActionResult<Order>> GetOrder(int orderID)
        {
            var order = await _context.Orders.FindAsync(orderID);

            if (order == null)
            {
                return NotFound();
            }
            else
            {
                await _context.Entry(order).Collection(o => o.OrderItems).LoadAsync();

                return order;
            }
        }
    }
}
