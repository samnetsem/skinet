using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Identity;
using Core.Interfaces;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class UserMangerExtensions
    {
        public static async Task<AppUser> FindByUserByClamisPrincipleWithAddressAsync(this UserManager<AppUser> input,
        ClaimsPrincipal user)
        {
             var email=user?.Claims?.FirstOrDefault(x=>x.Type==ClaimTypes.Email)?.Value;

             return await input.Users.Include(x=> x.Address).SingleOrDefaultAsync(x=>x.Email==email);

        }

        public static async Task<AppUser> FindByEmailFromClamisPrinciple(this UserManager<AppUser> input,
        ClaimsPrincipal user)
        {
             var email=user?.Claims?.FirstOrDefault(x=>x.Type==ClaimTypes.Email)?.Value;

                return await input.Users.SingleOrDefaultAsync(x=>x.Email==email);

        }
    }
}