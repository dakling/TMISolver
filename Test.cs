using System;
using Symbolism;

using Symbolism.RationalizeExpression;

namespace TMISolver {
    public class Tests {
	public static void BalkenTest() {
	    Console.WriteLine("Normaler Balken");
	    // introduce symbols
	    var a = new Symbol("a");
	    var F = new Symbol("F");
	    var Ax = new Symbol("A_x");
	    var Ay = new Symbol("A_y");
	    var By = new Symbol("B_y");
	    var M = new Symbol("M");
	    // relevant points
	    var A = new Point(0, 0);
	    var B = new Point(a, 0);
	    var C = new Point(a/2, 0);
	    // support reactions
	    var F_A = new Force(A, Ax, Ay);
	    var F_B = new Force(B, 0, By);
	    // external forces and moments
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
	    Console.WriteLine("Beam with hinge");
	    // introduce Symbols
	    var a = new Symbol("a");
	    var F = new Symbol("F");
	    var Ax = new Symbol("A_x");
	    var Ay = new Symbol("A_y");
	    var Ma = new Symbol("M_a");
	    var By = new Symbol("B_y");
	    var Gx = new Symbol("G_x");
	    var Gy = new Symbol("G_y");
	    var M = new Symbol("M");
	    // relevant points
	    var A = new Point(0, 0);
	    var B = new Point(a, 0);
	    var C = new Point(a/2, 0);
	    var D = new Point(3*a/4, 0);
	    // support reactions
	    var F_A = new Force(A, Ax, Ay);
	    var M_A = new Moment(Ma);
	    var F_G = new Force(C, Gx, Gy);
	    var F_B = new Force(B, 0, By);
	    // external forces and moments
	    var F_Ext = new Force(D, 0, -F);
	    
	    // full system
	    var ReactionForcesFull = new Force[]{F_A, F_B};
	    var ExternalForcesFull= new Force[]{F_Ext};
	    var ReactionMomentsFull= new Moment[]{M_A};
	    var ExternalMomentsFull= new Moment[0];
	    var FullSystem = new Subsystem2D(ReactionForcesFull, ReactionMomentsFull, ExternalForcesFull, ExternalMomentsFull);

	    var BalanceEquationsFull = (FullSystem.AssembleEquations(A));
	    Console.WriteLine("Gesamtsystem");
	    FullSystem.PrintEquations(A);
	    Console.WriteLine("Using Latex output:");
	    FullSystem.PrintEquationsLatex(A);
	    // right Subsystem
	    var ReactionForcesRight = new Force[]{F_G, F_B};
	    var ExternalForcesRight = new Force[]{F_Ext};
	    var ReactionMomentsRight= new Moment[0];
	    var ExternalMomentsRight= new Moment[0];
	    var RightSystem = new Subsystem2D(ReactionForcesRight, ReactionMomentsRight, ExternalForcesRight, ExternalMomentsRight);
	    Console.WriteLine("Rechtes Teilsystem");
	    var BalanceEquationsRight = (RightSystem.AssembleEquations(C));
	    RightSystem.PrintEquations(C);

	    // everything together
	    var Unknowns = new Symbol[]{Ax, Ay, Ma, By, Gx, Gy};
	    var Exercise = new ReactionForceExercise2D(new Subsystem2D[]{FullSystem, RightSystem}, Unknowns);

	    Console.WriteLine("Everything together to calculate solution:");
	    var Sol = Exercise.SolveBalanceEquations();
	    Exercise.PrintSolution();
	}
    }
}
