using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    [SerializeField]
    GameObject personnagePrincipale;




    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(personnagePrincipale.transform.position.x, personnagePrincipale.transform.position.y, -10);

    }
}
