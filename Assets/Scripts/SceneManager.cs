using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    public static SceneManager instance;

    void Awake()
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