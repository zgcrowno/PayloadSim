using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputTab : SubTab
{

    public const string HeaderText = "INPUT";

    public GameObject iField;
    public RectTransform ifrt;

    new public void Start()
    {
        base.Start();
        headerText.text = HeaderText;
        iField = Instantiate(Resources.Load("Prefabs/InputTabInputFieldPrefab") as GameObject);
        iField.transform.SetParent(body.transform);
        ifrt = iField.GetComponent<RectTransform>();
        ifrt.anchoredPosition = Vector2.zero;
        ifrt.sizeDelta = Vector2.zero;
    }
}
