using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FocusTab : ObjectTab {

    public const string HeaderText = "FOCUS";

    public GameObject textScrollable;
    public GameObject viewport;
    public GameObject viewportContent;
    public GameObject textObject;
    public TextMeshProUGUI text;
    public RectTransform trt;

    new public void Start () {
        base.Start();
        objectCamera = GameObject.Find("FocusCamera").GetComponent<PayloadCamera>();
        headerText.text = HeaderText;
        textScrollable = Instantiate(Resources.Load("Prefabs/PlayerInterface/TextScrollableAutoSizePrefab") as GameObject);
        textScrollable.transform.SetParent(contentBody.transform);
        viewport = textScrollable.transform.Find("Viewport").gameObject;
        viewportContent = viewport.transform.Find("ViewportContent").gameObject;
        textObject = viewportContent.transform.Find("TextPrefab").gameObject;
        text = textObject.GetComponent<TextMeshProUGUI>();
        trt = textScrollable.GetComponent<RectTransform>();
        SetUp(new Vector2(superTab.brt.anchoredPosition.x, superTab.brt.anchoredPosition.y), new Vector2(superTab.brt.sizeDelta.x, superTab.brt.sizeDelta.y));
    }

    public new void FixedUpdate()
    {
        base.FixedUpdate();
        text.text = objectCamera.contentClickable.GenerateDescription();
    }

    public override void SetUp(Vector2 pos, Vector2 size)
    {
        base.SetUp(pos, size);
        trt.anchorMin = new Vector2(0, 1);
        trt.anchorMax = new Vector2(0, 1);
        trt.pivot = new Vector2(0, 1);
        trt.anchoredPosition = Vector2.zero;
        trt.sizeDelta = new Vector2(ort.position.x - ort.sizeDelta.x - crt.position.x, crt.sizeDelta.y);
    }
}
