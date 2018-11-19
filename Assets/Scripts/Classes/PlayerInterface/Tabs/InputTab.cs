using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTab : SubTab
{

    public const string HeaderText = "INPUT";

    public InputField inputField;
    public GUIStyle prevInputsStyle;
    public Rect prevInputsRect; //The rect in which are contained all previously entered inputs
    public Rect scrollRect; //The rect in which are contained all of the scrollable controls

    public Vector2 scrollPos = Vector2.zero;

    public float inputFieldTextHeight = 0;
    public float prevInputsTextHeight = 0;
    public float totalTextHeight = 0;

    public string prevInputs = "previous input of some sort used for testing purposes only" + "\n";

    new public void Awake()
    {
        base.Awake();
        headerText = HeaderText;

        inputField = gameObject.AddComponent<InputField>();

        prevInputsStyle = new GUIStyle
        {
            fontSize = 22,
            font = pi.font,
            alignment = TextAnchor.LowerLeft,
            wordWrap = true,
            clipping = TextClipping.Clip,
            normal = new GUIStyleState
            {
                textColor = Color.gray
            }
        };
        for (int i = 0; i < 100; i++)
        {
            prevInputs += "p";
            if (i != 99)
            {
                prevInputs += "\n";
            }
        }
    }

    // Use this for initialization
    new public void Start()
    {
        base.Start();

        //Not using bodyRect's exact values so as to avoid interference between textarea events and mouse events (MouseDown, MouseDrag and MouseUp have likely already been "used", and therefore reserved by TextArea) when resizing and dragging (and also to provide light text padding where needed)
        inputField.fieldRect = new Rect(bodyRect.x + ResizeOffset, bodyRect.y + (headerRect.height / 2) + ResizeOffset, bodyRect.width - (2 * ResizeOffset), bodyRect.height - (2 * ResizeOffset) - (headerRect.height / 2));

        inputFieldTextHeight = pi.regularFontStyle.CalcHeight(new GUIContent(inputField.input), inputField.fieldRect.width);
        prevInputsTextHeight = pi.regularFontStyle.CalcHeight(new GUIContent(prevInputs), inputField.fieldRect.width);

        prevInputsRect = new Rect(inputField.fieldRect.x, inputField.fieldRect.y + inputField.fieldRect.height - inputFieldTextHeight - prevInputsTextHeight, inputField.fieldRect.width, prevInputsTextHeight);
        scrollRect = new Rect(inputField.fieldRect.x, Mathf.Min(inputField.fieldRect.y, prevInputsRect.y), inputField.fieldRect.width, Mathf.Max(inputField.fieldRect.height, inputField.fieldRect.y + inputField.fieldRect.height - prevInputsRect.y));
    }

    // Update is called once per frame
    new public void Update()
    {

    }

    public override void Draw()
    {
        base.Draw();

        inputFieldTextHeight = pi.regularFontStyle.CalcHeight(new GUIContent(inputField.input), inputField.fieldRect.width);
        prevInputsTextHeight = pi.regularFontStyle.CalcHeight(new GUIContent(prevInputs), inputField.fieldRect.width);
        totalTextHeight = pi.regularFontStyle.CalcHeight(new GUIContent(prevInputs + inputField.input), inputField.fieldRect.width);

        //Have to use GUIStyle.none to prevent scrollbars from being utilized
        scrollPos = GUI.BeginScrollView(inputField.fieldRect, scrollPos, scrollRect, GUIStyle.none, GUIStyle.none);

        if (!beingDragged)
        {
            inputField.Draw();
            GUI.Label(prevInputsRect, prevInputs, prevInputsStyle);
        }

        GUI.EndScrollView();

        //Set up prevInputsRect and scrollRect to accurately reflect scrollable content
        prevInputsRect.x = inputField.fieldRect.x;
        prevInputsRect.y = inputField.fieldRect.y + inputField.fieldRect.height - inputFieldTextHeight - prevInputsTextHeight;
        prevInputsRect.width = inputField.fieldRect.width;
        prevInputsRect.height = prevInputsTextHeight;

        scrollRect.x = inputField.fieldRect.x;
        scrollRect.y = Mathf.Min(inputField.fieldRect.y, prevInputsRect.y);
        scrollRect.width = inputField.fieldRect.width;
        scrollRect.height = Mathf.Max(inputField.fieldRect.height, inputField.fieldRect.y + inputField.fieldRect.height - prevInputsRect.y);
    }

    public override void SetUpWholeRect(float x, float y, float width, float height)
    {
        base.SetUpWholeRect(x, y, width, height);

        inputField.fieldRect.x = bodyRect.x + ResizeOffset;
        inputField.fieldRect.y = bodyRect.y + (headerRect.height / 2) + ResizeOffset;
        inputField.fieldRect.width = bodyRect.width - (2 * ResizeOffset);
        inputField.fieldRect.height = bodyRect.height - (2 * ResizeOffset) - (headerRect.height / 2);

        inputFieldTextHeight = pi.regularFontStyle.CalcHeight(new GUIContent(inputField.input), inputField.fieldRect.width);

        prevInputsRect.x = inputField.fieldRect.x;
        prevInputsRect.y = inputField.fieldRect.y + inputField.fieldRect.height - inputFieldTextHeight - prevInputsTextHeight;
        prevInputsRect.width = inputField.fieldRect.width;
        prevInputsRect.height = prevInputsTextHeight;

        scrollRect.x = inputField.fieldRect.x;
        scrollRect.y = Mathf.Min(inputField.fieldRect.y, prevInputsRect.y);
        scrollRect.width = inputField.fieldRect.width;
        scrollRect.height = Mathf.Max(inputField.fieldRect.height, inputField.fieldRect.y + inputField.fieldRect.height - prevInputsRect.y);
    }
}
