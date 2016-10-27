using System.Collections.Generic;
using System.Linq;
using Bangazon.Models;
using BangazonWeb.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BangazonWeb.ViewModels
{
  public class PaymentTypeView : BaseViewModel
  {
    public string Description { get; set; }    
    public string AccountNumber { get; set; }
    public PaymentTypeView(BangazonContext ctx) : base(ctx) { }
    public PaymentTypeView() { }
  }
}