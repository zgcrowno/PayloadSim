using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionsUtil {

	public static void Swap<T>(IList<T> list, int indexA, int indexB)
    {
        T tmp = list[indexA];
        list[indexA] = list[indexB];
        list[indexB] = tmp;
    }
}
