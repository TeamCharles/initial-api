using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BangazonWeb.Data;
using Bangazon.Models;
using BangazonWeb.ViewModels;
using Microsoft.AspNetCore.Routing;

namespace BangazonWeb.Controllers
{
    /**
     * Class: PaymentTypesController
     * Purpose: Create a new Payment Method for the Logged User
     * Author: Anulfo Ordaz
     * Methods:
     *   IActionResult Create() - Returns the PaymentType Creation view.
     *   Task<IActionResult> Create(PaymentTypeView paymentType) - Posts new Payment Type to the database and redirects user to the Cart view.
     *          - PaymentTypeView paymentType: ViewModel returned on submission of the Create Payment Type form.
     */
    public class PaymentTypesController : Controller
    {
        private BangazonContext context;

        /**
         * Purpose: Initializes the PaymentTypesController with a reference to the database context
         * Arguments:
         *      ctx - Reference to the database context
         */
        public PaymentTypesController(BangazonContext ctx)
        {
            context = ctx;
        }

        /**
         * Purpose: Redirects the user to the Payment Type Creation view
         * Return:
         *      Payment Type creation view
         */
        public IActionResult Create()
        {
            var model = new PaymentTypeView(context);

            return View(model);
        }

        /**
         * Purpose: Creates a new payment type for the user in the database
         * Arguments:
         *      paymentType - A new payment type for a user
         * Return:
         *      Redirects a user to the order confirmation page when a new payment type is created
         */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PaymentTypeView paymentType)
        {
            paymentType.NewPaymentType.UserId = ActiveUser.Instance.User.UserId;
            if (ModelState.IsValid)
            {
                context.Add(paymentType.NewPaymentType);
                await context.SaveChangesAsync();
                return RedirectToAction( "Final", new RouteValueDictionary(

                     new { controller = "Order", action = "Final", Id = paymentType.NewPaymentType.PaymentTypeId } ) );
            }

            var model = new PaymentTypeView(context);
            model.NewPaymentType = paymentType.NewPaymentType;

            return View(model);
        }
    }

}