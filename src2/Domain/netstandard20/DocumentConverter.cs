using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace TaleLearnCode.SpeakingEngagementManager.Domain
{

	/// <summary>
	/// Convert an object implementing the <see cref="IDocument"/> interface to and from JSON.
	/// </summary>
	/// <seealso cref="JsonConverter" />
	public class DocumentConverter : JsonConverter
	{

		/// <summary>
		/// Determines whether this instance can convert the specified object type.
		/// </summary>
		/// <param name="objectType">Type of the object.</param>
		/// <returns>
		/// <c>true</c> if this instance can convert the specified object type; otherwise, <c>false</c>.
		/// </returns>
		public override bool CanConvert(Type objectType)
		{
			return (objectType == typeof(IDocument));
		}

		/// <summary>
		/// Reads the JSON representation of the object.
		/// </summary>
		/// <param name="reader">The <see cref="T:Newtonsoft.Json.JsonReader" /> to read from.</param>
		/// <param name="objectType">Type of the object.</param>
		/// <param name="existingValue">The existing value of object being read.</param>
		/// <param name="serializer">The calling serializer.</param>
		/// <returns>
		/// The object value.
		/// </returns>
		/// <exception cref="System.Exception">Invalid discriminator found in the supplied JSON; unable to deserialize</exception>
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{

			JObject jObject = JObject.Load(reader);
			var discriminator = (string)jObject["discriminator"];

			object target;
			switch (discriminator)
			{
				case Discriminators.Country:
					target = new Country();
					break;
				case Discriminators.CountryDivision:
					target = new CountryDivision();
					break;
				case Discriminators.Presentation:
					target = new Presentation();
					break;
				case Discriminators.Shindig:
					target = new Shindig();
					break;
				case Discriminators.ShindigPresentation:
					target = new ShindigPresentation();
					break;
				case Discriminators.ShindigSubmission:
					target = new ShindigSubmission();
					break;
				default:
					throw new Exception("Invalid discriminator found in the supplied JSON; unable to deserialize");
			}

			serializer.Populate(jObject.CreateReader(), target);
			return target;

		}

		/// <summary>
		/// Writes the JSON representation of the object.
		/// </summary>
		/// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
		/// <param name="value">The value.</param>
		/// <param name="serializer">The calling serializer.</param>
		/// <exception cref="System.Exception">Unrecognizable document type; unable to serialize.</exception>
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{

			Type writeType;
			if (value is Country)
				writeType = typeof(Country);
			else if (value is CountryDivision)
				writeType = typeof(CountryDivision);
			else if (value is Presentation)
				writeType = typeof(Presentation);
			else if (value is Shindig)
				writeType = typeof(Shindig);
			else if (value is ShindigPresentation)
				writeType = typeof(ShindigPresentation);
			else if (value is ShindigSubmission)
				writeType = typeof(ShindigSubmission);
			else
				throw new Exception("Unrecognizable document type; unable to serialize.");

			serializer.Serialize(writer, value, writeType);

		}

	}

}