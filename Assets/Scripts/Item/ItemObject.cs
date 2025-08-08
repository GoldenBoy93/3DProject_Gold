using UnityEngine;

// ���ͷ��� ������ ��ü�� ����� �������̽�
public interface IInteractable
{
    public string GetInteractPrompt();  // UI�� ǥ���� ����
    public void OnInteract();           // ���ͷ��� ȣ��
}

public class ItemObject : MonoBehaviour, IInteractable
{
    public ItemData data;

    public string GetInteractPrompt()
    {
        string str = $"{data.displayName}\n{data.description}";
        return str;
    }

    public void OnInteract()
    {
        //Player ��ũ��Ʈ�� ��ȣ�ۿ� ������ data �ѱ��.
        GameManager.Instance.Player.itemData = data;
        GameManager.Instance.Player.addItem?.Invoke();
        Destroy(gameObject);
    }
}