using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using UnityEngine;

public class SaveAndLoad
{
    static readonly string path = $"{Application.persistentDataPath}/saves";
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
    }

    public static object Load(string saveName)
    {
        if (!File.Exists($"{path}/{saveName}.{ext}"))
            return null;

        BinaryFormatter formatter = new BinaryFormatter();

        string savePath = $"{path}/{saveName}.{ext}";

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
    }
}
