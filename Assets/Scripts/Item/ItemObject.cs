using UnityEngine;

// ���ͷ��� ������ ��ü�� ����� �������̽�
public interface IInteractable
{
    public string GetInteractPrompt();  // UI�� ǥ���� ����
    public void OnInteract(GameObject curInteractGameObject);           // ���ͷ��� ȣ��
}

public class ItemObject : MonoBehaviour, IInteractable
{
    public ItemData data;

    public string GetInteractPrompt()
    {
        string str = $"{data.displayName}\n{data.description}";
        return str;
    }

    // �Ű������� ���� ��ȣ�ۿ� ���� ���� ������Ʈ�� ����
    public void OnInteract(GameObject curInteractGameObject)
    {
        // �ֿ� �� �ִ� �������̶��,
        if (data.isGetable)
        {
            //Player ��ũ��Ʈ�� ��ȣ�ۿ� ������ data �ѱ��.
            GameManager.Instance.Player.itemData = data;
            GameManager.Instance.Player.addItem?.Invoke();
            Destroy(gameObject);
        }
        else if (curInteractGameObject.GetComponent<Door>() != null)
        {
            // ���� ��ȣ�ۿ� ���� ������Ʈ�� Door ������Ʈ�� ������ �ִٸ�,
            // Door�� SetState �޼��带 ȣ���Ͽ� ���¸� ����
            curInteractGameObject.GetComponent<Door>().SetState();
        }
        return;
    }
}