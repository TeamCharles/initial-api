/* THIS CODE IS CURRENTLY DEPRECATED DUE TO THIS CODE BEING WRITTEN BEFORE VIEW MODELS WERE INTRODUCTED */


// using System;
// using System.Collections.Generic;
// using System.Linq;
// using Bangazon.Models;
// using BangazonWeb.Data;
// using Microsoft.AspNetCore.Mvc.Rendering;

// namespace Bangazon.Helpers {
//     /**
//      * Class: SessionHelper
//      * Purpose: Stores the logged in user
//      * Author: Matt Hamil
//      * Properties:
//      *   ActiveUser - Stores the logged in user object
//      */
//     public static class SessionHelper
//     {
//         public static int? ActiveUser { get; set; } = null;
//     }
//     /**
//      * Class: Users
//      * Purpose: Gets all users to populate user selection dropdown
//      * Author: Dayne Wright
//      * Methods:
//      *   IEnumerable<SelectListItem> GetAllUsers(BangazonContext ctx) - Gets all users from the DB and formats as select options
//      *     ctx - The current DB context to run the query
//      */
//     public static class Users 
//     {
//         public static IEnumerable<SelectListItem> GetAllUsers(BangazonContext ctx)
//         {
//             var users =  ctx.User.OrderBy(l => l.LastName)
//                         .AsEnumerable()
//                         .Select(li => new SelectListItem { 
//                             Text = $"{li.FirstName} {li.LastName}",
//                             Value = li.UserId.ToString(),
//                             Selected = (li.UserId == SessionHelper.ActiveUser)
//                         });
//             return users;
//         } 
//     }
// }