using System.Collections.Generic;
using Bangazon.Models;
using BangazonWeb.Data;

namespace BangazonWeb.ViewModels
{
  public class ProductSubTypeList : BaseViewModel
  {
    public IEnumerable<ProductSubType> ProductSubTypes { get; set; }
    public IEnumerable<Product> Products { get; set; }
    public ProductType ProductType { get; set; }
    public ProductSubType ProductSubType { get; set; }
    public ProductSubTypeList(BangazonContext ctx) : base(ctx) { }
  }
}