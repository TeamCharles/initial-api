using System.Collections.Generic;
using System.Linq;
using Bangazon.Models;
using BangazonWeb.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BangazonWeb.ViewModels
{
  public class OrderView : BaseViewModel
  {
    public float TotalPrice { get; set; }
    public IEnumerable<Product> ActiveProducts { get; set; }
    public IEnumerable<SelectListItem> AvailablePaymentType {get; set; }
    public IEnumerable<SelectListItem> PaymentType {get; set; }
    public OrderView(BangazonContext ctx) : base(ctx)
    {
        AvailablePaymentType = context.PaymentType
                              .OrderBy(d => d.Description)
                              .AsEnumerable()
                              .Select(li => new SelectListItem { 
                              Text = li.Description,
                              Value = li.PaymentTypeId.ToString()
                              });
    }
    public OrderView() { }
  }
}