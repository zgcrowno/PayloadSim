using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTab : SubTab {

    public const string HeaderText = "INPUT";

    public Rect textRect; //Using this instead of bodyRect to avoid interference between textarea events and mouse events when resizing and dragging (and also to provide light text padding where needed)
    public GUIStyle inputStyle;
    public GUIStyle prevInputsStyle;

    public Vector2 scrollPos = Vector2.zero;

    public float textHeight = 0;

    public string input = "new input";
    public string prevInputs = "previous input of some sort used for testing purposes only" + "\n";

    new public void Awake()
    {
        base.Awake();
        headerText = HeaderText;
        
        textRect = new Rect(bodyRect.x + ResizeOffset, bodyRect.y + (headerRect.height / 2) + ResizeOffset, bodyRect.width - (2 * ResizeOffset), bodyRect.height - (2 * ResizeOffset) - (headerRect.height / 2));
        inputStyle = new GUIStyle
        {
            fontSize = 22,
            font = (Font)Resources.Load("Fonts/FontCommodoreAngled"),
            alignment = TextAnchor.LowerLeft,
            wordWrap = true,
            clipping = TextClipping.Clip,
            normal = new GUIStyleState
            {
                textColor = Color.white
            }
        };
        prevInputsStyle = new GUIStyle
        {
            fontSize = 22,
            font = (Font)Resources.Load("Fonts/FontCommodoreAngled"),
            alignment = TextAnchor.LowerLeft,
            wordWrap = true,
            clipping = TextClipping.Clip,
            normal = new GUIStyleState
            {
                textColor = Color.gray
            }
        };
        for(int i = 0; i < 100; i++)
        {
            prevInputs += "p\n";
        }
    }

	// Use this for initialization
	new public void Start () {
        base.Start();
	}
	
	// Update is called once per frame
	new public void Update () {
        
	}

    public override void Draw()
    {
        base.Draw();

        textHeight = inputStyle.CalcHeight(new GUIContent(prevInputs + input), textRect.width);

        //Have to use GUIStyle.none to prevent scrollbars from being utilized
        GUI.BeginScrollView(textRect, scrollPos, new Rect(textRect.x, textRect.y, textRect.width, textHeight), GUIStyle.none, GUIStyle.none);

        if(!beingDragged)
        {
            GUI.TextArea(textRect, input, inputStyle);
            GUI.TextArea(textRect, prevInputs, prevInputsStyle);
        }

        GUI.EndScrollView();
    }

    public override void SetUpWholeRect(float x, float y, float width, float height)
    {
        base.SetUpWholeRect(x, y, width, height);

        textRect.x = bodyRect.x + ResizeOffset;
        textRect.y = bodyRect.y + (headerRect.height / 2) + ResizeOffset;
        textRect.width = bodyRect.width - (2 * ResizeOffset);
        textRect.height = bodyRect.height - (2 * ResizeOffset) - (headerRect.height / 2);
    }
}
