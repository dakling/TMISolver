using System;
using Symbolism;

namespace TMISolver{
    public static class TMISolver
    {
	static Equation AssembleForceEquation(int index, Force[] Forces)
	{
	    var Balance = new Equation(0,0);
	    foreach (var Force in Forces)
	    {
		Balance.a = Balance.a + Force.Index(index);
	    }
	    return Balance;
	    
	}

	static Equation AssembleMomentEquation(int index, Point Reference, Force[] Forces, Moment[] Moments)
	{
	    var Balance = new Equation(0,0);
	    foreach (var Moment in Moments)
	    {
		Balance.a = Balance.a + Moment.Index(index);
	    }
	    foreach (var Force in Forces)
	    {
		var distance = Extensions.Distance( Force.position, Reference);
		Balance.a = Balance.a - Extensions.CrossProduct(Force.vec, distance, index);
	    }
	    return Balance;
	}

	public static And AssembleEquations(Point2D Reference, Force2D[] Forces, Moment2D[] Moments)
	{
	    int dimensions = 2;
	    var Balance = new Equation[dimensions+1];
	    for (int i = 0; i < dimensions; ++i)
	    {
		Balance[i] = AssembleForceEquation(i, Forces);
	    }
	    Balance[2] = AssembleMomentEquation(2, Reference, Forces, Moments);
	    return new And(Balance);
	}
	public static And AssembleEquations(Point3D Reference, Force3D[] Forces, Moment3D[] Moments)
	{
	    int dimensions = 3;
	    var Balance = new Equation[2*dimensions];
	    for (int i = 0; i < dimensions; ++i)
	    {
		Balance[i] = AssembleForceEquation(i, Forces);
		Balance[i+dimensions] = AssembleMomentEquation(i, Reference, Forces, Moments);
	    }
	    return new And(Balance);
	}

	// TODO
    // 	static MathObject SolveBalanceEquations(And BalanceEquations, Symbol[] Unknowns, Force[] ExternalForces, Moment[] ExternalMoments, Symbol WantedVariable)
    // 	{
    // 	    return
    // 		BalanceEquations
    // 	    	.EliminateVariables(Unknowns.Where(val => val != WantedVariable).ToArray())
    // 	    	.IsolateVariable(WantedVariable);
    // 	}
    }
}
