using Bangazon.Models;

namespace BangazonWeb.Data
{
	public class ActiveUser
	{
		// Make the class a singleton to maintain state across all uses
		private static ActiveUser _instance;
		public static ActiveUser Instance
		{
			get {
				// First time an instance of this class is requested
				if (_instance == null) {
					_instance = new ActiveUser();
				}
				return _instance;
			}
		}

		// To track the currently active user - selected by user
		public User User { get; set; }

	}
}