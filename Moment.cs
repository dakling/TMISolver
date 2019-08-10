using System;
using Symbolism;

namespace TMISolver{
    public class Moment
    {
	public Vector vec;
	public void Print()
	{
	    Console.WriteLine("Moment:");
	    this.vec.Print();
	}
	public Moment(MathObject _Mz)
	{
	    vec = new Vector(new Symbol("0"), new Symbol("0"), _Mz);
	}
	public Moment(MathObject _Mx, MathObject _My, MathObject _Mz)
	{
	    vec = new Vector(_Mx, _My, _Mz);
	}
	public MathObject Index(int index){
	    switch (index) {
		case 0: {
		    return this.vec.x();
		}
		case 1: {
		    return this.vec.y();
		}
		case 2: {
		    return this.vec.z();
		}
		default:
		  throw new Exception("Index out of bounds!");
	    }
	}
    }
}
