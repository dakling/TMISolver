using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using Symbolism;
using Symbolism.IsolateVariable;
using Symbolism.EliminateVariable;

namespace TMISolver{
    public abstract class Subsystem{
	protected Force[] ReactionForces;
	protected Moment[] ReactionMoments;
	protected Force[] ExternalForces;
	protected Moment[] ExternalMoments;
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
	public abstract Equation[] AssembleEquations(Point origin);
	public void PrintEquations(Point Reference){
	    var Equations = this.AssembleEquations(Reference);
	    Extensions.PrintArray(Equations);
	}
	public void PrintEquationsLatex(Point Reference){
	    var Equations = this.AssembleEquations(Reference);
	    Console.WriteLine("\\(");
	    foreach (var Equation in Equations)
	    {
		Console.Write(Equation.a);
		Console.Write(" = ");
		Console.Write(Equation.b);
		Console.Write("\\\\ \n");
	    }
	    Console.WriteLine("\\)");
	}
    }
    public class Subsystem2D : Subsystem{
	public Subsystem2D(Force[] _ReactionForces, Moment[] _ReactionMoments, Force[] _ExternalForces, Moment[] _ExternalMoments) {
	    ReactionForces = _ReactionForces;
	    ReactionMoments = _ReactionMoments;
	    ExternalForces = _ExternalForces;
	    ExternalMoments = _ExternalMoments;
	}
	public override Equation[] AssembleEquations(Point Reference) {
	    Force[] Forces = ReactionForces.Concat(ExternalForces).ToArray();
	    Moment[] Moments = ReactionMoments.Concat(ExternalMoments).ToArray();
	    int dimensions = 2;
	    var Balance = new Equation[dimensions+1];
	    for (int i = 0; i < dimensions; ++i)
	    {
		Balance[i] = this.AssembleForceEquation(i);
	    }
	    Balance[2] = this.AssembleMomentEquation(2, Reference);
	    return Balance;
	}
    }
    public class Subsystem3D : Subsystem{
	public Subsystem3D(Force[] _ReactionForces, Moment[] _ReactionMoments, Force[] _ExternalForces, Moment[] _ExternalMoments) {
	    ReactionForces = _ReactionForces;
	    ReactionMoments = _ReactionMoments;
	    ExternalForces = _ExternalForces;
	    ExternalMoments = _ExternalMoments;
	}
	public override Equation[] AssembleEquations(Point Reference) {
	    Force[] Forces = ReactionForces.Concat(ExternalForces).ToArray();
	    Moment[] Moments = ReactionMoments.Concat(ExternalMoments).ToArray();
	    int dimensions = 3;
	    var Balance = new Equation[2*dimensions];
	    for (int i = 0; i < dimensions; ++i)
	    {
		Balance[i] = this.AssembleForceEquation(i);
		Balance[i+dimensions] = this.AssembleMomentEquation(i, Reference);
	    }
	    return Balance;
	}
    }
}
