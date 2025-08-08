using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    // private�� ���� ���� ����
    private static SceneManager _instance;

    // public Instance ������Ƽ�� ���� �ܺο��� SceneManager�� ������ �� �ֵ��� ��
    public static SceneManager Instance
    {
        get
        {
            // �Ҵ���� �ʾ��� ��, �ܺο��� SceneManager.Instance �� �����ϴ� ���
            // ���� ������Ʈ�� ������ְ� SceneManager ��ũ��Ʈ�� AddComponent�� �ٿ��ش�.
            if (_instance == null)
            {
                // ���ӿ�����Ʈ�� ��� ���۽� ���°� Ȯ���� �Ŵ����� ���ӿ�����Ʈ�� ����
                _instance = new GameObject("SceneManager").AddComponent<SceneManager>();
            }
            return _instance;
        }
    }

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ChangeToNextScene()
    {
        // ���� �� �ε��� �Ҵ�
        int currentSceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;

        // ���� �� �ε��� �Ҵ� (���� �� �ε��� + 1)
        int nextSceneIndex = currentSceneIndex + 1;

        // ���� ���� �� �ε����� ���� �������� ���ǵ� ���� ������ �ʰ��ϸ�, ù ��° ������ ���ư�
        UnityEngine.SceneManagement.SceneManager.LoadScene(nextSceneIndex);
    }
}