using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DroneCtrl : MonoBehaviour
{
    //gere les tirs
    [SerializeField]
    GameObject balle;
    private float cadenceTir = 1;
    private float minuteurTir = 0.0f;
    private float tempRealite = 1.0f;
    [SerializeField]
    float decalageHorizontale= 5f;
    [SerializeField]
    float decalageVertical =  5f;
    [SerializeField]
    float vitesse = 0.1f;
    float vitesseActuelle;
    [SerializeField]
    private GameObject personnagePrincipale;
    [SerializeField]
    private int pointVie;
    [SerializeField]
    private Animator animation;
    private bool faceDroite;
    private float tailleAxeX;

    // Start is called before the first frame update
    void Start()
    {
        vitesseActuelle = vitesse;
        tailleAxeX = this.transform.localScale.x;
        Time.timeScale = tempRealite;
    }

    // Update is called once per frame
    void Update(){
        //lance le script seuelement si le drone est a une certaine Distance du personnage principale
        if(Vector3.Distance(transform.position, personnagePrincipale.transform.position) < 10f){
            gererAnimation();
            //rajoute du temps au minuteur de tir
            minuteurTir += Time.deltaTime;

            //fait que le drone fait face au personnage principale
            if(personnagePrincipale.transform.position.x > this.transform.position.x){
                faceDroite = true;
                this.transform.localScale = new Vector2(-tailleAxeX, this.transform.localScale.y);
            } else {
                faceDroite = false;
                this.transform.localScale = new Vector2(tailleAxeX, this.transform.localScale.y);
            }

            if(Input.GetButton("MouvementCourir")){
                vitesseActuelle = vitesse +vitesse *0.5f;
            }


            //peut etre qu'un jour nous aurons l'esquive
            // if(Input.GetAxisRaw("JoueurTir") > 0){
            //     System.Random rand = new System.Random();
            //     if(rand.Next(0, 9) == 1){
            //         Debug.Log("ESQUIVE");
            //         transform.position =  Vector3.MoveTowards(this.transform.position, new Vector3(personnagePrincipale.transform.position.x + 2f, personnagePrincipale.transform.position.y +2f, personnagePrincipale.transform.position.z), 0.3f);
            //     }
            // }

            //permet au drone de tirer
            if(personnagePrincipale.transform.position.y + personnagePrincipale.GetComponent<personnagePrincipaleCtrl>().getCorpActuelle().size.y/2 > this.transform.position.y && personnagePrincipale.transform.position.y - personnagePrincipale.GetComponent<personnagePrincipaleCtrl>().getCorpActuelle().size.y/2 < this.transform.position.y - 0.2f){
                if(minuteurTir > cadenceTir){
                    tirer();
                    minuteurTir = 0;
                    Time.timeScale = tempRealite;
                }
            }
            if(Vector3.Distance(transform.position, personnagePrincipale.transform.position) < 2f){
                if(faceDroite){
                    transform.position =  Vector3.MoveTowards(this.transform.position, new Vector3(personnagePrincipale.transform.position.x + -3f, personnagePrincipale.transform.position.y, personnagePrincipale.transform.position.z), vitesseActuelle);
                } else {
                    transform.position =  Vector3.MoveTowards(this.transform.position, new Vector3(personnagePrincipale.transform.position.x + 3f, personnagePrincipale.transform.position.y, personnagePrincipale.transform.position.z), vitesseActuelle);
                }
            } else if( Vector3.Distance(transform.position, personnagePrincipale.transform.position) > 3f){
                    transform.position =  Vector3.MoveTowards(this.transform.position, new Vector3(personnagePrincipale.transform.position.x, personnagePrincipale.transform.position.y, personnagePrincipale.transform.position.z), vitesseActuelle);
            }
        }
    }

    void LateUpdate(){
        gererAnimation();
        //detruit le drone si il elle n'a plus de point de vie
        if(pointVie <= 0){
            Destroy(this.gameObject, 0.2f);
        }
    }

    private void gererAnimation(){
        animation.SetInteger("pointVie", pointVie);
    }



    public void touche(){
        pointVie -= 1;
    }

    private void tirer(){
        GameObject clone = Instantiate(balle) as GameObject;

        clone.GetComponent<balleCtrl>().setTireur(this.tag);
        if(this.transform.localScale.x < 0){
            clone.transform.position = new Vector3(this.transform.position.x + decalageHorizontale, this.transform.position.y + decalageVertical, 0);
        } else{
            clone.transform.position = new Vector3(this.transform.position.x - decalageHorizontale, this.transform.position.y + decalageVertical, 0);
            clone.GetComponent<balleCtrl>().tirerGauche();
        }
    }
}
