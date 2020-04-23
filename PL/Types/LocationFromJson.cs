using System.Windows;
using System.Windows.Data;
using BE;

namespace PL.Types
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using Microsoft;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class Welcome
    {
        [JsonProperty("results")]
        public Result[] Results { get; set; }
    }

    public class Result
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("highlightedTitle")]
        public string HighlightedTitle { get; set; }

        [JsonProperty("vicinity", NullValueHandling = NullValueHandling.Ignore)]
        public string Vicinity { get; set; }

        [JsonProperty("highlightedVicinity", NullValueHandling = NullValueHandling.Ignore)]
        public string HighlightedVicinity { get; set; }

        [JsonProperty("position", NullValueHandling = NullValueHandling.Ignore)]
        public double[] Position { get; set; }

        [JsonProperty("category", NullValueHandling = NullValueHandling.Ignore)]
        public string Category { get; set; }

        [JsonProperty("categoryTitle", NullValueHandling = NullValueHandling.Ignore)]
        public string CategoryTitle { get; set; }

        [JsonProperty("href")]
        public Uri Href { get; set; }

        [JsonProperty("type")]
        public TypeEnum Type { get; set; }

        [JsonProperty("resultType")]
        public ResultType ResultType { get; set; }

        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty("distance", NullValueHandling = NullValueHandling.Ignore)]
        public long? Distance { get; set; }

        [JsonProperty("bbox", NullValueHandling = NullValueHandling.Ignore)]
        public double[] Bbox { get; set; }

        [JsonProperty("completion", NullValueHandling = NullValueHandling.Ignore)]
        public string Completion { get; set; }

        public override string ToString()
        {
            return $"{Title}";
        }
    }

    public class ResultConverter 
    {
       

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Result)
            {
                Result result = value as Result;
                if (parameter is FallReport)
                {
                    FallReport report = parameter as FallReport;
                   
                    report.ReportLocation.Latitude = result.Position[0];
                    report.ReportLocation.Longitude = result.Position[1];
                    return report;
                }
            }
            return new { Address = "", Latitude = 0f, Longitude = 0f };
        }
    }

    public enum ResultType
    {
        Address,
        Place,
        Query
    };

    public enum TypeEnum
    {
        UrnNlpTypesAutosuggest,
        UrnNlpTypesPlace
    };

    public partial class Welcome
    {
        public static Welcome FromJson(string json) =>
            JsonConvert.DeserializeObject<Welcome>(json, PL.Types.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Welcome self) =>
            JsonConvert.SerializeObject(self, PL.Types.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
                {
                    ResultTypeConverter.Singleton,
                    TypeEnumConverter.Singleton,
                    new IsoDateTimeConverter {DateTimeStyles = DateTimeStyles.AssumeUniversal}
                },
        };
    }

    internal class ResultTypeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(ResultType) || t == typeof(ResultType?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "address":
                    return ResultType.Address;
                case "place":
                    return ResultType.Place;
                case "query":
                    return ResultType.Query;
            }
            throw new Exception("Cannot unmarshal type ResultType");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (ResultType)untypedValue;
            switch (value)
            {
                case ResultType.Address:
                    serializer.Serialize(writer, "address");
                    return;
                case ResultType.Place:
                    serializer.Serialize(writer, "place");
                    return;
                case ResultType.Query:
                    serializer.Serialize(writer, "query");
                    return;
            }
            throw new Exception("Cannot marshal type ResultType");
        }

        public static readonly ResultTypeConverter Singleton = new ResultTypeConverter();
    }

    internal class TypeEnumConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(TypeEnum) || t == typeof(TypeEnum?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "urn:nlp-types:autosuggest":
                    return TypeEnum.UrnNlpTypesAutosuggest;
                case "urn:nlp-types:place":
                    return TypeEnum.UrnNlpTypesPlace;
                default:
                    return null;
            }
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (TypeEnum)untypedValue;
            switch (value)
            {
                case TypeEnum.UrnNlpTypesAutosuggest:
                    serializer.Serialize(writer, "urn:nlp-types:autosuggest");
                    return;
                case TypeEnum.UrnNlpTypesPlace:
                    serializer.Serialize(writer, "urn:nlp-types:place");
                    return;
            }
            throw new Exception("Cannot marshal type TypeEnum");
        }

        public static readonly TypeEnumConverter Singleton = new TypeEnumConverter();
    }
}