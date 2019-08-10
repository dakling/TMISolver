using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using Symbolism;
using Symbolism.IsolateVariable;
using Symbolism.EliminateVariable;

namespace TMISolver{
    public abstract class ReactionForceExercise{
	protected Force[] ReactionForces;
	protected Moment[] ReactionMoments;
	protected Force[] ExternalForces;
	protected Moment[] ExternalMoments;
	protected Symbol[] AllUnknowns;
	// int Dimension;

	protected Equation AssembleForceEquation(int index) {
	    Force[] Forces = ReactionForces.Concat(ExternalForces).ToArray();
	    var Balance = new Equation(0,0);
	    foreach (var Force in Forces)
	    {
		Balance.a = Balance.a + Force.Index(index);
	    }
	    return Balance;
	}

	protected Equation AssembleMomentEquation(int index, Point Reference) {
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
	public abstract And AssembleEquations(Point origin);
    	public MathObject[] SolveBalanceEquations()
    	{
	    var Origin = new Point(0,0,0);
	    var BalanceEquations = this.AssembleEquations(Origin);
	    List<MathObject> Solution = new List<MathObject>();
	    foreach (Symbol Unknown in this.AllUnknowns)
	    {
		Symbol[] VariablesToEliminate = Extensions.AllBut(AllUnknowns, Unknown);
		Solution.Add(
					   BalanceEquations
					   .EliminateVariables(VariablesToEliminate)
					   .IsolateVariable(Unknown)
					   );
	    }
    	    return Solution.ToArray();
    	}
    }
    public class ReactionForceExercise2D : ReactionForceExercise{
	public ReactionForceExercise2D(Force[] _ReactionForces, Moment[] _ReactionMoments, Force[] _ExternalForces, Moment[] _ExternalMoments, Symbol[] _AllUnknowns) {
	    // TODO assert all either 2d or 3d
	    ReactionForces = _ReactionForces;
	    ReactionMoments = _ReactionMoments;
	    ExternalForces = _ExternalForces;
	    ExternalMoments = _ExternalMoments;
	    AllUnknowns = _AllUnknowns;
	}
	public override And AssembleEquations(Point Reference) {
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
    }
    public class ReactionForceExercise3D : ReactionForceExercise{
	public ReactionForceExercise3D(Force[] _ReactionForces, Moment[] _ReactionMoments, Force[] _ExternalForces, Moment[] _ExternalMoments, Symbol[] _AllUnknowns) {
	    // TODO assert all either 2d or 3d
	    ReactionForces = _ReactionForces;
	    ReactionMoments = _ReactionMoments;
	    ExternalForces = _ExternalForces;
	    ExternalMoments = _ExternalMoments;
	    AllUnknowns = _AllUnknowns;
	}
	public override And AssembleEquations(Point Reference) {
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
    }
}
