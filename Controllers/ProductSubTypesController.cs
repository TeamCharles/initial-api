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
     * CLASS: ProductSubTypes
     * PURPOSE:
     * AUTHOR: Dayne Wright/Garrett Vangilder
     * METHODS:
     *   Task<IActionResult> Index() - Shows all product types and counts
     *   Task<IActionResult> list() - Shows all products that match a specified ProductTypeId
     *   CalculateTypeQuantities(ProductType) - Queries the Product table to return a new ProductType object...
     *        ...new ProductType object contains a value for Quantity, based on number of Products with that Type
     **/
    public class ProductSubTypesController : Controller
    {
        private BangazonContext context;

        public ProductSubTypesController(BangazonContext ctx)
        {
            context = ctx;
        }
        //list of all subtypes with quantity from a main product type (id)
        public async Task<IActionResult> List([FromRoute]int id)
        {
            List<ProductSubType> ProductSubTypeList = await context.ProductSubType.OrderBy(s => s.Label).Where(p => p.ProductTypeId == id).ToListAsync();

            ProductSubTypeList.ForEach(CalculateTypeQuantities);

            var model = new ProductSubTypeList(context);
            model.ProductSubTypes = ProductSubTypeList;
            model.ProductType = await context.ProductType.SingleAsync(t => t.ProductTypeId == id);

            return View(model);
        }
        //list of all products in the subtype
        public async Task<IActionResult> Products([FromRoute]int id)
        {
            var model = new ProductSubTypeList(context);

            model.Products = await context.Product.OrderBy(s => s.Name).Where(p => p.ProductSubTypeId == id && p.IsActive == true).ToListAsync();
            model.ProductSubType = await context.ProductSubType.SingleAsync(p => p.ProductSubTypeId == id);
            model.ProductType = await context.ProductType.SingleAsync(p => p.ProductTypeId == model.ProductSubType.ProductTypeId);

            return View(model);
        }

        public void CalculateTypeQuantities(ProductSubType productSubType)
        {
            int quantity = context.Product.Count(p => p.ProductSubTypeId == productSubType.ProductSubTypeId && p.IsActive == true);
            productSubType.Quantity = quantity;
        }
    }
}
