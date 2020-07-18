using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public List<WeaponMountButton> weaponMountButtons = new List<WeaponMountButton>();

    [Header("Prefabs - Weapons")]
    public List<Weapon> weaponPrefabs = new List<Weapon>();

    [Header("Prefabs - Ships")]
    public List<GameObject> shipPrefabs = new List<GameObject>();
    int shipIndexPointer = 0;

    [Header("FX")]
    public GameObject spinTable;
    public bool spinTableSpinning = true;
    public float spinTableSpeed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        init();
        //loadConfigurations();
    }

    // Update is called once per frame
    void Update()
    {
        spinSpinTable();
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
            weaponMountButtons.Add(btn);
            btn.init(this, n);

        }
    }

    public void removeButtons()
    {
        foreach(WeaponMountButton n in weaponMountButtons)
        {
            Destroy(n.gameObject);
        }
        weaponMountButtons = new List<WeaponMountButton>();
    }

    public List<string> loadConfigurationNames()
    {
        string[] configNames = System.IO.Directory.GetFiles(Application.persistentDataPath + Model.DataFolder + "/");
        List<string> fileNames = new List<string>();
        List<string> filteredList = new List<string>();
        foreach(string s in configNames)
        {
            fileNames.Add(System.IO.Path.GetFileName(s));
        }

        for(int n = 0; n < fileNames.Count; n++)
        {
            string prefix = fileNames[n].Split("_".ToCharArray())[0];
            if (prefix.Equals(ship.displayName))
            {
                filteredList.Add(fileNames[n]);
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
        configurationDropdown.AddOptions(new List<string> { "" });
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

    public void nextShip()
    {
        removeButtons();
        Destroy(ship.gameObject);
        if(shipIndexPointer +1 < shipPrefabs.Count)
        {
            shipIndexPointer++;
            loadShipPrefabFromList(shipIndexPointer);
        }
        else
        {
            shipIndexPointer = 0;
            loadShipPrefabFromList(shipIndexPointer);
        }
        initButtons();
        populateConfigurationDropdown();
    }

    public void loadShipPrefabFromList(int index)
    {
        GameObject shipInstance = Instantiate(shipPrefabs[index], spinTable.transform);
        Ship shipComponent = shipInstance.GetComponent<Ship>();
        ship = shipComponent;
        shipComponent.turnOnCustomize();
    }

    void spinSpinTable()
    {
        if(spinTableSpinning)
        {
            spinTable.transform.Rotate(Vector3.up * Time.deltaTime * spinTableSpeed);
        }
    }

    public void toggleSpinTable()
    {
        if(spinTableSpinning)
        {
            spinTableSpinning = false;
        }
        else
        {
            spinTableSpinning = true;
        }
    }

    public void backToMainMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
