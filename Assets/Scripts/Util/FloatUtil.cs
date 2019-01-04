using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatUtil {
    
    /*
     * Returns whether or not f1 is roughly equal to f2
     * @param f1 The first float for comparison
     * @param f2 The second float for comparison
     * @return A bool representing whether or not f1 is roughly equal to f2
     */
    public static bool Equals(float f1, float f2)
    {
        return Mathf.Approximately(f1, f2);
    }

    /*
     * Returns whether or not f1 is greater than f2 without being roughly equal to it
     * @param f1 The first float for comparison
     * @param f2 The second float for comparison
     * @return A bool representing whether or not f1 is greater than f2 without being roughly equal to it
     */
    public static bool GT(float f1, float f2)
    {
        return !Equals(f1, f2) && f1 > f2;
    }
    
    /*
     * Returns whether or not f1 is less than f2 without being roughly equal to it
     * @param f1 The first float for comparison
     * @param f2 The second float for comparison
     * @return A bool representing whether or not f1 is less than f2 without being roughly equal to it
     */
    public static bool LT(float f1, float f2)
    {
        return !Equals(f1, f2) && f1 < f2;
    }
    
    /*
     * Returns whether or not f1 is roughly equal to f2 OR f1 is greater than f2
     * @param f1 The first float for comparison
     * @param f2 The second float for comparison
     * @return A bool representing whether or not f1 is roughly equal to f2 OR f1 is greater than f2
     */
    public static bool GTE(float f1, float f2)
    {
        return Equals(f1, f2) || f1 > f2;
    }

    /*
     * Returns whether or not f1 is roughly equal to f2 OR f1 is less than f2
     * @param f1 The first float for comparison
     * @param f2 The second float for comparison
     * @return A bool representing whether or not f1 is roughly equal to f2 OR f1 is less than f2
     */
    public static bool LTE(float f1, float f2)
    {
        return Equals(f1, f2) || f1 < f2;
    }

    /*
     * Returns an int representing the percentage f1 is of f2
     * @param f1 The float whose percentage of f2 is being determined
     * @param f2 The float of whom f1's percentage is being determined
     * @return An int representing the percentage f1 is of f2
     */ 
    public static int AsPercent(float f1, float f2)
    {
        return Mathf.CeilToInt((f1 * 100) / f2);
    }

    /*
     * Returns a string representation of the percentage f1 is of f2, complete with percent sign
     * @param f1 The float whose percentage of f2 is being determined
     * @param f2 The float of whom f1's percentage is being determined
     * @return A string representing the percentage f1 is of f2
     */
    public static string AsPercentString(float f1, float f2)
    {
        return AsPercent(f1, f2) + "%";
    }

    /*
     * Returns a string representation of the passed float, complete with degree symbol
     * @param f The float we're to represent as a degree value
     * @return A string representing the passed float with a degree symbol appended
     */
     public static string AsDegreeString(float f)
    {
        return f + "\u00B0";
    }
}
