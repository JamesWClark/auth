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
		string orgUnit;
		private List<string> badSamAccountNames = new List<string>();

		public Task_BatchCreateUsers(string pathToUserCsv, bool hasHeaders, string orgUnit) {
			this.hasHeaders = hasHeaders;
			this.pathToUserCsv = pathToUserCsv;
			this.orgUnit = orgUnit;
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

					string domain = "student.rockhurst.int";

					try {
						//OU=Hurtados,OU=Lightly Managed,OU=Users,OU=Student.Greenlease,DC=student,DC=rockhurst,DC=int
						//OU=2019,OU=Highly Managed,OU=Users,OU=Student.Greenlease,DC=student,DC=rockhurst,DC=int
						using (var context = new PrincipalContext(ContextType.Domain, domain, orgUnit)) {

							
							// this is used only to batch reset student passwords on accounts that already exist
							using(var user = UserPrincipal.FindByIdentity(context, IdentityType.SamAccountName, samAccountName)) {
								user.SetPassword(password);
							}
							
							/*
							// this is used to create new user accounts
							using (var user = new UserPrincipal(context)) {
								user.SamAccountName = samAccountName;
								user.UserPrincipalName = samAccountName + "@" + domain;
								user.GivenName = fname;
								user.Surname = lname;
								user.EmailAddress = email;
								user.SetPassword(password);
								user.Enabled = true;
								user.Save();
							}
							 * */
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
