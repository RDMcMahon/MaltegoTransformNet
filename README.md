# MaltegoTransformNet
A .NET library for writing Maltego Transforms

# Basic Usage
<pre>
<code>
using System;
using MaltegoTransformNet.Core;
using MaltegoTransformNet.Core.Enums;

namespace MaltegoTransformNet.Cmd
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			//Parse the maltego arguments

			//Write your code to get data


			//Create the maltego response generator
			var maltego = new MaltegoResponseGenerator();

			//Add entities from the helper functions
			maltego.AddPersonEntity("John Doe", "John", "Doe", "Store Manager", "US", "Here are some notes");

			//Or you can make an entity from scratch and add it
			var entity = new Entity();
			entity.Type = EntityEnum.Phrase.ToString ();
			entity.Value = "John Doe";
			entity.AdditionalFields.Add (new Field {
				DisplayName = "#firstname",
				Name = "FirstName",
				Value = "John"
			});

			//Write the xml to the console
			Console.WriteLine (maltego.GetMaltegoMessageText());

		}
	}

}
</code>
</pre>
