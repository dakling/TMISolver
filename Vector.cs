using System;
using Symbolism;

namespace TMISolver{
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
	    Console.WriteLine(this.x + " , " + this.y);
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
			      + " , " +
			      this.y
			      + " , " +
			      this.z);
	}
    }
}
