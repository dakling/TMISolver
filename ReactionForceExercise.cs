using System;
using System.Linq;
using System.Diagnostics;
using Symbolism;

namespace TMISolver{
    public class ReactionForceExercise{
	Force[] ReactionForces;
	Moment[] ReactionMoments;
	Force[] ExternalForces;
	Moment[] ExternalMoments;
	// int Dimension;
	Point Origin;
	public ReactionForceExercise(Force[] _ReactionForces, Moment[] _ReactionMoments, Force[] _ExternalForces, Moment[] _ExternalMoments) {
	    // TODO assert all either 2d or 3d
	    ReactionForces = _ReactionForces;
	    ReactionMoments = _ReactionMoments;
	    ExternalForces = _ExternalForces;
	    ExternalMoments = _ExternalMoments;
	    if (ReactionForces is Force2D[]){
		// Dimension = 2;
		var Origin = new Point2D(0,0);
	    }
	    else if (ReactionForces is Force3D[]){
		// Dimension = 3;
		var Origin = new Point3D(0,0,0);
	    }
	    else{
		throw new Exception("Must be 2D or 3D");
	    }
	}

	Equation AssembleForceEquation(int index)
	{
	    Force[] Forces = ReactionForces.Concat(ExternalForces).ToArray();
	    var Balance = new Equation(0,0);
	    foreach (var Force in Forces)
	    {
		Balance.a = Balance.a + Force.Index(index);
	    }
	    return Balance;
	}

	Equation AssembleMomentEquation(int index, Point Reference)
	{
	    Force[] Forces = ReactionForces.Concat(ExternalForces).ToArray();
	    Moment[] Moments = ReactionMoments.Concat(ExternalMoments).ToArray();
	    var Balance = new Equation(0,0);
	    foreach (var Moment in Moments)
	    {
		Balance.a = Balance.a + Moment.Index(index);
	    }
	    foreach (var Force in Forces)
	    {
		var distance = Extensions.Distance(Force.position, Reference);
		Balance.a = Balance.a - Extensions.CrossProduct(Force.vec, distance, index);
	    }
	    return Balance;
	}

	public And AssembleEquations(Point2D Reference)
	{
	    Force[] Forces = ReactionForces.Concat(ExternalForces).ToArray();
	    Moment[] Moments = ReactionMoments.Concat(ExternalMoments).ToArray();
	    int dimensions = 2;
	    var Balance = new Equation[dimensions+1];
	    for (int i = 0; i < dimensions; ++i)
	    {
		Balance[i] = this.AssembleForceEquation(i);
	    }
	    Balance[2] = this.AssembleMomentEquation(2, Reference);
	    return new And(Balance);
	}
	public And AssembleEquations(Point3D Reference)
	{
	    Force[] Forces = ReactionForces.Concat(ExternalForces).ToArray();
	    Moment[] Moments = ReactionMoments.Concat(ExternalMoments).ToArray();
	    int dimensions = 3;
	    var Balance = new Equation[2*dimensions];
	    for (int i = 0; i < dimensions; ++i)
	    {
		Balance[i] = this.AssembleForceEquation(i);
		Balance[i+dimensions] = this.AssembleMomentEquation(i, Reference);
	    }
	    return new And(Balance);
	}
    	// public MathObject[] SolveBalanceEquations()
    	// {
	//     var BalanceEquations = this.AssembleEquations(Origin);
	//     MathObject[] Solution = new MathObject[0];
	//     foreach (var ReactionForce in ReactionForces)
	//     {
	// 	var VariablesToEliminate = 
	// 	Solution = Solution.Concat(
	// 				   BalanceEquations
	// 				   .EliminateVariables()
	// 				   .IsolateVariable(ReactionForce)
	// 				   );
	//     }
    	//     return
    	// 	BalanceEquations
    	//     	.EliminateVariables()
    	//     	.IsolateVariable(WantedVariable)
	// 	;
    	// }
    }
}
