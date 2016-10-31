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

        /**
         * Purpose: Initializes the ProductTypesController with a reference to the database context
         * Arguments:
         *      ctx - Reference to the database context
         */
        public ProductTypesController(BangazonContext ctx)
        {
            context = ctx;
        }

        /**
         * Purpose: Creates a sorted product type list view
         * Return:
         *      Product type list view
         */
        public async Task<IActionResult> Buy()
        {
            List<ProductType> ProductTypeList = await context.ProductType.OrderBy(s => s.Label).ToListAsync();
            ProductTypeList.ForEach(CalculateTypeQuantities);
            var model = new ProductTypeList(context);
            model.ProductTypes = ProductTypeList;
            return View(model);
        }

        /**
         * Purpose: Creates a sorted product type list that shows all products of that type
         * Arguments:
         *      id - product type id
         * Return:
         *      Product type list view for a specific product type
         */
        public async Task<IActionResult> List([FromRoute]int? id)
        {
            var model = new ProductList(context);
            model.Products = await context.Product.OrderBy(s => s.Name).Where(p => p.ProductTypeId == id).ToListAsync();
            return View(model);
        }

        /**
         * Purpose: Counts the number of products within a certain product type
         * Arguments:
         *      productType - A product type to be counted
         * Return:
         *      void
         */
        public void CalculateTypeQuantities(ProductType productType)
        {
            int quantity = context.Product.Count(p => p.ProductTypeId == productType.ProductTypeId && p.IsActive == true);
            productType.Quantity = quantity;
        }
    }
}
