using System.Collections.Generic;
using Bangazon.Models;
using BangazonWeb.Data;

namespace BangazonWeb.ViewModels
{
  /**
   * Class: ProductTypeList
   * Purpose: ViewModel for the ProductType views.
   * Author: Matt Kraatz
   * Methods:
   *   ProductTypeList(BangazonContext ctx) - Constructor that calls the BaseViewModel constructor.
   */
  public class ProductTypeList : BaseViewModel
  {
    public IEnumerable<ProductType> ProductTypes { get; set; }
    
    /**
     * Purpose: Saves a reference to the database context.
     * Arguments:
     *      ctx - Database context.
     * Return:
     *      An instance of the class.
     */
    public ProductTypeList(BangazonContext ctx) : base(ctx) { }
  }
}