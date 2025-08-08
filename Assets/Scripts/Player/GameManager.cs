using UnityEngine;

// 싱글톤 패턴
public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    private UIManager uiManager;

    private SceneManager sceneManager;

    public static GameManager Instance
    {
        get
        {
            // 할당되지 않았을 때, 외부에서 GameManager.Instance 로 접근하는 경우
            // 게임 오브젝트를 만들어주고 GameManager 스크립트를 AddComponent로 붙여준다.
            if (_instance == null)
            {
                // 게임오브젝트가 없어도 시작시 없는걸 확인후 매니저를 게임오브젝트로 생성해줌(매우편리하네)
                _instance = new GameObject("GameManager").AddComponent<GameManager>();
            }
            return _instance;
        }
    }

    // 나중에 수정될 경우를 고려하여 원본(_player)과 접근(Player)을 구별
    // 아래 프로퍼티를 사용해서 GameManager.Instance.Player.Health와 같이 다른 스크립트에서 플레이어정보에 접근할 수 있도록 함
    public Player Player
    {
        get { return _player; }
        set { _player = value; }
    }
    private Player _player;

    private void Awake()
    {
        // Awake가 호출 될 때라면 이미 매니저 오브젝트는 생성되어 있는 것이고, '_instance'에 자신을 할당
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // 이미 오브젝트가 존재하는 경우 '자신'을 파괴해서 중복방지
            if (_instance != this)
            {
                Destroy(gameObject);
            }
        }

        uiManager = FindObjectOfType<UIManager>();
    }

    public void StartGame()
    {
        // UIManager의 SetPlayGame 메서드를 호출하여 게임 시작 UI로 전환
        uiManager.SetPlayGame();

        // 씬 매니저를 통해 다음 씬으로 전환
        SceneManager.instance.ChangeToNextScene();
    }

    public void GameOver()
    {
        uiManager.SetGameOver();
    }
}