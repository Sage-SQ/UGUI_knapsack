using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//public class DragMove : MonoBehaviour,IDragHandler,IPointerDownHandler,IPointerUpHandler {

//    GameObject PoolCanvas = null;
//    GameObject Instead_gameObject = null;

//    void Start()
//    {
//        PoolCanvas = GameObject.Find("Knapsack").transform.Find("PoolCanvas").gameObject;
//    }

//    public void OnDrag(PointerEventData eventData)
//    { //拖拽
//        if (Input.GetMouseButton(0))
//        {
//            GetComponent<RectTransform>().pivot.Set(0, 0);
//            transform.position = Input.mousePosition;
//        }
//    }
//    public void OnPointerDown(PointerEventData eventData)
//    { //鼠标按下
//        if (Input.GetMouseButtonDown(0))
//        {
//            transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
//            Instead_gameObject = transform.parent.gameObject;
//            transform.SetParent(PoolCanvas.transform, true);
//            transform.GetComponent<CanvasGroup>().blocksRaycasts = false;
//        }
//    }
//    public void OnPointerUp(PointerEventData eventData)
//    { //鼠标放开
//        if (Input.GetMouseButtonUp(0))
//        {
//            transform.localScale = new Vector3(1f, 1f, 1f);

//            //print(eventData.pointerCurrentRaycast.gameObject);

//            if (eventData.pointerCurrentRaycast.gameObject != null && eventData.pointerCurrentRaycast.gameObject.tag == "UCell")
//            {
//                transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform);
//            }
//            else if (eventData.pointerCurrentRaycast.gameObject != null && eventData.pointerCurrentRaycast.gameObject.tag == "UItem")
//            {//交换
//                Transform transform_A = eventData.pointerCurrentRaycast.gameObject.transform;
//                Transform transform_B_parent = transform_A.parent.transform;
//                transform_A.SetParent(Instead_gameObject.transform);
//                transform_A.localPosition = Vector3.zero;
//                transform.SetParent(transform_B_parent);
//            }
//            else
//            {
//                transform.SetParent(Instead_gameObject.transform);//放回原位
//            }
//            transform.GetComponent<CanvasGroup>().blocksRaycasts = true;
//            transform.localPosition = Vector3.zero;
//        }
//    }
//}
public class DragMove : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{

    GameObject PoolCanvas = null;
    GameObject Instead_gameObject = null;
    BaseItem item;
    BaseItem itemInstead;

    void Start()
    {
        PoolCanvas = GameObject.Find("Knapsack").transform.Find("PoolCanvas").gameObject;
    }

    public void OnDrag(PointerEventData eventData)
    { //拖拽
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            GetComponent<RectTransform>().pivot.Set(0, 0);
            transform.position = Input.mousePosition;
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    { 
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            item = StoreItem.GetItem(transform.parent.name);
            //删除背包格名称（因为背包格名称不同，但是物品名称相同）
            StoreItem.DeleteItem(transform.parent.name);
            //print(transform.parent.name);

            transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
            Instead_gameObject = transform.parent.gameObject;
            transform.SetParent(PoolCanvas.transform, true);
            transform.GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);

            //print(eventData.pointerCurrentRaycast.gameObject);

            if (eventData.pointerCurrentRaycast.gameObject != null && eventData.pointerCurrentRaycast.gameObject.tag == "UCell")
            {
                transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform);

                //存储背包格名称（因为背包格名称不同，但是物品名称相同）
                StoreItem.Store(eventData.pointerCurrentRaycast.gameObject.transform.name, item);
                //print(eventData.pointerCurrentRaycast.gameObject.transform.name);

            }
            else if (eventData.pointerCurrentRaycast.gameObject != null && eventData.pointerCurrentRaycast.gameObject.tag == "UItem")
            {//交换//提示，先删再存，再存
                print("test");
                Transform transform_A = eventData.pointerCurrentRaycast.gameObject.transform;
                Transform transform_B_parent = transform_A.parent.transform;

                itemInstead = StoreItem.GetItem(eventData.pointerCurrentRaycast.gameObject.transform.parent.name);
                //删除背包格名称（因为背包格名称不同，但是物品名称相同）
                StoreItem.DeleteItem(eventData.pointerCurrentRaycast.gameObject.transform.parent.name);

                transform_A.SetParent(Instead_gameObject.transform);
                transform_A.localPosition = Vector3.zero;

                StoreItem.Store(Instead_gameObject.transform.name, itemInstead);

                transform.SetParent(transform_B_parent);

                //存储背包格名称（因为背包格名称不同，但是物品名称相同）
                StoreItem.Store(transform_B_parent.name, item);
                //print(transform_B_parent.name);

            }
            else
            {
                transform.SetParent(Instead_gameObject.transform);//放回原位
                //存储背包格名称（因为背包格名称不同，但是物品名称相同）
                StoreItem.Store(Instead_gameObject.transform.name, item);
            }
            transform.GetComponent<CanvasGroup>().blocksRaycasts = true;
            transform.localPosition = Vector3.zero;
        }
    }
}
