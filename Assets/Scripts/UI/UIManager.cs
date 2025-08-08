using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������ UI ���¸� �����ϴ� ������
public enum UIState
{
    Intro,
    Game,
    GameOver,
}

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    IntroUI introUI;
    GameUI gameUI;
    //GameOverUI gameOverUI;
    private UIState currentState; // ���� UI ����

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // �ڽ� ������Ʈ���� ������ UI�� ã�� �ʱ�ȭ
        introUI = GetComponentInChildren<IntroUI>(true);
        introUI.Init(this);
        gameUI = GetComponentInChildren<GameUI>(true);
        gameUI.Init(this);
        //gameOverUI = GetComponentInChildren<GameOverUI>(true);
        //gameOverUI.Init(this);

        // �ʱ� ���¸� Ȩ ȭ������ ����
        ChangeState(UIState.Intro);
    }

    public void SetPlayGame()
    {
        ChangeState(UIState.Game);
    }

    public void SetGameOver()
    {
        ChangeState(UIState.GameOver);
    }

    // ���� UI ���¸� �����ϰ�, �� UI ������Ʈ�� ���¸� ����
    public void ChangeState(UIState state)
    {
        currentState = state;

        // �� UI�� �ڽ��� �������� �� �������� �Ǵ��ϰ� ǥ�� ���� ����
        introUI.SetActive(currentState);
        gameUI.SetActive(currentState);
        //gameOverUI.SetActive(currentState);
    }
}