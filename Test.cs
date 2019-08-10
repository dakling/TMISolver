using System;
using Symbolism;

namespace TMISolver{
    public class Tests
    {
	public static void BalkenTest()
	{
	    // introduce Symbols
	    var a = new Symbol("a");
	    var _F = new Symbol("F");
	    var _Ax = new Symbol("Ax");
	    var _Ay = new Symbol("Ay");
	    var _By = new Symbol("By");
	    var _M = new Symbol("M");
	    // relevante Punkte
	    var A = new Point(0, 0);
	    var B = new Point(a, 0);
	    var C = new Point(a/2, 0);
	    // Lagerreaktionen
	    var Ax = new Force(A, _Ax, 0);
	    var Ay = new Force(A, 0, _Ay);
	    var By = new Force(B, 0, _By);
	    // Externe Kräfte und Momente
	    var F = new Force(C, 0, -_F);
	    var M = new Moment(-_F*a);
	    
	    var ReactionForces = new Force[]{Ax, Ay, By};
	    var ExternalForces = new Force[]{F};
	    var ReactionMoments = new Moment[0];
	    var ExternalMoments = new Moment[]{M};
	    var BalkenExercise = new ReactionForceExercise(ReactionForces, ReactionMoments, ExternalForces, ExternalMoments);
	    var Balances = BalkenExercise.AssembleEquations2D(A);
	    // var testBalances = TMISolver.AssembleXEquation(testForces);
	    // var Sol = Balances
	    // 	.EliminateVariables(_Ay, _Ax)
	    // 	.IsolateVariable(_By);
	    // var Sol = TMISolver.SolveBalanceEquations(Balances, )
	    Console.WriteLine(Balances);
	}
    }
}
