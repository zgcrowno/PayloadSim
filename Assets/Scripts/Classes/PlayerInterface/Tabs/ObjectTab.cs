using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectTab : SubTab {

    public Camera objectCamera;
    public GameObject objectImage;
    public RawImage objectImageRaw;
    public RectTransform ort;
    public RenderTexture renderTexture;

	// Use this for initialization
	new public void Start () {
        base.Start();
        objectImage = Instantiate(Resources.Load("Prefabs/ObjectImagePrefab") as GameObject);
        objectImage.transform.SetParent(contentBody.transform);
        objectImageRaw = objectImage.GetComponent<RawImage>();
        ort = objectImage.GetComponent<RectTransform>();
        renderTexture = (RenderTexture) objectImageRaw.texture;
        SetUp(new Vector2(superTab.brt.anchoredPosition.x, superTab.brt.anchoredPosition.y), new Vector2(superTab.brt.sizeDelta.x, superTab.brt.sizeDelta.y));
    }
	
	// Update is called once per frame
	new public void Update () {
		
	}

    public override void SetUp(Vector2 pos, Vector2 size)
    {
        rt.anchoredPosition = new Vector2(pos.x, pos.y);
        rt.sizeDelta = new Vector2(size.x, size.y);
        prt = rt;
        hrt.anchoredPosition = new Vector2(HeaderRectOffset, rt.sizeDelta.y - (Screen.height / 20));
        hrt.sizeDelta = new Vector2(Screen.width / PlayerInterface.MaxSuperTabs, Screen.height / 20);
        brt.anchoredPosition = Vector2.zero;
        brt.sizeDelta = new Vector2(rt.sizeDelta.x, rt.sizeDelta.y - (hrt.sizeDelta.y / 2));
        crt.anchoredPosition = new Vector2(brt.anchoredPosition.x + ResizeOffset, brt.anchoredPosition.y + ResizeOffset);
        crt.sizeDelta = new Vector2(brt.sizeDelta.x - (2 * ResizeOffset), brt.sizeDelta.y - (hrt.sizeDelta.y / 2) - ResizeOffset);

        //Set up the object to display's RectTransform, based on whether crt's width or height is greater
        float newDimension = crt.sizeDelta.x > crt.sizeDelta.y ? crt.sizeDelta.y : crt.sizeDelta.x;
        ort.sizeDelta = new Vector2(newDimension, newDimension);

        //Left quadrant
        quadrants[0].x = rt.position.x;
        quadrants[0].y = rt.position.y;
        quadrants[0].width = rt.sizeDelta.x / 3;
        quadrants[0].height = rt.sizeDelta.y;

        //Right quadrant
        quadrants[1].x = rt.position.x + rt.sizeDelta.x - (rt.sizeDelta.x / 3);
        quadrants[1].y = rt.position.y;
        quadrants[1].width = rt.sizeDelta.x / 3;
        quadrants[1].height = rt.sizeDelta.y;

        //Upper quadrant
        quadrants[2].x = rt.position.x + (rt.sizeDelta.x / 3);
        quadrants[2].y = rt.position.y + (rt.sizeDelta.y / 2);
        quadrants[2].width = rt.sizeDelta.x / 3;
        quadrants[2].height = rt.sizeDelta.y / 2;

        //Lower quadrant
        quadrants[3].x = rt.position.x + (rt.sizeDelta.x / 3);
        quadrants[3].y = rt.position.y;
        quadrants[3].width = rt.sizeDelta.x / 3;
        quadrants[3].height = rt.sizeDelta.y / 2;

        //Left resizeRect
        resizeRects[0].x = brt.position.x;
        resizeRects[0].y = brt.position.y + ResizeOffset;
        resizeRects[0].width = ResizeOffset;
        resizeRects[0].height = brt.sizeDelta.y - (ResizeOffset * 2);

        //Right resizeRect
        resizeRects[1].x = brt.position.x + brt.sizeDelta.x - ResizeOffset;
        resizeRects[1].y = brt.position.y + ResizeOffset;
        resizeRects[1].width = ResizeOffset;
        resizeRects[1].height = brt.sizeDelta.y - (ResizeOffset * 2);

        //Top resizeRect
        resizeRects[2].x = brt.position.x + ResizeOffset;
        resizeRects[2].y = brt.position.y + brt.sizeDelta.y - ResizeOffset;
        resizeRects[2].width = brt.sizeDelta.x - (ResizeOffset * 2);
        resizeRects[2].height = ResizeOffset;

        //Bottom resizeRect
        resizeRects[3].x = brt.position.x + ResizeOffset;
        resizeRects[3].y = brt.position.y;
        resizeRects[3].width = brt.sizeDelta.x - (ResizeOffset * 2);
        resizeRects[3].height = ResizeOffset;

        //Bottom-left resizeRect
        resizeRects[4].x = brt.position.x;
        resizeRects[4].y = brt.position.y;
        resizeRects[4].width = ResizeOffset;
        resizeRects[4].height = ResizeOffset;

        //Top-left resizeRect
        resizeRects[5].x = brt.position.x;
        resizeRects[5].y = brt.position.y + brt.sizeDelta.y - ResizeOffset;
        resizeRects[5].width = ResizeOffset;
        resizeRects[5].height = ResizeOffset;

        //Top-right resizeRect
        resizeRects[6].x = brt.position.x + brt.sizeDelta.x - ResizeOffset;
        resizeRects[6].y = brt.position.y + brt.sizeDelta.y - ResizeOffset;
        resizeRects[6].width = ResizeOffset;
        resizeRects[6].height = ResizeOffset;

        //Bottom-right resizeRect
        resizeRects[7].x = brt.position.x + brt.sizeDelta.x - ResizeOffset;
        resizeRects[7].y = brt.position.y;
        resizeRects[7].width = ResizeOffset;
        resizeRects[7].height = ResizeOffset;

        //Reassign RenderTexture to get correct size
        if(objectCamera.targetTexture != null)
        {
            renderTexture = new RenderTexture(Mathf.FloorToInt(ort.sizeDelta.x), Mathf.FloorToInt(ort.sizeDelta.y), renderTexture.depth);
            objectCamera.targetTexture.Release();
            objectCamera.targetTexture = renderTexture;
            objectImageRaw.texture = renderTexture;
        }
    }
}
