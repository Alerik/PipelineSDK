namespace Pipeline.Generators
{
	/// <summary>
	/// Type of generator that is pure
	/// That is same input gives same output
	/// Memoizable
	/// </summary>
	public abstract class ProceduralGenerator<T> : Generator<T>
	{
		public sealed override T Generate()
		{
			return GenerateValue();
		}

		public abstract T GenerateValue();
	}
}
