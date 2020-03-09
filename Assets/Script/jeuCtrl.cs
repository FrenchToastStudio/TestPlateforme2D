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
    // Start is called before the first frame update
    void Start()
    {
        menuPerdant.SetActive(false);
        menuNiveauTerminer.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(personnagePrincipale.GetComponent<personnagePrincipaleCtrl>().getPointVieActuelle() <= 0){
            Time.timeScale = 0;
            menuPerdant.SetActive(true);
        }
    }

    void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.tag == "personnagePrincipale"){
            Time.timeScale = 0;
            menuNiveauTerminer.SetActive(true);
        }
    }

}
