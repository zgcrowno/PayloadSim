using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionsUtil {

    /*
     * Swaps two elements in a collection (in terms of their indeces)
     * @param list The collection in which the swap is to take place
     * @param indexA The index of the first item to be swapped
     * @param indexB The index of the second item to be swapped
     */
    public static void Swap<T>(IList<T> list, int indexA, int indexB)
    {
        T tmp = list[indexA];
        list[indexA] = list[indexB];
        list[indexB] = tmp;
    }
}
