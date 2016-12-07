using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StoreItem {

    private static Dictionary<string, BaseItem> GridItem = new Dictionary<string, BaseItem>();

    public static void Store(string name,BaseItem item)
    {
        if (GridItem.ContainsKey(name))
            return;
        GridItem.Add(name, item);
    }

    public static void DeleteItem(string name)
    {
        if (GridItem.ContainsKey(name))
            GridItem.Remove(name);
    }

    public static BaseItem GetItem(string name)
    {
        if (GridItem.ContainsKey(name))
        {
            return GridItem[name];
        }
        else
            return null;
    }
}
