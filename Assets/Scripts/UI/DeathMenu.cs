using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{

    [SerializeField]
    Canvas deathMenu;
    [SerializeField]
    Camera pauseCam;

    AudioSource deathsource;




    // Start is called before the first frame update
    void Start()
    {
        DungeonMaster.Instance.deathMenu = this;
        pauseCam.gameObject.SetActive(false);
        deathMenu.gameObject.SetActive(false);
        deathsource = GetComponent<AudioSource>();
    }


    public void ShowMenu()
    {

        deathsource.Play();
        DungeonMaster.Instance.descriptionText.gameObject.SetActive(false);
        DungeonMaster.Instance.mainCamera.gameObject.SetActive(false);
        DungeonMaster.Instance.player.gameObject.SetActive(false);
        pauseCam.transform.position = DungeonMaster.Instance.mainCamera.transform.position;
        pauseCam.transform.rotation = DungeonMaster.Instance.mainCamera.transform.rotation;
        pauseCam.gameObject.SetActive(true);
        deathMenu.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    public void GotoMenu()
    {
        Time.timeScale = 1;
        DungeonMaster.Instance.descriptionText.gameObject.SetActive(false);
        GameAudio.Instance.SetMenuMusic();
        SceneManager.LoadScene("MainMenu");
    }
}
