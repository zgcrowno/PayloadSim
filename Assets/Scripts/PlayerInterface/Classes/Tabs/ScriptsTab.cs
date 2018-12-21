using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScriptsTab : TextListTab {

    public const string HeaderText = "SCRIPTS";
    
	new public void Start() {
        base.Start();
        headerText.text = HeaderText;

        for (int i = 0; i < 10; i++)
        {
            GameObject collapsibleListItem = Instantiate(Resources.Load("Prefabs/PlayerInterface/CollapsibleListItemPrefab") as GameObject);
            collapsibleListItem.transform.SetParent(viewportContent.transform);
            GameObject collapsibleListItemHeader = collapsibleListItem.transform.Find("Header").gameObject;
            GameObject collapsibleListItemContent = collapsibleListItem.transform.Find("ItemContent").gameObject;
            TextMeshProUGUI headerText = collapsibleListItemHeader.transform.Find("TextMeshPro Text").GetComponent<TextMeshProUGUI>();
            headerText.text = "Header plus some more text in order to test this header's text wrapping capabilities" + i;
            if (i < 9)
            {
                GameObject contentTextObject = Instantiate(Resources.Load("Prefabs/PlayerInterface/TextPrefab") as GameObject);
                contentTextObject.transform.SetParent(collapsibleListItemContent.transform);
                TextMeshProUGUI contentText = contentTextObject.GetComponent<TextMeshProUGUI>();
                contentText.text = "Some nested text for testing purposes that needs to be a certain length in order to test this HelpTab's text wrapping capability" + i;
                contentText.fontSize = 16;
            }
            else
            {
                GameObject contentListObject = Instantiate(Resources.Load("Prefabs/PlayerInterface/CollapsibleListPrefab") as GameObject);
                contentListObject.transform.SetParent(collapsibleListItemContent.transform);
                GameObject contentListContent = contentListObject.transform.Find("Content").gameObject;
                GameObject newCollapsibleListItem = Instantiate(Resources.Load("Prefabs/PlayerInterface/CollapsibleListItemPrefab") as GameObject);
                newCollapsibleListItem.transform.SetParent(contentListContent.transform);
                GameObject newCollapsibleListItemHeader = newCollapsibleListItem.transform.Find("Header").gameObject;
                GameObject newCollapsibleListItemContent = newCollapsibleListItem.transform.Find("ItemContent").gameObject;
                TextMeshProUGUI newHeaderText = newCollapsibleListItemHeader.transform.Find("TextMeshPro Text").GetComponent<TextMeshProUGUI>();
                newHeaderText.text = "Header plus some more text in order to test this header's text wrapping capabilities" + i;
                GameObject newContentTextObject = Instantiate(Resources.Load("Prefabs/PlayerInterface/TextPrefab") as GameObject);
                newContentTextObject.transform.SetParent(newCollapsibleListItemContent.transform);
                TextMeshProUGUI newContentText = newContentTextObject.GetComponent<TextMeshProUGUI>();
                newContentText.text = "Some nested text for testing purposes that needs to be a certain length in order to test this HelpTab's text wrapping capability" + i;
                newContentText.fontSize = 16;
            }
        }
    }
}
