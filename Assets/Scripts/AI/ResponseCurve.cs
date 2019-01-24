using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class ResponseCurve {

    public int maxIndex = 0;
    public int startingBucketIndex = 0;

    public List<Bucket> buckets;

    public ResponseCurve()
    {
        buckets = new List<Bucket>();
    }

    public ResponseCurve(List<Bucket> buckets)
    {
        this.buckets = buckets;
        foreach(Bucket bucket in buckets)
        {
            maxIndex += bucket.size;
        }
        Sort();
        SetStartingBucketIndex();
    }

    /*
     * Adds a new Bucket object to buckets, necessarily modifying maxIndex and startingBucketIndex along the way
     * @param bucket The Bucket object to be added
     */ 
    public void Add(Bucket bucket)
    {
        maxIndex += bucket.size;

        buckets.Add(bucket);
        Sort();
        SetStartingBucketIndex();
    }

    /*
     * Returns a pseudo-random ActionType associated with one of the Bucket objects in buckets
     * @return The ActionType associated with the pseudo-randomly chosen Bucket object in buckets
     */ 
    public BehaviorType GetBehaviorType()
    {
        int index = Random.Range(0, maxIndex);
        return buckets[GetBucketIndexByValue(index, true)].behaviorType;
    }

    /*
     * Returns the index of the Bucket whose right edge is greater than the passed value, and whose left edge is less than the passed value
     * @param value The value by which we're searching for a Bucket object
     * @param optimized Represents whether or not iGuess is set to startingBucketIndex or iLow + ((iHigh - iLow) / 2). This variable is set to false when we wish to set startingBucketIndex, and true when we wish to use startingBucketIndex
     * @return The index of the Bucket whose right edge is greater than the passed value, and whose left edge is less than the passed value
     */
    public int GetBucketIndexByValue(int value, bool optimized)
    {
        int iHigh = buckets.Count - 1;
        int iLow = 0;
        int iGuess;
        bool found = false;
        bool usedStartingBucketIndex = false; //Once we've used startingBucketIndex in our binary search, we don't wish to use it again

        while (!found)
        {
            //In the event that iLow == buckets.Count - 2, set nonOptimizedGuess to buckets.Count - 1 because C#'s always rounds ints down; this is the only way to make [buckets.Count - 1] a retrievable index
            int nonOptimizedGuess = iLow == buckets.Count - 2 ? buckets.Count - 1 : iLow + ((iHigh - iLow) / 2);

            if(optimized && !usedStartingBucketIndex)
            {
                iGuess = startingBucketIndex;
                usedStartingBucketIndex = true;
            }
            else
            {
                iGuess = nonOptimizedGuess;
            }

            if (InBucket(iGuess, value))
            {
                return iGuess;
            }

            if (value > buckets[iGuess].edge)
            {
                iLow = iGuess;
            }
            else
            {
                iHigh = iGuess;
            }
        }

        return 0; //Returning a default value of 0
    }

    /*
     * Sets startingBucketIndex to the appropriate value, given buckets' individual buckets' varying widths/sizes. This method is used to optimize searches.
     */ 
    public void SetStartingBucketIndex()
    {
        startingBucketIndex = GetBucketIndexByValue(buckets[buckets.Count - 1].edge / 2, false);
    }

    /*
     * Returns whether or not index is contained within the bucket whose index within buckets is i
     * @return A bool representing whether or not index is contained within the bucket whose index within buckets is i
     */
    public bool InBucket(int i, int index)
    {
        if(i == 0 && index <= buckets[i].edge)
        {
            return true;
        }

        if(index <= buckets[i].edge && index > buckets[i - 1].edge)
        {
            return true;
        } else
        {
            return false;
        }
    }

    /*
     * Recalculates the edges of every bucket within buckets from index iStartBucket up
     * @param iStartBucket The Bucket from which we start recalculating edges
     */ 
    public void RebuildEdges(int iStartBucket)
    {
        Sort();
        for (int i = iStartBucket; i < buckets.Count; i++)
        {
            if(i > 0)
            {
                buckets[i].edge = buckets[i - 1].edge + buckets[i].size;
            } else
            {
                buckets[i].edge = buckets[i].size;
            }
        }
        maxIndex = buckets[buckets.Count - 1].edge;
        SetStartingBucketIndex();
    }

    /*
     * Sorts this ResponseCurve's buckets in ascending order of their widths
     */ 
    public void Sort()
    {
        buckets = buckets.OrderBy(b => b.size).ToList();
    }

    /*
     * Calculates the utility (size) for every Bucket in buckets for the passed NPC, and subsequently modifies itself accordingly
     * @param npc The passed NPC for whom we're calculating various utilities
     */ 
    public void CalculateUtility(NPC npc)
    {
        foreach(Bucket bucket in buckets)
        {
            bucket.CalculateUtility(npc);
        }
        
        RebuildEdges(0);
    }
}
