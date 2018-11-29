using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class InputTab : SubTab
{

    public const string HeaderText = "INPUT";

    public const float ScrollSpeed = 100;

    public List<string> prevInputs = new List<string>();

    public GameObject iFieldObject;
    public TMP_InputField iField;
    public RectTransform ifrt;
    public TMP_Text input;

    new public void Start()
    {
        base.Start();
        headerText.text = HeaderText;
        iFieldObject = Instantiate(Resources.Load("Prefabs/InputTabInputFieldPrefab") as GameObject);
        iFieldObject.transform.SetParent(body.transform);
        iField = iFieldObject.GetComponent<TMP_InputField>();
        iField.onSubmit.AddListener(ProcessInput);
        ifrt = iFieldObject.GetComponent<RectTransform>();
        ifrt.anchoredPosition = Vector2.zero;
        ifrt.sizeDelta = Vector2.zero;
        input = iField.textComponent;
    }

    public void ProcessInput(string str)
    {
        prevInputs.Add(str);
        iField.text = "";
        iField.ActivateInputField();
    }
}
