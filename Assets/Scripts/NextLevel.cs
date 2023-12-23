using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel : MonoBehaviour
{
    [SerializeField] private string _sceneToLoad;
    private List<Characters> _characters;
    private void Awake()
    {
        _characters = new List<Characters>();
    }
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        _characters.Add( other.gameObject.GetComponent<Characters>());
        if(_characters.Count == 2)
        {
            foreach (Characters character in _characters)
            {
                if (character == null)
                    return;
                

            }

            SceneManager.LoadScene(_sceneToLoad);
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        _characters.Remove( other.gameObject.GetComponent<Characters>());
    }

}
