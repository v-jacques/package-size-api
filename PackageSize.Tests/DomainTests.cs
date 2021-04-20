using PackageSize.Domain;
using System.Collections.Generic;
using Xunit;

namespace PackageSize.Tests
{
    public class DomainTests
    {
        [Fact]
        public void Required_bin_width_four_mugs()
        {
            var orderItem = new OrderItem
            {
                ProductType = ProductType.Mug,
                Quantity = 4
            };

            var order = new Order
            {
                OrderID = 1,
                OrderItems = new List<OrderItem>
                    {
                        orderItem
                    }
            };

            Assert.Equal(94, order.RequiredBinWidth);
        }

        [Fact]
        public void Required_bin_width_five_mugs()
        {
            var orderItem = new OrderItem
            {
                ProductType = ProductType.Mug,
                Quantity = 5
            };

            var order = new Order
            {
                OrderID = 1,
                OrderItems = new List<OrderItem>
                    {
                        orderItem
                    }
            };

            Assert.Equal(188, order.RequiredBinWidth);
        }

        [Fact]
        public void Required_bin_width_all_products()
        {
            var orderItemOne = new OrderItem
            {
                ProductType = ProductType.Calendar,
                Quantity = 1
            };

            var orderItemTwo = new OrderItem
            {
                ProductType = ProductType.Canvas,
                Quantity = 1
            };

            var orderItemThree = new OrderItem
            {
                ProductType = ProductType.Cards,
                Quantity = 1
            };

            var orderItemFour = new OrderItem
            {
                ProductType = ProductType.Mug,
                Quantity = 1
            };

            var orderItemFive = new OrderItem
            {
                ProductType = ProductType.PhotoBook,
                Quantity = 1
            };

            var order = new Order
            {
                OrderID = 1,
                OrderItems = new List<OrderItem>
                    {
                        orderItemOne,
                        orderItemTwo,
                        orderItemThree,
                        orderItemFour,
                        orderItemFive
                    }
            };

            Assert.Equal(143.7, order.RequiredBinWidth);
        }
    }
}
