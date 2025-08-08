using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JumpingPad : MonoBehaviour
{
    public int padJumpPower; // JumpingPad 점프 파워
    public float padJumpRate; // JumpingPad 점프 빈도

    private List<IJumpable> things = new List<IJumpable>();

    private void Start()
    {
        InvokeRepeating("PadJump", 0, padJumpRate);
    }

    void PadJump()
    {
        for (int i = 0; i < things.Count; i++)
        {
            things[i].HighJump(padJumpPower);
        }
    }

    // 충돌된 객체가 PlayerController를 상속받고 있으면 점프
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name + " on JumpingPad");

        // 충돌된 객체에 IDamagable이 상속되어 있으면 List에 추가
        if (other.TryGetComponent(out IJumpable jumpable))
        {
            Debug.Log("Jumpable detected: " + other.name);
            things.Add(jumpable);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Exit 되는 객체에 IDamagable이 상속되어 있으면 List에서 제거
        if (other.TryGetComponent(out IJumpable jumpable))
        {
            things.Remove(jumpable);
        }
    }
}
