using System.Collections.Generic;
using System.Linq;
using Bangazon.Models;
using BangazonWeb.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BangazonWeb.ViewModels
{
  public class CartView : BaseViewModel
  {
    public float TotalPrice { get; set; }
    public int OrderId { get; set; }
    public IEnumerable<Product> ActiveProducts { get; set; }
    public IEnumerable<Product> LineItems { get; set; }
    public CartView(BangazonContext ctx) : base(ctx) { }
    public CartView() { }
  }
}