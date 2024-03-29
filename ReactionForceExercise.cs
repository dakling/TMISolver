using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using Symbolism;
using Symbolism.IsolateVariable;
using Symbolism.EliminateVariable;

namespace TMISolver{
    public abstract class ReactionForceExercise{
	protected Symbol[] AllUnknowns;
	public Subsystem[] Subsystems;
	public Equation[] AssembleEquations(Point Reference) {
	    List<Equation> BalanceEquations = new List<Equation>();
	    foreach (var Subsystem in this.Subsystems)
	    {
		BalanceEquations.AddRange(Subsystem.AssembleEquations(Reference));
	    }
	    return BalanceEquations.ToArray();
	}
    	public MathObject[] SolveBalanceEquations()
    	{
	    var Origin = new Point(0,0,0);
	    var BalanceEquations = new And(this.AssembleEquations(Origin));
	    List<MathObject> Solution = new List<MathObject>();
	    foreach (Symbol Unknown in this.AllUnknowns) {
		Symbol[] VariablesToEliminate = Extensions.AllBut(AllUnknowns, Unknown);
		Solution.Add(
					   BalanceEquations
					   .EliminateVariables(VariablesToEliminate)
					   .IsolateVariable(Unknown)
					   );
	    }
    	    return Solution.ToArray();
    	}
	public void PrintEquations(Point Reference){
	    foreach (var Subsystem in this.Subsystems)
	    {
		Subsystem.PrintEquations(Reference);
	    }
	}
	public void PrintEquationsLatex(Point Reference){
	    foreach (var Subsystem in this.Subsystems)
	    {
		Subsystem.PrintEquationsLatex(Reference);
	    }
	}
	public void PrintSolution(){
	    var Solutions = this.SolveBalanceEquations();
	    Extensions.PrintArray(Solutions);
	}
    }
    public class ReactionForceExercise2D : ReactionForceExercise{
	public ReactionForceExercise2D(Subsystem2D[] _Subsystems, Symbol[] _AllUnknowns){
	    Subsystems = _Subsystems;
	    AllUnknowns = _AllUnknowns;
	}
	public ReactionForceExercise2D(Force[] _ReactionForces, Moment[] _ReactionMoments, Force[] _ExternalForces, Moment[] _ExternalMoments, Symbol[] _AllUnknowns) {
	    Subsystems = new Subsystem2D[]{new Subsystem2D (_ReactionForces, _ReactionMoments, _ExternalForces, _ExternalMoments)};
	    AllUnknowns = _AllUnknowns;
	}
    }
    public class ReactionForceExercise3D : ReactionForceExercise{
	public ReactionForceExercise3D(Subsystem3D[] _Subsystems, Symbol[] _AllUnknowns){
	    Subsystems = _Subsystems;
	    AllUnknowns = _AllUnknowns;
	}
	public ReactionForceExercise3D(Force[] _ReactionForces, Moment[] _ReactionMoments, Force[] _ExternalForces, Moment[] _ExternalMoments, Symbol[] _AllUnknowns) {
	    Subsystems = new Subsystem3D[]{new Subsystem3D (_ReactionForces, _ReactionMoments, _ExternalForces, _ExternalMoments)};
	    AllUnknowns = _AllUnknowns;
	}
    }
}
