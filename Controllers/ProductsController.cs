using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bangazon.Helpers;
using BangazonWeb.ViewModels;
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
     * Author: Garrett/Anulfo
     * Methods:
     *   Index() - shows index view of products
     *   Detail() - shows detailed view of individual product
     *   EditInfo() - allows users to fill form to change product information
     *   Edit() - executes the edit within the database
     *   New() - allows for users to navigate to form
     *   New(Product product) - updates database with new product information.
     */
    public class ProductsController : Controller
    {
        private BangazonContext context;

        public ProductsController(BangazonContext ctx)
        {
            context = ctx;
        }

        public async Task<IActionResult> Detail(int? id)
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

            var model = new ProductDetail(context);
            model.CurrentProduct = product;
            return View(model);
        }

        public async Task<IActionResult> EditInfo([FromRoute]int? id)
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

            var model = new ProductEdit(context);
            model.CurrentProduct = product;
            return View(model);
        }

        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Edit(ProductEdit product)
        {
            Product originalProduct = await context.Product.SingleAsync(p => p.ProductId == product.CurrentProduct.ProductId);

            if (!ModelState.IsValid)
            {
                
                return RedirectToAction( "EditInfo", new RouteValueDictionary( 
                     new { controller = "Products", action = "EditInfo", Id = originalProduct.ProductId } ) );
            }
            
            
            originalProduct.ProductId = product.CurrentProduct.ProductId;
            originalProduct.Price = product.CurrentProduct.Price;
            originalProduct.Description = product.CurrentProduct.Description;
            originalProduct.Name = product.CurrentProduct.Name;
            originalProduct.ProductTypeId = product.CurrentProduct.ProductTypeId;
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
            
            return RedirectToAction( "Detail", new RouteValueDictionary( 
                     new { controller = "Products", action = "Detail", Id = originalProduct.ProductId } ) );
        }

        public IActionResult New()
        {
            var model = new ProductCreate(context);
            return View(model); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCreate product)
        {
            
            if (ModelState.IsValid)
            {
                context.Add(product.NewProduct);
                await context.SaveChangesAsync();
                return RedirectToAction( "Detail", new RouteValueDictionary( 
                     new { controller = "Products", action = "Detail", Id = product.ProductId } ) );
            }

            var model = new ProductCreate(context);
            model.NewProduct = product.NewProduct;
            return View(model);
        }
        public IActionResult Error()
        {
            var model = new BaseViewModel(context);
            return View(model);
        }
    }
}



