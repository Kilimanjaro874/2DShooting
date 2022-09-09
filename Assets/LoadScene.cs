using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [SerializeField]
    private string _sceneName;

    public void OnLoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
