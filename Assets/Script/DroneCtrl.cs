using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneCtrl : MonoBehaviour
{
    [SerializeField]
    private int pointVie;
    [SerializeField]
    private Animator animation;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void LateUpdate(){
        gererAnimation();
        if(pointVie == 0){
            Destroy(this.gameObject, 0.2f);
        }
    }

    private void gererAnimation(){
        animation.SetInteger("pointVie", pointVie);
    }



    public void touche(){
        pointVie -= 1;
        Debug.Log(pointVie);
    }
}
