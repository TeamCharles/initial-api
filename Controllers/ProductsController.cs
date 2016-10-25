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

/**
 * Class: ProductsController
 * Purpose: Provide methods for the different products views
 * Author: Anulfo Ordaz
 * Methods:
 *   ProductsController() - retrieve data from context
 *   Task<IActionResult>Index() - returns a list of every product
 *   Task<IActionResult> Detail() - returns the information for an individual product
 *   Task<IActionResult> Create() - retrieve the types and users for the dropdowns and return the form view
 *   Task<IActionResult> Create(Product Product) - post the new item to the database and redirects to the index view
 */
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
        public IActionResult Create()
        {
           ViewData["ProductTypeId"] = context.ProductType
                .OrderBy(l => l.Label)
                .AsEnumerable()
                .Select(li => new SelectListItem { 
                    Text = li.Label,
                    Value = li.ProductTypeId.ToString()
                    });

            ViewData["UserId"] = context.User
                .OrderBy(l => l.LastName)
                .AsEnumerable()
                .Select(li => new SelectListItem { 
                    Text = $"{li.FirstName} {li.LastName}",
                    Value = li.UserId.ToString()
                });

            return View(); 
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {

            if (ModelState.IsValid)
            {
                context.Add(product);
                await context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(product);
        }
        public IActionResult Error()
        {
            return View();
        }
    }
}
