using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Names
{
    public string Name;
    public bool IsAlreadyUses;

    public Names(string name, bool isAlreadyUses)
    {
        this.Name = name;
        this.IsAlreadyUses = isAlreadyUses;
    }
}
