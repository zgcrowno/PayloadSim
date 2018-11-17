using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputField : MonoBehaviour {

    public const float CaretVisibilityLimit = 30; //The caretVisibilityTimer value to reach before negating caretIsVisible
    public const string FieldName = "input";

    public PlayerInterface pi; //The PlayerInterface object of which this InputField is a part
    public GUIStyle caretOverlayStyle; //The style to use for the caret
    public Texture2D caretTexture; //The texture to use for the caret

    public Rect caretRect = new Rect();
    public Rect fieldRect = new Rect();

    public int caretIndex = 0;

    public float caretVisibilityTimer = 0;
    public float inputHeight = 0;

    public bool caretIsVisible = false;
    
    public string input = "";

    void Awake()
    {
        pi = GameObject.Find("/World").GetComponent<PlayerInterface>();
        
        caretOverlayStyle = new GUIStyle
        {
            fontSize = 22,
            font = pi.font,
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
        //Set the control name of input's TextArea so we can access it by name farther down
        GUI.SetNextControlName(FieldName);

        input = GUI.TextArea(fieldRect, input, pi.regularFontStyle);

        //Instantiate this TextEditor object so we can access required caret information
        TextEditor editor = (TextEditor)GUIUtility.GetStateObject(typeof(TextEditor), GUIUtility.keyboardControl);

        //Set the caret's index within input
        caretIndex = editor.selectIndex;

        //Set the caret's position and size
        Vector2 caretPosition = pi.regularFontStyle.GetCursorPixelPosition(fieldRect, new GUIContent(input), caretIndex);
        caretRect.x = caretPosition.x;
        caretRect.y = caretPosition.y;
        caretRect.width = pi.regularCharWidth;
        caretRect.height = pi.regularCharHeight;

        //Draw the caret
        if (caretIsVisible && GUI.GetNameOfFocusedControl().Equals(FieldName))
        {
            //This InputField has focus, and caretIsVisible == true

            GUI.DrawTexture(caretRect, caretTexture);
            if(caretIndex < input.Length)
            {
                //Ensure that text is still legible even when covered by caret

                GUI.Label(caretRect, new GUIContent(input[caretIndex].ToString()), caretOverlayStyle);
            }
        }
    }
}
