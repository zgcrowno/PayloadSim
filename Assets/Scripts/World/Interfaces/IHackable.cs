using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHackable {

    FirewallLevel firewallLevel
    {
        get;
        set;
    }
}

public enum FirewallLevel
{
    None,
    One,
    Two,
    Three
}
