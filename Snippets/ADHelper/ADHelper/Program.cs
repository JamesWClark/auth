using System;
using System.Configuration;
using System.DirectoryServices;
using System.Text.RegularExpressions;

namespace ADHelper {
	/// <summary>
	/// 
	/// </summary>
	class Program {

		/**
		 * @TODO use parameterized input instead of the app.config file name
		 */ 
		
		static void Main(string[] args) {

			string ou = "OU=2020,OU=Highly Managed,OU=Users,OU=Student.Greenlease,DC=student,DC=rockhurst,DC=int";

			var username = ConfigurationManager.AppSettings["ldap_admin_username"];
			var password = ConfigurationManager.AppSettings["ldap_admin_password"];
			var file = ConfigurationManager.AppSettings["ldap_user_file"];

			//batch create new users from csv, set email property, set password, enable, join hard-coded ou
			Tasks.Task_BatchCreateUsers task = new Tasks.Task_BatchCreateUsers(file, true, ou);
			task.Run();

			//ADClasses.AD_UsersCollection users = new ADClasses.AD_UsersCollection(file, true);
			//Tasks.Task_GeneratePasswords task = new Tasks.Task_GeneratePasswords(users, 8);
			//task.Run();
		}
	}
}
