using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputField : MonoBehaviour {

    public const float CaretVisibilityLimit = 30;

    public const string FieldName = "input";

    public GUIStyle inputStyle;
    public GUIStyle caretOverlayStyle;
    public Texture2D caretTexture;

    public Rect caretRect = new Rect();
    public Rect fieldRect = new Rect();
    public Rect textRect = new Rect();

    public int caretIndex = 0;

    public float caretVisibilityTimer = 0;

    public bool caretIsVisible = false;
    
    public string input = "";

    void Awake()
    {
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
        caretOverlayStyle = new GUIStyle
        {
            fontSize = 22,
            font = (Font)Resources.Load("Fonts/FontCommodoreAngled"),
            alignment = TextAnchor.MiddleCenter,
            wordWrap = true,
            clipping = TextClipping.Clip,
            normal = new GUIStyleState
            {
                textColor = Color.black
            }
        };

        caretTexture = Texture2D.whiteTexture;
    }
    
    void Start () {
		
	}
	
	void FixedUpdate () {
        caretVisibilityTimer++;

        if(caretVisibilityTimer >= CaretVisibilityLimit)
        {
            caretVisibilityTimer = 0;
            caretIsVisible = !caretIsVisible;
        }
	}

    public void Draw()
    {
        GUI.SetNextControlName(FieldName);
        input = GUI.TextArea(fieldRect, input, inputStyle);

        //Set the caret's index within input
        TextEditor editor = (TextEditor)GUIUtility.GetStateObject(typeof(TextEditor), GUIUtility.keyboardControl);
        caretIndex = editor.selectIndex;

        //Set the caret's position and size
        Vector2 caretPosition = inputStyle.GetCursorPixelPosition(fieldRect, new GUIContent(input), caretIndex);
        float charWidth = inputStyle.CalcSize(new GUIContent(" ")).x;
        float charHeight = inputStyle.CalcHeight(new GUIContent(" "), charWidth);
        float maxCharactersInLine = fieldRect.width / charWidth;
        caretRect.x = caretPosition.x;
        caretRect.y = caretPosition.y;
        caretRect.width = charWidth;
        caretRect.height = charHeight;

        //Draw the caret
        if(caretIsVisible && GUI.GetNameOfFocusedControl().Equals(FieldName))
        {
            GUI.DrawTexture(caretRect, caretTexture);
            if(caretIndex < input.Length)
            {
                GUI.Label(caretRect, new GUIContent(input[caretIndex].ToString()), caretOverlayStyle);
            }
        }
    }
}
