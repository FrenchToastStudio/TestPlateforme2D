
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TourelleCtrl : MonoBehaviour
{
    private float tailleAxeX;
    [SerializeField]
    private int pointVie;
    private bool faceDroite;
    [SerializeField]
    private int valeurEnPoint;

    //parametre qui gere les tirs
    [SerializeField]
    private float decalageHorizontale;
    [SerializeField]
    private float decalageVertical;
    [SerializeField]
    private GameObject balle;
    [SerializeField]
    private float cadenceTir;
    private float minuteurTir;
    private float tempRealite = 1;

    [SerializeField]
    private GameObject personnagePrincipale;
    [SerializeField]
    private Animator animation;

    // Start is called before the first frame update
    void Start()
    {
        tailleAxeX = this.transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        //fait en sorte que la tour s'active que si elle est sur l'ecran (A compelter)
        if(Vector3.Distance(transform.position, personnagePrincipale.transform.position) < 10f){

            float positionHaut = personnagePrincipale.transform.position.y + personnagePrincipale.GetComponent<personnagePrincipaleCtrl>().getCorpActuelle().size.y;
            float positionBas = personnagePrincipale.transform.position.y - personnagePrincipale.GetComponent<personnagePrincipaleCtrl>().getCorpActuelle().size.y;
            if(positionHaut > this.transform.position.y){
                Debug.Log("true 1");
            }
            if(positionBas < this.transform.position.y){
                Debug.Log("true 2");
            }
            //permet a la tourelle de tirer
            if(positionHaut > this.transform.position.y && positionBas < this.transform.position.y){
                if(minuteurTir > cadenceTir){
                    tirer();
                    minuteurTir = 0;
                    Time.timeScale = tempRealite;
                }
            }

            //rajoute du temps au minuteur de tir
            minuteurTir += Time.deltaTime;

            //fait que la tourelle fait face au personnage principale
            if(personnagePrincipale.transform.position.x > this.transform.position.x){
                this.transform.localScale = new Vector2(-tailleAxeX, this.transform.localScale.y);
            } else {
                this.transform.localScale = new Vector2(tailleAxeX, this.transform.localScale.y);
            }

        }
    }
    void LateUpdate(){
        gererAnimation();
        if(pointVie <= 0){
            Destroy(this.gameObject, 0.2f);
        }
    }

    private void gererAnimation(){
        animation.SetInteger("pointVie", pointVie);
    }

    private void tirer(){
        Debug.Log("Genere la balle");
        GameObject clone = Instantiate(balle) as GameObject;

        clone.GetComponent<balleCtrl>().setTireur(this.tag);
        if(this.transform.localScale.x < 0){
            clone.transform.position = new Vector3(this.transform.position.x + decalageHorizontale, this.transform.position.y + decalageVertical, 0);
        } else{
            clone.transform.position = new Vector3(this.transform.position.x - decalageHorizontale, this.transform.position.y + decalageVertical, 0);
            clone.GetComponent<balleCtrl>().tirerGauche();
        }
    }

    public void touche(){
        Debug.Log("estTouche");
        pointVie -= 1;
    }

    public int getValeurEnPoint(){
            return valeurEnPoint;
    }
}
