using UnityEngine;

// 모든 아이템 효과의 기본이 되는 추상 클래스
public abstract class ItemEffect : ScriptableObject
{
    public abstract void ApplyEffect(ItemData selectedItem);
}
