using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NotesTab : TextInputTab {

    public const string HeaderText = "NOTES";

    new public void Start() {
        base.Start();
        headerText.text = HeaderText;
        iField.lineType = TMP_InputField.LineType.MultiLineNewline;
    }
}
