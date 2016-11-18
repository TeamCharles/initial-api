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

            context.SaveChanges();  // Seed users added


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

            context.SaveChanges();  // Seed product types added

              var productSubTypes =  new ProductSubType[]
              {
                  //1
                  new ProductSubType {
                      Label = "Portable Electronics",
                      ProductTypeId = 1
                  },

                  //2
                  new ProductSubType {
                      Label = "Video Game Systems",
                      ProductTypeId = 1
                  },

                  //3
                  new ProductSubType {
                      Label = "Kitchen Appliances",
                      ProductTypeId = 2
                  },

                  //4
                  new ProductSubType {
                      Label = "Yard Equipment",
                      ProductTypeId = 2
                  },

                  //5
                  new ProductSubType {
                      Label = "Living Room",
                      ProductTypeId = 3
                  },

                  //6
                  new ProductSubType {
                      Label = "Bedroom",
                      ProductTypeId = 3
                  }
              };

             foreach (ProductSubType i in productSubTypes)
              {
                  context.ProductSubType.Add(i);
              }
              context.SaveChanges();

            context.SaveChanges();  // Seed sub product types added

              var products = new Product[]
              {
                  new Product {
                      Description = "Colorful throw pillows to liven up your home",
                      ProductTypeId = productTypes.Single(s => s.Label == "Housewares").ProductTypeId,
                      ProductSubTypeId = 5,
                      Name = "Throw Pillow",
                      Price = 7.49M,
                      UserId = users.Single(s => s.FirstName == "Tractor").UserId,
                      IsActive = true
                  },
                  new Product {
                      Description = "A 2012 iPod Shuffle. Headphones are included. 16G capacity.",
                      ProductTypeId = productTypes.Single(s => s.Label == "Electronics").ProductTypeId,
                      ProductSubTypeId = 1,
                      Name = "iPod Shuffle",
                      Price = 18.00M,
                      UserId = users.Single(s => s.FirstName == "Steve").UserId,
                      IsActive = true
                  },
                  new Product {
                      Description = "Stainless steel refrigerator. Three years old. Minor scratches.",
                      ProductTypeId = productTypes.Single(s => s.Label == "Appliances").ProductTypeId,
                      ProductSubTypeId = 3,
                      Name = "Samsung refrigerator",
                      Price = 500.0M,
                      UserId = users.Single(s => s.FirstName == "Carson").UserId,
                      IsActive = true
                  },
                  new Product {
                      Description = "Metal Lemon Squeezer",
                      ProductTypeId = productTypes.Single(s => s.Label == "Housewares").ProductTypeId,
                      ProductSubTypeId = 5,
                      Name = "Supreme Housewares Lemon Squeezer",
                      Price = 5.49M,
                      UserId = users.Single(s => s.FirstName == "Tractor").UserId,
                      IsActive = true
                  },
                  new Product {
                      Description = "Factory Unlocked Phone. 32GB Memory",
                      ProductTypeId = productTypes.Single(s => s.Label == "Electronics").ProductTypeId,
                      ProductSubTypeId = 1,
                      Name = "Samsung Galaxy S7",
                      Price = 563.49M,
                      UserId = users.Single(s => s.FirstName == "Steve").UserId,
                      IsActive = true
                  },
                  new Product {
                      Description = "Factory Unlocked Phone. 32GB Memory.Retina Display",
                      ProductTypeId = productTypes.Single(s => s.Label == "Electronics").ProductTypeId,
                      ProductSubTypeId = 1,
                      Name = "Apple Iphone 7",
                      Price = 7.49M,
                      UserId = users.Single(s => s.FirstName == "Tractor").UserId,
                      IsActive = true
                  },
                  new Product {
                      Description = "Big sound--with deep bass--for a full-range listening experience",
                      ProductTypeId = productTypes.Single(s => s.Label == "Electronics").ProductTypeId,
                      ProductSubTypeId = 1,
                      Name = "Bose Soundlink Mini",
                      Price = 199.00M,
                      UserId = users.Single(s => s.FirstName == "Steve").UserId,
                      IsActive = true
                  },
                  new Product {
                      Description = "Nose Pad and Special designed Head Strap - increase your comfort.",
                      ProductTypeId = productTypes.Single(s => s.Label == "Electronics").ProductTypeId,
                      ProductSubTypeId = 2,
                      Name = "Google Cardboard V2",
                      Price = 10.49M,
                      UserId = users.Single(s => s.FirstName == "Carson").UserId,
                      IsActive = true
                  },
                  new Product {
                      Description = "4K Enhancement Technology - accepts 4K input and supports HDCP 2.2 for truly immersive scenes with 4K content",
                      ProductTypeId = productTypes.Single(s => s.Label == "Electronics").ProductTypeId,
                      ProductSubTypeId = 2,
                      Name = "Epson Home Cinema 5040UB",
                      Price = 7.49M,
                      UserId = users.Single(s => s.FirstName == "Tractor").UserId,
                      IsActive = true
                  },
                  new Product {
                      Description = "Grills sandwiches of any thickness.",
                      ProductTypeId = productTypes.Single(s => s.Label == "Appliances").ProductTypeId,
                      ProductSubTypeId = 3,
                      Name = "Hamilton Beach 25460A Panini Press",
                      Price = 24.94M,
                      UserId = users.Single(s => s.FirstName == "Steve").UserId,
                      IsActive = true
                  },
                  new Product {
                      Description = "Simply fill the measuring up to the line of desired consistency, and push the on/off button to start and it will automatically shut off when eggs are done",
                      ProductTypeId = productTypes.Single(s => s.Label == "Appliances").ProductTypeId,
                      ProductSubTypeId = 3,
                      Name = "Dash Go Rapid Egg Cooker,",
                      Price = 14.49M,
                      UserId = users.Single(s => s.FirstName == "Tractor").UserId,
                      IsActive = true
                  },
                  new Product {
                      Description = "Makes 6-8 Cups of Kettle Style Popcorn.",
                      ProductTypeId = productTypes.Single(s => s.Label == "Appliances").ProductTypeId,
                      ProductSubTypeId = 3,
                      Name = "Disney Mickey Kettle Style Popcorn Popper",
                      Price =25.00M,
                      UserId = users.Single(s => s.FirstName == "Steve").UserId,
                      IsActive = true
                  },
                  new Product {
                      Description = "DURABILITY - The higest level of cut resistant material - Level FIVE. 4x stronger than leather!",
                      ProductTypeId = productTypes.Single(s => s.Label == "Housewares").ProductTypeId,
                      ProductSubTypeId = 5,
                      Name = "SimpleHouseware Cut Resistant Gloves",
                      Price = 7.87M,
                      UserId = users.Single(s => s.FirstName == "Carson").UserId,
                      IsActive = true
                  },
                  new Product {
                      Description = "Made of high quality porcelain. 14-Ounce capacity",
                      ProductTypeId = productTypes.Single(s => s.Label == "Housewares").ProductTypeId,
                      ProductSubTypeId = 5,
                      Name = "Yedi Houseware Classic Coffee and Tea Siena Tea",
                      Price = 21.49M,
                      UserId = users.Single(s => s.FirstName == "Tractor").UserId,
                      IsActive = true
                  },
                  new Product {
                      Description = "Solid pinewood exterior bucket; aluminum mixing canister for fast freezing",
                      ProductTypeId = productTypes.Single(s => s.Label == "Appliances").ProductTypeId,
                      ProductSubTypeId = 3,
                      Name = "Ice-Cream Maker",
                      Price = 49.50M,
                      UserId = users.Single(s => s.FirstName == "Steve").UserId,
                      IsActive = true
                  },
                  new Product {
                      Description = " Its unique Patented suction bottom and round design makes it very easy to use.",
                      ProductTypeId = productTypes.Single(s => s.Label == "Appliances").ProductTypeId,
                      ProductSubTypeId = 3,
                      Name = "SunrisePro Knife Sharpener",
                      Price = 7.49M,
                      UserId = users.Single(s => s.FirstName == "Tractor").UserId,
                      IsActive = true
                  },
                  new Product {
                      Description = "Patented Real Flame appears identical to a lit candle using electromagnetics and glowing LEDs",
                      ProductTypeId = productTypes.Single(s => s.Label == "Housewares").ProductTypeId,
                      ProductSubTypeId = 6,
                      Name = "Mystique Flameless Candle",
                      Price = 35.60M,
                      UserId = users.Single(s => s.FirstName == "Steve").UserId,
                      IsActive = true
                  },
                  new Product {
                      Description = "Type: top load. Agitator: yes. Stainless Steel drum: yes",
                      ProductTypeId = productTypes.Single(s => s.Label == "Appliances").ProductTypeId,
                      ProductSubTypeId = 3,
                      Name = "Top Load Washer",
                      Price = 800.49M,
                      UserId = users.Single(s => s.FirstName == "Carson").UserId,
                      IsActive = true
                  },
                  new Product {
                      Description = "Small Portable washing machine goes anywhere with only 28lbs weight",
                      ProductTypeId = productTypes.Single(s => s.Label == "Appliances").ProductTypeId,
                      ProductSubTypeId = 3,
                      Name = "Portable Washing Machine",
                      Price = 149.49M,
                      UserId = users.Single(s => s.FirstName == "Tractor").UserId,
                      IsActive = true
                  },
                  new Product {
                      Description = "A 2015 Powerful vacuum cleaner",
                      ProductTypeId = productTypes.Single(s => s.Label == "Appliances").ProductTypeId,
                      ProductSubTypeId = 4,
                      Name = "Commercial Upright Vacuum",
                      Price = 18.00M,
                      UserId = users.Single(s => s.FirstName == "Steve").UserId,
                      IsActive = true
                  },
                  new Product {
                      Description = "Full keyboard with LCD, speaker, microphone & flash memory",
                      ProductTypeId = productTypes.Single(s => s.Label == "Electronics").ProductTypeId,
                      ProductSubTypeId = 2,
                      Name = "Speaking Vocabulary Builder ",
                      Price = 9.99M,
                      UserId = users.Single(s => s.FirstName == "Tractor").UserId,
                      IsActive = true
                  },
                  new Product {
                      Description = "Provides cleaner, fresher air in your home using the power of UV-C light technology.",
                      ProductTypeId = productTypes.Single(s => s.Label == "Appliances").ProductTypeId,
                      ProductSubTypeId = 4,
                      Name = "UV Sanitizer and Odor Reducer",
                      Price = 35.00M,
                      UserId = users.Single(s => s.FirstName == "Steve").UserId,
                      IsActive = true
                  },
                  new Product {
                      Description = "Set of 4 Pink bath towels for your home, dorm room or Spa.",
                      ProductTypeId = productTypes.Single(s => s.Label == "Housewares").ProductTypeId,
                      ProductSubTypeId = 6,
                      Name = "Cotton Bath Towels",
                      Price = 324.00M,
                      UserId = users.Single(s => s.FirstName == "Carson").UserId,
                      IsActive = true
                  },
                  new Product {
                      Description = "Compatible with your favorite classic NES games!",
                      ProductTypeId = productTypes.Single(s => s.Label == "Electronics").ProductTypeId,
                      ProductSubTypeId = 2,
                      Name = "Retro Bit Nintendo NES Entertainment System ",
                      Price = 7.49M,
                      UserId = users.Single(s => s.FirstName == "Tractor").UserId,
                      IsActive = true
                  },
                  new Product {
                      Description = "250-watt food processor with 3-cup plastic work bowl",
                      ProductTypeId = productTypes.Single(s => s.Label == "Appliances").ProductTypeId,
                      ProductSubTypeId = 3,
                      Name = "Food Processor",
                      Price = 35.99M,
                      UserId = users.Single(s => s.FirstName == "Steve").UserId,
                      IsActive = true
                  }
              };

              foreach (Product i in products)
              {
                  context.Product.Add(i);
              }

            context.SaveChanges();  // Seed products added


              // Creating a new Order for Carson Alexander
              Order carsonOrder = new Order {
                  UserId = 1,
              };

            context.Order.Add(carsonOrder);
            context.SaveChanges();    // Seed orders added


              Order otherOrder = new Order {
                  UserId = 2,
              };

              context.Order.Add(otherOrder);
              context.SaveChanges();    // Seed orders added

                PaymentType PayPal = new PaymentType
                {
                    Description = "Paypal",
                    AccountNumber = "12345",
                    UserId = 1
                };

                context.PaymentType.Add(PayPal);
                context.SaveChanges();

                Order completedOrder = new Order
                {
                    UserId = 1,
                    DateCompleted = new DateTime(2016, 10, 15),
                    PaymentTypeId = 1
                };

                context.Order.Add(completedOrder);
                context.SaveChanges();    // Seed orders added


                // Populating the Order with Line Items
                LineItem[] carsonOrderLineItems = new LineItem[] {
                  new LineItem(){ OrderId = 1, ProductId = 1 },
                  new LineItem(){ OrderId = 1, ProductId = 2 },
              };

              foreach (LineItem item in carsonOrderLineItems)
              {
                  context.LineItem.Add(item);
              }

              context.SaveChanges();    // Seed order items added


              // Populating the Other Order with Line Items
              LineItem[] otherOrderLineItems = new LineItem[] {
                  new LineItem(){ OrderId = 2, ProductId = 3 }
              };

              foreach (LineItem item in otherOrderLineItems)
              {
                  context.LineItem.Add(item);
              }

              context.SaveChanges();    // Seed order items added
          }
       }
    }
}