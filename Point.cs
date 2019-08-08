using System;
using Symbolism;

namespace TMISolver{
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
}
