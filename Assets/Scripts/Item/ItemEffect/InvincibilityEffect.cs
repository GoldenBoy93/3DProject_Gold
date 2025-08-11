using UnityEngine;

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