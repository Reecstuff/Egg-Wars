using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{

    [SerializeField]
    string SceneName;

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene(SceneName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
