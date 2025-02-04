using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace NetCoreSecurity.TokenService
{
    public class Config
    {
        /// <summary>
        /// Setting Identity Server
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>()
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResource
                {
                    Name="Role",
                    UserClaims=new List<string>(){"Role"}
                }

            };
        }
        /// <summary>
        /// Client Identity Settings
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>()
            {
                new Client()
                {
                    ClientId="NetCoreSecurity",
                    ClientName="NetCoreSecurity Client",
                    AllowedGrantTypes=GrantTypes.Implicit,//Token Base
                    RedirectUris={"http://localhost:5002/signin-oidc"},
                    PostLogoutRedirectUris={"http://localhost:5002/signout-callback-oidc"},
                    AllowedScopes=new List<string>()
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Profile,
                    }
                }

            };
        }
    }
}
