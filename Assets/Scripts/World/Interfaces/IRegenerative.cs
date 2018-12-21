using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRegenerative {

    float regenRate //The rate at which this implementation improves its interactor's energy, relaxation and/or hygiene
    {
        get;
        set;
    } 

    /*
     * Regenerates field(s) such as energy, relaxation and hygiene associated with the passed NPC
     * @param npc The passed NPC
     */ 
    void Regen(NPC npc);
}
