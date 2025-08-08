using UnityEngine;

// 개별 Condition 바의 조합으로 이루어진 UICondition
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