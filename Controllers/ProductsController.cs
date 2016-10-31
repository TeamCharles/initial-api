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
     *   Edit() - allows users to fill form to change product information
     *   Edit(ProductEdit) - executes the edit within the database
     *   New() - allows for users to navigate to form
     *   New(Product product) - updates database with new product information.
     *   Delete() - deletes product from database and view of customer.
     *   Index() - returns a view of all products in the database
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

        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute]int? id)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductEdit product)
        {
            Product originalProduct = await context.Product.SingleAsync(p => p.ProductId == product.CurrentProduct.ProductId);

            if (!ModelState.IsValid)
            {
                var model = new ProductEdit(context);
                model.CurrentProduct = product.CurrentProduct;
                return View(model);
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

            return RedirectToAction("Detail", new RouteValueDictionary(
                     new { controller = "Products", action = "Detail", Id = originalProduct.ProductId }));
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new ProductCreate(context);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCreate product)
        {

            if (ModelState.IsValid && product.NewProduct.ProductTypeId > 0 && product.NewProduct.ProductSubTypeId > 0)
            {
                product.NewProduct.UserId = ActiveUser.Instance.User.UserId;
                product.NewProduct.IsActive = true;
                context.Add(product.NewProduct);
                await context.SaveChangesAsync();
                return RedirectToAction("Detail", new RouteValueDictionary(
                     new { controller = "Products", action = "Detail", Id = product.NewProduct.ProductId }));
            }

            var model = new ProductCreate(context);
            model.NewProduct = product.NewProduct;
            if (product.NewProduct.ProductTypeId > 0)
            {
                model.ProductSubTypes = context.ProductSubType
                    .OrderBy(l => l.Label)
                    .AsEnumerable()
                    .Where(t => t.ProductTypeId == product.NewProduct.ProductTypeId)
                    .Select(li => new SelectListItem {
                        Text = li.Label,
                        Value = li.ProductSubTypeId.ToString()
                    });
            }
            return View(model);
        }

        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            Product originalProduct = await context.Product.SingleAsync(p => p.ProductId == id);

            if (originalProduct == null)
            {
                return RedirectToAction("List", new RouteValueDictionary(
                    new { controller = "ProductTypes", action = "List", Id = originalProduct.ProductTypeId }));
            }
            else
            {

                try
                {
                    context.Remove(originalProduct);
                    await context.SaveChangesAsync();
                    return RedirectToAction("List", new RouteValueDictionary(
                        new { controller = "ProductTypes", action = "List", Id = originalProduct.ProductTypeId }));
                }
                catch (DbUpdateException)
                {
                    throw;
                }
            }
        }

        public async Task<IActionResult> Index()
        {
            var model = new ProductList(context);
            model.Products = await context.Product.OrderBy(s => s.Name.ToLower()).ToListAsync();
            return View(model);
        }

        [HttpPost]
        public IActionResult GetSubTypes(int id, [FromBody] ProductSubTypeForm productCreate)
        {
            ProductEdit model = new ProductEdit(context);

            model.CurrentProduct = new Product();

            model.CurrentProduct.Name = productCreate.Name;
            model.CurrentProduct.Description = productCreate.Description;
            model.CurrentProduct.Price = (decimal)productCreate.Price * 10;
            model.CurrentProduct.ProductTypeId = id;

            return View(model);
        }

        public IActionResult Error()
        {
            var model = new BaseViewModel(context);
            return View(model);
        }
    }
}



