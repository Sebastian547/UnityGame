using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MiniMapUI : MonoBehaviour
{
    bool zoomed = false;
    RectTransform canv;
    RectTransform miniMap;

    Vector3 orgPos;
    Vector3 orgScal;

    void Start()
    {
        miniMap = GetComponent<RectTransform>();
        orgPos = miniMap.position;
        orgScal = miniMap.localScale;

        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().showMapEvent += MapZoom;
        canv = GetComponentInParent<Canvas>().GetComponent<RectTransform>(); 
    }
    

    
    private void MapZoom()
    {
        zoomed = !zoomed;

        if (zoomed)
        {
            miniMap.localScale = new Vector3 (3, 3, 1);
            miniMap.position = canv.position;
        }
        else
        {
            miniMap.localScale = orgScal;
            miniMap.position = orgPos;
        };
    }
}
