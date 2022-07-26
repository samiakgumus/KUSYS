 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public static class UserClaimExtensions
    {
      

            public static string GetClaimValue(this IPrincipal currentPrincipal, string key)
            {
                var identity = currentPrincipal.Identity as ClaimsIdentity;
                if (identity == null)
                    return null;

                var claim = identity.Claims.FirstOrDefault(c => c.Type == key);
                return claim.Value;
            }
        
    }
}
