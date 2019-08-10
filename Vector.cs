using System;
using Symbolism;

namespace TMISolver{
    public class Vector
    {
	public MathObject[] vec;
	public Vector(MathObject _x, MathObject _y)
	{
	    vec = new MathObject[]{_x, _y, 0};
	}
	public Vector(MathObject _x, MathObject _y, MathObject _z)
	{
	    vec = new MathObject[]{_x, _y, _z};
	}
	public MathObject x(){
	    return this.vec[0];
	}
	public MathObject y(){
	    return this.vec[1];
	}
	public MathObject z(){
	    return this.vec[2];
	}
	public MathObject Index(int index){
	    switch (index) {
		case 0: {
		    return this.x();
		}
		case 1: {
		    return this.y();
		}
		case 2: {
		    return this.z();
		}
		default:
		  throw new Exception("Index out of bounds!");
	    }
	}
	public void Print()
	{
	    Console.WriteLine(this.x()
			      + " , " +
			      this.y()
			      + " , " +
			      this.z());
	}
    }
}
