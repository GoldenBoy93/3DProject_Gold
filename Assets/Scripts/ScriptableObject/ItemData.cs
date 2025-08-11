using System;
using UnityEngine;

public enum ItemType
{
    Equipable,
    Consumable,
    Resource,
    Interactable,
}

public enum ConsumableType
{
    Health,
    Hunger,
    Stamina,
    Speed,
    Second,
}

[Serializable]
public class ItemDataConsumable
{
    public ConsumableType type;
    public float value;
}

[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class ItemData : ScriptableObject
{
    [Header("Info")]
    public string displayName;
    public string description;
    public ItemType type;
    public Sprite icon;
    public GameObject dropPrefab;

    [Header("Stacking")]
    public bool canStack;
    public int maxStackAmount;

    [Header("Consumable")]
    public ItemDataConsumable[] consumables;

    [Header("Equip")]
    public GameObject equipPrefab;

    [Header("Getable")]
    public bool isGetable = false;

    // �� �迭�� HealEffect�� StaminaEffect ScriptableObject�� �Ҵ�
    public ItemEffect[] effects;
}
