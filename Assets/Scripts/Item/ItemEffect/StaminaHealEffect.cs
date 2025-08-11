using UnityEngine;

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
