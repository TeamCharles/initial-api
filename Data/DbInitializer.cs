using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Bangazon.Models;
using System.Collections.Generic;

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
                  },
                  new Product {
                      Description = "Metal Lemon Squeezer",
                      ProductTypeId = productTypes.Single(s => s.Label == "Housewares").ProductTypeId,
                      Name = "Supreme Housewares Lemon Squeezer",
                      Price = 5.49f,
                      UserId = users.Single(s => s.FirstName == "Tractor").UserId,
                      IsActive = true
                  },
                  new Product {
                      Description = "Factory Unlocked Phone. 32GB Memory",
                      ProductTypeId = productTypes.Single(s => s.Label == "Electronics").ProductTypeId,
                      Name = "Samsung Galaxy S7",
                      Price = 563.49f,
                      UserId = users.Single(s => s.FirstName == "Steve").UserId,
                      IsActive = true
                  },
                  new Product {
                      Description = "Factory Unlocked Phone. 32GB Memory.Retina Display",
                      ProductTypeId = productTypes.Single(s => s.Label == "Electronics").ProductTypeId,
                      Name = "Apple Iphone 7",
                      Price = 7.49f,
                      UserId = users.Single(s => s.FirstName == "Tractor").UserId,
                      IsActive = true
                  },
                  new Product {
                      Description = "Big sound--with deep bass--for a full-range listening experience",
                      ProductTypeId = productTypes.Single(s => s.Label == "Electronics").ProductTypeId,
                      Name = "Bose Soundlink Mini",
                      Price = 199.00f,
                      UserId = users.Single(s => s.FirstName == "Steve").UserId,
                      IsActive = true
                  },
                  new Product {
                      Description = "Nose Pad and Special designed Head Strap - increase your comfort.",
                      ProductTypeId = productTypes.Single(s => s.Label == "Electronics").ProductTypeId,
                      Name = "Google Cardboard V2",
                      Price = 10.49f,
                      UserId = users.Single(s => s.FirstName == "Carson").UserId,
                      IsActive = true
                  },
                  new Product {
                      Description = "4K Enhancement Technology - accepts 4K input and supports HDCP 2.2 for truly immersive scenes with 4K content",
                      ProductTypeId = productTypes.Single(s => s.Label == "Electronics").ProductTypeId,
                      Name = "Epson Home Cinema 5040UB",
                      Price = 7.49f,
                      UserId = users.Single(s => s.FirstName == "Tractor").UserId,
                      IsActive = true
                  },
                  new Product {
                      Description = "Grills sandwiches of any thickness.",
                      ProductTypeId = productTypes.Single(s => s.Label == "Appliances").ProductTypeId,
                      Name = "Hamilton Beach 25460A Panini Press",
                      Price = 24.94f,
                      UserId = users.Single(s => s.FirstName == "Steve").UserId,
                      IsActive = true
                  },
                  new Product {
                      Description = "Simply fill the measuring up to the line of desired consistency, and push the on/off button to start and it will automatically shut off when eggs are done",
                      ProductTypeId = productTypes.Single(s => s.Label == "Appliances").ProductTypeId,
                      Name = "Dash Go Rapid Egg Cooker,",
                      Price = 14.49f,
                      UserId = users.Single(s => s.FirstName == "Tractor").UserId,
                      IsActive = true
                  },
                  new Product {
                      Description = "Makes 6-8 Cups of Kettle Style Popcorn.",
                      ProductTypeId = productTypes.Single(s => s.Label == "Appliances").ProductTypeId,
                      Name = "Disney Mickey Kettle Style Popcorn Popper",
                      Price =25.00f,
                      UserId = users.Single(s => s.FirstName == "Steve").UserId,
                      IsActive = true
                  },
                  new Product {
                      Description = "DURABILITY - The higest level of cut resistant material - Level FIVE. 4x stronger than leather!",
                      ProductTypeId = productTypes.Single(s => s.Label == "Housewares").ProductTypeId,
                      Name = "SimpleHouseware Cut Resistant Gloves",
                      Price = 7.87f,
                      UserId = users.Single(s => s.FirstName == "Carson").UserId,
                      IsActive = true
                  },
                  new Product {
                      Description = "Made of high quality porcelain. 14-Ounce capacity",
                      ProductTypeId = productTypes.Single(s => s.Label == "Housewares").ProductTypeId,
                      Name = "Yedi Houseware Classic Coffee and Tea Siena Tea",
                      Price = 21.49f,
                      UserId = users.Single(s => s.FirstName == "Tractor").UserId,
                      IsActive = true
                  },
                  new Product {
                      Description = "Solid pinewood exterior bucket; aluminum mixing canister for fast freezing",
                      ProductTypeId = productTypes.Single(s => s.Label == "Appliances").ProductTypeId,
                      Name = "Ice-Cream Maker",
                      Price = 49.50f,
                      UserId = users.Single(s => s.FirstName == "Steve").UserId,
                      IsActive = true
                  },
                  new Product {
                      Description = " Its unique Patented suction bottom and round design makes it very easy to use.",
                      ProductTypeId = productTypes.Single(s => s.Label == "Appliances").ProductTypeId,
                      Name = "SunrisePro Knife Sharpener",
                      Price = 7.49f,
                      UserId = users.Single(s => s.FirstName == "Tractor").UserId,
                      IsActive = true
                  },
                  new Product {
                      Description = "Patented Real Flame appears identical to a lit candle using electromagnetics and glowing LEDs",
                      ProductTypeId = productTypes.Single(s => s.Label == "Housewares").ProductTypeId,
                      Name = "Mystique Flameless Candle",
                      Price = 35.60f,
                      UserId = users.Single(s => s.FirstName == "Steve").UserId,
                      IsActive = true
                  },
                  new Product {
                      Description = "Type: top load. Agitator: yes. Stainless Steel drum: yes",
                      ProductTypeId = productTypes.Single(s => s.Label == "Appliances").ProductTypeId,
                      Name = "Top Load Washer",
                      Price = 800.49f,
                      UserId = users.Single(s => s.FirstName == "Carson").UserId,
                      IsActive = true
                  },
                  new Product {
                      Description = "Small Portable washing machine goes anywhere with only 28lbs weight",
                      ProductTypeId = productTypes.Single(s => s.Label == "Appliances").ProductTypeId,
                      Name = "Portable Washing Machine",
                      Price = 149.49f,
                      UserId = users.Single(s => s.FirstName == "Tractor").UserId,
                      IsActive = true
                  },
                  new Product {
                      Description = "A 2015 Powerful vacuum cleaner",
                      ProductTypeId = productTypes.Single(s => s.Label == "Appliances").ProductTypeId,
                      Name = "Commercial Upright Vacuum",
                      Price = 18.00f,
                      UserId = users.Single(s => s.FirstName == "Steve").UserId,
                      IsActive = true
                  },
                  new Product {
                      Description = "Full keyboard with LCD, speaker, microphone & flash memory",
                      ProductTypeId = productTypes.Single(s => s.Label == "Electronics").ProductTypeId,
                      Name = "Speaking Vocabulary Builder ",
                      Price = 9.99f,
                      UserId = users.Single(s => s.FirstName == "Tractor").UserId,
                      IsActive = true
                  },
                  new Product {
                      Description = "Provides cleaner, fresher air in your home using the power of UV-C light technology.",
                      ProductTypeId = productTypes.Single(s => s.Label == "Appliances").ProductTypeId,
                      Name = "UV Sanitizer and Odor Reducer",
                      Price = 35.00f,
                      UserId = users.Single(s => s.FirstName == "Steve").UserId,
                      IsActive = true
                  },
                  new Product {
                      Description = "Set of 4 Pink bath towels for your home, dorm room or Spa.",
                      ProductTypeId = productTypes.Single(s => s.Label == "Housewares").ProductTypeId,
                      Name = "Cotton Bath Towels",
                      Price = 324.00f,
                      UserId = users.Single(s => s.FirstName == "Carson").UserId,
                      IsActive = true
                  },
                  new Product {
                      Description = "Compatible with your favorite classic NES games!",
                      ProductTypeId = productTypes.Single(s => s.Label == "Electronics").ProductTypeId,
                      Name = "Retro Bit Nintendo NES Entertainment System ",
                      Price = 7.49f,
                      UserId = users.Single(s => s.FirstName == "Tractor").UserId,
                      IsActive = true
                  },
                  new Product {
                      Description = "250-watt food processor with 3-cup plastic work bowl",
                      ProductTypeId = productTypes.Single(s => s.Label == "Appliances").ProductTypeId,
                      Name = "Food Processor",
                      Price = 35.99f,
                      UserId = users.Single(s => s.FirstName == "Steve").UserId,
                      IsActive = true
                  }
              };

              foreach (Product i in products)
              {
                  context.Product.Add(i);
              }

              // Creating a new Order for Carson Alexander
              Order carsonOrder = new Order {
                  UserId = 1,
              };

              Order otherOrder = new Order {
                  UserId = 2,
              };

              context.Order.Add(carsonOrder);
              context.SaveChanges();

              context.Order.Add(otherOrder);
              context.SaveChanges();

              // Populating the Order with Line Items
              LineItem[] carsonOrderLineItems = new LineItem[] {
                  new LineItem(){ OrderId = 1, ProductId = 1 },
                  new LineItem(){ OrderId = 1, ProductId = 2 },
              };

              foreach (LineItem item in carsonOrderLineItems)
              {
                  context.LineItem.Add(item);
              }

              context.SaveChanges();

              // Populating the Other Order with Line Items
              LineItem[] otherOrderLineItems = new LineItem[] {
                  new LineItem(){ OrderId = 2, ProductId = 3 }
              };

              foreach (LineItem item in otherOrderLineItems)
              {
                  context.LineItem.Add(item);
              }

              context.SaveChanges();
          }
       }
    }
}