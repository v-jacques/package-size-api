using Microsoft.EntityFrameworkCore;
using PackageSize.Web.Models;

namespace PackageSize.Tests
{
    public class InMemoryOrderControllerTests : OrderControllerTests
    {
        public InMemoryOrderControllerTests()
            : base(
                  new DbContextOptionsBuilder<OrderContext>()
                    .UseInMemoryDatabase("TestOrders")
                    .Options)
        {

        }
    }
}
