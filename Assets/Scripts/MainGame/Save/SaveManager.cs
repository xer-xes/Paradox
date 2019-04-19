using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{

    private static SaveManager instance = null;

    public static SaveManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new GameObject("SaveManager").AddComponent<SaveManager>();
            }
            return instance;
        }
    }

    public void OnApplicationQuit()
    {
        DestroyInstance();
    }

    public void DestroyInstance()
    {
        Debug.Log("SaveManager Instance Destroyed");
        instance = null;
    }

    public List<ISerializable> ObjToSave;

    private void Awake()
    {
        ObjToSave = new List<ISerializable>();
    }

    public void Save()
    {
        JObject jSaveData = new JObject();
        for (int i = 0; i < ObjToSave.Count; i++)
        {
            jSaveData.Add(ObjToSave[i].GetJsonKey(), ObjToSave[i].Serialize());
        }
        string filePath = Application.persistentDataPath + "/Paradox.sav";
        Debug.Log("Saving Data into " + filePath);
        StreamWriter sw = new StreamWriter(filePath);
        sw.WriteLine(jSaveData.ToString());
        sw.Close();
    }

    public void Load()
    {
        string filePath = Application.persistentDataPath + "/Paradox.sav";
        Debug.Log("Loading Data From " + filePath);
        StreamReader sr = new StreamReader(filePath);
        string jsonString = sr.ReadToEnd();
        JObject jobj = JObject.Parse(jsonString);
        for (int i = 0; i < ObjToSave.Count; i++)
        {
            string jsaveString = jobj[ObjToSave[i].GetJsonKey()].ToString();
            ObjToSave[i].DeSerialize(jsaveString);
        }
        sr.Close();
    }
}
