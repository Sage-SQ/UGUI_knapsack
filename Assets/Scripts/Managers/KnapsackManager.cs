using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class KnapsackManager : MonoBehaviour {

    private static KnapsackManager _instance;

    public static KnapsackManager Instance
    {
        get
        {
            return _instance;
        }
    }

    GameObject item;
    public GameObject[] cells;
    public static Dictionary<int, BaseItem> ItemList;
    public static Dictionary<int, BaseItem> ItemStore;
    Image Imagesingle;

    Text index;//物品右下角数量
    int IndexInt = 0;
    string IndexStr = "";

    public GameObject PoolCanvas;

    void Awake()
    {
        //单例模式
        _instance = this;
        //加载数据
        Load();
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.X))
        {
            int index = Random.Range(0, 5);
            Pickup(ItemList[index]);
        }
	}

    private void Load()
    {
        ItemList = new Dictionary<int, BaseItem>();

        Weapons sword_blade = new Weapons(0, "刃剑", "锋利的剑", 20, 10, "Pictures/blade_sword", 100);
        Weapons sword_blood = new Weapons(1, "嗜血剑", "杀戮之剑", 30, 15, "Pictures/blood_sword", 300);
        Weapons sword_double = new Weapons(2, "双手剑", "多为女子用剑", 18, 9, "Pictures/double_sword", 80);

        Consumables HP_back = new Consumables(3, "培元术", "回复气血", 10, 5, "Pictures/add_HP", 20, 0);
        Consumables MP_back = new Consumables(4, "无忧仙丹", "用于回复能量", 10, 5, "Pictures/add_MP", 0, 15);

        ItemList.Add(sword_blade.ID, sword_blade);
        ItemList.Add(sword_blood.ID, sword_blood);
        ItemList.Add(sword_double.ID, sword_double);
        ItemList.Add(HP_back.ID, HP_back);
        ItemList.Add(MP_back.ID, MP_back);
    }

    public void Pickup(BaseItem baseItem)
    {
        bool isFind = false;
        //item = Instantiate(Resources.Load("prefabs/UItem"), transform.position, transform.rotation) as GameObject;//获取预制物体
        item = ObjectPool.Get("UItem", transform.position, transform.rotation) as GameObject;
        Imagesingle = item.transform.GetComponent<Image>();									   //获取预制物体的image组件
        Imagesingle.overrideSprite = Resources.Load(baseItem.Icon, typeof(Sprite)) as Sprite;	   //动态加载image组件的sprite

        for (int i = 0; i < cells.Length; i++)
        {
            if (cells[i].transform.childCount > 0)//判断当前格子是否有物体
            {
                //如果有则判断是否相同
                //判断的是加载图片的名称
                if (Imagesingle.overrideSprite.name == cells[i].transform.GetChild(0).transform.GetComponent<Image>().overrideSprite.name)
                {
                    isFind = true;
                    index = cells[i].transform.GetChild(0).transform.GetChild(0).GetComponent<Text>();
                    IndexInt = int.Parse(index.text);
                    IndexInt += 1;
                    IndexStr = IndexInt.ToString();
                    index.text = IndexStr;

                    item.transform.SetParent(PoolCanvas.transform, true);
                    //Destroy(item);
                    StartCoroutine(ReturnToPool());
                }
            }
        }
        if (isFind == false)
        {
            for (int i = 0; i < cells.Length; i++)
            {
                if (cells[i].transform.childCount == 0)
                {
                    //检测没有物体
                    item.transform.SetParent(cells[i].transform);
                    item.transform.localPosition = Vector3.zero;

                    //存储背包格名称（因为背包格名称不同，但是物品名称相同）
                    StoreItem.Store(cells[i].transform.name, baseItem);
                    //print(cells[i].transform.name);
                    break;
                }
            }
        }
    }

    IEnumerator ReturnToPool()
    {
        yield return new WaitForSeconds(0f);
        ObjectPool.Return(item);
    }
}
