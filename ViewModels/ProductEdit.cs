using System.Collections.Generic;
using System.Linq;
using Bangazon.Models;
using BangazonWeb.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BangazonWeb.ViewModels
{
  public class ProductEdit : BaseViewModel
  {
    public Product CurrentProduct { get; set; }
    public IEnumerable<SelectListItem> ProductTypes { get; set; }
    public ProductEdit(BangazonContext ctx) : base(ctx)
    {
      ProductTypes = context.ProductType
                .OrderBy(l => l.Label)
                .AsEnumerable()
                .Select(li => new SelectListItem { 
                    Text = li.Label,
                    Value = li.ProductTypeId.ToString()
                    });
    }
    public ProductEdit() { }
  }

  public class ProductSubTypeForm
  {
    public string Name { get; set; }
    public string Description { get; set; }
    public float Price { get; set; }
  }
}