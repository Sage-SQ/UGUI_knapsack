using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ConsumeManager : MonoBehaviour,IPointerDownHandler {

    public GameObject PoolCanvas;

    GameObject item;
    Text index;  //Item数量
    int IndexInt = 0;  //数量转换为INT型
    string IndexStr = "";  //数量转换为STRING型

	// Use this for initialization
	void Start () {
        PoolCanvas = GameObject.Find("Knapsack").transform.Find("PoolCanvas").gameObject;
	}

    public void OnPointerDown(PointerEventData eventData)
    { //鼠标按下
        if (Input.GetMouseButtonDown(1))
        {
            //print("test");
            //transform.localScale=new Vector3(0.7f,0.7f,0.7f);
            index = transform.GetChild(0).GetComponent<Text>();
            IndexInt = int.Parse(index.text);
            if (IndexInt != 1)
            {
                IndexInt -= 1;
                IndexStr = IndexInt.ToString();
                index.text = IndexStr;
            }
            else
            {
                item = eventData.pointerCurrentRaycast.gameObject;

                //删除背包格名称（因为背包格名称不同，但是物品名称相同）
                StoreItem.DeleteItem(item.transform.parent.name);
                //print(item.transform.parent.name);

                //Destroy(item);
                item.transform.SetParent(PoolCanvas.transform, true);
                StartCoroutine(ReturnToPool());
            }
        }
    }

    IEnumerator ReturnToPool()
    {
        yield return new WaitForSeconds(0f);
        ObjectPool.Return(item);
    }

	// Update is called once per frame
	void Update () {
	
	}
}
