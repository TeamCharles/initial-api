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
    public int selectedPaymentId {get; set;}
    public OrderView(BangazonContext ctx) : base(ctx)
    {

    }
    public OrderView() { }
  }
}