using UnityEngine;
using System.Collections;

public class Weapons : BaseItem {

    public int Attack { get; private set; }

    public Weapons(int id, string name, string description, int buyPrice, int sellPrice, string icon,int attack)
        : base(id, name, description, buyPrice, sellPrice, icon)
    {
        this.Attack = attack;
    }

}
