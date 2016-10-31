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
     *   Task<IActionResult> List(int id) - Returns a view for all ProductSubTypes with a given ProductTypeId.
     *          - int id: ProductTypeId for the ProductSubTypes being requested to view. 
     *   Task<IActionResult> Products(int id) - Returns a view for all products with a given ProductSubTypeId.
     *          - int id: ProductSubTypeId for the Products being requested to view. 
     *   void CalculateTypeQuantities(ProductSubType productSubType) - Queries the Product table to count the number of Products in a given ProductSubType. Updates the ProductSubType.Quantity property.
     *          - ProductSubType productSubType: ProductSubType to be updated with Quantity.
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

            model.Products = await context.Product.OrderBy(s => s.Name.ToLower()).Where(p => p.ProductSubTypeId == id).ToListAsync();
            model.ProductSubType = await context.ProductSubType.SingleAsync(p => p.ProductSubTypeId == id);
            model.ProductType = await context.ProductType.SingleAsync(p => p.ProductTypeId == model.ProductSubType.ProductTypeId);

            return View(model);
        }

        public void CalculateTypeQuantities(ProductSubType productSubType)
        {
            int quantity = context.Product.Count(p => p.ProductSubTypeId == productSubType.ProductSubTypeId);
            productSubType.Quantity = quantity;
        }
    }
}
