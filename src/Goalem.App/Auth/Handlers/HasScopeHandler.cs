using System.Linq;
using System.Threading.Tasks;
using Goalem.App.Auth.Constants;
using Goalem.App.Auth.Requirements;
using Microsoft.AspNetCore.Authorization;

namespace Goalem.App.Auth.Handlers
{
	public class HasScopeHandler : AuthorizationHandler<HasScopeRequirement>
	{
		protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasScopeRequirement requirement)
		{
			// If user does not have the scope claim, get out of here
			if (!context.User.HasClaim(c => c.Type == GoalemClaimTypes.Scope
				&& c.Issuer == requirement.Issuer))
				return Task.CompletedTask;

			// Split the scopes string into an array
			var scopes = context.User.FindFirst(c => c.Type == GoalemClaimTypes.Scope
				&& c.Issuer == requirement.Issuer).Value.Split(' ');

			// Succeed if the scope array contains the required scope
			if (scopes.Any(s => s == requirement.Scope))
				context.Succeed(requirement);

			return Task.CompletedTask;
		}
	}
}
