using UnityEngine;

// �̱��� ����
public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    private UIManager uiManager;

    private SceneManager sceneManager;

    public static GameManager Instance
    {
        get
        {
            // �Ҵ���� �ʾ��� ��, �ܺο��� GameManager.Instance �� �����ϴ� ���
            // ���� ������Ʈ�� ������ְ� GameManager ��ũ��Ʈ�� AddComponent�� �ٿ��ش�.
            if (_instance == null)
            {
                // ���ӿ�����Ʈ�� ��� ���۽� ���°� Ȯ���� �Ŵ����� ���ӿ�����Ʈ�� ��������(�ſ����ϳ�)
                _instance = new GameObject("GameManager").AddComponent<GameManager>();
            }
            return _instance;
        }
    }

    // ���߿� ������ ��츦 ����Ͽ� ����(_player)�� ����(Player)�� ����
    // �Ʒ� ������Ƽ�� ����ؼ� GameManager.Instance.Player.Health�� ���� �ٸ� ��ũ��Ʈ���� �÷��̾������� ������ �� �ֵ��� ��
    public Player Player
    {
        get { return _player; }
        set { _player = value; }
    }
    private Player _player;

    private void Awake()
    {
        // Awake�� ȣ�� �� ����� �̹� �Ŵ��� ������Ʈ�� �����Ǿ� �ִ� ���̰�, '_instance'�� �ڽ��� �Ҵ�
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // �̹� ������Ʈ�� �����ϴ� ��� '�ڽ�'�� �ı��ؼ� �ߺ�����
            if (_instance != this)
            {
                Destroy(gameObject);
            }
        }

        uiManager = FindObjectOfType<UIManager>();
    }

    public void StartGame()
    {
        // UIManager�� SetPlayGame �޼��带 ȣ���Ͽ� ���� ���� UI�� ��ȯ
        uiManager.SetPlayGame();

        // �� �Ŵ����� ���� ���� ������ ��ȯ
        SceneManager.instance.ChangeToNextScene();
    }

    public void GameOver()
    {
        uiManager.SetGameOver();
    }
}