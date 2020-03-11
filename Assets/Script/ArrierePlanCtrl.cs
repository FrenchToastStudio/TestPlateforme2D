using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrierePlanCtrl : MonoBehaviour
{
    [SerializeField]
    GameObject[] ArrierePlan;
    [SerializeField]
    GameObject personnagePrincipale;
    private Camera cameraJoueur;
    private Vector2 tailleEcran;
    private int nombreCadreVoulu;
    private List<Cadre> cadres = new List<Cadre>();
    private float largeurSprite;
    //objet cadre
    public class Cadre{
        private int numero{get; set;}
        private List<GameObject> arrièrePlans{get; set;}


        public List<GameObject> getArrièrePlan(){
            return arrièrePlans;
        }

        public void setArrièrePlan(List<GameObject> arrièrePlans){
            this.arrièrePlans = arrièrePlans;
        }

        public Cadre(int numero, List<GameObject> arrièrePlans){
            this.numero = numero;
            this.arrièrePlans = arrièrePlans;
        }

        //ajoute un arriere plan a la liste d'arrière plan
        public void ajouterArrièreplan(GameObject arrièrePlan){
            arrièrePlans.Add(arrièrePlan);
        }
    }

    // Start is called before the first frame update
    void Start()

    {
        cameraJoueur = gameObject.GetComponent<Camera>();
        tailleEcran = cameraJoueur.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, cameraJoueur.transform.position.z));
        foreach(GameObject spriteArrierePlan in ArrierePlan){
            chargerArrierePlan(spriteArrierePlan);
        }
    }

    void chargerArrierePlan(GameObject spriteArrierePlan){
        largeurSprite = spriteArrierePlan.GetComponent<SpriteRenderer>().bounds.size.x;
        nombreCadreVoulu = (int)Mathf.Ceil(tailleEcran.x * 2 / largeurSprite);
        GameObject clone = Instantiate(spriteArrierePlan) as GameObject;
        générerCadre();
        genèreArrièrePlan(clone, spriteArrierePlan);
        Destroy(clone);
        Destroy(spriteArrierePlan.GetComponent<SpriteRenderer>());
    }
    // Update is called once per frame
    void Update()
    {
        gèreArrièrePlan();
    }

    void genèreArrièrePlan(GameObject clone, GameObject spriteArrierePlan) {
        for(int i = 0; i <= nombreCadreVoulu; i++) {
            clone.transform.position = new Vector3(largeurSprite * i, spriteArrierePlan.transform.position.y, spriteArrierePlan.transform.position.z);
            GameObject c = Instantiate(clone) as GameObject;
            c.name = spriteArrierePlan.name + i;
            cadres[i].ajouterArrièreplan(c);
        }
    }

    void générerCadre() {
        for(int i = 0; i > nombreCadreVoulu; i++) {
            cadres.Add(new Cadre(nombreCadreVoulu, new List<GameObject>()));
        }
    }

    void gèreArrièrePlan() {
        int objetHorsPlan = 0;
        foreach(Cadre cadre in cadres) {
            objetHorsPlan = 0;
            foreach(GameObject arrièrePlan in cadre.getArrièrePlan()){
                if(vérifierPostionArrièrePlan(arrièrePlan))
                    objetHorsPlan += 1;
            }
            if(objetHorsPlan >= cadre.getArrièrePlan().Count){
                bougerCadre(cadre);
            }
        }
    }

    //retourne vrai si l'ArrierePlan est hors de l'ecran
    bool vérifierPostionArrièrePlan(GameObject arrièrePlan) {
        if(personnagePrincipale.transform.localScale.x > 0) {
            if(arrièrePlan.transform.position.x > personnagePrincipale.transform.position.x + Screen.width/2) {
                return true;
            } else {
                return false;
            }
        } else if (personnagePrincipale.transform.localScale.x < 0) {
            if(arrièrePlan.transform.position.x < personnagePrincipale.transform.position.x - Screen.width/2) {
                return true;
            } else {
                return false;
            }
        }
        return false;
    }

    //bouge le cadre plus en avant du joueur
    void bougerCadre(Cadre cadre){
        foreach(GameObject arrièrePlan in cadre.getArrièrePlan()){
            if(personnagePrincipale.transform.localScale.x > 0) {
                arrièrePlan.transform.position = new Vector3(personnagePrincipale.transform.position.x + Screen.width, arrièrePlan.transform.position.y, arrièrePlan.transform.position.z);
            }
            if (personnagePrincipale.transform.localScale.x < 0) {
                arrièrePlan.transform.position = new Vector3(personnagePrincipale.transform.position.x - Screen.width, arrièrePlan.transform.position.y, arrièrePlan.transform.position.z);
            }
        }
    }

}
