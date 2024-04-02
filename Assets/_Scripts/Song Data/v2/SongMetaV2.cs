using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

[Serializable]
public class SongMetaV2
{
    [JsonProperty("Song Name")]
    public string songName;
    [JsonProperty("Song Credits")]
    public Dictionary<string, string> credits;
    [JsonProperty("Song Difficulties")]
    public Dictionary<string, Color> difficulties;
    [JsonProperty("Song Description")]
    public string songDescription;
    
    //NOT SERIALIZED
    [JsonIgnore] public string songPath;
    [JsonIgnore] public Sprite songCover;
    [JsonIgnore] public BundleMeta bundleMeta;
    [JsonIgnore] public bool isFromModPlatform;
    [JsonIgnore] public string modURL;

}