using Newtonsoft.Json.Linq;

namespace ObjectFilter.Extensions;

public static class GetObjectProperty
{
    public static object? GetPropertyValue(this object obj, string jsonPath)
    {
        var json = JObject.FromObject(obj);
        var token = json.SelectToken(jsonPath, true);
        
        return token?.ToObject<object>();
    }
    
    public static IEnumerable<object>? GetArrayValue(this object obj, string jsonPath)
    {
        var json = JObject.FromObject(obj);
        var token = json.SelectToken(jsonPath, true);
        
        return token?.ToObject<List<object>>();
    }
}