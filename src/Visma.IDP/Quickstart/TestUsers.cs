// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityModel;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

namespace IdentityServerHost.Quickstart.UI
{
    public class TestUsers
    {
        public static List<TestUser> Users
        {
            get
            {
                return new List<TestUser>
                {
                    new TestUser
                    {
                        SubjectId = "141",
                        Username = "florin.ciobanu",
                        Password = "Parola@141",
                        Claims =
                        {
                            new Claim(JwtClaimTypes.Name, "Florin CIOBANU"),
                            new Claim(JwtClaimTypes.GivenName, "Florin"),
                            new Claim(JwtClaimTypes.FamilyName, "CIOBANU"),
                            new Claim(JwtClaimTypes.Email, "florin.ciobanu@email.com"),
                            new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        }
                    },
                    new TestUser
                    {
                        SubjectId = "142",
                        Username = "ionut.ciobanu",
                        Password = "Parola@142",
                        Claims =
                        {
                            new Claim(JwtClaimTypes.Name, "Ionut CIOBANU"),
                            new Claim(JwtClaimTypes.GivenName, "Ionut"),
                            new Claim(JwtClaimTypes.FamilyName, "CIOBANU"),
                            new Claim(JwtClaimTypes.Email, "ionut.ciobanu@email.com"),
                            new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        }
                    }
                };
            }
        }
    }
}