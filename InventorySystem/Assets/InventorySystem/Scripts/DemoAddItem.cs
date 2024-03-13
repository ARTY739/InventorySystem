using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoAddItem : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public InventoryItem[] itemToPickup;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            UseSelectedItem();
        }
    }

    public void PickupItem(int id)
    {
        bool result = inventoryManager.AddItem(itemToPickup[id]);
        if (result)
        {
            Debug.Log("Item added");
        }
        else
        {
            Debug.Log("Item not added");
        }
    }

    public void GetSelectedItem()
    {
        InventoryItem receivedItem = inventoryManager.GetSelectedItem();
        if (receivedItem != null)
        {
            Debug.Log("Received item: " + receivedItem);
        }
        else
        {
            Debug.Log("No item received!");
        }
    }

    public void UseSelectedItem()
    {
        InventoryItem receivedItem = inventoryManager.UseSelectedItem(true);
        if (receivedItem != null)
        {
            Debug.Log("Used item: " + receivedItem);
        }
        else
        {
            Debug.Log("No item used!");
        }
    }

    public void DropSelectedItem()
    {
        InventoryItem receivedItem = inventoryManager.UseSelectedItem(false);
        if (receivedItem != null)
        {
            Debug.Log("Dropped item: " + receivedItem);
        }
        else
        {
            Debug.Log("No item dropped!");
        }
    }
}
