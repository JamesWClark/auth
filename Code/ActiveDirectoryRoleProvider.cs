using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.DirectoryServices;
using System.Globalization;
using System.Web.Security;

namespace RHSauth.Code
{
    // custom role provider must inherit RoleProvider
    public class ActiveDirectoryRoleProvider : RoleProvider
    {
        //inherited member accessors
        private string ConnectionStringName { get; set; }
        private string ConnectionUsername { get; set; }
        private string ConnectionPassword { get; set; }
        private string AttributeMapUsername { get; set; }

        #region OVERRIDES - IMPLEMENTED

        public override void Initialize(string name, NameValueCollection config)
        {
            ConnectionStringName = config["connectionStringName"];
            ConnectionUsername = config["connectionUsername"];
            ConnectionPassword = config["connectionPassword"];
            AttributeMapUsername = config["attributeMapUsername"];

            base.Initialize(name, config);
        }
        public override string[] GetRolesForUser(string username)
        {
            var allRoles = new List<string>();
            var root = new DirectoryEntry(ConfigurationManager.ConnectionStrings[ConnectionStringName].ConnectionString,
                ConnectionUsername, ConnectionPassword);
            var searcher = new DirectorySearcher(root, String.Format(CultureInfo.InvariantCulture,
                "(&(objectClass=user)({0}={1}))", AttributeMapUsername, username));

            searcher.PropertiesToLoad.Add("memberOf");
            SearchResult result = searcher.FindOne();
            if (result != null && !string.IsNullOrEmpty(result.Path))
            {
                DirectoryEntry user = result.GetDirectoryEntry();
                PropertyValueCollection groups = user.Properties["memberOf"];

                foreach (string path in groups)
                {
                    string[] parts = path.Split(',');
                    if (parts.Length > 0)
                    {
                        foreach (string part in parts)
                        {
                            string[] p = part.Split('=');
                            if (p[0].Equals("cn", StringComparison.OrdinalIgnoreCase))
                            {
                                allRoles.Add(p[1]);
                            }
                        }
                    }
                }
            }
            return allRoles.ToArray();
        }
        public override bool IsUserInRole(string username, string roleName)
        {
            string[] roles = GetRolesForUser(username);

            foreach (string role in roles)
            {
                if (role.Equals(roleName, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region OVERRIDES - NOT YET IMPLEMENTED

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }
        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }
        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }
        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }
        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }
        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}