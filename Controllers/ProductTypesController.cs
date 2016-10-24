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
     * AUTHOR: Dayne Wright
     * METHODS:
     *   Task<IActionResult> Buy() - Shows all product types and counts that do not belong to the logged in user
     *      Task<IActionResult> - Returns list of productTypes not belonging to the user
     *   Task<IActionResult> Sell() - Shows all product types and amounts that the logged in user has for sale
     *       Task<IActionResult> - Returns list of productTypes the user owns
     **/
    public class ProductTypesController : Controller
    {
        private BangazonContext context;

        public ProductTypesController(BangazonContext ctx)
        {
            context = ctx;
        }

        public List<ProductType> Buy()
        { 
            return context.ProductType.ToList();
            //return View(await context.ProductType.ToListAsync()); 
        }
        public List<ProductType> Sell()
        {
            return context.ProductType.ToList();
            //return View(await context.ProductType.ToListAsync()); 
        }

    }
}
