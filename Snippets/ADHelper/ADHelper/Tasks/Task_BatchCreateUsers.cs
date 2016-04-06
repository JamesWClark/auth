using System;
using System.Collections.Generic;
using System.Configuration;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.IO;

namespace ADHelper.Tasks {
	class Task_BatchCreateUsers {

		bool hasHeaders;
		string pathToUserCsv;
		private List<string> badSamAccountNames = new List<string>();

		public Task_BatchCreateUsers(string pathToUserCsv, bool hasHeaders) {
			this.hasHeaders = hasHeaders;
			this.pathToUserCsv = pathToUserCsv;
		}

		public void Run() {
			try {
				//open a users csv and start reading it
				StreamReader reader = new StreamReader(pathToUserCsv, true);

				//if headers, burn the first line
				if (hasHeaders) {
					reader.ReadLine();
				}
				//read the rest of the file
				int count = 0;
				string line;
				while ((line = reader.ReadLine()) != null) {
					count++;
					//foreach user in file
					string[] columns = line.Split(',');

					//try without removing punctuation
					string fname = columns[1];
					string lname = columns[3];
					string samAccountName = columns[6];
					string email = columns[5];
					string password = columns[8];

					try {
						using (var context = new PrincipalContext(ContextType.Domain, "student.rockhurst.int", "OU=2019,OU=Highly Managed,OU=Users,OU=Student.Greenlease,DC=student,DC=rockhurst,DC=int")) {
							using(var user = UserPrincipal.FindByIdentity(context, IdentityType.SamAccountName, samAccountName)) {
								user.SetPassword(password);
							}

							/*using (var user = new UserPrincipal(context)) {
								
								 new accounts only
								user.SamAccountName = samAccountName;
								user.GivenName = fname;
								user.Surname = lname;
								user.EmailAddress = email;
								user.SetPassword(password);
								user.Enabled = true;
								user.Save();
								
							 }*/
						}
						Console.WriteLine("ok: " + email);
					} catch (Exception ex) {
						Console.WriteLine("ex when email = " + email);
						Console.WriteLine(ex.Message);
						
						badSamAccountNames.Add(samAccountName);
					}
				}
			} catch (IOException e) {
				Console.WriteLine(e.Message);
			}
			Console.WriteLine("\n\nfails:");
			foreach (string s in badSamAccountNames) {
				Console.WriteLine(s);
			}
		}
	}
}
