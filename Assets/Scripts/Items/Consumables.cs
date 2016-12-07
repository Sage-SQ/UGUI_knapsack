using UnityEngine;
using System.Collections;

public class Consumables : BaseItem {

    public int BackHP { get; private set; }
    public int BackMP { get; private set; }

    public Consumables(int id, string name, string description, int buyPrice, int sellPrice, string icon, int backHP, int backMP)
        : base(id, name, description, buyPrice, sellPrice, icon)
    {
        this.BackHP = backHP;
        this.BackMP = backMP;
    }
}
