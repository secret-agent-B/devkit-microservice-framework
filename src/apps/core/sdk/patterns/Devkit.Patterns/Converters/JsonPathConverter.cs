// -----------------------------------------------------------------------
// <copyright file="JsonPathConverter.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Patterns.Converters
{
    using System;
    using System.Collections;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Reflection;
    using System.Text.RegularExpressions;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using Newtonsoft.Json.Serialization;

    /// <summary>
    /// Json path converter allows JsonProperty to map complex json objects into C# objects.
    /// </summary>
    public class JsonPathConverter : JsonConverter
    {
        /// <inheritdoc />
        [SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "Not needed for Json converters.")]
        public override bool CanConvert(Type objectType)
        {
            // CanConvert is not called when [JsonConverter] attribute is used
            return objectType.GetCustomAttributes(true).OfType<JsonPathConverter>().Any();
        }

        /// <inheritdoc />
        [SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "Not needed for Json converters.")]
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jo = JObject.Load(reader);
            var targetObj = Activator.CreateInstance(objectType);

            foreach (var prop in objectType.GetProperties().Where(p => p.CanRead && p.CanWrite))
            {
                var att = prop.GetCustomAttributes(true)
                                                .OfType<JsonPropertyAttribute>()
                                                .FirstOrDefault();

                var jsonPath = att == null ? prop.Name : att.PropertyName;

                if (serializer.ContractResolver is DefaultContractResolver resolver)
                {
                    jsonPath = resolver.GetResolvedPropertyName(jsonPath);
                }

                if (!Regex.IsMatch(jsonPath, @"^[a-zA-Z0-9_.-]+$"))
                {
                    throw new InvalidOperationException($"JProperties of JsonPathConverter can have only letters, numbers, underscores, dashes and dots but name was ${jsonPath}."); // Array operations not permitted
                }

                var token = jo.SelectToken(jsonPath) ?? jo.SelectToken(prop.Name);

                if (token != null && token.Type != JTokenType.Null)
                {
                    var value = token.ToObject(prop.PropertyType, serializer);
                    prop.SetValue(targetObj, value, null);
                }
            }

            return targetObj;
        }

        /// <inheritdoc />
        [SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "Not needed for Json converters.")]
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var properties = value.GetType().GetRuntimeProperties().Where(p => p.CanRead && p.CanWrite);
            var main = new JObject();

            foreach (var prop in properties)
            {
                var att = prop.GetCustomAttributes(true)
                    .OfType<JsonPropertyAttribute>()
                    .FirstOrDefault();

                var jsonPath = att != null ? att.PropertyName : prop.Name;

                if (serializer.ContractResolver is DefaultContractResolver resolver)
                {
                    jsonPath = resolver.GetResolvedPropertyName(jsonPath);
                }

                var nesting = jsonPath.Split('.');
                var lastLevel = main;

                for (var i = 0; i < nesting.Length; i++)
                {
                    if (i == nesting.Length - 1)
                    {
                        if (prop.GetValue(value) is IEnumerable && prop.PropertyType != typeof(string))
                        {
                            lastLevel[nesting[i]] = JArray.FromObject(prop.GetValue(value));
                        }
                        else
                        {
                            lastLevel[nesting[i]] = new JValue(prop.GetValue(value));
                        }
                    }
                    else
                    {
                        if (lastLevel[nesting[i]] == null)
                        {
                            lastLevel[nesting[i]] = new JObject();
                        }

                        lastLevel = (JObject)lastLevel[nesting[i]];
                    }
                }
            }

            serializer.Serialize(writer, main);
        }
    }
}