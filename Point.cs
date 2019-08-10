using System;
using Symbolism;

namespace TMISolver{
    public class Point
    {
	public Vector vec;
	public void Print() {
	    this.vec.Print();
	}
	public Point(MathObject x, MathObject y) {
	    vec = new Vector(x, y);
	}
	public Point(MathObject x, MathObject y, MathObject z) {
	    vec = new Vector(x, y, z);
	}
    }
}
