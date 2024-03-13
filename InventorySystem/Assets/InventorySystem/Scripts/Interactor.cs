using TMPro;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    public float sphereRadius;
    public float maxDistance;

    public TMP_Text text;

    private Transform selection;

    private void Update()
    {
        // Set UI
        if (text != null)
        {
            text.gameObject.SetActive(selection != null);
            if (selection != null)
            {
                Interactable interactable = selection.GetComponent<Interactable>();
                if (interactable == null)
                    interactable = selection.GetComponentInParent<Interactable>();
                text.text = interactable.text;
            }
        }

        // Input checker
        if (Input.GetKeyDown(KeyCode.F) && selection != null)
        {
            Interactable interactable = selection.GetComponent<Interactable>();
            if (interactable == null)
                interactable = selection.GetComponentInParent<Interactable>();
            if (interactable.isInteracted == false)
            {
                interactable.isInteracted = true;
            }
        }

        // Reset selection
        if (selection != null)
        {
            Interactable interactable = selection.GetComponent<Interactable>();
            if (interactable == null)
                interactable = selection.GetComponentInParent<Interactable>();
            if (interactable.outline != null) interactable.outline.enabled = false;
            selection = null;
        }

        // Create ray and find new selection;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, maxDistance) != null)
        {
            if (hit.transform != null)
            {
                if (hit.transform.GetComponent<Interactable>() != null)
                {
                    Interactable interactable = hit.transform.GetComponent<Interactable>();
                    if (interactable.outline != null) interactable.outline.enabled = true;
                    selection = hit.transform;
                }
                else if (hit.transform.GetComponentInParent<Interactable>() != null)
                {
                    Interactable interactable = hit.transform.GetComponentInParent<Interactable>();
                    if (interactable.outline != null) interactable.outline.enabled = true;
                    selection = hit.transform;
                }
            }

        }
        else 
            selection = null;
    }
}
