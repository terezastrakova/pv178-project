using JapaneseProject.Components.Account.Pages;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System.Security.Claims;
using System.Text.Json;

namespace Microsoft.AspNetCore.Routing
{
	internal static class IdentityComponentsEndpointRouteBuilderExtensions
	{
		// These endpoints are required by the Identity Razor components defined in the /Components/Account/Pages directory of this project.
		public static IEndpointConventionBuilder MapAdditionalIdentityEndpoints(this IEndpointRouteBuilder endpoints)
		{
			ArgumentNullException.ThrowIfNull(endpoints);

			var accountGroup = endpoints.MapGroup("/Account");

			accountGroup.MapPost("/Logout", async (
				ClaimsPrincipal user,
				SignInManager<DataAccessLayer.Entities.ApplicationUser> signInManager,
				[FromForm] string returnUrl) =>
			{
				await signInManager.SignOutAsync();
				return TypedResults.LocalRedirect($"~/{returnUrl}");
			});
			endpoints.MapGroup("/Test").RequireAuthorization();
			return accountGroup;
		}
	}
}
