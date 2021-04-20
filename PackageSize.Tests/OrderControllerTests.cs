using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PackageSize.Domain;
using PackageSize.Web.Controllers;
using PackageSize.Web.Models;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace PackageSize.Tests
{
    public abstract class OrderControllerTests
    {
        protected DbContextOptions<OrderContext> ContextOptions { get; }

        protected OrderControllerTests(DbContextOptions<OrderContext> contextOptions)
        {
            ContextOptions = contextOptions;
        }

        private void Seed()
        {
            using (var context = new OrderContext(ContextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var orderItemOne = new OrderItem
                {
                    ProductType = ProductType.PhotoBook,
                    Quantity = 1
                };

                var orderItemTwo = new OrderItem
                {
                    ProductType = ProductType.Mug,
                    Quantity = 3
                };

                var orderOne = new Order
                {
                    OrderID = 1,
                    OrderItems = new List<OrderItem>
                    {
                        orderItemOne,
                        orderItemTwo
                    }
                };

                context.Add(orderOne);
                context.SaveChanges();
            }
        }

        [Fact]
        public void Can_get_order()
        {
            Seed();

            using (var context = new OrderContext(ContextOptions))
            {
                var controller = new OrderController(context);

                var order = controller.GetOrder(1).Result.Value;

                Assert.Equal(1, order.OrderID);
                Assert.Equal(2, order.OrderItems.Count());
                Assert.Equal(ProductType.PhotoBook, order.OrderItems.ElementAt(0).ProductType);
                Assert.Equal(1, order.OrderItems.ElementAt(0).Quantity);
                Assert.Equal(ProductType.Mug, order.OrderItems.ElementAt(1).ProductType);
                Assert.Equal(3, order.OrderItems.ElementAt(1).Quantity);
                Assert.Equal(113, order.RequiredBinWidth);
            }
        }

        [Fact]
        public void Can_post_order()
        {
            using (var context = new OrderContext(ContextOptions))
            {
                var controller = new OrderController(context);

                var orderItemOne = new OrderItem
                {
                    ProductType = ProductType.PhotoBook,
                    Quantity = 1
                };

                var orderItemTwo = new OrderItem
                {
                    ProductType = ProductType.Mug,
                    Quantity = 3
                };

                var orderTwo = new Order
                {
                    OrderID = 2,
                    OrderItems = new List<OrderItem>
                    {
                        orderItemOne,
                        orderItemTwo
                    }
                };

                var order = controller.PostOrder(orderTwo).Result.Result as CreatedAtActionResult;
                var orderValue = order.Value as Order;

                Assert.Equal(2, orderValue.OrderID);
                Assert.Equal(2, orderValue.OrderItems.Count());
                Assert.Equal(ProductType.PhotoBook, orderValue.OrderItems.ElementAt(0).ProductType);
                Assert.Equal(1, orderValue.OrderItems.ElementAt(0).Quantity);
                Assert.Equal(ProductType.Mug, orderValue.OrderItems.ElementAt(1).ProductType);
                Assert.Equal(3, orderValue.OrderItems.ElementAt(1).Quantity);
                Assert.Equal(113, orderValue.RequiredBinWidth);
            }
        }

        [Fact]
        public void Can_not_get_nonexistent_order()
        {
            using (var context = new OrderContext(ContextOptions))
            {
                var controller = new OrderController(context);

                var order = controller.GetOrder(999).Result.Result as NotFoundResult;

                Assert.Equal(404, order.StatusCode);
            }
        }

        [Fact]
        public void Can_not_post_empty_order()
        {
            using (var context = new OrderContext(ContextOptions))
            {
                var controller = new OrderController(context);

                var emptyOrder = new Order
                {
                    OrderID = 3,
                    OrderItems = new List<OrderItem>()
                };

                var order = controller.PostOrder(emptyOrder).Result.Result as BadRequestResult;

                Assert.Equal(400, order.StatusCode);
            }
        }

        [Fact]
        public void Can_not_post_duplicated_order()
        {
            using (var context = new OrderContext(ContextOptions))
            {
                var controller = new OrderController(context);

                var orderItemOne = new OrderItem
                {
                    ProductType = ProductType.PhotoBook,
                    Quantity = 1
                };

                var orderItemTwo = new OrderItem
                {
                    ProductType = ProductType.Mug,
                    Quantity = 3
                };

                var orderThree = new Order
                {
                    OrderID = 3,
                    OrderItems = new List<OrderItem>
                    {
                        orderItemOne,
                        orderItemTwo
                    }
                };

                var orderFour = new Order
                {
                    OrderID = 3,
                    OrderItems = new List<OrderItem>
                    {
                        orderItemOne,
                        orderItemTwo
                    }
                };

                _ = controller.PostOrder(orderThree);
                var order = controller.PostOrder(orderFour).Result.Result as BadRequestResult;
                
                Assert.Equal(400, order.StatusCode);
            }
        }
    }
}
