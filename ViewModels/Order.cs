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
    public OrderView(BangazonContext ctx) : base(ctx) { }
    public PaymentTypeSelect(BangazonContext ctx) : base(ctx)
    {
        PaymentTypes = context.ProductType
        .OrderBy(l => l.Label)
        .AsEnumerable()
        .Select(li => new SelectListItem { 
        Text = li.Label,
        Value = li.ProductTypeId.ToString()
        });
    }
    public OrderView() { }
  }
}