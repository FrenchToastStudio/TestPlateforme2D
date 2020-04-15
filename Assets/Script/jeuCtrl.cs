using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jeuCtrl : MonoBehaviour
{
    [SerializeField]
    private GameObject personnagePrincipale;
    [SerializeField]
    private GameObject menuNiveauTerminer;
    [SerializeField]
    private GameObject menuPerdant;
    [SerializeField]
    private GameObject menuPause;
    [SerializeField]
    private GameObject afficahgeInfoJoueur;
    // Start is called before the first frame update
    void Start() {
        menuPerdant.SetActive(false);
        menuNiveauTerminer.SetActive(false);
        afficahgeInfoJoueur.SetActive(true);
        menuPause.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        if(Input.GetButtonDown("pause")) {
            if(menuPause.activeSelf){
                reprendreJeu();
            } else {
                pause();
            }
        }

        if(personnagePrincipale.GetComponent<personnagePrincipaleCtrl>().getPointVieActuelle() <= 0) {
            afficahgeInfoJoueur.SetActive(false);
            menuPerdant.SetActive(true);
            Time.timeScale = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
        if(col.gameObject.tag == "personnagePrincipale") {
            afficahgeInfoJoueur.SetActive(false);
            menuNiveauTerminer.SetActive(true);
            Time.timeScale = 0;
        }
    }

    private void pause() {
        menuPause.SetActive(true);
        afficahgeInfoJoueur.SetActive(false);
        Time.timeScale = 0;
    }

    public void reprendreJeu() {
        afficahgeInfoJoueur.SetActive(true);
        Time.timeScale = 1;
        menuPause.SetActive(false);
    }

}
