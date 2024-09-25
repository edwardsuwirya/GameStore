using System.Text.Json;

namespace GameStore.Shared.Helpers;

public static class Serialization
{
    public static string Serialize<T>(T obj) => JsonSerializer.Serialize(obj);

    public static T Deserialize<T>(string json) => JsonSerializer.Deserialize<T>(json);

    public static List<T> DeserializeList<T>(string json) => JsonSerializer.Deserialize<List<T>>(json);
}