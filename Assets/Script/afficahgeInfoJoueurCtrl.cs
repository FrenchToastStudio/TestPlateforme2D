using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class afficahgeInfoJoueurCtrl : MonoBehaviour            // <---Erreur dans le nom de la classe
{                                                                            //Aucune affichage n'est présente pour les infos du joueurs
    //afficahge nombreMunition
    [SerializeField]
    private GameObject compteurBalle;
    private Color couleurPrincipale;
    [SerializeField]
    GameObject[] listeAffichageMuntions;
    [SerializeField]
    private Slider barreDeVie;
    private int pointTotale = 0;
    private int pointMax = 999999;
    [SerializeField]
    private GameObject affichagePoint;

    // Start is called before the first frame update
    void Start()
    {
        couleurPrincipale = listeAffichageMuntions[1].GetComponent<Image>().color;
    }

    // Update is called once per frame
    void Update()
    {
        affichagePoint.GetComponent<Text>().text = "Point: " + pointTotale.ToString("D6");
    }

    public void updateNombreMunition(int muntionActuelle){
        for(int i = 0; i < muntionActuelle; i++){
            listeAffichageMuntions[i].GetComponent<Image>().color = Color.grey;
        }
    }

    public void resetNombreMunition(){
            foreach(GameObject unSpriteRenderer in listeAffichageMuntions){
                unSpriteRenderer.GetComponent<Image>().color = couleurPrincipale;
            }
    }

    public void mettreAjourVie(int pointVie){
        if(pointVie > 0){
            barreDeVie.value = pointVie;
        } else{
            barreDeVie.value = 0;
        }
        Debug.Log(pointVie);
    }

    public void resetPointVie(int pointVie){
        barreDeVie.maxValue = pointVie;
    }


    public void ajouterPoint(int point){
        if((pointTotale + point) > pointMax){
        pointTotale += point;
        } else {
            pointTotale = pointMax;
        }
    }

}
