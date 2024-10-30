using UnityEngine;
using UnityEngine.SceneManagement;

// Remember to add the desired scenes to build settings
public class SceneChanger : MonoBehaviour
{
    public string scene;
    public void loadScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(scene);
    }
}