using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatUtil {

    //Returns whether or not f1 is roughly equal to f2
	public static bool Equals(float f1, float f2)
    {
        return Mathf.Approximately(f1, f2);
    }

    //Returns whether or not f1 is greater than f2 without being roughly equal to it
    public static bool GT(float f1, float f2)
    {
        return !Equals(f1, f2) && f1 > f2;
    }

    //Returns whether or not f2 is less than f2 without being roughly equal to it
    public static bool LT(float f1, float f2)
    {
        return !Equals(f1, f2) && f1 < f2;
    }

    //Returns whether or not f1 is roughly equal to f2 OR f1 is greater than f2
    public static bool GTE(float f1, float f2)
    {
        return Equals(f1, f2) || f1 > f2;
    }

    //Returns whether or not f1 is roughly equal to f2 OR f1 is less than f2
    public static bool LTE(float f1, float f2)
    {
        return Equals(f1, f2) || f1 < f2;
    }
}
