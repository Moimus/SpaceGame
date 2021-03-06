﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class ShipModel : Model
{
    public List<string> weaponPrefabPaths = new List<string>();

    public ShipModel(string prefabPath, string displayName, List<string> weaponPrefabPaths) : base(prefabPath, displayName)
    {
        this.weaponPrefabPaths = weaponPrefabPaths;
    }

    public override void export(string fileName)
    {
        //C:/Users/Admin/AppData/LocalLow/DefaultCompany/SpaceGame
        Debug.Log("Writing to: " + Application.persistentDataPath);
        if(!Directory.Exists(Application.persistentDataPath + Model.DataFolder))
        {
            Directory.CreateDirectory(Application.persistentDataPath + Model.DataFolder);
        }
        System.IO.File.WriteAllText(Application.persistentDataPath + Model.DataFolder + "/" + name + "_" + fileName + Model.FileExtension, toJSON());
    }

    public static ShipModel import(string fileName)
    {
        string jsonFromFile = System.IO.File.ReadAllText(Application.persistentDataPath + Model.DataFolder + "/" + fileName + Model.FileExtension);
        ShipModel model = JsonUtility.FromJson<ShipModel>(jsonFromFile);
        return model;
    }
}
