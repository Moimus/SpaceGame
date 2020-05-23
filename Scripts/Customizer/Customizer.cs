using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Customizer : MonoBehaviour
{
    [Header("Attributes")]
    public Ship ship;
    public Canvas uiCanvas;
    public Camera mainCamera;
    public InputField inputField;
    public Dropdown configurationDropdown;

    [Header("Attributes - Runtime")]
    public WeaponMountButton selectedWeaponMount;

    [Header("Prefabs - Generic")]
    public GameObject weaponMountButtonPrefab;

    [Header("Prefabs - Weapons")]
    public List<Weapon> weaponPrefabs = new List<Weapon>();

    // Start is called before the first frame update
    void Start()
    {
        init();
        //loadConfigurations();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void init()
    {
        initButtons();
        ship.turnOnCustomize();
        populateConfigurationDropdown();
    }

    public void initButtons()
    {
        foreach(GameObject n in ship.weaponMounts)
        {
            GameObject mountButtonInstance = Instantiate(weaponMountButtonPrefab, uiCanvas.transform);
            WeaponMountButton btn = mountButtonInstance.GetComponent<WeaponMountButton>();
            btn.init(this, n);

        }
    }

    public List<string> loadConfigurationNames()
    {
        string[] configNames = System.IO.Directory.GetFiles(Application.persistentDataPath + Model.DataFolder + "/");
        List<string> filteredList = new List<string>();
        foreach(string s in configNames)
        {
            filteredList.Add(System.IO.Path.GetFileName(s));
        }

        for(int n = 0; n < filteredList.Count; n++)
        {
            string prefix = filteredList[n].Split("_".ToCharArray())[0];
            if (prefix != ship.displayName)
            {
                filteredList.Remove(filteredList[n]);
            }
        }

        for(int n = 0; n < filteredList.Count; n++)
        {
            filteredList[n] = filteredList[n].Split(".".ToCharArray())[0];
            filteredList[n] = filteredList[n].Split("_".ToCharArray())[1];
        }

        return filteredList;
    }

    public void populateConfigurationDropdown()
    {
        configurationDropdown.ClearOptions();
        List<string> optionsList = loadConfigurationNames();
        configurationDropdown.AddOptions(optionsList);
    }

    public void saveConfiguration()
    {
        ship.toModel().export(inputField.text);
        populateConfigurationDropdown();
    }

    public void loadConfiguration()
    {
        ShipModel model = ShipModel.import(ship.displayName + "_" + configurationDropdown.options[configurationDropdown.value].text);
        ship.fromModel(model);
        Debug.Log("imported: " + model.toJSON());
    }
}
