using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���¸� �����ϱ� ���� enum ����
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
        // Open �����϶� �ִϸ������� PlayAnimation �Ķ���͸� true�� ����
        animator.SetBool("PlayAnimation", state == State.Open);
    }

    public void SetState()
    {
        switch (state)
        {
            // ���� Oen ���¶�� Closed�� ����
            case State.Open:
                state = State.Closed;
                Debug.Log("Door is now Closed");
                return;

            // ���� Closed ���¶�� Open���� ����
            case State.Closed:
                state = State.Open;
                Debug.Log("Door is now Open");
                return;
        }
    }
}
