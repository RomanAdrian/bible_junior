using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSlot : MonoBehaviour
{
    public string SaveFile = "player_saves.json";
    public string CreateFile = "creeaza.json";
    public int SaveIndex;
    public SceneLoader sceneLoader;
    public bool IsPlayerSave { get; private set; }

    // Update is called once per frame
    public void SetData(bool isPlayerSave)
    {
        IsPlayerSave = isPlayerSave;
    }

    public void LoadScene()
    {
        if (!IsPlayerSave) return;
        PlayerPrefs.SetString("SaveFile", SaveFile);
        SaveIndexClass.index = transform.GetSiblingIndex();
        sceneLoader.enter("Create_FromSave");
    }
    public void SaveScene()
    {
        GetComponent<Save>().SaveGame(SaveFile, CreateFile, SaveIndex);
    }
}
