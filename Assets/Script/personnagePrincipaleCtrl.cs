using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class personnagePrincipaleCtrl : MonoBehaviour
{
    float decalageHorizontale;
    float decalageVertical;
    private int pointVieActuelle;
    [SerializeField]
    private int pointVieMax;

    //parametre qui gere les tir de balle
    [SerializeField]
    private GameObject balle;
    [SerializeField]
    private int munitionMaximum = 3;
    private int munition = 0;
    private bool peutTirer = true;
    [SerializeField]
    private float tempRecharge = 2f;
    [SerializeField]
    private float cadenceTir = 2;
    private float minuteurTir = 0.0f;
    private float tempRealite = 1.0f;
    [SerializeField]
    private GameObject gereurAffichage;

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
    [SerializeField]
    private BoxCollider2D corp;
    [SerializeField]
    private BoxCollider2D corpAccroupi;
    private float tailleColliderY;

    // Start is called before the first frame update
    void Start()
    {
        pointVieActuelle = pointVieMax;
        rb = GetComponent<Rigidbody2D>();;
        tailleAxeX = this.transform.localScale.x;
        Time.timeScale = tempRealite;
        tailleColliderY = corp.size.y;
        gereurAffichage.GetComponent<afficahgeInfoJoueurCtrl>().resetPointVie(pointVieMax);
    }
    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale == 1){
            gereurAffichage.GetComponent<afficahgeInfoJoueurCtrl>().mettreAjourVie(pointVieActuelle);
            //rajoute du temps au minuteur de tir
                minuteurTir += Time.deltaTime;

                enCourse = Input.GetButton("MouvementCourir");
                vitesseActuelle = vitesseAuSol * Input.GetAxisRaw("MouvementHorizontale");


                //fait monter le personnage lorsque la touche de saut est appuyer}
                if(Input.GetButtonDown("MouvementSaut") && enSaut == false && enChute == false){
                    sauter();
                }
                //recharge si le personnage n<a plus de balle
                if(munition > munitionMaximum){
                    animation.SetBool("chargeurVide", true);
                    if(minuteurTir > tempRecharge){
                        peutTirer = true;
                        minuteurTir = 0;
                        Time.timeScale = tempRealite;
                        munition = 0;
                        animation.SetBool("chargeurVide", false);
                        gereurAffichage.GetComponent<afficahgeInfoJoueurCtrl>().resetNombreMunition();
                    }
                } else {
                    if(minuteurTir > cadenceTir){
                        peutTirer = true;
                        minuteurTir -= cadenceTir;
                        Time.timeScale = tempRealite;
                    }
                }

                //gere les tir du hero
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
                if(enCourse){
                    vitesseActuelle += vitesseActuelle * 0.5f;
                }

                //change ou le personnage facDroneCtrle
                if(Input.GetAxisRaw("MouvementHorizontale") < 0){
                    this.transform.localScale = new Vector2(-tailleAxeX, this.transform.localScale.y);
                } else if (Input.GetAxisRaw("MouvementHorizontale") > 0){
                    this.transform.localScale = new Vector2(tailleAxeX, this.transform.localScale.y);
                }
                if(estAccroupi){
                    enablecorpAccroupi();
                } else {
                    enableCorp();
                }

                Deplacer();

                rafrachirEtatJoueur();

                gererAnimation();
            }

    }

    //depalce le personnage de gauche a droite
    void Deplacer(){
        this.transform.Translate(new Vector2(vitesseActuelle, 0));
    }

    //m/thode qui permet au joeur de tirer
    public void tirer(){
        animation.SetBool("enTir", true);
        if(enCourse || auSol ==false || enSaut){
        } else{
            vitesseActuelle = 0;
        }
        if(peutTirer){
            GameObject clone = Instantiate(balle) as GameObject;
            decalageHorizontale = 0.4f;
            if(estAccroupi){
                decalageVertical = -0.26f;
            } else if(enCourse){
                decalageHorizontale = 0.9f ;
                decalageVertical = 0.12f;
            } else{
                decalageHorizontale = 0.25f;
                decalageVertical = 0.25f;
            }
            clone.GetComponent<balleCtrl>().setTireur(this.tag);
            if(this.transform.localScale.x < 0){
                clone.transform.position = new Vector3(this.transform.position.x - decalageHorizontale, this.transform.position.y + decalageVertical, 0);
                clone.GetComponent<balleCtrl>().tirerGauche();
            } else{
                clone.transform.position = new Vector3(this.transform.position.x + decalageHorizontale, this.transform.position.y + decalageVertical, 0);
            }
            munition += 1;
            gereurAffichage.GetComponent<afficahgeInfoJoueurCtrl>().updateNombreMunition(munition);
            clone.name = balle.name + munition;
            Time.timeScale = tempRealite;
            peutTirer = false;
        }
    }

    //Permet de decider si le personnage tombe, est au sol, ou si il saute
    public void rafrachirEtatJoueur(){                                                      // rafraichir est mal écrit
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

    public void touche(){
        Debug.Log("toucher");
        pointVieActuelle -= 1;
    }

    //fonction qui retourne la hitbox actuelle du personnage
    public BoxCollider2D getCorpActuelle(){
        if(this.corp.enabled){
            return this.corp;
        } else {
            return this.corpAccroupi;
        }
    }

    //retourne le nombre de point de vie que le personnage a
    public int getPointVieActuelle(){
        return pointVieActuelle;
    }

    //fonctione qui change la hitbox pour le sprite non accroupi
    private void enableCorp(){
        this.corpAccroupi.enabled = false;
        this.corp.enabled = true;
    }
    //fonctione qui change la hitbox pour le sprite accroupi
    private void enablecorpAccroupi(){                                                                 // Le 'c' n'est pas en majuscule dans le nom de la méthode
        this.corpAccroupi.enabled = true;
        this.corp.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D col){
        //detecte la collsion avec les medpack et rempli la vie du joueur
        if(col.gameObject.tag == "medpack"){
            pointVieActuelle = pointVieMax;
            Destroy(col.gameObject);
        }

        //detecte la collision si le joueur tombe trop bas
        if(col.gameObject.tag == "limiteJeu"){
            pointVieActuelle = 0;
        }
    }
}
