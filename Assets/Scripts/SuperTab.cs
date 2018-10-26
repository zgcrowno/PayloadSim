using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperTab : MonoBehaviour {

    public World world; //The overarching World of which everything else is a child
    public SubTab[] subTabs; //The SubTabs of which this SuperTab is a parent
    public Rect headerRect; //The Rect making up this SubTab's header
    public Rect bodyRect; //The Rect making up this SubTab's body

    public float x; //The x-coordinate held by the SuperTab
    public float y; //The y-coordinate held by the SuperTab
    public float width; //The width of the SuperTab
    public float height; //The height of the SuperTab

    // Use this for initialization
    void Start () {
        world = GameObject.Find("/World").GetComponent<World>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
