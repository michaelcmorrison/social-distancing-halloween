using System;
using System.Collections.Generic;

[Serializable]
public class HitChain
{
    public List<Kid> kids;

    public HitChain(Kid firstKid)
    {
        kids = new List<Kid> {firstKid};
    }

    public HitChain(List<Kid> kids, Kid kidToAdd)
    {
        this.kids = new List<Kid>(kids) {kidToAdd};
    }
}