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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void init()
    {
        initButtons();
        ship.turnOnCustomize();
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

    public void saveConfiguration()
    {
        ship.toModel().export(inputField.text);
    }

    public void loadConfiguration()
    {
        ShipModel model = ShipModel.import("TestB");
        ship.fromModel(model);
        Debug.Log("imported: " + model.toJSON());
    }
}
