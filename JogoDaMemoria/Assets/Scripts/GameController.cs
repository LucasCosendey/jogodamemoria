//using System;
using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public int linhasGrid = 2;
    public int colunasGrid = 4;

    //Distancia entre as cartas
    public float offsetX = 4f;
    public float offsetY = 5f;

    [SerializeField] Carta cartaOriginal;
    [SerializeField] Sprite[] imagens;
    public int[] numeros = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8 };
    public float tempoEspera = 0.5f;

    private bool podeCombo = false;
    public static int combo = 0;
    public static int score = 0;

    //Quantidade de pontos
    public int scoreSimples = 10;
    public int scoreCombo = 30;

    private Carta primeiraVirada;
    private Carta segundaVirada;

    public static GameController instance;

    [SerializeField] Text scoreMarcador;
    [SerializeField] Text comboMarcador;
    //quantos pares 9 
    // Precisa colocar o numero de pares aqui
    
    private void Start()
    {
        DontDestroyOnLoad(this);
        Vector3 posInicial = cartaOriginal.transform.position;

        // Precisa colocar o numero de pares aqui
        numeros = Embaralhar(numeros);

        for(int i = 0; i < colunasGrid; i++)
        {
            for(int j = 0; j < linhasGrid; j++)
            {
                Carta carta;
                if(i == 0 && j == 0)
                {
                    carta = cartaOriginal;
                }
                else
                {
                    carta = Instantiate(cartaOriginal) as Carta;
                }

                int index = j * colunasGrid + i;
                int id = numeros[index];
                carta.MudarImagem(id, imagens[id]);

                float posX = (offsetX * i) + posInicial.x;
                float posY = (offsetY * j) + posInicial.y;

                carta.transform.position = new Vector3(posX, posY, posInicial.z);
            }
        }
    }

    private int[] Embaralhar(int [] numeros)
    {
        int[] newArray = numeros.Clone() as int[];
        for(int i = 0; i < newArray.Length; i++)
        {
            int tmp = newArray[i];
            int r = Random.Range(i, newArray.Length);
            newArray[i] = newArray[r];
            newArray[r] = tmp;
        }
        return newArray;
    }


    public bool podeRevelar { get { return segundaVirada == null; } }

    public void RevelacaoCarta(Carta carta)
    {
        if(primeiraVirada == null)
        {
            primeiraVirada = carta;
        }
        else
        {
            segundaVirada = carta;
            StartCoroutine(ChecarPar());
            
        }
    }

 

    private IEnumerator ChecarPar()
    {
        
        if(primeiraVirada.id == segundaVirada.id)
        {
            
            
            
            if (podeCombo)
            {
                score += scoreCombo;
                combo++;
                comboMarcador.text = combo.ToString();
            }
            else
            {
                score += scoreSimples;
                
                podeCombo = true;                
                Debug.Log("COMBO LIBERADO");
                
            }
            scoreMarcador.text = score.ToString();
        }
        else //Aqui é se as pecas não forem iguais
        {
            yield return new WaitForSeconds(tempoEspera);
            primeiraVirada.Desvirado();
            segundaVirada.Desvirado();
            podeCombo = false;
            Debug.Log("COMBO BLOQUEADO");
            

        }

        primeiraVirada = null;
        segundaVirada = null;
    }
   
}
