using System;
using Symbolism;

namespace TMISolver{
    public abstract class Force : DynamicVariable
    {
	public Point position;
	public override void Print()
	{
	    Console.WriteLine("Postion:");
	    this.position.Print();
	    Console.WriteLine("Force:");
	    this.vec.Print();
	}
    }

    public class Force2D : Force
    {
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
}
