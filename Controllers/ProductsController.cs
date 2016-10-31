using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
     * Purpose: Allows users to view, create and edit products
     * Author: Garrett/Anulfo
     * Methods:
     *   Task<IActionResult> Detail(int id) - Returns Detail view for an individual product.
     *          - int id: ProductId for the Product being viewed.
     *   Task<IActionResult> Edit(int id) - Returns a form view that allows you to edit an existing Product.
     *          - int id: ProductId for the Product being edited.
     *   Task<IActionResult> Edit(ProductEdit) - Executes a Product edit within the database and returns that Product's Detail view.
     *          - ProductEdit product: ProductEdit viewmodel posted on form submission. 
     *   Task<IActionResult> Create() - Returns a form view that allows a user to create a new product.
     *   Task<IActionResult> Create(Product product) - Posts a new product to the database and returns the Detail view for that Product.
     *          - ProductCreate product: ProductCreate viewmodel posted on form submission.
     *   Task<IActionResult> Delete(int id) - Sets the IsActive property on a Product to false and commits to the database. Redirects a user to the ProductTypes List page.
     *          - int id: ProductId of the Product being updated.
     *   Task<IActionResult> Index() - Returns a view of all active products in the database.
     *   IActionResult Error() - Returns an Error view. Currently not in use.
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
            var model = new ProductList(context);
            model.Products = await context.Product.Where(s => s.IsActive == true).OrderBy(s => s.Name).ToListAsync();
            return View(model);
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

            var productSubTypes = context.ProductSubType
                    .OrderBy(l => l.Label)
                    .AsEnumerable()
                    .Where(t => t.ProductTypeId == product.ProductTypeId)
                    .Select(li => new SelectListItem {
                        Text = li.Label,
                        Value = li.ProductSubTypeId.ToString()
                    });

            // If product not found, return 404
            if (product == null)
            {
                return NotFound();
            }

            var model = new ProductEdit(context);
            model.CurrentProduct = product;
            model.ProductSubTypes = productSubTypes;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductEdit product)
        {
            Product originalProduct = await context.Product.SingleAsync(p => p.ProductId == product.CurrentProduct.ProductId);

            if (ModelState.IsValid && product.CurrentProduct.ProductSubTypeId > 0)
            {
                originalProduct.ProductId = product.CurrentProduct.ProductId;
                originalProduct.Price = product.CurrentProduct.Price;
                originalProduct.Description = product.CurrentProduct.Description;
                originalProduct.Name = product.CurrentProduct.Name;
                originalProduct.ProductTypeId = product.CurrentProduct.ProductTypeId;
                originalProduct.ProductSubTypeId = product.CurrentProduct.ProductSubTypeId;

                context.Entry(originalProduct).State = EntityState.Modified;

                context.Update(originalProduct);
                context.SaveChanges();

                
                return RedirectToAction("Detail", new RouteValueDictionary(
                     new { controller = "Products", action = "Detail", Id = originalProduct.ProductId }));
            }

            var model = new ProductEdit(context);
                model.CurrentProduct = product.CurrentProduct;

                model.ProductSubTypes = context.ProductSubType
                    .OrderBy(l => l.Label)
                    .AsEnumerable()
                    .Where(t => t.ProductTypeId == model.CurrentProduct.ProductTypeId)
                    .Select(li => new SelectListItem {
                        Text = li.Label,
                        Value = li.ProductSubTypeId.ToString()
                    });

            return View(model);
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
                    new { controller = "ProductSubTypes", action = "List", Id = originalProduct.ProductSubTypeId }));
            }
            else
            {

                try
                {
                    originalProduct.IsActive = false;
                    context.Update(originalProduct);
                    await context.SaveChangesAsync();
                    return RedirectToAction("Products", new RouteValueDictionary(
                        new { controller = "ProductSubTypes", action = "Products", Id = originalProduct.ProductSubTypeId }));
                }
                catch (DbUpdateException)
                {
                    throw;
                }
            }
        }
        public IActionResult Error()
        {
            var model = new BaseViewModel(context);
            return View(model);
        }
    }
}



