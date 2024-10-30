using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject settingsPanel;
    private bool isPaused = false;
    // Start is called before the first frame update
    void Start()
    {
        pausePanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
            if (isPaused)
            {
                if (settingsPanel.activeInHierarchy)
                {
                    return;
                }
                else
                {
                Continue(); // Reanudar el juego
                }
            }
            else
            {
                Pause(); // Pausar el juego
            }
        }
    }

    public void Pause()
    {
        pausePanel.SetActive(true);
        isPaused = true;
        Time.timeScale = 0;
    }

    public void Continue() 
    { 
        pausePanel.SetActive(false);
        isPaused = false;
        Time.timeScale = 1;
    }

}