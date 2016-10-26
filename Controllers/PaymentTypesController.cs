using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BangazonWeb.Data;
using Bangazon.Models;
using Bangazon.Helpers;



namespace BangazonWeb.Controllers
{
    public class PaymentTypesController : Controller
    {
        private BangazonContext context;

        public PaymentTypesController(BangazonContext ctx)
        {
            context = ctx;
        }
        public async Task<IActionResult> Create()
        {
        
        ViewBag.Users = Users.GetAllUsers(context);

        return View();
        }
    
        [HttpPost]

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PaymentType paymentType)
        {
            if (ModelState.IsValid)
            {
                context.Add(paymentType);
                await context.SaveChangesAsync();
                return RedirectToAction("Cart", "Index");
            }
            
            ViewBag.Users = Users.GetAllUsers(context);

            return View(paymentType);
        }
    }

}