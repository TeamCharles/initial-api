using System;
using System.Collections.Generic;
using System.Linq;
using Bangazon.Models;
using System.Threading.Tasks;
using BangazonWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BangazonWeb.Controllers
{
    /**
     * CLASS: ProductTypes
     * PURPOSE: Creates routes for main index view (buy method) and seller view (sell method)
     * AUTHOR: Dayne Wright/Matt Kraatz
     * METHODS:
     *   Task<IActionResult> Buy() - Shows all product types and counts
     *   Task<IActionResult> Sell() - Shows all product types
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
            List<ProductType> ProductTypeList = await context.ProductType.ToListAsync();
            ProductTypeList.ForEach(CalculateTypeQuantities);
            return View(ProductTypeList);
        }

        public async Task<IActionResult> List([FromRoute]int? id)
        {
            return View(await context.Product.Where(p => p.ProductTypeId == id).ToListAsync());
        }

        public async Task<IActionResult> Sell()
        {
            return View(await context.ProductType.ToListAsync()); 
        }

        public void CalculateTypeQuantities(ProductType productType)
        {
            int quantity = context.Product.Count(p => p.ProductTypeId == productType.ProductTypeId);
            productType.Quantity = quantity;
        }
    }
}
