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
    public static class Extensions
    {
	public static Vector2D Distance(Point2D p1, Point2D p2)
	{
	    var dx = p1.vec.x - p2.vec.x;
	    var dy = p1.vec.y - p2.vec.y;
	    return new Vector2D(dx, dy);
	}

	public static Vector3D Distance(Point3D p1, Point3D p2)
	{
	    var dx = p1.vec.x - p2.vec.x;
	    var dy = p1.vec.y - p2.vec.y;
	    var dz = p1.vec.z - p2.vec.z;
	    return new Vector3D(dx, dy, dz);
	}
    }
    public abstract class Vector
    {
	public MathObject x;
	public MathObject y;
	public MathObject z;
	public abstract void Print();
    }

    public class Vector2D : Vector
    {
	public Vector2D(MathObject _x, MathObject _y)
	{
	    x = _x;
	    y = _y;
	    z = new Symbol("0");
	}
	public override void Print()
	{
	    Console.WriteLine(this.x + " " + this.y);
	}
	public static MathObject CrossProduct(Vector2D a, Vector2D b)
	{
	    return a.x * b.y - a.y * b.x;
	}
    }

    public class Vector3D : Vector
    {
	public Vector3D(MathObject _x, MathObject _y, MathObject _z)
	{
	    x = _x;
	    y = _y;
	    z = _z;
	}
	public override void Print()
	{
	    Console.WriteLine(this.x
			      + " " +
			      this.y
			      + " " +
			      this.z);
	}
	public static MathObject CrossProduct(Vector3D a, Vector3D b, int i)
	{
	    switch (i) {
		case 0:
		    return a.y * b.z - a.z * b.y;
		case 1:
		    return a.z * b.x - a.x * b.z;
		case 2:
		    return a.x * b.y - a.y * b.x;
		default:
		    throw new Exception("Index out of bounds");
	    }
	}
    }
    public abstract class Point
    {
	public Vector vec;
	public void Print()
	{
	    this.vec.Print();
	}
    }

    public class Point2D : Point
    {
	public Point2D(MathObject x, MathObject y)
	{
	    vec = new Vector2D(x, y);
	}


	// public Point2D Point2DFromPolar(MathObject r, MathObject phi)
	// {
	//     var x = r*cos(phi);
	//     MathObject y = r*sin(phi);
	//     return new Point2D(x,y);
	// }

	// public MathObject R()
	// {
	//     return Math.Sqrt(xCoordinate*xCoordinate + yCoordinate*yCoordinate);
	// }

	// public MathObject Phi()
	// {
	//     return Math.Tan(yCoordinate/xCoordinate);
	// }


	// public void PrintPolar()
	// {
	//     Console.WriteLine(this.R() + " " + this.Phi());
	// }

    }
    public class Point3D : Point
    {
	public Point3D(MathObject x, MathObject y, MathObject z)
	{
	    vec = new Vector3D(x, y, z);
	}
    }

    public abstract class Force
    {
	public Point position;
	public Vector vec;
	public void Print()
	{
	    this.position.Print();
	    this.vec.Print();
	}
	public abstract MathObject Index(int i);
    }

    public class Force2D : Force
    {
	// public Point2D position;
	// public Vector2D vec;

	public Force2D(Point2D pos, MathObject _Fx, MathObject _Fy)
	{
	    position = pos;
	    vec = new Vector2D(_Fx, _Fy);
	}

	public override MathObject Index(int i)
	{
	    switch (i)
	    {
		case 0:
		    return this.vec.x;
		case 1:
		    return this.vec.y;
		default:
		    throw new Exception("Index out of bounds!");
	    }
	}
    }

    public class Force3D : Force
    {
	// public Point3D position;
	// public Vector3D vec;

	Force3D(Point3D pos, MathObject _Fx, MathObject _Fy, MathObject _Fz)
	{
	    position = pos;
	    vec = new Vector3D(_Fx, _Fy, _Fz);
	}

	public override MathObject Index(int i)
	{
	    switch (i)
	    {
		case 0:
		    return this.vec.x;
		case 1:
		    return this.vec.y;
		case 2:
		    return this.vec.z;
		default:
		    throw new Exception("Index out of bounds!");
	    }
	}
    }

    public abstract class Moment
    {
	public Vector vec;
	public void Print()
	{
	    this.vec.Print();
	}
	public abstract MathObject Index(int i);

    }

    public class Moment2D : Moment
    {
	// public Vector2D vec;
	public Moment2D(MathObject _Mz)
	{
	    vec = new Vector3D(new Symbol("0"), new Symbol("0"), _Mz);
	}
	public override MathObject Index(int i)
	{
	    switch (i)
	    {
		case 2:
		    return this.vec.z;
		default:
		    throw new Exception("Index out of bounds!");
	    }
	}
    }

    public class Moment3D : Moment
    {
	public Moment3D(MathObject _Mx, MathObject _My, MathObject _Mz)
	{
	    vec = new Vector3D(_Mx, _My, _Mz);
	}
	public override MathObject Index(int i)
	{
	    switch (i)
	    {
		case 0:
		    return this.vec.x;
		case 1:
		    return this.vec.y;
		case 2:
		    return this.vec.z;
		default:
		    throw new Exception("Index out of bounds!");
	    }
	}
    }

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
	static Equation AssembleMomentEquation(Point2D Reference, Force2D[] Forces, Moment2D[] Moments)
	{
	    var Balance = new Equation(0,0);
	    foreach (var Moment in Moments)
	    {
		Balance.a = Balance.a + Moment.Index(2);
	    }
	    foreach (var Force in Forces)
	    {
		var distance = Extensions.Distance((Point2D) Force.position, (Point2D) Reference);
		Balance.a = Balance.a - Vector2D.CrossProduct((Vector2D) Force.vec, distance);
	    }
	    return Balance;
	}
	static Equation AssembleMomentEquation(int index, Point3D Reference, Force3D[] Forces, Moment3D[] Moments)
	{
	    var Balance = new Equation(0,0);
	    foreach (var Moment in Moments)
	    {
		Balance.a = Balance.a + Moment.Index(index);
	    }
	    foreach (var Force in Forces)
	    {
		var distance = Extensions.Distance((Point3D) Force.position, (Point3D) Reference);
		Balance.a = Balance.a - Vector3D.CrossProduct((Vector3D) Force.vec, distance, index);
	    }
	    return Balance;
	}
	// static Equation AssembleMomentEquation(int index, Point Reference, Force[] Forces, Moment[] Moments)
	// {
	//     var Balance = new Equation(0,0);
	//     foreach (var Moment in Moments)
	//     {
	// 	Balance.a = Balance.a + Moment.Index(index);
	//     }
	//     foreach (var Force in Forces)
	//     {
	// 	var distance = Extensions.Distance((Point3D) Force.position, (Point3D) Reference);
	// 	Balance.a = Balance.a - Vector3D.CrossProduct((Vector3D) Force.vec, distance, index);
	//     }
	//     return Balance;
	// }

    	// static And AssembleEquations(int dimensions, Point Reference, Force[] Forces, Moment[] Moments)
    	// {
	//     var Balance = new Equation[2*dimensions];
	//     for (int i = 0; i < dimensions; ++i)
	//     {
	// 	Balance[i] = AssembleForceEquation(i, Forces);
	// 	Balance[i+dimensions] = AssembleMomentEquation(i, Reference, Forces, Moments);
	//     }
	//     return new And(Balance);
	// }

	public static And AssembleEquations(Point2D Reference, Force2D[] Forces, Moment2D[] Moments)
	{
	    // return AssembleEquations(2, Reference, Forces, Moments);
	    int dimensions = 2;
	    var Balance = new Equation[dimensions+1];
	    for (int i = 0; i < dimensions; ++i)
	    {
		Balance[i] = AssembleForceEquation(i, Forces);
	    }
	    Balance[2] = AssembleMomentEquation(Reference, Forces, Moments);
	    return new And(Balance);
	}
	public static And AssembleEquations(Point3D Reference, Force3D[] Forces, Moment3D[] Moments)
	{
	    // return AssembleEquations(3, Reference, Forces, Moments);
	    int dimensions = 3;
	    var Balance = new Equation[2*dimensions];
	    for (int i = 0; i < dimensions; ++i)
	    {
		Balance[i] = AssembleForceEquation(i, Forces);
		Balance[i+dimensions] = AssembleMomentEquation(i, Reference, Forces, Moments);
	    }
	    return new And(Balance);
	}
    }

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
	    var A = new Point2D(0, 0);
	    var B = new Point2D(a, 0);
	    var C = new Point2D(a/2, 0);
	    // Lagerreaktionen
	    var Ax = new Force2D(A, _Ax, 0);
	    var Ay = new Force2D(A, 0, _Ay);
	    var By = new Force2D(B, 0, _By);
	    // Externe Kräfte und Momente
	    var F = new Force2D(C, 0, -_F);
	    var M = new Moment2D(-_F*a);
	    
	    var Forces = new Force2D[]{Ax, Ay, By, F};
	    var Moments = new Moment2D[]{M};
	    var Balances = TMISolver.AssembleEquations(A, Forces, Moments);
	    // var testBalances = TMISolver.AssembleXEquation(testForces);
	    Console.WriteLine(Balances);
	}
    }
    public class MainClass
    {
	public static void Main()
	{
	    Tests.BalkenTest();
	}
    }
}
