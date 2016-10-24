using System;
using Bangazon.Models;

namespace Bangazon.Helpers {
    /**
     * Class: SessionHelper
     * Purpose: Stores the logged in user
     * Author: Matt Hamil
     * Properties:
     *   ActiveUser - Stores the logged in user object
     */
    public static class SessionHelper
    {
        public static User ActiveUser { get; set; } = null;
    }
}