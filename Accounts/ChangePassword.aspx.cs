using System;
using System.Web.Security;

namespace RHSauth.Accounts
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ddlDomain_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ddlDomain.SelectedValue)
            {
                case "STUDENT":
                    ChangePassword1.MembershipProvider = Membership.Providers["ActiveDirectoryMembershipProvider_STUDENT"].Name;
                    break;
                case "ROCKHURST":
                    ChangePassword1.MembershipProvider = Membership.Providers["ActiveDirectoryMembershipProvider_ROCKHURST"].Name;
                    break;
            }
        }
    }
}