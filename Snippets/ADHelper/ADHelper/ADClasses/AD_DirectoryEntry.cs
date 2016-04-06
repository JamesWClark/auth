using System;
using System.Configuration;
using System.DirectoryServices;

namespace ADHelper.ADClasses
{
    class AD_DirectoryEntry
    {
        public DirectoryEntry dirEntry;

        AD_DirectoryEntry()
        {
            dirEntry = GetDirectoryObject();
        }

        public static DirectoryEntry GetDirectoryObject()
        {
            DirectoryEntry oDE;
            oDE = new DirectoryEntry(
                ConfigurationManager.AppSettings["ldap_root"],
                ConfigurationManager.AppSettings["ldap_admin_username"], 
                ConfigurationManager.AppSettings["ldap_admin_password"], 
                AuthenticationTypes.Secure
            );
            return oDE;
        }
    }

}
