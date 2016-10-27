using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BangazonWeb.Data;
using Bangazon.Models;
using Bangazon.Helpers;
using BangazonWeb.ViewModels;


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

        var model = new PaymentTypeView(context);
        
        return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PaymentTypeView paymentType)
        {
            paymentType.NewPaymentType.UserId = ActiveUser.Instance.User.UserId;
            if (ModelState.IsValid)
            {
                context.Add(paymentType.NewPaymentType);
                await context.SaveChangesAsync();
                return RedirectToAction("Index", "Cart");
            }
            
            var model = new PaymentTypeView(context);
            model.NewPaymentType = paymentType.NewPaymentType;

            return View(model);
        }
    }

}