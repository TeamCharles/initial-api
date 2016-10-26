using System.Collections.Generic;
using Bangazon.Models;
using BangazonWeb.Data;

namespace BangazonWeb.ViewModels
{
  public class ProductTypeList : BaseViewModel
  {
    public IEnumerable<ProductType> ProductTypes { get; set; }
    
    public ProductTypeList(BangazonContext ctx) : base(ctx) { }
  }
}