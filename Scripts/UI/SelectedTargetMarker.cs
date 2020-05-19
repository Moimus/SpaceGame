using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedTargetMarker : Marker
{
    // Start is called before the first frame update
    void Start()
    {
        init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override IEnumerator onDestroy()
    {
        try
        {
            //uiOwner.scannedShips.Remove(markedObject.GetComponent<Ship>());
            uiOwner.selectedTargetPointer = -1;
            StopCoroutine(lifeCycle());
            Destroy(gameObject);
        }
        catch (MissingReferenceException)
        {

        }

        yield return null;
    }
}
