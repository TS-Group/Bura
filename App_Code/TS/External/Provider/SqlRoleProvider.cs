using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace TS.Web.Provider
{
    /// <summary>
    /// Summary description for OracleRoleProvider
    /// </summary>
    public class SqlRoleProvider : RoleProvider
    {

        private string _ApplicationName;
        private char[] _Separator = { ',' };

        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
        {

            if (config == null)
                throw new ArgumentNullException("config");

            //REM Set the Appname
            if (string.IsNullOrEmpty(config["applicationName"]))
            {
                ApplicationName = System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath;
            }
            else
            {
                ApplicationName = config["applicationName"].ToString();

            }

            base.Initialize(name, config);
            string connect = config["connectionStringName"];

            //REM Set the ConnectionString name
            if (string.IsNullOrEmpty(config["connectionStringName"]))
            {
                //_connStr = "";
            }
            else
            {
                /*   _connStr = ConfigurationManager.ConnectionStrings[connect].ConnectionString;
                   cn.ConnectionString = _connStr;
                   cmd.Connection = cn;
                 */
            }

        }


        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override string ApplicationName
        {
            get
            {
                return _ApplicationName;
            }
            set
            {
                _ApplicationName = value;
            }
        }

        public override void CreateRole(string roleName)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override string[] GetAllRoles()
        {
            string roles = "MaxUser";
            return roles.Split(_Separator);
        }

        public override string[] GetRolesForUser(string username)
        {
            string roles = "MaxUser";
            return roles.Split(_Separator);
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            if (roleName.Equals("MaxUser"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool RoleExists(string roleName)
        {
            return roleName.Equals("MaxUser");
        }
    }

}