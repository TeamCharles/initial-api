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