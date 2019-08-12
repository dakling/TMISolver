using System;
using Symbolism;

using Symbolism.RationalizeExpression;

namespace TMISolver {
    public class Tests {
	public static void BalkenTest() {
	    Console.WriteLine("Normaler Balken");
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
	    BalkenExercise.PrintEquations(A);
	    BalkenExercise.PrintSolution();
	}
	public static void GelenkBalkenSystem(){
	    Console.WriteLine("Balken mit Gelenk");
	    // introduce Symbols
	    var a = new Symbol("a");
	    var F = new Symbol("F");
	    var Ax = new Symbol("Ax");
	    var Ay = new Symbol("Ay");
	    var Ma = new Symbol("Ma");
	    var By = new Symbol("By");
	    var Gx = new Symbol("Gx");
	    var Gy = new Symbol("Gy");
	    var M = new Symbol("M");
	    // relevante Punkte
	    var A = new Point(0, 0);
	    var B = new Point(a, 0);
	    var C = new Point(a/2, 0);
	    var D = new Point(3*a/4, 0);
	    // Lagerreaktionen
	    var F_A = new Force(A, Ax, Ay);
	    var M_A = new Moment(Ma);
	    var F_G = new Force(C, Gx, Gy);
	    var F_B = new Force(B, 0, By);
	    // Externe Kräfte und Momente
	    var F_Ext = new Force(D, 0, -F);
	    
	    // Gesamtsystem
	    var ReactionForcesFull = new Force[]{F_A, F_B};
	    var ExternalForcesFull= new Force[]{F_Ext};
	    var ReactionMomentsFull= new Moment[]{M_A};
	    var ExternalMomentsFull= new Moment[0];
	    var FullSystem = new Subsystem2D(ReactionForcesFull, ReactionMomentsFull, ExternalForcesFull, ExternalMomentsFull);

	    var BalanceEquationsFull = (FullSystem.AssembleEquations(A));
	    Console.WriteLine("Gesamtsystem");
	    FullSystem.PrintEquationsLatex(A);
	    // Right Subsystem
	    var ReactionForcesRight = new Force[]{F_G, F_B};
	    var ExternalForcesRight = new Force[]{F_Ext};
	    var ReactionMomentsRight= new Moment[0];
	    var ExternalMomentsRight= new Moment[0];
	    var RightSystem = new Subsystem2D(ReactionForcesRight, ReactionMomentsRight, ExternalForcesRight, ExternalMomentsRight);
	    Console.WriteLine("Rechtes Teilsystem");
	    var BalanceEquationsRight = (RightSystem.AssembleEquations(C));
	    RightSystem.PrintEquations(C);

	    // Everything together
	    var Unknowns = new Symbol[]{Ax, Ay, Ma, By, Gx, Gy};
	    var Exercise = new ReactionForceExercise2D(new Subsystem2D[]{FullSystem, RightSystem}, Unknowns);

	    Console.WriteLine("Alles zusammen");
	    var Sol = Exercise.SolveBalanceEquations();
	    Exercise.PrintSolution();
	}
    }
}
