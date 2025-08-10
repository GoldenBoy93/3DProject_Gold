using System;
using System.Collections;
using UnityEngine;

// 데미지를 받을 때 필요한 인터페이스 작성
// Player, Monster에 모두 사용 가능
public interface IDamagable
{
    void TakePhysicalDamage(int damageAmount);
}

// UI를 참조할 수 있는 PlayerCondition
// 외부에서 능력치 변경 기능은 이곳을 통해서 호출. 내부적으로 UI 업데이트 수행.
public class PlayerCondition : MonoBehaviour, IDamagable
{
    public UICondition uiCondition;

    Condition health { get { return uiCondition.health; } }
    Condition hunger { get { return uiCondition.hunger; } }
    Condition stamina { get { return uiCondition.stamina; } }

    public float noHungerHealthDecay;   // hunger가 0일때 사용할 값 (value > 0)
    public event Action onTakeDamage;   // Damage 받을 때 호출할 Action

    private void Update()
    {
        // hunger, stamina의 passiveValue를 이용하여 자동으로 감소/증가
        hunger.Subtract(hunger.passiveValue * Time.deltaTime);
        stamina.Add(stamina.passiveValue * Time.deltaTime);

        // hunger가 0 이하일 때 health 감소
        if (hunger.curValue == 0f)
        {
            health.Subtract(noHungerHealthDecay * Time.deltaTime);
        }

        if (health.curValue == 0f)
        {
            Die();
        }
    }

    // 그냥 힐
    public void Heal(float amount)
    {
        health.Add(amount);
    }

    // 음식 먹었을 때 -> 코루틴힐
    public void FoodHeal(float amount)
    {
        StartCoroutine(DotHeal(amount));
    }

    public IEnumerator DotHeal(float amount)
    {
        // 회복 총량이 될 때까지 1씩 증가시키는 코루틴
        for (float i = 0; i < amount; i += 1f)
        {
            health.Add(1f);
            yield return new WaitForSeconds(0.5f); // 회복 애니메이션 시간
        }
    }

    public void Eat(float amount)
    {
        hunger.Add(amount);
    }

    public void StaminaHeal(float amount)
    {
        stamina.Add(amount);
    }

    public void Die()
    {
        Debug.Log("플레이어가 죽었다.");
    }

    bool Invincibility = false; // 무적 상태 여부

    public void TakePhysicalDamage(int damageAmount)
    {
        if (Invincibility)
        {
            Debug.Log("무적 상태라서 데미지를 받지 않습니다.");
            return; // 무적 상태면 데미지 받지 않음
        }
        health.Subtract(damageAmount);
        onTakeDamage?.Invoke();
    }

    // 무적 코루틴함수 발사대
    public void SetInvincibility(float second)
    {
        StartCoroutine(InvincibilityCountdownSecond(second));
    }

    // 무적 코루틴함수
    public IEnumerator InvincibilityCountdownSecond(float Second)
    {
        Invincibility = true;
        Debug.Log($"무적상태");

        yield return new WaitForSeconds(Second);
        Debug.Log($"무적끝");

        Invincibility = false;
        Debug.Log($"TimeOver");
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