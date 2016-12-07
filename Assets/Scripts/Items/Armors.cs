using UnityEngine;
using System.Collections;

public class Armors : BaseItem {

    public int Defense { get; private set; }

    public Armors(int id, string name, string description, int buyPrice, int sellPrice, string icon,int defense)
        : base(id, name, description, buyPrice, sellPrice, icon)
    {
        this.Defense = defense;
    }
}
