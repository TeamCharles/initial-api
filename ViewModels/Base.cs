using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;
using Bangazon.Models;
using BangazonWeb.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BangazonWeb.ViewModels
{
  /**
   * Class: BaseViewModel
   * Purpose: Base ViewModels for all views to help create a more complex model.
   * Author: Matt Kraatz/Dayne Wright/Matt Hamil
   * Methods:
   *   Constructor BaseViewModel(ctx)  -  ctx is the current database context that is connected to query for data.
   *      this.Users - Adds all users from database for navbar user selection.
   *      this.CartProducts - Adds all products on the active order for the currently logged in user.
   *      this.TotalCount - Total number of products on the active order for the current user.  Used for cart icon notification.
   **/
  public class BaseViewModel
  {
    public IEnumerable<SelectListItem> Users { get; set; }
    public List<Product> CartProducts { get; set; }
    protected BangazonContext context;
    private ActiveUser singleton = ActiveUser.Instance;
    public int TotalCount { get;  private set; }
    public User ChosenUser
    {
      get
      {
        // Get the current value of the user property of our singleton
        User user = singleton.User;

        // If no user has been chosen yet, it's value will be null
        if (user == null)
        {
          // Return fake user for now
          return new User () {
            FirstName = "LogInRequired",
            LastName = "dummy",
            StreetAddress = "dummy",
            City = "dummy",
            State = "dummy",
            ZipCode = 12345,
            UserId = 0
          };
        }

        // If there is a user chosen, return it
        return user;
      }
      set
      {
        if (value != null)
        {
          singleton.User = value;
        }
      }
    }
    public BaseViewModel(BangazonContext ctx)
    {
        context = ctx;

        this.Users = context.User
            .OrderBy(l => l.LastName)
            .AsEnumerable()
            .Select(li => new SelectListItem {
                Text = $"{li.FirstName} {li.LastName}",
                Value = li.UserId.ToString()
            });

        // For help with this LINQ query, refer to
        // https://stackoverflow.com/questions/373541/how-to-do-joins-in-linq-on-multiple-fields-in-single-join
        this.CartProducts = (
                from product in context.Product
                from lineItem in context.LineItem
                    .Where(lineItem => lineItem.OrderId == context.Order.SingleOrDefault(o => o.DateCompleted == null && o.User == ChosenUser).OrderId && lineItem.ProductId == product.ProductId)
                select product).ToList();

        foreach (Product product in this.CartProducts)
            {
                if (product.IsActive)
                    this.TotalCount += 1;
            }

    }
    public BaseViewModel() { }
  }
}