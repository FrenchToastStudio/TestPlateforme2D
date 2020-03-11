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
    private int childNeded;
    private List<ArrierePlan> arrierePlans = new List<ArrierePlan>();
    //ArrierePlan
    public class ArrierePlan{
        private int cadre{get; set;}
        private GameObject image{get; set;}

        public ArrierePlan(int cadre, GameObject image){
            this.cadre = cadre;
            this.image = image;
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
        float largeurSprite = spriteArrierePlan.GetComponent<SpriteRenderer>().bounds.size.x;
        childNeded = (int)Mathf.Ceil(tailleEcran.x * 2 / largeurSprite);
        GameObject clone = Instantiate(spriteArrierePlan) as GameObject;
        genèreArrièrePlan(clone);
        Destroy(clone);
        Destroy(spriteArrierePlan.GetComponent<SpriteRenderer>());
    }
    // Update is called once per frame
    void Update()
    {
        gererArrièrePlan();
    }

    void genèreArrièrePlan(GameObject clone){
        for(int i = 0; i <= childNeded; i++){
            GameObject c = Instantiate(clone) as GameObject;
            c.transform.position = new Vector3(largeurSprite * i, spriteArrierePlan.transform.position.y, spriteArrierePlan.transform.position.z);
            c.name = spriteArrierePlan.name + i;
            arrierePlans.add(new ArrierePlan(i, c))
        }
    }

    void gèreArrièrePlan(){
        foreach(ArrierePlan spriteArrierePlan in arrierePlans){
            
        }
    }
}
