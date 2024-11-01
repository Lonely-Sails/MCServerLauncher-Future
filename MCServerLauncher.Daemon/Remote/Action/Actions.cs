using MCServerLauncher.Daemon.Storage;
using MCServerLauncher.Daemon.Minecraft.Server;
using MCServerLauncher.Daemon.Minecraft.Server.Factory;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MCServerLauncher.Daemon.Remote.Action;

/// <summary>
/// Action的消息模版,采用meta-program
/// Generated by "MCServerLauncher.Daemon/Resources/Action/actions_meta.yml"
/// </summary>
public static class Actions
{
    private static readonly JsonSerializer Serializer = JsonSerializer.Create(WebJsonConverter.Settings);

    private static T Deserialize<T>(JObject? data) =>
        data!.ToObject<T>(Serializer) ?? throw new ArgumentException("Action request deserialize failed.");


    public interface IActionResponse
    {
        public JObject Into(JsonSerializer serializer)
        {
            return JObject.FromObject(this, serializer);
        }
    }

    public static class Empty
    {
        public struct Request {}

        private struct Response : IActionResponse {}

        public static Request RequestOf()
        {
            return new Request();
        }

        public static IActionResponse ResponseOf()
        {
            return new Response();
        }
    }

        public static class Ping
        {
            public struct Request
            {

            }

            private struct Response : IActionResponse
            {
                public long Time;
            }

            public static Empty.Request RequestOf(JObject? data)
            {
                return new Empty.Request();
            }

            public static IActionResponse ResponseOf(long time)
            {
                return new Response { Time = time };
            }
        }

        public static class GetJavaList
        {
            public struct Request
            {

            }

            private struct Response : IActionResponse
            {
                public List<JavaScanner.JavaInfo> JavaList;
            }

            public static Empty.Request RequestOf(JObject? data)
            {
                return new Empty.Request();
            }

            public static IActionResponse ResponseOf(List<JavaScanner.JavaInfo> javaList)
            {
                return new Response { JavaList = javaList };
            }
        }

        public static class FileUploadRequest
        {
            public struct Request
            {
                public string? Path;
                public string? Sha1;
                public long ChunkSize;
                public long Size;
            }

            private struct Response : IActionResponse
            {
                public Guid FileId;
            }

            public static Request RequestOf(JObject? data)
            {
                return Deserialize<Request>(data);
            }

            public static IActionResponse ResponseOf(Guid fileId)
            {
                return new Response { FileId = fileId };
            }
        }

        public static class FileUploadChunk
        {
            public struct Request
            {
                public Guid FileId;
                public long Offset;
                public string Data;
            }

            private struct Response : IActionResponse
            {
                public bool Done;
                public long Received;
            }

            public static Request RequestOf(JObject? data)
            {
                return Deserialize<Request>(data);
            }

            public static IActionResponse ResponseOf(bool done, long received)
            {
                return new Response { Done = done, Received = received };
            }
        }

        public static class FileUploadCancel
        {
            public struct Request
            {
                public Guid FileId;
            }

            private struct Response : IActionResponse
            {

            }

            public static Request RequestOf(JObject? data)
            {
                return Deserialize<Request>(data);
            }

            public static IActionResponse ResponseOf()
            {
                return new Response {  };
            }
        }

        public static class FileDownloadRequest
        {
            public struct Request
            {
                public string Path;
            }

            private struct Response : IActionResponse
            {
                public Guid FileId;
                public long Size;
                public string Sha1;
            }

            public static Request RequestOf(JObject? data)
            {
                return Deserialize<Request>(data);
            }

            public static IActionResponse ResponseOf(Guid fileId, long size, string sha1)
            {
                return new Response { FileId = fileId, Size = size, Sha1 = sha1 };
            }
        }

        public static class FileDownloadRange
        {
            public struct Request
            {
                public Guid FileId;
                public string Range;
            }

            private struct Response : IActionResponse
            {
                public string Content;
            }

            public static Request RequestOf(JObject? data)
            {
                return Deserialize<Request>(data);
            }

            public static IActionResponse ResponseOf(string content)
            {
                return new Response { Content = content };
            }
        }

        public static class FileDownloadClose
        {
            public struct Request
            {
                public Guid FileId;
            }

            private struct Response : IActionResponse
            {

            }

            public static Request RequestOf(JObject? data)
            {
                return Deserialize<Request>(data);
            }

            public static IActionResponse ResponseOf()
            {
                return new Response {  };
            }
        }

        public static class GetFileInfo
        {
            public struct Request
            {
                public string Path;
            }

            private struct Response : IActionResponse
            {
                public FileMetadata Meta;
            }

            public static Request RequestOf(JObject? data)
            {
                return Deserialize<Request>(data);
            }

            public static IActionResponse ResponseOf(FileMetadata meta)
            {
                return new Response { Meta = meta };
            }
        }

        public static class GetDirectoryInfo
        {
            public struct Request
            {
                public string Path;
            }

            private struct Response : IActionResponse
            {
                public string? Parent;
                public DirectoryEntry.FileInformation[] Files;
                public DirectoryEntry.DirectoryInformation[] Directories;
            }

            public static Request RequestOf(JObject? data)
            {
                return Deserialize<Request>(data);
            }

            public static IActionResponse ResponseOf(string? parent, DirectoryEntry.FileInformation[] files, DirectoryEntry.DirectoryInformation[] directories)
            {
                return new Response { Parent = parent, Files = files, Directories = directories };
            }
        }

        public static class TryAddInstance
        {
            public struct Request
            {
                public InstanceFactorySetting Setting;
                public InstanceFactories Factory;
            }

            private struct Response : IActionResponse
            {
                public bool Done;
            }

            public static Request RequestOf(JObject? data)
            {
                return Deserialize<Request>(data);
            }

            public static IActionResponse ResponseOf(bool done)
            {
                return new Response { Done = done };
            }
        }

        public static class TryRemoveInstance
        {
            public struct Request
            {
                public string Name;
            }

            private struct Response : IActionResponse
            {
                public bool Done;
            }

            public static Request RequestOf(JObject? data)
            {
                return Deserialize<Request>(data);
            }

            public static IActionResponse ResponseOf(bool done)
            {
                return new Response { Done = done };
            }
        }

        public static class TryStartInstance
        {
            public struct Request
            {
                public string Name;
            }

            private struct Response : IActionResponse
            {
                public bool Done;
            }

            public static Request RequestOf(JObject? data)
            {
                return Deserialize<Request>(data);
            }

            public static IActionResponse ResponseOf(bool done)
            {
                return new Response { Done = done };
            }
        }

        public static class TryStopInstance
        {
            public struct Request
            {
                public string Name;
            }

            private struct Response : IActionResponse
            {
                public bool Done;
            }

            public static Request RequestOf(JObject? data)
            {
                return Deserialize<Request>(data);
            }

            public static IActionResponse ResponseOf(bool done)
            {
                return new Response { Done = done };
            }
        }

        public static class SendToInstance
        {
            public struct Request
            {
                public string Name;
                public string Message;
            }

            private struct Response : IActionResponse
            {

            }

            public static Request RequestOf(JObject? data)
            {
                return Deserialize<Request>(data);
            }

            public static IActionResponse ResponseOf()
            {
                return new Response {  };
            }
        }

        public static class KillInstance
        {
            public struct Request
            {
                public string Name;
            }

            private struct Response : IActionResponse
            {

            }

            public static Request RequestOf(JObject? data)
            {
                return Deserialize<Request>(data);
            }

            public static IActionResponse ResponseOf()
            {
                return new Response {  };
            }
        }

        public static class GetInstanceStatus
        {
            public struct Request
            {
                public string Name;
            }

            private struct Response : IActionResponse
            {
                public InstanceStatus Status;
            }

            public static Request RequestOf(JObject? data)
            {
                return Deserialize<Request>(data);
            }

            public static IActionResponse ResponseOf(InstanceStatus status)
            {
                return new Response { Status = status };
            }
        }

        public static class GetAllStatus
        {
            public struct Request
            {

            }

            private struct Response : IActionResponse
            {
                public IDictionary<string, InstanceStatus> Status;
            }

            public static Empty.Request RequestOf(JObject? data)
            {
                return new Empty.Request();
            }

            public static IActionResponse ResponseOf(IDictionary<string, InstanceStatus> status)
            {
                return new Response { Status = status };
            }
        }

}

/// <summary>
/// Enum 转换器, 使枚举字面值(BigCamelCase)与json(snake_case)互转
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
            if (Enum.TryParse(pascalCase, out T result))
            {
                return result;
            }
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
/// 解析 Guid,若字符串解析失败则返回 Guid.Empty,方便带上下文的异常检查
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
