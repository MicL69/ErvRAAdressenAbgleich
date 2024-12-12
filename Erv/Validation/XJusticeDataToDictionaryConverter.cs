using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Erv.Validation
{
    internal class XJusticeDataToDictionaryConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType) {
            return objectType == typeof(Dictionary<string, string>);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer) {
            var dictionary = new Dictionary<string, string>();
            var outerArray = JArray.Load(reader);

            foreach (var innerArray in outerArray.Cast<JArray>()) {
                if (innerArray.Count != 2) continue;
                var key = innerArray[0].ToString();
                var value = innerArray[1].ToString();
                dictionary[key] = value; // Using indexer for potential duplicate keys
            }

            return dictionary;
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer) {
            if (!(value is Dictionary<string, string> dictionary)) {
                writer.WriteNull();
                return;
            }

            writer.WriteStartArray(); // Start the outer array

            foreach (var kvp in dictionary) {
                writer.WriteStartArray(); // Start the inner array
                writer.WriteValue(kvp.Key);
                writer.WriteValue(kvp.Value);
                writer.WriteEndArray(); // End the inner array
            }

            writer.WriteEndArray(); // End the outer array
        }
    }
}
