using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 모든 아이템 효과의 기본이 되는 추상 클래스
public abstract class ItemEffect : ScriptableObject
{
    public abstract void ApplyEffect();
}

[CreateAssetMenu(menuName = "Item Effects/Heal")]
public class HealEffect : ItemEffect
{
    public float healAmount; // 체력 회복량
                             // ItemObject 로직에서 Player에 넘겨준 정보를 가지고 옴
    
    public override void ApplyEffect()
    {
        PlayerCondition condition = GameManager.Instance.Player.condition;
        
        condition.Heal(healAmount);
        Debug.Log(healAmount + " 만큼 체력을 회복했습니다.");
    }
}

[CreateAssetMenu(menuName = "Item Effects/DotHeal")]
public class DotHealEffect : ItemEffect
{
    public float healAmount; // 체력 회복량
                             // ItemObject 로직에서 Player에 넘겨준 정보를 가지고 옴

    public override void ApplyEffect()
    {
        PlayerCondition condition = GameManager.Instance.Player.condition;

        condition.FoodHeal(healAmount);
        Debug.Log(healAmount + " 만큼 체력을 서서히 회복합니다.");
    }
}
