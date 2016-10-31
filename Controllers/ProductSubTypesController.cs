using System;
using System.Collections.Generic;
using System.Linq;
using Bangazon.Models;
using System.Threading.Tasks;
using BangazonWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BangazonWeb.ViewModels;

namespace BangazonWeb.Controllers
{
    /**
     * CLASS: ProductSubTypes
     * PURPOSE: 
     * AUTHOR: Dayne Wright/Garrett Vangilder
     * METHODS:
     *   Task<IActionResult> List() - Shows all products that match a specified ProductTypeId
     *   Task<IActionResult> Products() - Shows all products of a subtype
     *   CalculateTypeQuantities(ProductType) - Queries the Product table to return a new ProductType object...
     *        ...new ProductType object contains a value for Quantity, based on number of Products with that Type
     **/
    public class ProductSubTypesController : Controller
    {
        private BangazonContext context;

        /**
         * Purpose: Constructor that initates an instance of the ProductSubTypeController
         * Arguments:
         *      ctx - Database context reference 
         * Returns: 
         *      instance
         */
        public ProductSubTypesController(BangazonContext ctx)
        {
            context = ctx;
        }


        /**
         * Purpose: List the different subproducts for the user according to producttypes
         * Arguments:
         *      id - subtype id
         * Return:
         *      View(model) within the model there is a list that holds the different subproducts associated with each product type
         */
        public async Task<IActionResult> List([FromRoute]int id)
        {
            List<ProductSubType> ProductSubTypeList = await context.ProductSubType.OrderBy(s => s.Label).Where(p => p.ProductTypeId == id).ToListAsync();

            ProductSubTypeList.ForEach(CalculateTypeQuantities);

            var model = new ProductSubTypeList(context);
            model.ProductSubTypes = ProductSubTypeList;
            model.ProductType = await context.ProductType.SingleAsync(t => t.ProductTypeId == id);
            
            return View(model);
        }
        /**
         * Purpose: List the different products associated with each subproductType
         * Arguments:
         *      id - subtype id
         * Return:
         *      Redirects user to a list view of products
         */
        public async Task<IActionResult> Products([FromRoute]int id)
        {
            var model = new ProductSubTypeList(context);

            model.Products = await context.Product.OrderBy(s => s.Name).Where(p => p.ProductSubTypeId == id).ToListAsync();
            model.ProductSubType = await context.ProductSubType.SingleAsync(p => p.ProductSubTypeId == id);
            model.ProductType = await context.ProductType.SingleAsync(p => p.ProductTypeId == model.ProductSubType.ProductTypeId);

            return View(model);
        }

        /**
         * Purpose: calculates the quantity of each product subtype
         * Arguments:
         *      productSubType to be calculated
         * Return:
         *      Void
         */
        public void CalculateTypeQuantities(ProductSubType productSubType)
        {
            int quantity = context.Product.Count(p => p.ProductSubTypeId == productSubType.ProductSubTypeId);
            productSubType.Quantity = quantity;
        }
    }
}
