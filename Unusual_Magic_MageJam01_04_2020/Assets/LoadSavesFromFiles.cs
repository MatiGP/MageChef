using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LoadSavesFromFiles : MonoBehaviour
{
    [SerializeField] Save[] saves;

    // Start is called before the first frame update
    void Awake()
    {

        for (int i = 0; i < 3; i++)
        {
            print("Loading...");
            if (File.Exists(Application.persistentDataPath + "/Save" + i + ".json"))
            {
                SaveFile currentSave = new SaveFile();               
                currentSave = JsonUtility.FromJson<SaveFile>(File.ReadAllText(Application.persistentDataPath + "/Save" + i + ".json"));

                saves[i].level = currentSave.level;
            }
            else
            {
                saves[i].ResetSave();
            }
        }
    }

  
}
