using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponMountButton : MonoBehaviour
{
    public Customizer customizer;
    public GameObject relatedWeaponMount;
    RectTransform rectTransform;
    public Dropdown dropdown;
    public GameObject dropdownContent;
    public GameObject dropDownItem;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void init(Customizer customizer, GameObject relatedWeaponMount)
    {
        this.customizer = customizer;
        this.relatedWeaponMount = relatedWeaponMount;
        rectTransform = GetComponent<RectTransform>();
        dropdown.ClearOptions();
        populateDropdown();
        StartCoroutine(updatePosition());
    }

    IEnumerator updatePosition()
    {
        while (true) //TODO test for memory leaks
        {
            try
            {
                Vector3 targetRelativePos = customizer.mainCamera.WorldToScreenPoint(relatedWeaponMount.transform.position);
                Vector2 localPoint;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), targetRelativePos, customizer.mainCamera, out localPoint);
                if (localPoint.x > -Screen.width && localPoint.x < Screen.width && localPoint.y > -Screen.height / 2 && localPoint.y < Screen.height / 2 && targetRelativePos.z > 0)
                {
                    rectTransform.localPosition = new Vector3(localPoint.x, localPoint.y, customizer.mainCamera.nearClipPlane + 0.1f);
                }
                else
                {
                    //StartCoroutine(onDestroy());
                }
            }
            catch(System.Exception e)
            {
                Debug.LogError(e.Message);
            }

            yield return null;
        }
    }

    private void OnMouseDown()
    {
        Debug.Log("Click!");
        if(customizer.selectedWeaponMount != null)
        {
            customizer.selectedWeaponMount.dropdown.gameObject.SetActive(false);
        }
        customizer.selectedWeaponMount = this;
        dropdown.gameObject.SetActive(true);
    }

    public void populateDropdown()
    {
        dropdown.ClearOptions();
        List<string> optionsList = new List<string>();
        foreach (Weapon w in customizer.weaponPrefabs)
        {
            optionsList.Add(w.name);
        }
        dropdown.AddOptions(optionsList);
    }

    public void assignWeaponsToDropdownItems() //has to be synchronous with customizer.weaponPrefabs : List 
    {
        dropdownContent = transform.Find("Dropdown").transform.Find("Dropdown List").transform.Find("Viewport").transform.Find("Content").gameObject;
        Debug.Log("childcount: " + dropdownContent.transform.childCount);
        for(int i = 0; i < dropdownContent.transform.childCount; i++)
        {
            if(dropdownContent.transform.GetChild(i).GetComponent<WeaponDropdownItem>() != null)
            {
                if(i -1 >= 0)
                {
                    dropdownContent.transform.GetChild(i).GetComponent<WeaponDropdownItem>().relatedWeapon = customizer.weaponPrefabs[i - 1].gameObject;
                }
            }
        }
    }

    public void onWeaponOptionChanged() //has to be synchronous with customizer.weaponPrefabs : List
    {
        Debug.Log("DropDown changed");
        foreach(Transform n in relatedWeaponMount.transform)
        {
            if(n.GetComponent<Weapon>() != null)
            {
                n.GetComponent<Weapon>().detachWeapon();
            }
        }
        Debug.Log("attaching " + dropdown.value);
        GameObject wpn = Instantiate(customizer.weaponPrefabs[dropdown.value].gameObject, relatedWeaponMount.transform);
        wpn.transform.position = relatedWeaponMount.transform.position;
        wpn.GetComponent<Weapon>().init(customizer.ship);
        wpn.GetComponent<Weapon>().attachWeapon();
    }
}
