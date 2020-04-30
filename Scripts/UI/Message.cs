using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Message : MonoBehaviour
{
    public float messageSpeed = 1f;
    public float lifeTime = 0.5f;
    public Text text;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        moveUp();
    }

    void moveUp()
    {
        transform.Translate(Vector3.up * Time.deltaTime * messageSpeed);
    }

    public void setText(string textInput)
    {
        text.text = textInput;
    }

    public void setColor(float r, float g, float b, float a)
    {
        text.color = new Color(r, g, b, a);
    }
}
