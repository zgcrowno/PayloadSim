using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class Clickable : MonoBehaviour {

    public const float MaxValue = 100; //The maximum value any of this Clickable's numerical attributes may reach
    
    public GameObject stage; //The stage in which this Clickable resides
    public LevelsTab levelsTab; //The LevelsTab in which we'll see this Clickable
    public GameObject nameIcon; //The Icon which will display this Clickable's name, and which resides directly above this Clickable
    public TextMeshProUGUI nameIconText; //The TextMeshProUGUI component of nameIcon
    public Camera levelsCamera; //The Camera used to produce a RenderTexture of the level
    public Renderer rend; //This Clickable's Renderer

    public string designation; //A string representing the type of object this is
    public string cls; //A string representing the "class" of the object (Human, Food, Personal Effect, etc.)
    public string description; //A long string representing the assets of this Clickable that will be visible to the player when focused upon

	// Use this for initialization
	public void Start () {
        stage = GameObject.Find("/Stage");
        levelsTab = FindObjectOfType<LevelsTab>();
        nameIcon = Instantiate(Resources.Load("Prefabs/PlayerInterface/IconPrefab") as GameObject, levelsTab.transform);
        nameIconText = nameIcon.GetComponentInChildren<TextMeshProUGUI>();
        levelsCamera = FindObjectOfType<LevelsCamera>().GetComponent<Camera>();
        rend = GetComponent<Renderer>();

        nameIcon.SetActive(false); //Ensure that this Clickable's nameIcon defaults to false; it should only ever be visible under specific circumstances
        nameIconText.text = name; //Set the nameIconText to the name of this Clickable
    }

    public void Update()
    {
        nameIcon.transform.position = new Vector2(levelsTab.ort.position.x - (levelsTab.ort.sizeDelta.x) + (levelsTab.ort.sizeDelta.x * levelsCamera.WorldToViewportPoint(rend.bounds.center).x), levelsTab.ort.position.y - (levelsTab.ort.sizeDelta.y / 2) + (levelsTab.ort.sizeDelta.y * levelsCamera.WorldToViewportPoint(rend.bounds.max).y));
        nameIcon.transform.SetAsLastSibling();
    }

    public bool Is(Type type)
    {
        if(GetType().IsInterface)
        {
            return GetType().IsAssignableFrom(type);
        }
        else
        {
            return type.IsAssignableFrom(GetType());
        }
    }

    /*
     * Returns the object of the passed type which is nearest to this Clickable
     * @param type The passed type for which we're checking proximity
     * @return The Clickable of the passed type which is nearest to this Clickable
     */
    public Clickable GetNearestObjectOfType(Type type)
    {
        Clickable[] stageClickables = stage.GetComponentsInChildren<Clickable>();
        List<Clickable> stageClickablesOfType = new List<Clickable>();
        Clickable nearestClickableOfType = null;

        for(int i = 0; i < stageClickables.Length; i++)
        {
            if(stageClickables[i].GetType() == type)
            {
                stageClickablesOfType.Add(stageClickables[i]);
            }
        }

        foreach(Clickable clickable in stageClickablesOfType)
        {
            if((nearestClickableOfType == null || Vector3.Distance(transform.position, clickable.transform.position) < Vector3.Distance(transform.position, nearestClickableOfType.transform.position)) && clickable != this)
            {
                nearestClickableOfType = clickable;
            }
        }
        
        return nearestClickableOfType;
    }

    public virtual string GenerateDescription()
    {
        return "DESIGNATION: " + designation + "\n" + "CLASS: " + cls;
    }
}
