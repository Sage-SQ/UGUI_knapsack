using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShowInfo : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler{

	Text ItemInfo;
	// Use this for initialization
	void Start () {
		ItemInfo = GameObject.Find("Bg").transform.Find("Info").gameObject.GetComponent<Text>();
	}

    public void OnPointerEnter(PointerEventData eventData)
    {
        //print("testtest1");
        //print(eventData.pointerCurrentRaycast.gameObject.name);
        //print(eventData.pointerEnter.transform.parent.name);
        //transform.GetComponent<CanvasGroup>().blocksRaycasts = false;
        BaseItem item = StoreItem.GetItem(eventData.pointerEnter.transform.parent.name);
		ItemInfo.text = item.Description;
        //print(item.Name);
        //print(item.Description);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        //print("testtest2");
    }
}
