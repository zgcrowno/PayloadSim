using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ObjectTab : SubTab
{

    public PayloadCamera objectCamera;
    public GameObject objectImage;
    public RawImage objectImageRaw;
    public RectTransform ort;
    public RenderTexture renderTexture;

	// Use this for initialization
	new public void Start () {
        base.Start();
        objectImage = Instantiate(Resources.Load("Prefabs/PlayerInterface/ObjectImagePrefab") as GameObject);
        objectImage.transform.SetParent(contentBody.transform);
        objectImageRaw = objectImage.GetComponent<RawImage>();
        ort = objectImage.GetComponent<RectTransform>();
        renderTexture = (RenderTexture) objectImageRaw.texture;
        SetUp(new Vector2(superTab.brt.anchoredPosition.x, superTab.brt.anchoredPosition.y), new Vector2(superTab.brt.sizeDelta.x, superTab.brt.sizeDelta.y));
    }

    public override void SetUp(Vector2 pos, Vector2 size)
    {
        base.SetUp(pos, size);

        //Set up the object to display's RectTransform, based on whether crt's width or height is greater
        float newDimension = crt.sizeDelta.x > crt.sizeDelta.y ? crt.sizeDelta.y : crt.sizeDelta.x;
        ort.sizeDelta = new Vector2(newDimension, newDimension);

        //Reassign RenderTexture to get correct size
        if (objectCamera.cam.targetTexture != null)
        {
            objectCamera.cam.targetTexture.Release();
        }
        renderTexture = new RenderTexture(Mathf.FloorToInt(ort.sizeDelta.x), Mathf.FloorToInt(ort.sizeDelta.y), renderTexture.depth);
        objectCamera.cam.targetTexture = renderTexture;
        objectImageRaw.texture = renderTexture;
    }
}
