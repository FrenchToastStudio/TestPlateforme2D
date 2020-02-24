using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class personnagePrincipaleCtrl : MonoBehaviour
{

    //parametre qui gere les mouvement a l'honrizontale
    [SerializeField]
    private float vitesseAuSol = 0.1f;
    private float vitesseActuelle = 0;
    private float tailleAxeX;
    private bool enCourse;
    //Parametre qui gere les saut
    [SerializeField]
    private float hauteurDuSaut = 10f;
    private bool enSaut = false;
    private bool auSol = true;
    private bool enChute = false;
    //autre parametre
    private bool estAccroupi;
    [SerializeField]
    private Animator animation;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();;
        tailleAxeX = this.transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
            enCourse = Input.GetButton("MouvementCourir");
            vitesseActuelle = vitesseAuSol * Input.GetAxisRaw("MouvementHorizontale");


            //fait monter le personnage lorsque la touche de saut est appuyer}
            if(Input.GetButtonDown("MouvementSaut") && enSaut == false && enChute == false){
                sauter();
            }

            //gere les tir du hero
            Debug.Log(auSol);
            if(Input.GetAxisRaw("JoueurTir") > 0){
                tirer();
            } else{
                animation.SetBool("enTir", false);
            }
            //permet au personnage de saccroupir Accroupir
            if(Input.GetAxisRaw("MouvementAcroupir") > 0){
                estAccroupi = true;
                vitesseActuelle = 0;
            } else {
                estAccroupi = false;
            }
            //augmente la vitesse du jouer si il touche la touche pour courir
            if(enCourse ){
                vitesseActuelle += vitesseActuelle * 0.5f;
            }

            //change ou le personnage face
            if(Input.GetAxisRaw("MouvementHorizontale") < 0){
                this.transform.localScale = new Vector2(-tailleAxeX, this.transform.localScale.y);
            } else if (Input.GetAxisRaw("MouvementHorizontale") > 0){
                this.transform.localScale = new Vector2(tailleAxeX, this.transform.localScale.y);
            }

            Deplacer();

            rafrachirEtatJoueur();

            gererAnimation();
    }

    //depalce le personnage de gauche a droite
    public void Deplacer(){
        this.transform.Translate(new Vector2(vitesseActuelle, 0));
    }

    public void tirer(){
        animation.SetBool("enTir", true);
        if(enCourse || auSol == false || enSaut){

        } else{
            vitesseActuelle = 0;
        }
    }

    //Permet de decider si le personnage tombe, est au sol, ou si il saute
    public void rafrachirEtatJoueur(){
        //personnage tombe
        if(rb.velocity.y < -0.001f){
            auSol = false;
            enChute = true;
        }
        //est en train de sauter
        else if (rb.velocity.y > 0.001f){
            enSaut = true;
            auSol = false;
        }
        //est au sol
        else{
            auSol = true;
            enChute = false;
            enSaut = false;
        }
    }

    //gere les animations
    public void gererAnimation(){

        animation.SetBool("estAccroupi", estAccroupi);
        animation.SetFloat("speed", Mathf.Abs(vitesseActuelle));
        animation.SetBool("enSaut", enSaut);
        animation.SetBool("auSol", auSol);
        animation.SetBool("enChute", enChute);
        animation.SetBool("enCourse", enCourse);
    }

    public void sauter(){
        rb.velocity = new Vector2(0.0f, hauteurDuSaut);
    }


}
