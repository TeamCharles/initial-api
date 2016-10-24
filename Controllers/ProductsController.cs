using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bangazon.Models;
using BangazonWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BangazonWeb.Controllers
{
    public class ProductsController : Controller
    {
        private BangazonContext context;

        public ProductsController(BangazonContext ctx)
        {
            context = ctx;
        }

        public async Task<IActionResult> Index()
        {
            return View(await context.Product.ToListAsync());
        }

        public async Task<IActionResult> Detail([FromRoute]int? id)
        {
            // If no id was in the route, return 404
            if (id == null)
            {
                return NotFound();
            }

            var product = await context.Product
                    .Include(s => s.User)
                    .SingleOrDefaultAsync(m => m.ProductId == id);

            // If product not found, return 404
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }


        public async Task<IActionResult> EditInformation([FromRoute]int? id)
        {
            ViewData["ProductTypeId"] = context.ProductType
                .OrderBy(l => l.Label)
                .AsEnumerable()
                .Select(li => new SelectListItem { 
                    Text = li.Label,
                    Value = li.ProductTypeId.ToString()
                    });
                                        
            // If no id was in the route, return 404
            if (id == null)
            {
                return NotFound();
            }

            var product = await context.Product
                    .Include(s => s.User)
                    .SingleOrDefaultAsync(m => m.ProductId == id);

            // If product not found, return 404
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }

            context.Product.Add(product);
            try
            {
                context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw;
            }
            
            return RedirectToAction("Index", "Products");
        }

        public IActionResult Type([FromRoute]int id)
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
