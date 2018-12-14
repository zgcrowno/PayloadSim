using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusObject : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    public void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, 0.5f, 0), Space.World);
    }
}
