using System.Collections.Generic;
using Bangazon.Models;
using BangazonWeb.Data;

namespace BangazonWeb.ViewModels
{
  public class ProductDetail : BaseViewModel
  {
    public Product CurrentProduct { get; set; }
    public ProductDetail(BangazonContext ctx) : base(ctx) { }
    public ProductDetail() { }
  }
}