using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseCtrl : MonoBehaviour
{

    [SerializeField]
    private GameObject menuPause;
    // Start is called before the first frame update
    void Start()
    {
        menuPause.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("pause")){
            if(menuPause.activeSelf){
                reprendreJeu();
            } else {
                pause();
            }
        }
    }

    private void pause(){
        Time.timeScale = 0;
        menuPause.SetActive(true);
    }

    public void reprendreJeu(){
        Time.timeScale = 1;
        menuPause.SetActive(false);
    }
}
