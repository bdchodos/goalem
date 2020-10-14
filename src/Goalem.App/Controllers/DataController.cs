using Goalem.App.Auth.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Goalem.App.Controllers
{
	[ApiController]
	[Authorize(GoalemPolicies.ReadData)]
	[Route("data")]
	public class DataController : ControllerBase
	{
		[HttpGet]
		public ActionResult<string> Get()
		{
			return "Hello!";
		}
	}
}
