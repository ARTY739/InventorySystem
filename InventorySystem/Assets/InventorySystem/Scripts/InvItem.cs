using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InvItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    

    [Header("UI")]
    public Image image;
    public Text countText;

    [SerializeField] UnityEvent OnUse;

    [HideInInspector] public Transform parentAfterDrag;
    [HideInInspector] public InventoryItem item;
    [HideInInspector] public int count = 1;

    private RectTransform rectTransform;
    private Canvas mainCanvas;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        mainCanvas = GetComponentInParent<Canvas>();
    }

    public void InitialiseItem(InventoryItem newItem)
    {
        item = newItem;
        image.sprite = newItem.image;
        RefreshCount();
    }

    public void RefreshCount()
    {
        countText.text = count.ToString();
        bool textActive = count > 1;
        countText.gameObject.SetActive(textActive);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
    }

    public void OnDrag(PointerEventData eventData)
    {
        //transform.position = Input.mousePosition;
        rectTransform.anchoredPosition += eventData.delta / mainCanvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;
        transform.SetParent(parentAfterDrag);
        transform.localPosition = Vector3.zero;
    }

    public void Use(bool use)
    {
        if (use)
        {
            if (count > 1)
            {
                count--;
                RefreshCount();
                item.Use();
            }
            else
            {
                item.Use();
                if (!item.infinityUse)
                    Destroy(gameObject);
            }
        }
        else
        {
            if (count > 1)
            {
                count--;
                RefreshCount();
                //GameObject droppedItem = Instantiate(item.dropedPref, GameObject.FindGameObjectWithTag("DropPoint").transform);
                //droppedItem.transform.localPosition = Vector3.zero;
                //droppedItem.transform.SetParent(null);
            }
            else
            {
                //GameObject droppedItem = Instantiate(item.dropedPref, GameObject.FindGameObjectWithTag("DropPoint").transform);
                //droppedItem.transform.localPosition = Vector3.zero;
                //droppedItem.transform.SetParent(null);
                Destroy(gameObject);
            }
        }
    }
}
