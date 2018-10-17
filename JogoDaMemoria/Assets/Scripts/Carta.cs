using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carta : MonoBehaviour {

    [SerializeField] GameController controller;
    [SerializeField] GameObject tampa;

    private void OnMouseDown()
    {
        if (tampa.activeSelf && controller.podeRevelar)
        {
            tampa.SetActive(false);
            controller.RevelacaoCarta(this);
        }

    }

    private int _id;
    public int id { get { return _id; } }

    //Metodo feito para mudar a imagem baseada no id da carta
    public void MudarImagem(int id, Sprite image)
    {
        _id = id;
        GetComponent<SpriteRenderer>().sprite = image; 
    }

    public void Desvirado()
    {
        tampa.SetActive(true);
    }

}
