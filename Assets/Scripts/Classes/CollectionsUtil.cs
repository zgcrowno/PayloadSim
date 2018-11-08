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

    public static void SortSubTabsByDepth(List<SubTab> list)
    {
        SubTab temp;
        int j;

        for (int i = 1; i <= list.Count - 1; i++)
        {
            temp = list[i];
            j = i - 1;
            while (j >= 0 && list[j].depth > temp.depth)
            {
                list[j + 1] = list[j];
                j--;
            }
            list[j + 1] = temp;
        }
    }
}
