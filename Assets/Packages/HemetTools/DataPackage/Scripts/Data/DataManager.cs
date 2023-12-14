using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using UnityEditor;
using UnityEngine;
using Newtonsoft.Json;
using System.Threading.Tasks;

public class DataManager
{
    public static void Save<T>(T obj) where T : class, new()
    {
        string typeName = typeof(T).Name; // Obtém o nome do tipo da classe
        string filePath = Application.persistentDataPath + "/" + typeName + ".json";

        string json = JsonConvert.SerializeObject(obj);
        File.WriteAllText(filePath, json);
    }

    public static T Load<T>() where T : class, new()
    {
        string typeName = typeof(T).Name; // Obtém o nome do tipo da classe
        string filePath = Application.persistentDataPath + "/" + typeName + ".json";

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<T>(json);
        }
        else
        {
            T newSave = new T();
            Save(newSave);

            return newSave;
        }
    }

    [MenuItem("HemetTools/Save System/Open save folder")]
    public static void OpenSaveFolder()
    {
        string appDataPath = Application.persistentDataPath;
        System.Diagnostics.Process.Start(appDataPath);
    }

    [MenuItem("HemetTools/Save System/Clear all saved data")]
    public static void ClearAllSavedData()
    {
        string savePath = Application.persistentDataPath;

        // Certifique-se de que o diretório de salvamento existe
        if (Directory.Exists(savePath))
        {
            string[] saveFiles = Directory.GetFiles(savePath);

            if (saveFiles.Length == 0)
            {
                Debug.Log("No data to clear.");
            }

            foreach (string saveFile in saveFiles)
            {
                bool IsSaveFile(string filePath)
                {
                    // Verifique se a extensão do arquivo é ".json"
                    return Path.GetExtension(filePath).Equals(".json", StringComparison.OrdinalIgnoreCase);
                }

                // Verifica se é um arquivo de salvamento (pode adicionar critérios adicionais)
                if (IsSaveFile(saveFile))
                {
                    File.Delete(saveFile);
                    Debug.Log($"{saveFile} has been deleted.");
                }
            }
        }
        else
        {
            Debug.LogWarning("Not found: " + savePath);
        }
    }
}