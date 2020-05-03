using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    bool pauseMenuOpen = false;

    Canvas pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        DescriptionText.Instance.gameObject.SetActive(true);
        pauseMenu = GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenuOpen)
            {
                CloseMenu();
            }
            else
            {
                ShowMenu();
            }
        }
    }

    void CloseMenu()
    {
        pauseMenu.gameObject.SetActive(false);


        Time.timeScale = 1;
        pauseMenuOpen = true;
    }

    void ShowMenu()
    {
        pauseMenu.gameObject.SetActive(true);


        Time.timeScale = 0f;
        pauseMenuOpen = false;
    }

    public void GotoMenu()
    {
        DescriptionText.Instance.gameObject.SetActive(false);
        GameAudio.Instance.SetMenuMusic();
        SceneManager.LoadScene("MainMenu");
    }
}
