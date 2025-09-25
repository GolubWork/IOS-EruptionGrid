using System.Collections.Generic;
using Newtonsoft.Json;

namespace Code.Progress.Data
{
  public class EntityData
  {
    [JsonProperty("meta")] public List<EntitySnapshot> MetaEntitySnapshots;
    [JsonProperty("serviceBridge")] public List<EntitySnapshot> ServiceBridgeEntitySnapshots;
    [JsonProperty("settings")] public List<EntitySnapshot> AudioEntitySnapshots;
  }
}