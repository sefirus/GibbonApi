using System.Text.Json;

namespace WebApi.Helpers;

public static class JsonReaderDraft
{
    public static void GetJson(Memory<byte> x)
    {
        var reader = new Utf8JsonReader(x.Span);
        while (reader.Read())
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.StartObject:
                    Console.WriteLine($"Start of object");
                    break;
                case JsonTokenType.EndObject:
                    Console.WriteLine($"End of object");
                    break;
                case JsonTokenType.StartArray:
                    Console.WriteLine();
                    Console.WriteLine($"Start of array");
                    break;
                case JsonTokenType.EndArray:
                    Console.WriteLine($"End of array");
                    break;
                case JsonTokenType.PropertyName:
                    Console.WriteLine($"Property: {reader.GetString()}");
                    break;
                case JsonTokenType.String:
                    Console.WriteLine($" Value: {reader.GetString()}");
                    break;
                case JsonTokenType.Number:
                    Console.WriteLine($" Value: {reader.GetInt32()}");
                    break;
                case JsonTokenType.None:
                case JsonTokenType.Comment:
                case JsonTokenType.True:
                case JsonTokenType.False:
                case JsonTokenType.Null:
                default:
                    Console.WriteLine($"No support for {reader.TokenType}");
                    break;
            }
        }
    }
}