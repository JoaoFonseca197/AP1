using UnityEngine.SceneManagement;
using UnityEditor.SearchService;
using UnityEngine;

public class ScenesManager : MonoBehaviour
{
    [SerializeField] private string _sceneName;

    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(_sceneName);
    }

}
