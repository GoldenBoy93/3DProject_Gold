using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.PlayerLoop;

// 모든 아이템 효과의 기본이 되는 추상 클래스
public abstract class ItemEffect : ScriptableObject
{
    public abstract void ApplyEffect(ItemData selectedItem);
}

// 힐
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
                Debug.Log(selectedItem.consumables[i].value + " 만큼 체력을 회복했습니다.");
            }
        }
    }
}

// 도트힐
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
                Debug.Log(selectedItem.consumables[i].value + " 만큼 체력을 서서히 회복합니다.");
            }
        }
    }
}

// 포만감
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
                Debug.Log(selectedItem.consumables[i].value + " 만큼 포만감을 획득합니다.");
            }
        }
    }
}

// 스태미너 회복
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
                Debug.Log(selectedItem.consumables[i].value + " 만큼 기력을 회복합니다.");
            }
        }
    }
}

// 스피드 부스트
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

        Debug.Log($"{value2}초 동안 {value}만큼 속도가 증가합니다.");
    }
}

// 무적
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

        Debug.Log($"{value}초 동안 무적이 됩니다.");
    }
}
