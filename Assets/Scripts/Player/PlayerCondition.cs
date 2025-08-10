using System;
using System.Collections;
using UnityEngine;

// �������� ���� �� �ʿ��� �������̽� �ۼ�
// Player, Monster�� ��� ��� ����
public interface IDamagable
{
    void TakePhysicalDamage(int damageAmount);
}

// UI�� ������ �� �ִ� PlayerCondition
// �ܺο��� �ɷ�ġ ���� ����� �̰��� ���ؼ� ȣ��. ���������� UI ������Ʈ ����.
public class PlayerCondition : MonoBehaviour, IDamagable
{
    public UICondition uiCondition;

    Condition health { get { return uiCondition.health; } }
    Condition hunger { get { return uiCondition.hunger; } }
    Condition stamina { get { return uiCondition.stamina; } }

    public float noHungerHealthDecay;   // hunger�� 0�϶� ����� �� (value > 0)
    public event Action onTakeDamage;   // Damage ���� �� ȣ���� Action

    private void Update()
    {
        // hunger, stamina�� passiveValue�� �̿��Ͽ� �ڵ����� ����/����
        hunger.Subtract(hunger.passiveValue * Time.deltaTime);
        stamina.Add(stamina.passiveValue * Time.deltaTime);

        // hunger�� 0 ������ �� health ����
        if (hunger.curValue == 0f)
        {
            health.Subtract(noHungerHealthDecay * Time.deltaTime);
        }

        if (health.curValue == 0f)
        {
            Die();
        }
    }

    // �׳� ��
    public void Heal(float amount)
    {
        health.Add(amount);
    }

    // ���� �Ծ��� �� -> �ڷ�ƾ��
    public void FoodHeal(float amount)
    {
        StartCoroutine(DotHeal(amount));
    }

    public IEnumerator DotHeal(float amount)
    {
        // ȸ�� �ѷ��� �� ������ 1�� ������Ű�� �ڷ�ƾ
        for (float i = 0; i < amount; i += 1f)
        {
            health.Add(1f);
            yield return new WaitForSeconds(0.5f); // ȸ�� �ִϸ��̼� �ð�
        }
    }

    public void Eat(float amount)
    {
        Debug.Log("���� �Ծ���.");
        hunger.Add(amount);
    }

    public void Die()
    {
        Debug.Log("�÷��̾ �׾���.");
    }

    public void TakePhysicalDamage(int damageAmount)
    {
        health.Subtract(damageAmount);
        onTakeDamage?.Invoke();
    }

    public bool UseStamina(float amount)
    {
        if (stamina.curValue - amount < 0)
        {
            return false;
        }
        stamina.Subtract(amount);
        return true;
    }
}