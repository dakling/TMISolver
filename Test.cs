using System;
using Symbolism;

using Symbolism.RationalizeExpression;

namespace TMISolver{
    public class Tests
    {
	public static void BalkenTest() {
	    // introduce Symbols
	    var a = new Symbol("a");
	    var F = new Symbol("F");
	    var Ax = new Symbol("Ax");
	    var Ay = new Symbol("Ay");
	    var By = new Symbol("By");
	    var M = new Symbol("M");
	    // relevante Punkte
	    var A = new Point(0, 0);
	    var B = new Point(a, 0);
	    var C = new Point(a/2, 0);
	    // Lagerreaktionen
	    var F_A = new Force(A, Ax, Ay);
	    var F_B = new Force(B, 0, By);
	    // Externe Kräfte und Momente
	    var F_Ext = new Force(C, 0, -F);
	    var M_Ext = new Moment(-F*a);
	    
	    var ReactionForces = new Force[]{F_A, F_B};
	    var ExternalForces = new Force[]{F_Ext};
	    var ReactionMoments = new Moment[0];
	    var ExternalMoments = new Moment[]{M_Ext};
	    var Unknowns = new Symbol[]{Ax, Ay, By};
	    var BalkenExercise = new ReactionForceExercise2D(ReactionForces, ReactionMoments, ExternalForces, ExternalMoments, Unknowns);
	    var Balances = BalkenExercise.AssembleEquations(A);
	    var Sol = BalkenExercise.SolveBalanceEquations();
	    Console.WriteLine(Balances);
	    foreach (var item in Sol)
	    {
	    	Console.WriteLine(item);
	    }
	}
	// public static void GelenkBalkenSystem(){
	//     // introduce Symbols
	//     var a = new Symbol("a");
	//     var F = new Symbol("F");
	//     var Ax = new Symbol("Ax");
	//     var Ay = new Symbol("Ay");
	//     var Ma = new Symbol("Ma");
	//     var By = new Symbol("By");
	//     var Gx = new Symbol("Gx");
	//     var Gy = new Symbol("Gy");
	//     var M = new Symbol("M");
	//     // relevante Punkte
	//     var A = new Point(0, 0);
	//     var B = new Point(a, 0);
	//     var C = new Point(a/2, 0);
	//     var D = new Point(3*a/4, 0);
	//     // Lagerreaktionen
	//     var F_A = new Force(A, _Ax, _Ay);
	//     var Ma = new Moment(_Ma);
	//     var Gx = new Force(C, _Gx, 0);
	//     var Gy = new Force(C, 0, _Gy);
	//     var By = new Force(B, 0, _By);
	//     // Externe Kräfte und Momente
	//     var F = new Force(D, 0, -_F);
	    
	//     // Gesamtsystem
	//     var ReactionForcesFull = new Force[]{Ax, Ay, By};
	//     var ExternalForcesFull= new Force[]{F};
	//     var ReactionMomentsFull= new Moment[]{Ma};
	//     var ExternalMomentsFull= new Moment[0];
	//     var FullSystem = new Subsystem2D(ReactionForcesFull, ReactionMomentsFull, ExternalForcesFull, ExternalMomentsFull)

	//     var Unknowns = new Symbol[]{_Ax, _Ay, _Ma, _By, _Gx, _Gy};
	// }
    }
}
