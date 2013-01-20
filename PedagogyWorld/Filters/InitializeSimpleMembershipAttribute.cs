using System;
using System.Collections.Generic;
using System.Threading;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;

namespace PedagogyWorld.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class InitializeSimpleMembershipAttribute : ActionFilterAttribute
    {
        private static SimpleMembershipInitializer _initializer;
        private static object _initializerLock = new object();
        private static bool _isInitialized;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            LazyInitializer.EnsureInitialized(ref _initializer, ref _isInitialized, ref _initializerLock);
        }

        private class SimpleMembershipInitializer
        {
            public SimpleMembershipInitializer()
            {
                try
                {
                    WebSecurity.InitializeDatabaseConnection("ConnStringForWebSecurity", "UserProfile", "UserId", "UserName", autoCreateTables: true);
                    if (!((IList<string>) Roles.GetRolesForUser("omkar")).Contains("Administrator"))
                        Roles.AddUsersToRoles(new[] { "omkar" }, new[] { "Administrator" });
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("oops. websecurity error", ex);
                }
            }
        }
    }
}
