using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public InvSlot[] inventorySlots;
    public GameObject inventoryItemPref;

    [HideInInspector]
    public List<InvItem> inventoryItems;

    public float dropForce;

    public int selectedSlot = 0;

    private void Update()
    {
        if (Input.inputString != null)
        {
            bool isNumber = int.TryParse(Input.inputString, out int number);
            if (isNumber && number > 0 && number < 10)
            {
                ChangeSelectedSlot(number - 1);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            ChangeSelectedSlot(9);
        }
    }

    public void ChangeSelectedSlot(int newValue)
    {
        RefrashInventory();
        if (selectedSlot >= 0)
            inventorySlots[selectedSlot].Deselect();

        inventorySlots[newValue].Select();
        selectedSlot = newValue;
    }

    public void ChangeSelectedSlot(bool newValue)
    {
        RefrashInventory();
        if (newValue)
        {
            inventorySlots[selectedSlot].Deselect();
            selectedSlot = -1;
        }
    }

    public bool AddItem(InventoryItem item)
    {
        RefrashInventory();
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InvSlot slot = inventorySlots[i];
            InvItem itemInSlot = slot.GetComponentInChildren<InvItem>();
            if (itemInSlot != null && itemInSlot.item == item && 
                itemInSlot.count < item.maxCountInSlot && itemInSlot.item.stackable)
            {
                itemInSlot.count++;
                itemInSlot.RefreshCount();
                return true;
            }
        }

        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InvSlot slot = inventorySlots[i];
            InvItem itemInSlot = slot.GetComponentInChildren<InvItem>();
            if (itemInSlot == null)
            {
                SpawnNewItem(item, slot);
                return true;
            }
        }
        return false;
    }

    private void SpawnNewItem(InventoryItem item, InvSlot slot)
    {
        GameObject newItemGo = Instantiate(inventoryItemPref, slot.transform);
        InvItem inventoryItem = newItemGo.GetComponent<InvItem>();
        inventoryItem.InitialiseItem(item);
        RefrashInventory();
    }


    public InventoryItem GetSelectedItem()
    {
        RefrashInventory();
        InvSlot slot = inventorySlots[selectedSlot];
        InvItem itemInSlot = slot.GetComponentInChildren<InvItem>();
        if (itemInSlot != null)
        {
            InventoryItem item =  itemInSlot.item;
            return item;
        }
        return null;
    }

    public InventoryItem UseSelectedItem(bool use)
    {
        if (selectedSlot < 0)
            return null;

        InvSlot slot = inventorySlots[selectedSlot];
        InvItem itemInSlot = slot.GetComponentInChildren<InvItem>();
        if (itemInSlot != null)
        {
            InventoryItem item = itemInSlot.item;
            if (item != null)
            {
                itemInSlot.Use(use);
                RefrashInventory();
            }

            return item;
        }
        return null;
    }

    public List<InvItem> RefrashInventory()
    {
        inventoryItems = new List<InvItem>();

        foreach (var slot in inventorySlots)
        {
            if (slot.transform.GetComponentInChildren<InvItem>() != null)
            {
                inventoryItems.Add(slot.transform.GetComponentInChildren<InvItem>());
            }
        }
        return inventoryItems;
    }
}
