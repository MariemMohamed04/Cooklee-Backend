using Cooklee.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace Cooklee.Infrastructure.DataSeed
{
    public class TagEnumConverter : JsonConverter<Tag>
    {
        public override Tag Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.String)
            {
                throw new JsonException();
            }

            var value = reader.GetString();
            if (Enum.TryParse<Tag>(value, true, out var result))
            {
                return result;
            }

            throw new JsonException($"Unable to convert \"{value}\" to enum Tag.");
        }

        public override void Write(Utf8JsonWriter writer, Tag value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
