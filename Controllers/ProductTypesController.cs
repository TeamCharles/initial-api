using System;
using System.Collections.Generic;
using System.Linq;
using Bangazon.Models;
using System.Threading.Tasks;
using BangazonWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Bangazon.Helpers;
using BangazonWeb.ViewModels;

namespace BangazonWeb.Controllers
{
    /**
     * CLASS: ProductTypes
     * PURPOSE: Creates routes for main index view (buy method) and seller view (sell method)
     * AUTHOR: Dayne Wright/Matt Kraatz
     * METHODS:
     *   Task<IActionResult> Buy() - Returns a View listing all ProductTypes and a count of Products within that type.
     *   Task<IActionResult> List(int id) - Returns a view listing all Products that match a specified ProductTypeId.
     *          - int id: ProductTypeId for the Products being returned to the view.
     *   CalculateTypeQuantities(ProductType productType) - Queries the Product table to count the number of Products in a given ProductType. Updates the ProductType.Quantity property.
     *          - ProductType productType: ProductType to be updated with Quantity.
     **/
    public class ProductTypesController : Controller
    {
        private BangazonContext context;

        public ProductTypesController(BangazonContext ctx)
        {
            context = ctx;
        }

        public async Task<IActionResult> Buy()
        {
            List<ProductType> ProductTypeList = await context.ProductType.OrderBy(s => s.Label).ToListAsync();
            ProductTypeList.ForEach(CalculateTypeQuantities);
            var model = new ProductTypeList(context);
            model.ProductTypes = ProductTypeList;
            return View(model);
        }

        public async Task<IActionResult> List([FromRoute]int? id)
        {
            var model = new ProductList(context);
            model.Products = await context.Product.OrderBy(s => s.Name).Where(p => p.ProductTypeId == id).ToListAsync();
            return View(model);
        }

        public void CalculateTypeQuantities(ProductType productType)
        {
            int quantity = context.Product.Count(p => p.ProductTypeId == productType.ProductTypeId);
            productType.Quantity = quantity;
        }
    }
}
