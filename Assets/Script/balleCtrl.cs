using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class balleCtrl : MonoBehaviour
{
    private float vitesse = 0.3f;
    private string tirreur;

    // Start is called before the first frame update
    void Start() {
        Destroy(this.gameObject, 3);
    }

    // Update is called once per frame
    void Update() {
            this.transform.Translate(new Vector2(vitesse, 0));
    }

    void OnTriggerEnter2D(Collider2D col) {
        if(col.gameObject.tag == "drone" && tirreur != "drone" && tirreur != "tourelle") {
            col.gameObject.GetComponent<DroneCtrl>().touche();
            this.GetComponent<Animator>().SetBool("touche", true);
            Destroy(this.gameObject, 0.25f);
            vitesse = 0;
            GameObject.FindGameObjectWithTag("afficheurJoueur").GetComponent<afficahgeInfoJoueurCtrl>().ajouterPoint(col.gameObject.GetComponent<DroneCtrl>().getValeurEnPoint());
        } else if(col.gameObject.tag == "personnagePrincipale" && tirreur != "personnagePrincipale") {
             col.gameObject.GetComponent<personnagePrincipaleCtrl>().touche();
             Destroy(this.gameObject, 0.25f);
             this.GetComponent<Animator>().SetBool("touche", true);
             vitesse = 0;
         }
         else if (col.gameObject.tag == "tourelle" && tirreur != "drone" && tirreur != "tourelle") {
             col.gameObject.GetComponent<TourelleCtrl>().touche();
             this.GetComponent<Animator>().SetBool("touche", true);
             Destroy(this.gameObject, 0.25f);
             vitesse = 0;
             GameObject.FindGameObjectWithTag("afficheurJoueur").GetComponent<afficahgeInfoJoueurCtrl>().ajouterPoint(col.gameObject.GetComponent<TourelleCtrl>().getValeurEnPoint());
         }
    }

    public void tirerGauche() {
        vitesse = -vitesse;
        this.transform.localScale = new Vector3(-this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z);
    }

    public void setTireur(string unTagTirreur) {
        tirreur = unTagTirreur;
    }
}
