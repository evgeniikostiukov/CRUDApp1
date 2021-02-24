using CRUDApp.Models;
using System;
using System.Linq;


namespace CRUDApp.Data
{
    public static class DbInitializer
    {
        public static void Initialize(OrderContext context)
        {
            context.Database.EnsureCreated();

            if (context.Providers.Any())
            {
                return;
            }

            var providers = new Provider[]
            {
            new Provider{Name="Provider1"},
            new Provider{Name="Provider2"},
            new Provider{Name="Provider3"},
            new Provider{Name="Provider4"},
            new Provider{Name="Provider5"},
            new Provider{Name="Provider6"},
            new Provider{Name="Provider7"}
            };
            foreach (Provider e in providers)
            {
                context.Providers.Add(e);
            }
            context.SaveChanges();
            
            if (context.Orders.Any())
            {
                return;
            }

            var orders = new Order[]
            {
            new Order{Number="Order 1",Date=DateTime.Parse("2021-02-01"), ProviderID=1},
            new Order{Number="Order 2",Date=DateTime.Parse("2021-02-02"), ProviderID=2},
            new Order{Number="Order 3",Date=DateTime.Parse("2021-02-03"), ProviderID=3},
            new Order{Number="Order 4",Date=DateTime.Parse("2021-02-04"), ProviderID=3},
            new Order{Number="Order 4",Date=DateTime.Parse("2021-02-05"), ProviderID=4},
            new Order{Number="Order 5",Date=DateTime.Parse("2021-02-06"), ProviderID=5},
            new Order{Number="Order 6",Date=DateTime.Parse("2021-02-07"), ProviderID=6},
            new Order{Number="Order 7",Date=DateTime.Parse("2021-02-08"), ProviderID=7}
            };
            foreach (Order s in orders)
            {
                context.Orders.Add(s);
            }
            context.SaveChanges();

            if (context.OrderItems.Any())
            {
                return;
            }

            var orderItems = new OrderItem[]
            {
            new OrderItem{OrderID=2,Name="ExampleItem1",Quantity=2,Unit="km"},
            new OrderItem{OrderID=2,Name="ExampleItem2",Quantity=3,Unit="m"},
            new OrderItem{OrderID=3,Name="ExampleItem3",Quantity=2,Unit="kg"},
            new OrderItem{OrderID=4,Name="ExampleItem4",Quantity=5,Unit="sm"},
            new OrderItem{OrderID=5,Name="ExampleItem5",Quantity=10,Unit="ft"},
            new OrderItem{OrderID=5,Name="ExampleItem6",Quantity=11,Unit="cm2"},
            new OrderItem{OrderID=4,Name="ExampleItem7",Quantity=9,Unit="l"}
            };
            foreach (OrderItem c in orderItems)
            {
                context.OrderItems.Add(c);
            }
            context.SaveChanges();
        }
    }
}
