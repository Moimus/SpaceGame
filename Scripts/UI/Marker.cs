using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marker : MonoBehaviour
{
    public Ship uiOwner;
    public Transform markedObject;
    RectTransform rectTransform;
    public float uniformScale = 1;
    int screenWidthHalf = Screen.width / 2;
    int screenHeightQuarter = Screen.height / 4;

    // Start is called before the first frame update
    void Start()
    {
        init();
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected virtual void init()
    {
        rectTransform = GetComponent<RectTransform>();
        StartCoroutine(lifeCycle());
    }

    protected virtual IEnumerator lifeCycle()
    {
        while(true) //TODO test for memory leaks
        {
            
            Vector3 targetRelativePos = uiOwner.mainCamera.WorldToScreenPoint(markedObject.position);
            Debug.Log(targetRelativePos.ToString());
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), targetRelativePos, uiOwner.mainCamera, out localPoint);
            if(localPoint.x > -screenWidthHalf && localPoint.x < screenWidthHalf && localPoint.y > -screenHeightQuarter && localPoint.y < screenHeightQuarter && targetRelativePos.z > 0)
            {
                rectTransform.localPosition = new Vector3(localPoint.x, localPoint.y, uiOwner.mainCamera.nearClipPlane + 0.1f);
            }
            else
            {
                StartCoroutine(onDestroy());
            }
            yield return null;
        }
    }

    public virtual IEnumerator onDestroy()
    {
        uiOwner.scannedShips.Remove(markedObject.GetComponent<Ship>());
        StopCoroutine(lifeCycle());
        Destroy(gameObject);
        yield return null;
    }
}
