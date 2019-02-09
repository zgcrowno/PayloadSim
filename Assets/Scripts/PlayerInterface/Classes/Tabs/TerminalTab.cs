using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TerminalTab : SubTab {

    public const string HeaderText = "TERMINAL";

    public GameObject terminalInputFieldObject;
    public GameObject viewport;
    public GameObject content;
    public GameObject inputLine;
    public GameObject startOfLineObject;
    public GameObject inputFieldObject;
    public GameObject prevInputFieldObject;
    public Text startOfLine;
    public InputField inputField;
    public InputField prevInputField;
    public RectTransform ifrt;
    public RectTransform pifrt;
    public ScrollRect scrollRect;
    public GameObject scrollbar; //The scrollbar associated with the contentBody
    public RectTransform srt; //The RectTransform of the scrollbar

    public bool connected;
    public bool bypassing;
    public bool proxying;

    // Use this for initialization
    new public void Start () {
        base.Start();
        terminalInputFieldObject = Instantiate(Resources.Load("Prefabs/PlayerInterface/TerminalInputFieldPrefab") as GameObject);
        terminalInputFieldObject.transform.SetParent(crt);
        viewport = terminalInputFieldObject.transform.Find("Viewport").gameObject;
        content = viewport.transform.Find("Content").gameObject;
        inputLine = content.transform.Find("InputLine").gameObject;
        startOfLineObject = inputLine.transform.Find("StartOfLine").gameObject;
        startOfLine = startOfLineObject.GetComponent<Text>();
        inputFieldObject = inputLine.transform.Find("Input").gameObject;
        inputField = inputFieldObject.GetComponent<InputField>();
        ifrt = inputFieldObject.GetComponent<RectTransform>();
        prevInputFieldObject = content.transform.Find("PrevInput").gameObject;
        prevInputField = prevInputFieldObject.GetComponent<InputField>();
        pifrt = prevInputFieldObject.GetComponent<RectTransform>();
        scrollRect = terminalInputFieldObject.GetComponent<ScrollRect>();
        scrollbar = terminalInputFieldObject.transform.Find("Scrollbar").gameObject;
        srt = scrollbar.GetComponent<RectTransform>();

        headerText.text = HeaderText;
        inputField.lineType = InputField.LineType.MultiLineSubmit;
        inputField.onEndEdit.AddListener(ExecuteInput);
        inputField.onValueChanged.AddListener(ProcessInput);

        SetUp(new Vector2(superTab.brt.anchoredPosition.x, superTab.brt.anchoredPosition.y), new Vector2(superTab.brt.sizeDelta.x, superTab.brt.sizeDelta.y));
        StartCoroutine(UpdateScrollbarVisibility()); //Ensure the scrollbar has the correct visibility at start
    }

    new public void FixedUpdate()
    {
        base.FixedUpdate();
        UpdateCaretVisibility();
    }

    public override void SetUp(Vector2 pos, Vector2 size)
    {
        base.SetUp(pos, size);
        if (isActiveAndEnabled)
        {
            StartCoroutine(UpdateScrollbarVisibility());
        }
    }

    public void ProcessInput(string str)
    {
        str = str.Trim().ToLower();

        string[] strArr = str.Split(new char[] { Delimiter.Space }, Command.MaxElements);

        if (!connected)
        {
            if (strArr.Length >= 1)
            {
                if (strArr[0].Equals(Command.Bypass))
                {
                    if (strArr.Length >= 2)
                    {
                        foreach (Clickable hackable in pi.GetAllObjectsOfType(typeof(IHackable)))
                        {
                            if (hackable.name.Length >= strArr[1].Length && hackable.name.ToLower().Substring(0, strArr[1].Length).Equals(strArr[1]))
                            {
                                hackable.nameIcon.SetActive(true);
                            }
                            else
                            {
                                hackable.nameIcon.SetActive(false);
                            }
                        }
                    }
                    else
                    {
                        foreach (Clickable hackable in pi.GetAllObjectsOfType(typeof(IHackable)))
                        {
                            hackable.nameIcon.SetActive(true);
                        }
                    }
                }
                else
                {
                    foreach (Clickable hackable in pi.GetAllObjectsOfType(typeof(IHackable)))
                    {
                        hackable.nameIcon.SetActive(false);
                    }
                }
            }
        }
        else
        {

        }
    }

    public void ExecuteInput(string str)
    {
        //Have to add this input check because InputField's onEndEdit event is called on defocus as well as enter
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            if (str.Equals(Command.Clear))
            {
                //prevInput is empty
                prevInputField.text = "";
                prevInputFieldObject.SetActive(false);
            }
            else
            {
                if (!str.Equals(""))
                {
                    if (prevInputField.text.Equals(""))
                    {
                        //prevInput is empty
                        prevInputFieldObject.SetActive(true);
                        prevInputField.text += startOfLine.text + str;
                    }
                    else
                    {
                        prevInputField.text += "\n" + startOfLine.text + str;
                    }
                    FormulateOutput(str);
                }
            }

            str = str.Trim().ToLower();

            inputField.text = "";
            //iField.text = "";

            //outputTab.FormulateOutput(str);

            StartCoroutine(ScrollToBottom());

            inputField.ActivateInputField();
        }
    }

    public void FormulateOutput(string str)
    {
        prevInputField.text += "\n" + "The input [" + str + "] is not a recognized command.";

        if (!superTab.IsFrontmost())
        {
            seen = false;
        }
    }

    public IEnumerator ScrollToBottom()
    {
        //This needs to be delayed a frame because it takes one frame
        //to update the ScrollRect
        yield return new WaitForEndOfFrame();

        scrollRect.normalizedPosition = Vector2.zero;
    }

    public IEnumerator UpdateScrollbarVisibility()
    {
        // This needs to be delayed a frame because it relies on the rect
        // transform height which isn't always up to date because of the
        // dynamic layout.
        yield return new WaitForEndOfFrame();

        var preferredHeight = inputField.textComponent.preferredHeight;
        var currentHeight = ifrt.rect.height;
        scrollbar.SetActive(FloatUtil.GTE(preferredHeight, currentHeight) || !FloatUtil.Equals(inputField.textComponent.rectTransform.anchoredPosition.y, 0));
    }

    public void UpdateCaretVisibility()
    {
        if (inputField.isFocused)
        {
            inputField.caretColor = Color.white;
        }
        else
        {
            inputField.caretColor = new Color(0, 0, 0, 0);
        }
    }

    public override void OnPointerDown(PointerEventData ped)
    {
        base.OnPointerDown(ped);
        if(!RectTransformUtility.RectangleContainsScreenPoint(pifrt, ped.position)
            || prevInputField.text.Equals(""))
        {
            inputField.ActivateInputField();
        }
    }
}
