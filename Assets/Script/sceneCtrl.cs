using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class sceneCtrl : MonoBehaviour
{

    public Button loadSceneBtn;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void chargerScene(string sceneName){
        SceneManager.LoadSceneAsync(sceneName);
    }

    public void quitterjeu(){
        Application.Quit();
    }
}
