using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LoadSceneOnClick : MonoBehaviour
{
    [SerializeField]
    string SceneName;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => LoadButtonScene());
    }


    void LoadButtonScene()
    {
        if(string.IsNullOrEmpty(SceneName))
        {
            Application.Quit();
        }
        else
        {
            SceneManager.LoadScene(SceneName);
        }
    }

}
