using System;
using System.Collections.Generic;
using System.Linq;
using Symbolism;
using Symbolism.Substitute;
using Symbolism.EliminateVariable;
using Symbolism.IsolateVariable;
using Symbolism.LogicalExpand;
using Symbolism.SimplifyLogical;
using Symbolism.Trigonometric;

using Symbolism.Utils;

using static Symbolism.Constructors;
using static Symbolism.Trigonometric.Constructors;

namespace TMISolver
{
    public class MainClass
    {
	public static void Main()
	{
	    Tests.BalkenTest();
	    Tests.GelenkBalkenSystem();
	}
    }
}
