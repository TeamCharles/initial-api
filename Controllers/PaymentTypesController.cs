using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BangazonWeb.Data;
using Bangazon.Models;
using Bangazon.Helpers;


/**
 * Class: PaymentTypesController
 * Purpose: Create a new Payment Method for the Logged User 
 * Author: Anulfo Ordaz
 * Methods:
 *   IActionResult Create() - Returns the PaymentType view
 *   Task<IActionResult> Create(PaymentType paymentType) - Post new Payment Type to the database and get the user to the Cart view 
 */
namespace BangazonWeb.Controllers
{
    public class PaymentTypesController : Controller
    {
        private BangazonContext context;

        public PaymentTypesController(BangazonContext ctx)
        {
            context = ctx;
        }
        public IActionResult Create()
        {

        ViewBag.Users = Users.GetAllUsers(context);
        
        return View();
        }

        [HttpPost]

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PaymentType paymentType)
        {
            paymentType.UserId = 1;
            if (ModelState.IsValid)
            {
                context.Add(paymentType);
                await context.SaveChangesAsync();
                return RedirectToAction("Index", "Cart");
            }
            
            ViewBag.Users = Users.GetAllUsers(context);

            return View(paymentType);
        }
        public IActionResult Error()
        {
            ViewBag.Users = Users.GetAllUsers(context);
            return View();
        }
    }

}