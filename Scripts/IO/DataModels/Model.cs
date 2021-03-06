﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Model
{
    public static string DataFolder = "/Data"; //Folder where models are saved
    public static string FileExtension = ".g8Model";
    public string name = "noName";
    public string prefabPath = "notSet";

    public Model(string prefabPath, string displayName)
    {
        this.prefabPath = prefabPath;
        this.name = displayName;
    }

    public virtual void export(string filePath)
    {
        throw new System.NotImplementedException();
    }

    public virtual string toJSON()
    {
        string json = JsonUtility.ToJson(this);
        Debug.Log("ShipModel to JSON: -> " + JsonUtility.ToJson(this));
        return json;
    }
}
