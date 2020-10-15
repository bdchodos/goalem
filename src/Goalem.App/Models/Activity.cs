using System;

namespace Goalem.App.Models
{
	public abstract class Activity
	{
		public abstract DateTimeOffset ActivityDate { get; }

		public abstract ActivityType Type { get; }
	}
}
