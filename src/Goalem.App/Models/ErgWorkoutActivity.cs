using System;

namespace Goalem.App.Models
{
	public class ErgWorkoutActivity : Activity
	{
		public ErgWorkoutActivity(DateTimeOffset activityDate, int totalMeters)
		{
			ActivityDate = activityDate;
			TotalMeters = totalMeters;
		}

		public override DateTimeOffset ActivityDate { get; }

		public override ActivityType Type
			=> ActivityType.ErgWorkout;

		public int TotalMeters { get; }
	}
}
