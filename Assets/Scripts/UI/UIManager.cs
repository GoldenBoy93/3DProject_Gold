using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 게임의 UI 상태를 정의하는 열거형
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
    private UIState currentState; // 현재 UI 상태

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

        // 자식 오브젝트에서 각각의 UI를 찾아 초기화
        introUI = GetComponentInChildren<IntroUI>(true);
        introUI.Init(this);
        gameUI = GetComponentInChildren<GameUI>(true);
        gameUI.Init(this);
        //gameOverUI = GetComponentInChildren<GameOverUI>(true);
        //gameOverUI.Init(this);

        // 초기 상태를 홈 화면으로 설정
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

    // 현재 UI 상태를 변경하고, 각 UI 오브젝트에 상태를 전달
    public void ChangeState(UIState state)
    {
        currentState = state;

        // 각 UI가 자신이 보여져야 할 상태인지 판단하고 표시 여부 결정
        introUI.SetActive(currentState);
        gameUI.SetActive(currentState);
        //gameOverUI.SetActive(currentState);
    }
}