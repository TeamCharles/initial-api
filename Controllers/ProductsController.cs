using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bangazon.Models;
using BangazonWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace BangazonWeb.Controllers
{
    /**
     * Class: ProductsController
     * Purpose: Currently allows for users to view and edit different products
     * Author: Garrett
     * Methods:
     *   Index() - shows index view of products
     *   Detail() - shows detailed view of individual product
     *   EditInfo() - allows users to fill form to change product information
     *   Edit() - executes the edit within the database
     */
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


        public async Task<IActionResult> EditInfo([FromRoute]int? id)
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

        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Edit([FromRoute]int id, Product product)
        {
            Product originalProduct = await context.Product.SingleAsync(p => p.ProductId == id);

            if (!ModelState.IsValid)
            {
                return RedirectToAction( "EditInfo", new RouteValueDictionary( 
                     new { controller = "Products", action = "EditInfo", Id = originalProduct.ProductId } ) );
            }
            
            
            originalProduct.ProductId = id;
            originalProduct.Price =product.Price;
            originalProduct.Description = product.Description;
            originalProduct.Name = product.Name;
            originalProduct.ProductTypeId = product.ProductTypeId;
            context.Entry(originalProduct).State = EntityState.Modified;

            context.Update(originalProduct);
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
