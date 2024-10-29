﻿namespace Structured_Cabling_Studio.Models
{
	public class RecommendationsArguments
	{
		public IsolationType IsolationType { get; set; }

		public IsolationMaterial IsolationMaterial { get; set; }

		public ShieldedType ShieldedType { get; set; }

		public List<ConnectionInterfaceStandard> ConnectionInterfaces { get; set; } = new() { ConnectionInterfaceStandard.None };
	}
}
