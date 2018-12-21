using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectTransformUtil {

    /*
     * Returns a Rect interpretation of the passed RectTransform in screen coordinates
     * @param rt The passed RectTransform
     * @return The Rect interpretation of the passed RectTransform in screen coordinates
     */ 
	public static Rect ScreenRect(RectTransform rt)
    {
        return new Rect
        {
            x = rt.position.x,
            y = rt.position.y,
            width = rt.sizeDelta.x,
            height = rt.sizeDelta.y
        };
    }
}
