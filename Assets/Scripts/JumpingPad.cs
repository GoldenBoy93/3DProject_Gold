using UnityEngine;

public class JumpingPad : MonoBehaviour
{
    public int padJumpPower; // JumpingPad 점프 파워


    // 충돌된 객체가 PlayerController를 상속받고 있으면 점프
    private void OnTriggerEnter(Collider other)
    {
        PlayerController playerController = other.GetComponent<PlayerController>();

        playerController.HighJump(padJumpPower);
    }
}
