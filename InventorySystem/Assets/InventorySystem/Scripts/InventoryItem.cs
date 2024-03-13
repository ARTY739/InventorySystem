using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public abstract class InventoryItem : ScriptableObject
{
    public Sprite image;

    public GameObject dropedPref;

    public string name;
    public string id;
    public ItemType type;
    public bool stackable = true;
    [Range(1f, 99f)]
    public int maxCountInSlot = 1;

    public bool infinityUse;

    public bool canBeCrafted;

    public abstract void Use();
}

public enum ItemType {
    Food,
    Tool,
    Key,
    Weapon
}
