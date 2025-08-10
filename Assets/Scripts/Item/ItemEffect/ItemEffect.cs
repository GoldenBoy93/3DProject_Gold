using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��� ������ ȿ���� �⺻�� �Ǵ� �߻� Ŭ����
public abstract class ItemEffect : ScriptableObject
{
    public abstract void ApplyEffect();
}

[CreateAssetMenu(menuName = "Item Effects/Heal")]
public class HealEffect : ItemEffect
{
    public float healAmount; // ü�� ȸ����
                             // ItemObject �������� Player�� �Ѱ��� ������ ������ ��
    
    public override void ApplyEffect()
    {
        PlayerCondition condition = GameManager.Instance.Player.condition;
        
        condition.Heal(healAmount);
        Debug.Log(healAmount + " ��ŭ ü���� ȸ���߽��ϴ�.");
    }
}

[CreateAssetMenu(menuName = "Item Effects/DotHeal")]
public class DotHealEffect : ItemEffect
{
    public float healAmount; // ü�� ȸ����
                             // ItemObject �������� Player�� �Ѱ��� ������ ������ ��

    public override void ApplyEffect()
    {
        PlayerCondition condition = GameManager.Instance.Player.condition;

        condition.FoodHeal(healAmount);
        Debug.Log(healAmount + " ��ŭ ü���� ������ ȸ���մϴ�.");
    }
}
