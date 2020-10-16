using System.Linq;
using Goalem.App.Controllers;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Goalem.UnitTests.App.Controllers
{
	[TestClass]
	public class ActivitiesControllerTests
	{
		[TestMethod("GetErgWorkouts_should_return_10_records")]
		public void GetErgWorkoutsTest()
		{
			var controller = new ActivitiesController(
				new NullLogger<ActivitiesController>());

			var workouts = controller.GetErgWorkouts(
				page: 0,
				pageSize: 10);

			Assert.AreEqual(10, workouts.Count());
		}
	}
}
