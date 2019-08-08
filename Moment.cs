using System;
using Symbolism;

namespace TMISolver{
    public abstract class Moment : DynamicVariable
    {
	public override void Print()
	{
	    Console.WriteLine("Moment:");
	    this.vec.Print();
	}
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
}
