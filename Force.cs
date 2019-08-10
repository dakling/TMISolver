using System;
using Symbolism;

namespace TMISolver{
    public class Force {
	public Point position;
	public Vector vec;

	public Force(Point pos, MathObject _Fx, MathObject _Fy) {
	    position = pos;
	    vec = new Vector(_Fx, _Fy);
	}
	public Force(Point pos, MathObject _Fx, MathObject _Fy, MathObject _Fz) {
	    position = pos;
	    vec = new Vector(_Fx, _Fy, _Fz);
	}
	public void Print() {
	    Console.WriteLine("Postion:");
	    this.position.Print();
	    Console.WriteLine("Force:");
	    this.vec.Print();
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
