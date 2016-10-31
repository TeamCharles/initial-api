using System.Collections.Generic;
using System.Linq;
using Bangazon.Models;
using BangazonWeb.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BangazonWeb.ViewModels
{
  /**
   * Class: ProductCreate
   * Purpose: Used when a user creates a new product to sell
   * Author: Anulfo Ordaz/Matt Kraatz
   * Methods:
   *   Constructor ProductCreate(ctx) - Initiates a ProductCreate view model with a reference to the database context.
   */
  public class ProductCreate : BaseViewModel
  {
    public IEnumerable<SelectListItem> ProductTypes { get; set; }
    public IEnumerable<SelectListItem> ProductSubTypes { get; set; }
    public Product NewProduct { get; set; }

    /**
     * Purpose: Saves the reference to the database to display information about the user
     * Arguments:
     *      ctx - Database context reference
     */
    public ProductCreate(BangazonContext ctx) : base(ctx)
    {
      this.ProductTypes = context.ProductType
                .OrderBy(l => l.Label)
                .AsEnumerable()
                .Select(li => new SelectListItem {
                    Text = li.Label,
                    Value = li.ProductTypeId.ToString()
                    });
    }
    public ProductCreate() { }
  }
}