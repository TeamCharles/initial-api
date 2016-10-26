using System.Collections.Generic;
using System.Linq;
using System;
using Bangazon.Models;
using BangazonWeb.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BangazonWeb.ViewModels
{
  public class BaseViewModel
  {
    public IEnumerable<SelectListItem> Users { get; set; }
    protected BangazonContext context;
    private ActiveUser singleton = ActiveUser.Instance;
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
            FirstName = "Create",
            LastName = "Account"
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
    }
    public BaseViewModel() { }
  }
}