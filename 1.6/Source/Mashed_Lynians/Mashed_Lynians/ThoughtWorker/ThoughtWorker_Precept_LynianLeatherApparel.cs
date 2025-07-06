using System;
using Verse;
using RimWorld;

namespace Mashed_Lynians
{
    public class ThoughtWorker_Precept_LynianLeatherApparel : ThoughtWorker_Precept
	{
		protected override ThoughtState ShouldHaveThought(Pawn p)
		{
			return ThoughtWorker_LynianLeatherApparel.CurrentThoughtState(p);
		}
	}
}
