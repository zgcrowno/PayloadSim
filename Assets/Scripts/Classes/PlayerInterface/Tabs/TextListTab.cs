using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextListTab : SubTab {

    public GameObject collapsibleListScrollable;
    public GameObject viewport;
    public GameObject viewportContent;

    // Use this for initialization
    new public void Start()
    {
        base.Start();
        collapsibleListScrollable = Instantiate(Resources.Load("Prefabs/CollapsibleListScrollablePrefab") as GameObject);
        collapsibleListScrollable.transform.SetParent(contentBody.transform);
        viewport = collapsibleListScrollable.transform.Find("Viewport").gameObject;
        viewportContent = viewport.transform.Find("ViewportContent").gameObject;
        SetUp(new Vector2(superTab.brt.anchoredPosition.x, superTab.brt.anchoredPosition.y), new Vector2(superTab.brt.sizeDelta.x, superTab.brt.sizeDelta.y));
    }

    // Update is called once per frame
    new public void Update()
    {

    }
}
