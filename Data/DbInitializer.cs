using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Bangazon.Models;

namespace BangazonWeb.Data
{
    public static class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BangazonContext(serviceProvider.GetRequiredService<DbContextOptions<BangazonContext>>()))
            {
              // Look for any products.
              if (context.Product.Any())
              {
                  return;   // DB has been seeded
              }

              var users = new User[]
              {
                  new User { 
                      FirstName = "Carson",
                      LastName = "Alexander",
                      StreetAddress = "100 Infinity Way",
                      City = "St. Paul",
                      State = "Minnesota",
                      ZipCode = 12345
                  },
                  new User { 
                      FirstName = "Steve",
                      LastName = "Brownlee",
                      StreetAddress = "92 Main Street",
                      City = "Nashville",
                      State = "Tennessee",
                      ZipCode = 37212
                  },
                  new User { 
                      FirstName = "Tractor",
                      LastName = "Ryan",
                      StreetAddress = "1666 Catalina Blvd",
                      City = "Los Angeles",
                      State = "California",
                      ZipCode = 55555
                  }
              };

              foreach (User c in users)
              {
                  context.User.Add(c);
              }
              context.SaveChanges();

              var productTypes = new ProductType[]
              {
                  new ProductType { 
                      Label = "Electronics"
                  },
                  new ProductType { 
                      Label = "Appliances"
                  },
                  new ProductType { 
                      Label = "Housewares"
                  },
              };

              foreach (ProductType i in productTypes)
              {
                  context.ProductType.Add(i);
              }
              context.SaveChanges();


              var products = new Product[]
              {
                  new Product { 
                      Description = "Colorful throw pillows to liven up your home",
                      ProductTypeId = productTypes.Single(s => s.Label == "Housewares").ProductTypeId,
                      Name = "Throw Pillow",
                      Price = 7.49f,
                      UserId = users.Single(s => s.FirstName == "Tractor").UserId,
                      IsActive = true
                  },
                  new Product { 
                      Description = "A 2012 iPod Shuffle. Headphones are included. 16G capacity.",
                      ProductTypeId = productTypes.Single(s => s.Label == "Electronics").ProductTypeId,
                      Name = "iPod Shuffle",
                      Price = 18.00f,
                      UserId = users.Single(s => s.FirstName == "Steve").UserId,
                      IsActive = true
                  },
                  new Product { 
                      Description = "Stainless steel refrigerator. Three years old. Minor scratches.",
                      ProductTypeId = productTypes.Single(s => s.Label == "Appliances").ProductTypeId,
                      Name = "Samsung refrigerator",
                      Price = 500.00f,
                      UserId = users.Single(s => s.FirstName == "Carson").UserId,
                      IsActive = true
                  }
              };

              foreach (Product i in products)
              {
                  context.Product.Add(i);
              }
              context.SaveChanges();
          }
       }
    }
}