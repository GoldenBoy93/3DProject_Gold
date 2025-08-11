using UnityEngine;

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
