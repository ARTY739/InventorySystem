using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public bool isInteracted;

    public string text;

    public Outline outline;

    private void Awake()
    {
        if (GetComponent<Outline>() != null)
            outline = GetComponent<Outline>();
    }
}
