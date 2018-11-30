using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NotesTab : SubTab {

    public const string HeaderText = "NOTES";

    public GameObject iFieldObject;
    public TMP_InputField iField;
    public RectTransform ifrt;
    public TMP_Text input;

    new public void Start() {
        base.Start();
        headerText.text = HeaderText;
        iFieldObject = Instantiate(Resources.Load("Prefabs/NotesTabInputFieldPrefab") as GameObject);
        iFieldObject.transform.SetParent(body.transform);
        iField = iFieldObject.GetComponent<TMP_InputField>();
        ifrt = iFieldObject.GetComponent<RectTransform>();
        ifrt.anchoredPosition = Vector2.zero;
        ifrt.sizeDelta = Vector2.zero;
        input = iField.textComponent;
    }
}
