using UnityEngine;

// ��� ������ ȿ���� �⺻�� �Ǵ� �߻� Ŭ����
public abstract class ItemEffect : ScriptableObject
{
    public abstract void ApplyEffect(ItemData selectedItem);
}
