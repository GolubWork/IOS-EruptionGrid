using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Utilities;

namespace Code.Infrastructure.Serialization
{
  public static class JsonSerialization
  {
    public static string ToJson(this object self) =>
      JsonConvert.SerializeObject(self, new JsonSerializerSettings
      {
        TypeNameHandling = TypeNameHandling.Auto,
        Formatting = Formatting.Indented,
        TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple
      });
    
    public static string ToJsonWithPropertyName(this object self) => JsonConvert.SerializeObject(self,
      new JsonSerializerSettings
      {
        Formatting = Formatting.Indented,
        ContractResolver = new DefaultContractResolver()
      });
    
    public static T FromJson<T>(this string json) =>
      JsonConvert.DeserializeObject<T>(json, new JsonSerializerSettings
      {
        TypeNameHandling = TypeNameHandling.Auto,
        TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple
      });

    static JsonSerialization() => 
      AotHelper.Ensure(() => new ReferenceConverter(typeof(Dummy)));
    
    public class Dummy{}
  }}