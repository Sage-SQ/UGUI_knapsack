using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour
{

    private static Dictionary<string, ArrayList> pool = new Dictionary<string, ArrayList> { };

    // Use this for initialization
    void Start()
    {

    }

    public static Object Get(string prefabName, Vector3 position, Quaternion rotation)
    {
        string key = prefabName + "(Clone)";
        Object o;
        if (pool.ContainsKey(key) && pool[key].Count > 0)
        {
            ArrayList list = pool[key];
            o = list[0] as Object;
            list.RemoveAt(0);
            if (o == null)
            {
                o = Instantiate(Resources.Load("Prefabs/" + prefabName), position, rotation);
            }
            //初始化相关状态
            (o as GameObject).SetActive(true);
            (o as GameObject).transform.position = position;
            (o as GameObject).transform.rotation = rotation;
        }
        else
        {
            o = Instantiate(Resources.Load("prefabs/" + prefabName), position, rotation);
        }

        return o;
    }

    public static Object Return(GameObject o)
    {
        string key = o.name;

        if (pool.ContainsKey(key))
        {
            ArrayList list = pool[key];
            list.Add(o);
        }
        else
        {
            pool[key] = new ArrayList() { o };
        }

        o.SetActive(false);
        return o;
    }

}
