using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.PlayerLoop;

// ��� ������ ȿ���� �⺻�� �Ǵ� �߻� Ŭ����
public abstract class ItemEffect : ScriptableObject
{
    public abstract void ApplyEffect(ItemData selectedItem);
}

// ��
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

// ��Ʈ��
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

// ������
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

// ���¹̳� ȸ��
[CreateAssetMenu(menuName = "Item Effects/StaminaHeal")]
public class StaminaHealEffect : ItemEffect
{
    public override void ApplyEffect(ItemData selectedItem)
    {
        PlayerCondition condition = GameManager.Instance.Player.condition;

        for (int i = 0; i < selectedItem.consumables.Length; i++)
        {
            if (selectedItem.consumables[i].type == ConsumableType.Stamina)
            {
                condition.StaminaHeal(selectedItem.consumables[i].value);
                Debug.Log(selectedItem.consumables[i].value + " ��ŭ ����� ȸ���մϴ�.");
            }
        }
    }
}

// ���ǵ� �ν�Ʈ
[CreateAssetMenu(menuName = "Item Effects/SpeedBoost")]
public class SpeedBoostEffect : ItemEffect
{
    public override void ApplyEffect(ItemData selectedItem)
    {
        PlayerController controller = GameManager.Instance.Player.controller;

        float value = 0;
        float value2 = 0;

        for (int i = 0; i < selectedItem.consumables.Length; i++)
        {
            if (selectedItem.consumables[i].type == ConsumableType.Speed)
            {
                value = selectedItem.consumables[i].value;
            }

            if (selectedItem.consumables[i].type == ConsumableType.Second)
            {
                value2 = selectedItem.consumables[i].value;
            }
        }

        controller.SpeedBoost(value, value2);

        Debug.Log($"{value2}�� ���� {value}��ŭ �ӵ��� �����մϴ�.");
    }
}

// ����
[CreateAssetMenu(menuName = "Item Effects/Invincibility")]
public class InvincibilityEffect : ItemEffect
{
    public override void ApplyEffect(ItemData selectedItem)
    {
        PlayerCondition condition = GameManager.Instance.Player.condition;

        float value = 0;

        for (int i = 0; i < selectedItem.consumables.Length; i++)
        {
            if (selectedItem.consumables[i].type == ConsumableType.Second)
            {
                value = selectedItem.consumables[i].value;
            }
        }

        condition.SetInvincibility(value);

        Debug.Log($"{value}�� ���� ������ �˴ϴ�.");
    }
}
