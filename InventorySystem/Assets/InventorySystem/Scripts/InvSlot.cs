using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InvSlot : MonoBehaviour, IDropHandler
{
    public GameObject selector;

    private void Awake()
    {
        Deselect();
    }

    public void Select()
    {
        selector.SetActive(true);
    }

    public void Deselect()
    {
        selector.SetActive(false);
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 1)
        {
            InvItem inventoryItem = eventData.pointerDrag.GetComponent<InvItem>();
            inventoryItem.parentAfterDrag = transform;
        }
    }
}
