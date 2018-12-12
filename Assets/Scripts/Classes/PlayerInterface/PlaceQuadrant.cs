using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlaceQuadrant : MonoBehaviour
{

    public RectTransform rt;

    void Awake()
    {
        rt = GetComponent<RectTransform>();
    }
}
