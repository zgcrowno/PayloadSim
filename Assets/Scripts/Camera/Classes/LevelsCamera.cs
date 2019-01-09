using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsCamera : PayloadCamera {

    // Use this for initialization
    new public void Start () {
        base.Start();
        content = GameObject.Find("/Stage/Level");
        contentClickable = content.GetComponent<Clickable>();
        contentRenderers = content.GetComponentsInChildren<Renderer>();
        CenterContent();
    }

    new public void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
