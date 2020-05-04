using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    bool pauseMenuOpen = false;

    [SerializeField]
    Canvas pauseMenu;
    [SerializeField]
    Camera pauseCam;


    

    // Start is called before the first frame update
    void Start()
    {
        pauseCam.gameObject.SetActive(false);
        pauseMenu.gameObject.SetActive(false);
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

    public void CloseMenu()
    {
        DescriptionText.Instance.gameObject.SetActive(true);
        pauseMenu.gameObject.SetActive(false);
        pauseCam.gameObject.SetActive(false);
        DungeonMaster.Instance.player.gameObject.SetActive(true);
        Time.timeScale = 1;
        GameAudio.Instance.ManualPitch(1);


        pauseMenuOpen = false;
    }

    void ShowMenu()
    {
        DescriptionText.Instance.gameObject.SetActive(false);
        DungeonMaster.Instance.player.gameObject.SetActive(false);
        pauseCam.transform.position = DungeonMaster.Instance.mainCamera.transform.position;
        pauseCam.transform.rotation = DungeonMaster.Instance.mainCamera.transform.rotation;
        pauseCam.gameObject.SetActive(true);
        pauseMenu.gameObject.SetActive(true);
        Time.timeScale = 0f;
        pauseMenuOpen = true;

        GameAudio.Instance.ManualPitch(0.5f);

    }

    public void GotoMenu()
    {
        GameAudio.Instance.ManualPitch(1);
        Time.timeScale = 1;
        DescriptionText.Instance.gameObject.SetActive(false);
        GameAudio.Instance.SetMenuMusic();
        SceneManager.LoadScene("MainMenu");
    }
}
