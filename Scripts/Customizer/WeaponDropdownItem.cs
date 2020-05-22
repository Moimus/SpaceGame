using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponDropdownItem : MonoBehaviour
{
    public GameObject relatedWeapon;
    public string textValue;
    public Text text;
    public RectTransform rectTransform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void init(GameObject relatedWeapon, string text)
    {
        this.relatedWeapon = relatedWeapon;
        this.textValue = text;
        this.text.text = textValue;
        rectTransform = GetComponent<RectTransform>();
    }
}
