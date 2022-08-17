using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using System.Collections.Generic;

namespace CSite.Identity
{
    public static class SD
    {
        public const string Admin = "Admin";
        public const string Editor = "Editor";
        public const string Customer = "Customer";

        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile()
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope> {
                new ApiScope(name: "CSite_API_scope",   displayName: "CSite server scope."),
                new ApiScope(name: "read",   displayName: "Read your data."),
                new ApiScope(name: "write",  displayName: "Write your data."),
                new ApiScope(name: "delete", displayName: "Delete your data.")
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                // Client credentials
                new Client
                {
                    ClientId="client",
                    ClientSecrets= { new Secret("s923r4jvJ-DSvsxoi8y-9vJDf6-832bnFV".Sha256())},
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes={ "read", "write", "profile"}
                },

                // Authentication Code
                new Client
                {
                    ClientId="CSite_client",
                    ClientSecrets= { new Secret("s923r4jvJ-DSvsxoi8y-9vJDf6-832bnFV".Sha256())},
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris={ "https://localhost:5004/signin-oidc" },
                    PostLogoutRedirectUris={"https://localhost:5004/signout-callback-oidc" },
                    AllowedScopes=new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "CSite_API_scope"
                    }
                },
            };
    }
}
