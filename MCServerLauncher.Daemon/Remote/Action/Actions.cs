using MCServerLauncher.Daemon.Storage;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MCServerLauncher.Daemon.Remote.Action;

/// <summary>
///     Generated by "MCServerLauncher.Daemon/.Resources/Action/actions_meta.yml"
/// </summary>
internal static class Actions
{
    private static T Deserialize<T>(JObject data)
    {
        return JsonConvert.DeserializeObject<T>(data.ToString(), WebJsonConverter.Settings);
    }

    internal interface IActionResponse
    {
        JObject Into()
        {
            return JObject.FromObject(this);
        }
    }

    internal class Empty
    {
        public static Request RequestOf()
        {
            return new Request();
        }

        public static IActionResponse ResponseOf()
        {
            return new Response();
        }

        public class Request
        {
        }

        public class Response : IActionResponse
        {
        }
    }

    public class HeartBeat
    {
        public static Empty.Request RequestOf(JObject data)
        {
            return new Empty.Request();
        }

        public static IActionResponse ResponseOf(long Time)
        {
            return new Response { Time = Time };
        }

        private class Response : IActionResponse
        {
            public long Time;
        }
    }

    public class GetJavaList
    {
        public static Empty.Request RequestOf(JObject data)
        {
            return new Empty.Request();
        }

        public static IActionResponse ResponseOf(List<JavaScanner.JavaInfo> JavaList)
        {
            return new Response { JavaList = JavaList };
        }

        private class Response : IActionResponse
        {
            public List<JavaScanner.JavaInfo> JavaList;
        }
    }

    public class FileUploadRequest
    {
        public static Request RequestOf(JObject data)
        {
            return Deserialize<Request>(data);
        }

        public static IActionResponse ResponseOf(Guid FileId)
        {
            return new Response { FileId = FileId };
        }

        public class Request
        {
            public long ChunkSize;
            public string Path;
            public string Sha1;
            public long Size;
        }

        private class Response : IActionResponse
        {
            public Guid FileId;
        }
    }

    public class FileUploadChunk
    {
        public static Request RequestOf(JObject data)
        {
            return Deserialize<Request>(data);
        }

        public static IActionResponse ResponseOf(bool Done, long Received)
        {
            return new Response { Done = Done, Received = Received };
        }

        public class Request
        {
            public string Data;
            public Guid FileId;
            public long Offset;
        }

        private class Response : IActionResponse
        {
            public bool Done;
            public long Received;
        }
    }

    public class FileUploadCancel
    {
        public static Request RequestOf(JObject data)
        {
            return Deserialize<Request>(data);
        }

        public static IActionResponse ResponseOf()
        {
            return new Response();
        }

        public class Request
        {
            public Guid FileId;
        }

        private class Response : IActionResponse
        {
        }
    }
}

/// <summary>
///     Enum 转换器, 使枚举字面值(BigCamelCase)与json(snake_case)互转
/// </summary>
/// <typeparam name="T"></typeparam>
internal class SnakeCaseEnumConverter<T> : JsonConverter where T : struct, Enum
{
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        var pascalCase = value!.ToString();
        var snakeCase = ConvertPascalCaseToSnakeCase(pascalCase);
        writer.WriteValue(snakeCase);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
        JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.String)
        {
            var snakeCase = reader.Value!.ToString();
            var pascalCase = ConvertSnakeCaseToPascalCase(snakeCase);
            if (Enum.TryParse(pascalCase, out T result)) return result;
        }

        throw new JsonSerializationException($"Cannot convert {reader.Value} to {typeof(T)}");
    }

    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(T);
    }

    private static string ConvertSnakeCaseToPascalCase(string snakeCase)
    {
        return string.Join(string.Empty,
            snakeCase.Split('_').Select(s => char.ToUpperInvariant(s[0]) + s.Substring(1).ToLowerInvariant()));
    }

    private static string ConvertPascalCaseToSnakeCase(string pascalCase)
    {
        return string.Concat(pascalCase.Select((x, i) =>
            i > 0 && char.IsUpper(x) ? "_" + x.ToString().ToLowerInvariant() : x.ToString().ToLowerInvariant()));
    }
}

/// <summary>
///     解析 Guid,若字符串解析失败则返回 Guid.Empty,方便带上下文的异常检查
/// </summary>
internal class GuidJsonConverter : JsonConverter
{
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        writer.WriteValue(value!.ToString());
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
        JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.String)
        {
            var str = reader.Value!.ToString();

            return Guid.TryParse(str, out var result) ? result : Guid.Empty;
        }

        throw new JsonSerializationException($"Cannot convert {reader.Value} to Guid");
    }

    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(Guid);
    }
}