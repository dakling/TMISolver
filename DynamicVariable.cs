using Symbolism;

namespace TMISolver{
    public abstract class DynamicVariable
    {
	public Vector vec;
	public abstract void Print();
	public abstract MathObject Index(int i);
    }
}
