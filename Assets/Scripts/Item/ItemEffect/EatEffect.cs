using UnityEngine;

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