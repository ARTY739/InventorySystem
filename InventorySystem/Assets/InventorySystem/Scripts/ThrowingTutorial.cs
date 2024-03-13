using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class ThrowingTutorial : MonoBehaviour
{
    [Header("References")]
    public Transform cam;
    public Transform attackPoint;
    public GameObject objectToThrow;

    [Header("Settings")]
    public int totalThrows;
    public float throwCooldown;

    [Header("Throwing")]
    public KeyCode throwKey = KeyCode.Mouse0;
    public float throwForce;
    public float throwUpwardForce;

    bool readyToThrow;

    [SerializeField]
    private InventoryManager inventoryManager;

    private Animator animator;

    private void Start()
    {
        readyToThrow = true;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        totalThrows = 0;

        if (inventoryManager.inventorySlots[inventoryManager.selectedSlot].GetComponentInChildren<InvItem>() != null)
            totalThrows = inventoryManager.inventorySlots[inventoryManager.selectedSlot].GetComponentInChildren<InvItem>().count;

        if (totalThrows <= 0)
            return;

        if (Input.GetKeyDown(throwKey) && readyToThrow && totalThrows > 0)
        {
            animator.SetTrigger("Throw");
        }
    }

    private void Throw()
    {
        //readyToThrow = false;

        inventoryManager.UseSelectedItem(false);

        // instantiate object to throw
        GameObject projectile = Instantiate(objectToThrow, attackPoint.position, cam.rotation);

        // get rigidbody component
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        // calculate direction
        Vector3 forceDirection = cam.transform.forward;

        RaycastHit hit;

        if(Physics.Raycast(cam.position, cam.forward, out hit, 500f))
        {
            forceDirection = (hit.point - attackPoint.position).normalized;
        }

        // add force
        Vector3 forceToAdd = forceDirection * throwForce + transform.up * throwUpwardForce;

        projectileRb.AddForce(forceToAdd, ForceMode.Impulse);

        totalThrows--;

        // implement throwCooldown
        //Invoke(nameof(ResetThrow), throwCooldown);
    }

    private void CanThrow(int i)
    {
        if (i == 1)
        {
            readyToThrow = false;
        }
        else
        {
            readyToThrow = true;
        }
    }

    private void ResetThrow()
    {
        readyToThrow = true;
    }
}