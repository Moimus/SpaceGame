using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerUI : MonoBehaviour
{
    public GameObject markerPrefab;
    public GameObject hitMarkerPrefab;
    public GameObject crossHair;
    public Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void markTarget(Ship targetShip)
    {
        GameObject markerInstance = Instantiate(markerPrefab, canvas.transform);
        markerInstance.GetComponent<Marker>().uiOwner = transform.parent.GetComponent<Ship>();
        markerInstance.GetComponent<Marker>().markedObject = targetShip.transform;
        targetShip.relatedMarkers.Add(markerInstance.GetComponent<Marker>());
    }

    public void spawnHitMarker()
    {
        GameObject hitMarker = Instantiate(hitMarkerPrefab, canvas.transform);
        hitMarker.transform.localPosition = new Vector2(0, 100);
    }
}
