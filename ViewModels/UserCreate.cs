using System.Collections.Generic;
using System.Linq;
using Bangazon.Models;
using BangazonWeb.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BangazonWeb.ViewModels
{
  /**
   * Class: UserCreate
   * Purpose: ViewModel for the User/New view.
   * Author: Matt Kraatz
   * Methods:
   *   UserCreate(BangazonContext ctx) - Constructor that calls the BaseViewModel constructor.
   */
  public class UserCreate : BaseViewModel
  {
    public User NewUser { get; set; }
    
    /**
     * Purpose: Saves a reference to the database context.
     * Arguments:
     *      ctx - Database context.
     * Return:
     *      An instance of the class.
     */
    public UserCreate(BangazonContext ctx) : base(ctx) { }
    public UserCreate() { }
  }
}