namespace Pipeline.Generators
{
	/// <summary>
	/// Generator where result is random
	/// i.e. Random numbers, not really memoizable
	/// </summary>
	public abstract class UpdatableGenerator<T> : Generator<T>
	{
		private T cached = default(T);
		private bool changed = true;
		/// <summary>
		/// Generate method to take care of threading and memoization
		/// </summary>
		/// <returns></returns>
		public sealed override T Generate()
		{
			if (!changed)
				return cached;
			else
			{
				changed = false;
				return cached = GenerateValue();
			}
		}

		public override void Change()
		{
			changed = true;
		}

		/// <summary>
		/// User defined GenerateMethod
		/// </summary>
		/// <returns></returns>
		protected abstract T GenerateValue();
	}
}
