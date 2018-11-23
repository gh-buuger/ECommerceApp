using ECommerceApp.Models;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ECommerceApp.Security.OAuth
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly FlixOneStoreContext _context;

        public ResourceOwnerPasswordValidator(FlixOneStoreContext context)
        {
            _context = context;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            try
            {
                var customer = await _context.Customers.SingleOrDefaultAsync(x => x.Email == context.UserName);
                if (customer != null)
                {
                    if (customer.Password == context.Password)
                    {
                        context.Result = new GrantValidationResult(
                            subject: customer.Id.ToString(),
                            authenticationMethod: "database",
                            claims: GetUserClaims(customer));
                    }
                    else
                    {
                        context.Result = new GrantValidationResult(
                            TokenRequestErrors.InvalidGrant,
                            "Incorrect Password");
                    }
                }
                else
                {
                    context.Result = new GrantValidationResult(
                            TokenRequestErrors.InvalidGrant,
                            "User does not exist.");
                }
            }
            catch (Exception)
            {
                context.Result = new GrantValidationResult(
                            TokenRequestErrors.InvalidGrant,
                            "Invalid Username or Password.");
            }
        }

        public static IEnumerable<Claim> GetUserClaims(Customers customer)
        {
            string nameValue;
            if (string.IsNullOrEmpty(customer.Firstname) || string.IsNullOrEmpty(customer.Lastname))
            {
                nameValue = string.Empty;
            }
            else
            {
                nameValue = (customer.Firstname + " " + customer.Lastname);
            }


            return new Claim[]
            {
                new Claim(JwtClaimTypes.Id, customer.Id.ToString() ?? ""),
                new Claim(JwtClaimTypes.Name, nameValue),
                new Claim(JwtClaimTypes.GivenName, customer.Firstname ?? string.Empty),
                new Claim(JwtClaimTypes.FamilyName, customer.Lastname ?? string.Empty),
                new Claim(JwtClaimTypes.Email, customer.Email ?? string.Empty)
            };
        }
    }
}
