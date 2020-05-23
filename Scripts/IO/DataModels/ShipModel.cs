using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class ShipModel : Model
{
    public List<string> weaponPrefabPaths = new List<string>();

    public ShipModel(string prefabPath, List<string> weaponPrefabPaths) : base(prefabPath)
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
        System.IO.File.WriteAllText(Application.persistentDataPath + Model.DataFolder + "/" + fileName + Model.FileExtension, toJSON());
    }
}
