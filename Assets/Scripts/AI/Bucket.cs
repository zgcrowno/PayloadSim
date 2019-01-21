using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket {

    public ActionType actionType; //The possible action this bucket represents

    public int width; //The width of this bucket, which has purpose only when compared to the widths of its overarching data structure's other buckets
    public int edge; //The right edge of this bucket, with the left edge being retrievable only by consulting the overarching data structure

    public Bucket()
    {
        actionType = ActionType.Work;
        width = 0;
        edge = 0;
    }

    public Bucket(ActionType actionType, int width, int edge)
    {
        this.actionType = actionType;
        this.width = width;
        this.edge = edge;
    }
}
