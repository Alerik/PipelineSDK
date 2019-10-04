using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Pipeline.Controls;
using Pipeline.Plugins.Pipeline;

namespace Pipeline.Generators
{
	public abstract class Generator
	{
		[System.AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
		public class GeneratorInfo : System.Attribute
		{
			public const string DefaultDescription = "No description provided";
			public const string DefaultCategory = "Default";
			public const string DefaultHTMLPath = "";

			public string Name { get; private set; }
			public string Description { get; private set; }
			public string Category { get; private set; }
			public Type Plugin { get; private set; }
			public string HTMLPath { get; private set; }
			public string[] Tags { get; private set; }
			public GeneratorInfo(string _Name = null, string _Description = null, string _Category = null, 
				Type _Plugin = null, string _HTMLPath = null, params string[] _Tags
				)
			{
				this.Name = _Name;
				this.Description = _Description;
				this.Category = _Category;
				this.Plugin = _Plugin;
				this.HTMLPath = _HTMLPath;
				this.Tags = _Tags;
			}
		}
		internal class PipelineGeneratorInfo : GeneratorInfo
		{
			public PipelineGeneratorInfo(string _Name, string _Description = "No description provided", string _Category = "Default",
				 string _HTMLPath = "", params string[] _Tags
				) : base(_Name, _Description, _Category, typeof(PipelinePlugin), _HTMLPath, _Tags)
			{}
		}

		[System.AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
		public class ArgumentInfo : System.Attribute
		{
			public string Name { get; private set; }
			public string Hint { get; private set; }
			public ArgumentInfo(string _Name, string _Hint = "")
			{
				this.Name = _Name;
				this.Hint = _Hint;
			}
		}
	}
	public abstract class Generator<T> : Generator
	{
		public (PropertyInfo, ControlCreationAttribute)[] Properties
		{
			get
			{
				List<(PropertyInfo, ControlCreationAttribute)> properties = new List<(PropertyInfo, ControlCreationAttribute)>();
				foreach (PropertyInfo info in GetType().GetProperties())
				{
					ControlCreationAttribute attribute = info.GetCustomAttribute<ControlCreationAttribute>();
					if (attribute != null)
						properties.Add((info, attribute));
				}
				return properties.ToArray();
			}
		}

		/// <summary>
		/// This method returns the item created by GenerateResults
		/// This will memoize the values
		/// </summary>
		/// <returns>GenerateResults value</returns>
		public abstract T Generate();
		public Task<T> GenerateAsync()
		{
			return Task.Run(() => { return Generate(); });
		}
		public virtual void Change() { }
    }
}
