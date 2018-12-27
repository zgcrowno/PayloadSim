using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextInputTab : SubTab {

    public GameObject iFieldObject;
    public TMP_InputField iField;
    public RectTransform ifrt;
    public TMP_Text input;
    public GameObject textArea;
    public RectTransform trt;
    public GameObject scrollbar; //The scrollbar associated with the contentBody
    public RectTransform srt; //The RectTransform of the scrollbar

    // Use this for initialization
    new public void Start () {
        base.Start();
        iFieldObject = Instantiate(Resources.Load("Prefabs/PlayerInterface/InputFieldPrefab") as GameObject);
        iFieldObject.transform.SetParent(crt);
        iField = iFieldObject.GetComponent<TMP_InputField>();
        iField.onValueChanged.AddListener(delegate { StartCoroutine(UpdateScrollbarVisibility()); });
        ifrt = iFieldObject.GetComponent<RectTransform>();
        ifrt.anchoredPosition = Vector2.zero;
        ifrt.sizeDelta = Vector2.zero;
        input = iField.textComponent;
        textArea = ifrt.Find("Text Area").gameObject;
        trt = textArea.GetComponent<RectTransform>();
        scrollbar = ifrt.Find("Scrollbar").gameObject;
        srt = scrollbar.GetComponent<RectTransform>();
        SetUp(new Vector2(superTab.brt.anchoredPosition.x, superTab.brt.anchoredPosition.y), new Vector2(superTab.brt.sizeDelta.x, superTab.brt.sizeDelta.y));
        StartCoroutine(UpdateScrollbarVisibility()); //Ensure the scrollbar has the correct visibility at start
    }

    new public void FixedUpdate()
    {
        base.FixedUpdate();
        UpdateCaretVisibility();
    }

    public IEnumerator UpdateScrollbarVisibility()
    {
        // This needs to be delayed a frame because it relies on the rect
        // transform height which isn't always up to date because of the
        // dynamic layout.
        yield return new WaitForEndOfFrame();

        var preferredHeight = iField.textComponent.preferredHeight;
        var currentHeight = ifrt.rect.height;
        scrollbar.SetActive(FloatUtil.GTE(preferredHeight, currentHeight) || !FloatUtil.Equals(iField.textComponent.rectTransform.anchoredPosition.y, 0));
    }

    public void UpdateCaretVisibility()
    {
        if(iField.isFocused)
        {
            iField.caretColor = Color.white;
        }
        else
        {
            iField.caretColor = new Color(0, 0, 0, 0);
        }
    }
}
