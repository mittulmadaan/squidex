﻿// ==========================================================================
//  ICIS Copyright
// ==========================================================================
//  Copyright (c) ICIS
//  All rights reserved.
// ==========================================================================

using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Squidex.ICIS.Extensions;
using Squidex.ICIS.Interfaces;
using Squidex.ICIS.Models;
using Squidex.Infrastructure.Security;

namespace Squidex.ICIS
{
    public class ClaimsTransformer : IClaimsTransformation
    {
        private readonly IClaimsManager claimsManager;
        private readonly MemoryCache _memoryCache = new MemoryCache(Options.Create(new MemoryCacheOptions()));


        public ClaimsTransformer(IClaimsManager claimsManager)
        {
            this.claimsManager = claimsManager;
        }

        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            var identity = principal.Identities.First();

            var key = identity.Claims.FirstOrDefault(claim =>
                claim.Type.Equals(OpenIdClaims.Email, StringComparison.OrdinalIgnoreCase))?.Value;

            var user = await _memoryCache.GetOrCreateAsync(key, entry =>
            {
                entry.AbsoluteExpiration = DateTimeOffset.UtcNow.AddHours(8);
                return Task.FromResult(claimsManager.CreateUserWithClaims(identity));
            });

            AddClaims(identity, user);

            return principal;
        }

        private void AddClaims(ClaimsIdentity identity, UserInfo userInfo)
        {
            var squidexClaims = userInfo.ToUserValues().ToClaims();
            foreach (var squidexClaim in from squidexClaim in squidexClaims
                let found = identity.HasClaim((claim) => AreClaimsEqual(claim, squidexClaim))
                where !found
                select squidexClaim)
            {
                identity.AddClaim(squidexClaim);
            }

            var subClaim = new Claim(OpenIdClaims.Subject, userInfo.UserId);
            identity.AddClaim(subClaim);

        }

        private bool AreClaimsEqual(Claim identityClaim, Claim squidexClaim)
        {
            return identityClaim.Type.Equals(squidexClaim.Type, StringComparison.OrdinalIgnoreCase) &&
                   identityClaim.Value.Equals(squidexClaim.Value, StringComparison.OrdinalIgnoreCase);
        }
    }
}