using System;
using Symbolism;

namespace TMISolver {
    public static class Extensions {
	public static Vector Distance(Point p1, Point p2) {
	    var dx = p1.vec.x() - p2.vec.x();
	    var dy = p1.vec.y() - p2.vec.y();
	    var dz = p1.vec.z() - p2.vec.z();
	    return new Vector(dx, dy, dz);
	}
	public static MathObject CrossProduct(Vector a, Vector b, int i) {
	    switch (i) {
		case 0:
		    return a.y() * b.z() - a.z() * b.y();
		case 1:
		    return a.z() * b.x() - a.x() * b.z();
		case 2:
		    return a.x() * b.y() - a.y() * b.x();
		default:
		    throw new Exception("Index out of bounds");
	    }
	}
    }
}
    
