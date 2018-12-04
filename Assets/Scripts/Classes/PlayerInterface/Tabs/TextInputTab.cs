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
        iFieldObject = Instantiate(Resources.Load("Prefabs/InputFieldPrefab") as GameObject);
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

    new public void Update()
    {
        base.Update();
        UpdateCaretVisibility();
    }

    public override void SetUp(Vector2 pos, Vector2 size)
    {
        rt.anchoredPosition = new Vector2(pos.x, pos.y);
        rt.sizeDelta = new Vector2(size.x, size.y);
        prt = rt;
        hrt.anchoredPosition = new Vector2(HeaderRectOffset, rt.sizeDelta.y - (Screen.height / 20));
        hrt.sizeDelta = new Vector2(Screen.width / PlayerInterface.MaxSuperTabs, Screen.height / 20);
        brt.anchoredPosition = Vector2.zero;
        brt.sizeDelta = new Vector2(rt.sizeDelta.x, rt.sizeDelta.y - (hrt.sizeDelta.y / 2));
        crt.anchoredPosition = new Vector2(brt.anchoredPosition.x + ResizeOffset, brt.anchoredPosition.y + ResizeOffset);
        crt.sizeDelta = new Vector2(brt.sizeDelta.x - (2 * ResizeOffset), brt.sizeDelta.y - (hrt.sizeDelta.y / 2) - ResizeOffset);

        //Left quadrant
        quadrants[0].x = rt.position.x;
        quadrants[0].y = rt.position.y;
        quadrants[0].width = rt.sizeDelta.x / 3;
        quadrants[0].height = rt.sizeDelta.y;

        //Right quadrant
        quadrants[1].x = rt.position.x + rt.sizeDelta.x - (rt.sizeDelta.x / 3);
        quadrants[1].y = rt.position.y;
        quadrants[1].width = rt.sizeDelta.x / 3;
        quadrants[1].height = rt.sizeDelta.y;

        //Upper quadrant
        quadrants[2].x = rt.position.x + (rt.sizeDelta.x / 3);
        quadrants[2].y = rt.position.y + (rt.sizeDelta.y / 2);
        quadrants[2].width = rt.sizeDelta.x / 3;
        quadrants[2].height = rt.sizeDelta.y / 2;

        //Lower quadrant
        quadrants[3].x = rt.position.x + (rt.sizeDelta.x / 3);
        quadrants[3].y = rt.position.y;
        quadrants[3].width = rt.sizeDelta.x / 3;
        quadrants[3].height = rt.sizeDelta.y / 2;

        //Left resizeRect
        resizeRects[0].x = brt.position.x;
        resizeRects[0].y = brt.position.y + ResizeOffset;
        resizeRects[0].width = ResizeOffset;
        resizeRects[0].height = brt.sizeDelta.y - (ResizeOffset * 2);

        //Right resizeRect
        resizeRects[1].x = brt.position.x + brt.sizeDelta.x - ResizeOffset;
        resizeRects[1].y = brt.position.y + ResizeOffset;
        resizeRects[1].width = ResizeOffset;
        resizeRects[1].height = brt.sizeDelta.y - (ResizeOffset * 2);

        //Top resizeRect
        resizeRects[2].x = brt.position.x + ResizeOffset;
        resizeRects[2].y = brt.position.y + brt.sizeDelta.y - ResizeOffset;
        resizeRects[2].width = brt.sizeDelta.x - (ResizeOffset * 2);
        resizeRects[2].height = ResizeOffset;

        //Bottom resizeRect
        resizeRects[3].x = brt.position.x + ResizeOffset;
        resizeRects[3].y = brt.position.y;
        resizeRects[3].width = brt.sizeDelta.x - (ResizeOffset * 2);
        resizeRects[3].height = ResizeOffset;

        //Bottom-left resizeRect
        resizeRects[4].x = brt.position.x;
        resizeRects[4].y = brt.position.y;
        resizeRects[4].width = ResizeOffset;
        resizeRects[4].height = ResizeOffset;

        //Top-left resizeRect
        resizeRects[5].x = brt.position.x;
        resizeRects[5].y = brt.position.y + brt.sizeDelta.y - ResizeOffset;
        resizeRects[5].width = ResizeOffset;
        resizeRects[5].height = ResizeOffset;

        //Top-right resizeRect
        resizeRects[6].x = brt.position.x + brt.sizeDelta.x - ResizeOffset;
        resizeRects[6].y = brt.position.y + brt.sizeDelta.y - ResizeOffset;
        resizeRects[6].width = ResizeOffset;
        resizeRects[6].height = ResizeOffset;

        //Bottom-right resizeRect
        resizeRects[7].x = brt.position.x + brt.sizeDelta.x - ResizeOffset;
        resizeRects[7].y = brt.position.y;
        resizeRects[7].width = ResizeOffset;
        resizeRects[7].height = ResizeOffset;
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
