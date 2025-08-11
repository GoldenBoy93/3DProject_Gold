using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 상태를 구별하기 위한 enum 선언
public enum State
{
    Closed,
    Open,
}

public class Door : MonoBehaviour
{
    private State state;
    private Animator animator;

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Open 상태일때 애니메이터의 PlayAnimation 파라미터를 true로 설정
        animator.SetBool("PlayAnimation", state == State.Open);
    }

    public void SetState()
    {
        switch (state)
        {
            // 현재 Oen 상태라면 Closed로 변경
            case State.Open:
                state = State.Closed;
                Debug.Log("Door is now Closed");
                return;

            // 현재 Closed 상태라면 Open으로 변경
            case State.Closed:
                state = State.Open;
                Debug.Log("Door is now Open");
                return;
        }
    }
}
