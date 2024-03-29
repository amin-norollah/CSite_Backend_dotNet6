﻿using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using System.Collections.Generic;

namespace CSite.Identity
{
    public static class SD
    {
        public const string Admin = "Admin";
        public const string Supplier = "Supplier";
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
                new ApiScope(name: "CSite_API",   displayName: "CSite server scope."),
                new ApiScope(name: "read",   displayName: "Read your data."),
                new ApiScope(name: "write",  displayName: "Write your data."),
                new ApiScope(name: "delete", displayName: "Delete your data.")
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new List<ApiResource> {
                        new ApiResource("CSite_API", "CSite server resource.") { Scopes = { "CSite_API" } }
            };


        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                // Authentication Code
                new Client
                {
                    ClientId="CSite_client",
                    ClientSecrets= { new Secret("s923r4jvJ-DSvsxoi8y-9vJDf6-832bnFV".Sha256())}, //TODO: need to be changed!
                    RequireClientSecret=false,
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    RedirectUris=new List<string>{
                        "https://localhost:4200/signin-callback" ,
                        "https://localhost:4200/silent-callback"
                    },
                    PostLogoutRedirectUris={"https://localhost:4200/signout-callback" },
                    AllowedCorsOrigins = new List<string>
                    {
                        "http://localhost:4200",
                        "https://localhost:4200"
                    },
                    AllowOfflineAccess = true,
                    AllowedScopes=new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "roles",
                        "CSite_API"
                    }
                },

                // Swagger (Authentication Code)
                new Client
                {
                    ClientId = "CSite_swagger_client",
                    ClientName = "Swagger UI for CSite",
                    ClientSecrets = {new Secret("s923r4jvJ-DSvsxoi8y-9vJDf6-832bnFV".Sha256())}, //TODO: need to be changed!

                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris = {"https://localhost:8088/swagger/oauth2-redirect.html"},
                    AllowedCorsOrigins = new List<string>
                    {
                        "https://localhost:8800",
                        "https://localhost:8088",
                        "https://localhost:4200"
                    },
                    AllowedScopes= { "CSite_API" }
                },
            };
    }
}
