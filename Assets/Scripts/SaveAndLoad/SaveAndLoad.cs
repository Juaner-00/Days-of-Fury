using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using UnityEngine;

public class SaveAndLoad
{
    // #if UNITY_EDITOR
    static readonly string path = $"{Application.dataPath}/Saves";
    // #elif UNITY_STANDALONE
    //     static readonly string path = $"{Application.persistentDataPath}/Saves";
    // #endif
    static readonly string ext = "save";

    public static void Save(string saveName, object data)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        string savePath = $"{path}/{saveName}.{ext}";

        FileStream file = File.Create(savePath);

        formatter.Serialize(file, data);

        file.Close();
        // --------------------------

        // string json = JsonUtility.ToJson(data);

        // FileStream stream = new FileStream(savePath, FileMode.Create, FileAccess.Write);

        // using (StreamWriter writer = new StreamWriter(stream))
        //     writer.Write(json);
    }

    public static object Load(string saveName)
    {
        string savePath = $"{path}/{saveName}.{ext}";

        if (!File.Exists(savePath))
            SaveAndLoad.Save(saveName, GameManager.Instance.DataObject.Data);

        BinaryFormatter formatter = new BinaryFormatter();


        Stream file = new FileStream(savePath, FileMode.Open, FileAccess.Read);

        try
        {
            object save = formatter.Deserialize(file);
            file.Close();
            return save;
        }
        catch (System.Exception)
        {
            Debug.LogError($"Error al cargar {saveName} desde {savePath}");
            file.Close();
            return null;
        }
        // ---------------------------------

        // SaveData data = new SaveData();
        // string json = "";
        // if (File.Exists(savePath))
        //     using (StreamReader reader = new StreamReader(savePath))
        //         json = reader.ReadToEnd();

        // JsonUtility.FromJsonOverwrite(json, data);
        // return data;
    }
}
