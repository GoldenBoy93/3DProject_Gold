using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��� ������ ȿ���� �⺻�� �Ǵ� �߻� Ŭ����
public abstract class ItemEffect : ScriptableObject
{
    public abstract void ApplyEffect(ItemData selectedItem);
}

[CreateAssetMenu(menuName = "Item Effects/Heal")]
public class HealEffect : ItemEffect
{
    public override void ApplyEffect(ItemData selectedItem)
    {
        PlayerCondition condition = GameManager.Instance.Player.condition;

        for (int i = 0; i < selectedItem.consumables.Length; i++)
        {
            if (selectedItem.consumables[i].type == ConsumableType.Health)
            {
                condition.Heal(selectedItem.consumables[i].value);
                Debug.Log(selectedItem.consumables[i].value + " ��ŭ ü���� ȸ���߽��ϴ�.");
            }
        }
    }
}

[CreateAssetMenu(menuName = "Item Effects/DotHeal")]
public class DotHealEffect : ItemEffect
{
    public override void ApplyEffect(ItemData selectedItem)
    {
        PlayerCondition condition = GameManager.Instance.Player.condition;

        for (int i = 0; i < selectedItem.consumables.Length; i++)
        {
            if (selectedItem.consumables[i].type == ConsumableType.Health)
            {
                condition.FoodHeal(selectedItem.consumables[i].value);
                Debug.Log(selectedItem.consumables[i].value + " ��ŭ ü���� ������ ȸ���մϴ�.");
            }
        }
    }
}

[CreateAssetMenu(menuName = "Item Effects/Eat")]
public class EatEffect : ItemEffect
{
    public override void ApplyEffect(ItemData selectedItem)
    {
        PlayerCondition condition = GameManager.Instance.Player.condition;

        for (int i = 0; i < selectedItem.consumables.Length; i++)
        {
            if (selectedItem.consumables[i].type == ConsumableType.Hunger)
            {
                condition.Eat(selectedItem.consumables[i].value);
                Debug.Log(selectedItem.consumables[i].value + " ��ŭ �������� ȹ���մϴ�.");
            }
        }
    }
}
