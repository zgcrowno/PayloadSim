using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathUtil {

    /*
     * Returns the y value for the passed x, slope, and yIntercept values
     * @param x The passed x value
     * @param m The passed slope value
     * @param b The passed yIntercept value
     * @return The y value for the passed x, slope, and yIntercept values
     */
    public static float Linear(float x, float slope, float yIntercept)
    {
        return slope * x + yIntercept;
    }

    /*
     * Returns the y value for the passed x, exponent, verticalShift, horizontalShift, and tilt values
     * @param x The passed x value
     * @param exponent The power to which (x - horizontalShift) is raised
     * @param verticalShift The number of units which the resulting parabola of this equation is shifted up or down
     * @param horizontalShift The number of units which the resulting parabola of this equation is shifted left or right
     * @param tilt The amount by which the resulting parabola of this equation is tilted left or right
     * @param leftOfVertexPositive This bool determines whether or not we always want values to the left of the resulting parabola's vertex to be positive
     * @return The y value for the passed x, exponent, verticalShift, horizontalShift, and tilt values
     * NOTES: If exponent is odd, the portion of the parabola to the left of the vertex will curve downward instead of upward if...
     * ...leftOfVertexPositive is false; as exponent increases, the bowl of the resulting parabola narrows (unless exponent is between 0 and...
     * ...1, in which case as exponent decreases, the bowl of the resulting parabola narrows); If exponent is between 0 and 1, the resulting...
     * ...parabola's axis of symmetry is parallel to the x-axis rather than the y-axis
     */
    public static float Quadratic(float x, float exponent, float verticalShift, float horizontalShift, float tilt, bool leftOfVertexPositive)
    {
        float regularReturn = Mathf.Pow(x - horizontalShift, exponent) + verticalShift + (tilt * x);

        if(leftOfVertexPositive)
        {
            return Mathf.Abs(regularReturn);
        } else
        {
            return regularReturn;
        }
    }

    /*
     * Returns the y value for the passed x, logBase, horizontalShift, and flip values
     * @param x The passed x value
     * @param logBase The logarithm base used in the equation, commonly e (the natural logarithm)
     * @param horizontalShift The number of units which the resulting s-shaped graph of this equation is shifted left or right
     * @param flip This bool represents whether or not we wish to flip this curve upside down
     * @return The y value for the passed x, logBase, horizontalShift, and flip values
     * NOTES: Generally, e (the natural logarithm) is used as the value of logBase; The tails of the logistic curve asymptotically approach 0...
     * ...and 1, and when using the value of e (the natural logarithm), there really isn't much need to calculate the logistic curve outside...
     * ...the range of [-10...10]; as logBase increases in value, the shape of the logistic function narrows; logistic functions are good at...
     * ...representing decreasing marginal utility; because the line extends infinitely in both directions, using the point where y crosses...
     * the 0.5 mark is our best landmark
     */
    public static float Logistic(float x, float logBase, float horizontalShift, bool flip)
    {
        float unflippedReturn = (1 / (1 + Mathf.Pow(logBase, -x + horizontalShift)));

        if (flip)
        {
            return 1 - unflippedReturn;
        } else
        {
            return unflippedReturn;
        }
    }

    /*
     * Returns the y value for the passed x, logBase and shiftUp values
     * @param x The passed x value
     * @param logBase The logarithm base used in the equation, commonly e (the natural logarithm)
     * @param shiftUp This bool represents whether or not we wish to shift this curve up by five points, effectively changing its range from [-5...5] to [0...10]
     * @return The y value for the passed x, logBase and shiftUp values
     * NOTES: A logit curve is essentially a logistic curve rotated 90 degrees; generally, e (the natural logarithm) is used as the value of...
     * ...logBase; as logBase increases in value, the logit function's s-curve flattens horizontally; logit functions are good at representing...
     * ...increasing marginal utility
     */
    public static float Logit(float x, float logBase, bool shiftUp)
    {
        float unshiftedReturn = Mathf.Log(x / (1 - x), logBase);

        if(shiftUp)
        {
            return unshiftedReturn + 5;
        } else
        {
            return unshiftedReturn;
        }
    }
}
