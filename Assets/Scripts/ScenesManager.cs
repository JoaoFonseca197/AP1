using UnityEngine.SceneManagement;
using UnityEngine;

public class ScenesManager : MonoBehaviour
{
    [SerializeField] private string _sceneName;

    public static SceneManager Instance;
    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(_sceneName);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

}
