using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IContainer {

    int capacity //The amount (in trivial units) this Container can hold
    {
        get;
        set;
    }

    List<Consumable> contents //The amount (in trivial units) that this Container is currently holding
    {
        get;
        set;
    }

    bool wasteFriendly //Whether or not this Container is a viable holder of human waste (and thus usable as a toilet)
    {
        get;
        set;
    }
}
