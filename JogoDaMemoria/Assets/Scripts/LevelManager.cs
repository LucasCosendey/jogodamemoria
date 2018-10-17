using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    

    public void Recomecar(string nome)
    {
        SceneManager.LoadScene(nome);
        GameController.combo = 0;
        GameController.score = 0;
    }

    public void Final(string nome)
    {
        SceneManager.LoadScene(nome);
    }
}
