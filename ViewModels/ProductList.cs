using System.Collections.Generic;
using Bangazon.Models;
using BangazonWeb.Data;

namespace BangazonWeb.ViewModels
{
  /**
   * Class: ProductList
   * Purpose: ViewModel for the Products/Index view.
   * Author: Matt Kraatz
   * Methods:
   *   ProductList(BangazonContext ctx) - Constructor that calls the BaseViewModel constructor.
   */
  public class ProductList : BaseViewModel
  {
    public IEnumerable<Product> Products { get; set; }
    
    /**
     * Purpose: Saves a reference to the database context.
     * Arguments:
     *      ctx - Database context.
     * Return:
     *      Void
     */
    public ProductList(BangazonContext ctx) : base(ctx) { }
  }
}