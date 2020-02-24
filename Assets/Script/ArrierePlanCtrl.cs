using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrierePlanCtrl : MonoBehaviour
{
    [SerializeField]
    GameObject[] ArrierePlan;
    private Camera cameraJoueur;
    private Vector2 tailleEcran;
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
        int childNeded = (int)Mathf.Ceil(tailleEcran.x * 2 / largeurSprite);
        GameObject clone = Instantiate(spriteArrierePlan) as GameObject;
        for(int i = 0; i <= childNeded; i++){
            GameObject c = Instantiate(clone) as GameObject;
            c.transform.position = new Vector3(largeurSprite * i, spriteArrierePlan.transform.position.y, spriteArrierePlan.transform.position.z);
            c.name = spriteArrierePlan.name + i;
        }
        Destroy(clone);
        Destroy(spriteArrierePlan.GetComponent<SpriteRenderer>());
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
