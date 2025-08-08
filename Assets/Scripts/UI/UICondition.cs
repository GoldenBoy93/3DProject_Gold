using UnityEngine;

// ���� Condition ���� �������� �̷���� UICondition
public class UICondition : MonoBehaviour
{
    public Condition health;
    public Condition hunger;
    public Condition stamina;

    private void Start()
    {
        GameManager.Instance.Player.condition.uiCondition = this;
        Debug.Log(GameManager.Instance.Player.condition.uiCondition);
    }
}