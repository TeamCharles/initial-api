using System.Collections.Generic;
using System.Linq;
using Bangazon.Models;
using BangazonWeb.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BangazonWeb.ViewModels
{
  public class UserCreate : BaseViewModel
  {
    public User NewUser { get; set; }
    public UserCreate(BangazonContext ctx) : base(ctx) { }
    public UserCreate() { }
  }
}