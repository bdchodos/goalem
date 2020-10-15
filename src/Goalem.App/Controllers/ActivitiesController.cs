using System;
using System.Collections.Generic;
using System.Linq;
using Goalem.App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Goalem.App.Controllers
{
	[ApiController]
	[Route("activities")]
	public class ActivitiesController : ControllerBase
	{
		private readonly ILogger _logger;

		public ActivitiesController(
			ILogger<ActivitiesController> logger)
		{
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		[HttpGet]
		public IEnumerable<Activity> Get(
			[FromQuery] int page = 0,
			[FromQuery] int pageSize = 5)
		{
			return GetErgWorkouts(page, pageSize);
		}

		[HttpGet(nameof(ActivityType.ErgWorkout))]
		public IEnumerable<ErgWorkoutActivity> GetErgWorkouts(
			[FromQuery] int page = 0,
			[FromQuery] int pageSize = 5)
		{
			var rng = new Random();

			return Enumerable.Range(page*pageSize, pageSize)
				.Select(i => new ErgWorkoutActivity(
					DateTimeOffset.Now.AddDays(i),
					rng.Next(1, 4) * 5000))
				.ToArray();
		}
	}
}
