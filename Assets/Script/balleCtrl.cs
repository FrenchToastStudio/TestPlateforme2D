using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class balleCtrl : MonoBehaviour
{
    private float vitesse = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 3);
    }

    // Update is called once per frame
    void Update()
    {
            this.transform.Translate(new Vector2(vitesse, 0));
    }

    void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.tag == "drone"){
            col.gameObject.GetComponent<DroneCtrl>().touche();
            Destroy(this.gameObject);
         }
        //else if(col.gameObject.tag == "personnagePrincipale"){
        //
        //     Destroy(this.gameObject);
        // } else if (col.gameObject.tag == "tourelle"){
        //
        // }
    }

    public void tirerGauche(){
        vitesse = -vitesse;
        this.transform.localScale = new Vector3(-this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z);
    }
}
