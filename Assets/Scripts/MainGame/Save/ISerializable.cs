using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISerializable
{
    JObject Serialize();
    void DeSerialize(string jsonString);
    string GetJsonKey();
}
