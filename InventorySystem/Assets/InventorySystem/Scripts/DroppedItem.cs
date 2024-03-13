using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedItem : MonoBehaviour
{
    public InventoryItem item;

    public bool isActive = false;

    private InventoryManager inventoryManager;
    private Interactable interactable;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        inventoryManager = FindAnyObjectByType<InventoryManager>();
        interactable = GetComponent<Interactable>();
        StartCoroutine(Activate());
    }

    private void Update()
    {
        if (interactable != null && interactable.isInteracted)
        {
            PickUpItem();
        }
    }

    private void PickUpItem()
    {
        interactable.isInteracted = false;
        inventoryManager.AddItem(item);
        Destroy(gameObject);
    }

    private IEnumerator Activate()
    {
        yield return new WaitForSeconds(0.5f);
        isActive = true;
    }
}
