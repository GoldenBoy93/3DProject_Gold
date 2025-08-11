using UnityEngine;

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
                condition.DotHeal(selectedItem.consumables[i].value);
                Debug.Log(selectedItem.consumables[i].value + " ��ŭ ü���� ������ ȸ���մϴ�.");
            }
        }
    }
}
