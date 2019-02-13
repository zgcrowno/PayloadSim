using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class TerminalTab : SubTab {

    public const string HeaderText = "TERMINAL";

    public GameObject terminalInputFieldObject;
    public GameObject viewport;
    public GameObject content;
    public GameObject inputLineObject;
    public GameObject startOfLineObject;
    public GameObject inputFieldObject;
    public GameObject prevInputFieldObject;
    public GameObject bypassPromptObject;
    public GameObject ipGroup1Object;
    public GameObject aObject;
    public GameObject efgObject;
    public GameObject eObject;
    public GameObject fObject;
    public GameObject gObject;
    public GameObject ipPrompt1Object;
    public GameObject ipPrompt1Num1Object;
    public GameObject ipPrompt1Num2Object;
    public GameObject ipPrompt1Num3Object;
    public GameObject ipGroup2Object;
    public GameObject bObject;
    public GameObject hijObject;
    public GameObject hObject;
    public GameObject iObject;
    public GameObject jObject;
    public GameObject ipPrompt2Object;
    public GameObject ipPrompt2Num1Object;
    public GameObject ipPrompt2Num2Object;
    public GameObject ipPrompt2Num3Object;
    public GameObject ipGroup3Object;
    public GameObject cObject;
    public GameObject klmObject;
    public GameObject kObject;
    public GameObject lObject;
    public GameObject mObject;
    public GameObject ipPrompt3Object;
    public GameObject ipPrompt3Num1Object;
    public GameObject ipPrompt3Num2Object;
    public GameObject ipPrompt3Num3Object;
    public GameObject ipGroup4Object;
    public GameObject dObject;
    public GameObject nopObject;
    public GameObject nObject;
    public GameObject oObject;
    public GameObject pObject;
    public GameObject ipPrompt4Object;
    public GameObject ipPrompt4Num1Object;
    public GameObject ipPrompt4Num2Object;
    public GameObject ipPrompt4Num3Object;
    public GameObject numSecondsObject;
    public GameObject secondsTextObject;
    public TextMeshProUGUI aText;
    public TextMeshProUGUI bText;
    public TextMeshProUGUI cText;
    public TextMeshProUGUI dText;
    public TextMeshProUGUI eText;
    public TextMeshProUGUI fText;
    public TextMeshProUGUI gText;
    public TextMeshProUGUI hText;
    public TextMeshProUGUI iText;
    public TextMeshProUGUI jText;
    public TextMeshProUGUI kText;
    public TextMeshProUGUI lText;
    public TextMeshProUGUI mText;
    public TextMeshProUGUI nText;
    public TextMeshProUGUI oText;
    public TextMeshProUGUI pText;
    public TextMeshProUGUI ipPrompt1Num1Text;
    public TextMeshProUGUI ipPrompt1Num2Text;
    public TextMeshProUGUI ipPrompt1Num3Text;
    public TextMeshProUGUI ipPrompt2Num1Text;
    public TextMeshProUGUI ipPrompt2Num2Text;
    public TextMeshProUGUI ipPrompt2Num3Text;
    public TextMeshProUGUI ipPrompt3Num1Text;
    public TextMeshProUGUI ipPrompt3Num2Text;
    public TextMeshProUGUI ipPrompt3Num3Text;
    public TextMeshProUGUI ipPrompt4Num1Text;
    public TextMeshProUGUI ipPrompt4Num2Text;
    public TextMeshProUGUI ipPrompt4Num3Text;
    public TextMeshProUGUI numSecondsText;
    public TextMeshProUGUI secondsText;
    public Text startOfLine;
    public InputField inputField;
    public InputField prevInputField;
    public RectTransform ifrt;
    public RectTransform pifrt;
    public ScrollRect scrollRect;
    public GameObject scrollbar; //The scrollbar associated with the contentBody
    public RectTransform srt; //The RectTransform of the scrollbar
    public int ipPrompt1Num1Target;
    public int ipPrompt1Num2Target;
    public int ipPrompt1Num3Target;
    public int ipPrompt2Num1Target;
    public int ipPrompt2Num2Target;
    public int ipPrompt2Num3Target;
    public int ipPrompt3Num1Target;
    public int ipPrompt3Num2Target;
    public int ipPrompt3Num3Target;
    public int ipPrompt4Num1Target;
    public int ipPrompt4Num2Target;
    public int ipPrompt4Num3Target;
    public float numSecondsToBypass;
    public float ipClarificationInterval;
    public IHackable connectedDevice;
    public IHackable bypassingDevice;
    public bool proxying;

    // Use this for initialization
    new public void Start () {
        base.Start();
        terminalInputFieldObject = Instantiate(Resources.Load("Prefabs/PlayerInterface/TerminalInputFieldPrefab") as GameObject);
        terminalInputFieldObject.transform.SetParent(crt);
        viewport = terminalInputFieldObject.transform.Find("Viewport").gameObject;
        content = viewport.transform.Find("Content").gameObject;
        inputLineObject = content.transform.Find("InputLine").gameObject;
        startOfLineObject = inputLineObject.transform.Find("StartOfLine").gameObject;
        startOfLine = startOfLineObject.GetComponent<Text>();
        inputFieldObject = inputLineObject.transform.Find("Input").gameObject;
        inputField = inputFieldObject.GetComponent<InputField>();
        ifrt = inputFieldObject.GetComponent<RectTransform>();
        prevInputFieldObject = content.transform.Find("PrevInput").gameObject;
        prevInputField = prevInputFieldObject.GetComponent<InputField>();
        pifrt = prevInputFieldObject.GetComponent<RectTransform>();
        bypassPromptObject = content.transform.Find("BypassPrompt").gameObject;
        ipGroup1Object = bypassPromptObject.transform.Find("IPGroup1").gameObject;
        aObject = ipGroup1Object.transform.Find("A").gameObject;
        efgObject = ipGroup1Object.transform.Find("EFG").gameObject;
        eObject = efgObject.transform.Find("E").gameObject;
        fObject = efgObject.transform.Find("F").gameObject;
        gObject = efgObject.transform.Find("G").gameObject; ;
        ipPrompt1Object = ipGroup1Object.transform.Find("IPPrompt1").gameObject;
        ipPrompt1Num1Object = ipPrompt1Object.transform.Find("Num1").gameObject;
        ipPrompt1Num2Object = ipPrompt1Object.transform.Find("Num2").gameObject;
        ipPrompt1Num3Object = ipPrompt1Object.transform.Find("Num3").gameObject;
        ipGroup2Object = bypassPromptObject.transform.Find("IPGroup2").gameObject;
        bObject = ipGroup2Object.transform.Find("B").gameObject;
        hijObject = ipGroup2Object.transform.Find("HIJ").gameObject;
        hObject = hijObject.transform.Find("H").gameObject;
        iObject = hijObject.transform.Find("I").gameObject;
        jObject = hijObject.transform.Find("J").gameObject; ;
        ipPrompt2Object = ipGroup2Object.transform.Find("IPPrompt2").gameObject;
        ipPrompt2Num1Object = ipPrompt2Object.transform.Find("Num1").gameObject;
        ipPrompt2Num2Object = ipPrompt2Object.transform.Find("Num2").gameObject;
        ipPrompt2Num3Object = ipPrompt2Object.transform.Find("Num3").gameObject;
        ipGroup3Object = bypassPromptObject.transform.Find("IPGroup3").gameObject;
        cObject = ipGroup3Object.transform.Find("C").gameObject;
        klmObject = ipGroup3Object.transform.Find("KLM").gameObject;
        kObject = klmObject.transform.Find("K").gameObject;
        lObject = klmObject.transform.Find("L").gameObject;
        mObject = klmObject.transform.Find("M").gameObject; ;
        ipPrompt3Object = ipGroup3Object.transform.Find("IPPrompt3").gameObject;
        ipPrompt3Num1Object = ipPrompt3Object.transform.Find("Num1").gameObject;
        ipPrompt3Num2Object = ipPrompt3Object.transform.Find("Num2").gameObject;
        ipPrompt3Num3Object = ipPrompt3Object.transform.Find("Num3").gameObject;
        ipGroup4Object = bypassPromptObject.transform.Find("IPGroup4").gameObject;
        dObject = ipGroup4Object.transform.Find("D").gameObject;
        nopObject = ipGroup4Object.transform.Find("NOP").gameObject;
        nObject = nopObject.transform.Find("N").gameObject;
        oObject = nopObject.transform.Find("O").gameObject;
        pObject = nopObject.transform.Find("P").gameObject; ;
        ipPrompt4Object = ipGroup4Object.transform.Find("IPPrompt4").gameObject;
        ipPrompt4Num1Object = ipPrompt4Object.transform.Find("Num1").gameObject;
        ipPrompt4Num2Object = ipPrompt4Object.transform.Find("Num2").gameObject;
        ipPrompt4Num3Object = ipPrompt4Object.transform.Find("Num3").gameObject;
        numSecondsObject = bypassPromptObject.transform.Find("NumSeconds").gameObject;
        secondsTextObject = bypassPromptObject.transform.Find("SecondsText").gameObject;
        aText = aObject.GetComponentInChildren<TextMeshProUGUI>();
        bText = bObject.GetComponentInChildren<TextMeshProUGUI>();
        cText = cObject.GetComponentInChildren<TextMeshProUGUI>();
        dText = dObject.GetComponentInChildren<TextMeshProUGUI>();
        eText = eObject.GetComponentInChildren<TextMeshProUGUI>();
        fText = fObject.GetComponentInChildren<TextMeshProUGUI>();
        gText = gObject.GetComponentInChildren<TextMeshProUGUI>();
        hText = hObject.GetComponentInChildren<TextMeshProUGUI>();
        iText = iObject.GetComponentInChildren<TextMeshProUGUI>();
        jText = jObject.GetComponentInChildren<TextMeshProUGUI>();
        kText = kObject.GetComponentInChildren<TextMeshProUGUI>();
        lText = lObject.GetComponentInChildren<TextMeshProUGUI>();
        mText = mObject.GetComponentInChildren<TextMeshProUGUI>();
        nText = nObject.GetComponentInChildren<TextMeshProUGUI>();
        oText = oObject.GetComponentInChildren<TextMeshProUGUI>();
        pText = pObject.GetComponentInChildren<TextMeshProUGUI>();
        ipPrompt1Num1Text = ipPrompt1Num1Object.GetComponentInChildren<TextMeshProUGUI>();
        ipPrompt1Num2Text = ipPrompt1Num2Object.GetComponentInChildren<TextMeshProUGUI>();
        ipPrompt1Num3Text = ipPrompt1Num3Object.GetComponentInChildren<TextMeshProUGUI>();
        ipPrompt2Num1Text = ipPrompt2Num1Object.GetComponentInChildren<TextMeshProUGUI>();
        ipPrompt2Num2Text = ipPrompt2Num2Object.GetComponentInChildren<TextMeshProUGUI>();
        ipPrompt2Num3Text = ipPrompt2Num3Object.GetComponentInChildren<TextMeshProUGUI>();
        ipPrompt3Num1Text = ipPrompt3Num1Object.GetComponentInChildren<TextMeshProUGUI>();
        ipPrompt3Num2Text = ipPrompt3Num2Object.GetComponentInChildren<TextMeshProUGUI>();
        ipPrompt3Num3Text = ipPrompt3Num3Object.GetComponentInChildren<TextMeshProUGUI>();
        ipPrompt4Num1Text = ipPrompt4Num1Object.GetComponentInChildren<TextMeshProUGUI>();
        ipPrompt4Num2Text = ipPrompt4Num2Object.GetComponentInChildren<TextMeshProUGUI>();
        ipPrompt4Num3Text = ipPrompt4Num3Object.GetComponentInChildren<TextMeshProUGUI>();
        numSecondsText = numSecondsObject.GetComponentInChildren<TextMeshProUGUI>();
        secondsText = secondsTextObject.GetComponentInChildren<TextMeshProUGUI>();
        scrollRect = terminalInputFieldObject.GetComponent<ScrollRect>();
        scrollbar = terminalInputFieldObject.transform.Find("Scrollbar").gameObject;
        srt = scrollbar.GetComponent<RectTransform>();

        ipPrompt1Num1Target = 0;
        ipPrompt1Num2Target = 0;
        ipPrompt1Num3Target = 0;
        ipPrompt2Num1Target = 0;
        ipPrompt2Num2Target = 0;
        ipPrompt2Num3Target = 0;
        ipPrompt3Num1Target = 0;
        ipPrompt3Num2Target = 0;
        ipPrompt3Num3Target = 0;
        ipPrompt4Num1Target = 0;
        ipPrompt4Num2Target = 0;
        ipPrompt4Num3Target = 0;
        numSecondsToBypass = 25;
        ipClarificationInterval = 5;

        headerText.text = HeaderText;
        inputField.lineType = InputField.LineType.MultiLineSubmit;
        inputField.onEndEdit.AddListener(ExecuteInput);
        inputField.onValueChanged.AddListener(ProcessInput);

        SetUp(new Vector2(superTab.brt.anchoredPosition.x, superTab.brt.anchoredPosition.y), new Vector2(superTab.brt.sizeDelta.x, superTab.brt.sizeDelta.y));
    }

    public void Update()
    {
        //Bypassing Behavior
        if(bypassingDevice != null)
        {
            numSecondsToBypass -= Time.deltaTime;
            numSecondsText.text = "" + Mathf.CeilToInt(numSecondsToBypass);
            //numSecondsText.text = string.Format("{0:F2}", numSecondsToBypass);
        }
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

        if (connectedDevice == null)
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

        StartCoroutine(ScrollToBottom());
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

                    string lowerCaseString = str.Trim().ToLower();

                    string[] strArr = lowerCaseString.Split(new char[] { Delimiter.Space }, Command.MaxElements);

                    if(strArr[0].Equals(Command.Bypass))
                    {
                        if (strArr.Length >= 2)
                        {
                            foreach (Clickable hackable in pi.GetAllObjectsOfType(typeof(IHackable)))
                            {
                                if (hackable.name.ToLower().Equals(strArr[1]))
                                {
                                    Bypass((IHackable)hackable);
                                }
                            }
                        }
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
        else if(Input.GetKeyDown(KeyCode.Escape))
        {
            bypassPromptObject.SetActive(false);
            bypassingDevice = null;
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

    public void Bypass(IHackable hackable)
    {
        if(hackable.firewallLevel != FirewallLevel.None)
        {
            bypassingDevice = hackable;

            switch (hackable.firewallLevel)
            {
                case FirewallLevel.One:
                    ipPrompt1Num1Target = Random.Range(0, 9);
                    ipPrompt1Num2Target = Random.Range(0, 9);
                    ipPrompt1Num3Target = Random.Range(0, 9);
                    ipPrompt2Num1Target = Random.Range(0, 9);
                    ipPrompt2Num2Target = Random.Range(0, 9);
                    ipPrompt2Num3Target = Random.Range(0, 9);
                    ipPrompt3Num1Target = Random.Range(0, 9);
                    ipPrompt3Num2Target = Random.Range(0, 9);
                    ipPrompt3Num3Target = Random.Range(0, 9);
                    ipPrompt4Num1Target = Random.Range(0, 9);
                    ipPrompt4Num2Target = Random.Range(0, 9);
                    ipPrompt4Num3Target = Random.Range(0, 9);
                    numSecondsToBypass = 25f;
                    ipClarificationInterval = 5f;
                    break;
                case FirewallLevel.Two:
                    numSecondsToBypass = 20f;
                    ipClarificationInterval = 4f;
                    break;
                case FirewallLevel.Three:
                    numSecondsToBypass = 15f;
                    ipClarificationInterval = 3f;
                    break;
            }

            bypassPromptObject.SetActive(true);
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
