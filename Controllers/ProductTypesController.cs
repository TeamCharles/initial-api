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
     *   Task<IActionResult> Buy() - Shows all product types and counts
     *   Task<IActionResult> list() - Shows all products that match a specified ProductTypeId
     *   CalculateTypeQuantities(ProductType) - Queries the Product table to return a new ProductType object...
     *        ...new ProductType object contains a value for Quantity, based on number of Products with that Type
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
