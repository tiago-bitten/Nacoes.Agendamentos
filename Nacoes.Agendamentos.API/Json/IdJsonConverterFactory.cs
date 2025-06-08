using Nacoes.Agendamentos.Domain.ValueObjects;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Nacoes.Agendamentos.API.Json;

public class IdJsonConverterFactory : JsonConverterFactory
{
    public override bool CanConvert(Type typeToConvert)
    {
        return typeToConvert.IsGenericType &&
               typeToConvert.GetGenericTypeDefinition() == typeof(Id<>);
    }

    public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        var valueType = typeToConvert.GenericTypeArguments[0];
        var converterType = typeof(IdConverterInner<>).MakeGenericType(valueType);
        return (JsonConverter)Activator.CreateInstance(converterType)!;
    }

    private class IdConverterInner<T> : JsonConverter<Id<T>>
    {
        public override Id<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.String)
                throw new JsonException("Esperado um GUID em formato string.");

            var stringValue = reader.GetString();
            if (!Guid.TryParse(stringValue, out var guid))
                throw new JsonException("Valor inválido para GUID.");

            return new Id<T>(guid);
        }

        public override void Write(Utf8JsonWriter writer, Id<T> value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.Value.ToString());
        }
    }
}
