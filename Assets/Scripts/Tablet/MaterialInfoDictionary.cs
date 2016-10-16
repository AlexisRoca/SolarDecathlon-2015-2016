using System;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;

[Serializable]
public class MaterialInfoDictionary : MonoBehaviour
{
    public MaterialInfo this[Material material]
    {
        get
        {
            if (material == null || material.mainTexture == null)
                return null;

            return MaterialInfos.FirstOrDefault(x => x.Filename == material.mainTexture.name.Replace("(Instance)","").Trim());
        }
    }

    public MaterialInfo[] MaterialInfos;
}

[Serializable]
public class MaterialInfo
{
    public string Filename;
    public string Name;

    [Multiline]
    public string Description;

    public string[] Advantages;
}