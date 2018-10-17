using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;


public class InformationController : MonoBehaviour {

    [SerializeField] Text marcadorScoreFinal;
    [SerializeField] Text marcadorComboFinal;
    [SerializeField] InputField nomeInput;
    

    private int scoreFinal;
    private int comboFinal;
    private string nomeUsuario;

    //Iniciando criacao do arquivo JSON
    private string dataP;
    Informacao info1;


    // Use this for initialization
    void Start () {
        scoreFinal =  GameController.score;
        comboFinal = GameController.combo;

        marcadorComboFinal.text = comboFinal.ToString();
        marcadorScoreFinal.text = scoreFinal.ToString();


        info1 = new Informacao();
        

    }
    private void Update()
    {
        nomeUsuario = nomeInput.text;
        
    }
    public void Salvando()
    {
        //Arquivo de save

        info1.nome = nomeUsuario;
        info1.score = scoreFinal;
        info1.comboMaximo = comboFinal;
        dataP = Path.Combine(Application.dataPath, "ArquivoInformacoes.json");
        string jsonString = JsonUtility.ToJson(info1);
        File.WriteAllText(dataP, jsonString);

        //StartCoroutine(Upload());
        Request();
    }





    //--------------------------------------

    public Text respostaText;
    //IEnumerator Upload()
    //{
    //    WWWForm form = new WWWForm();

    //    byte[] rawFormData = form.data;
    //    form.AddBinaryData("fileUpload", rawFormData, "screenShot.png", "ArquivoInformacoes.json");
    //    //form.AddField("myField", "myData");

    //    //    

    //    using (UnityWebRequest www = UnityWebRequest.Post("https://us-central1-huddle-team.cloudfunctions.net/api/memory/lcosendey@gmail.com", form))
    //    {
    //        yield return www.SendWebRequest();

    //        if (www.isNetworkError || www.isHttpError)
    //        {
    //            Debug.Log(www.error);
    //        }
    //        else
    //        {
    //            Debug.Log("Form upload complete!");
    //        }
    //    }
    //}
    private string endereco = "https://us-central1-huddle-team.cloudfunctions.net/api/memory/lcosendey@gmail.com";
    public void Request()
    {
        WWWForm form = new WWWForm();

        Dictionary<string, string> headers = form.headers;
        //form.AddField("username", nomeUsuario);
        //form.AddField("score", scoreFinal);
        //form.AddField("combo", comboFinal);
        //form.AddBinaryData
        string jsonString = File.ReadAllText(dataP);
        UnityWebRequest.Put(endereco, jsonString);
        byte[] rawFormData = form.data;

        WWW request = new WWW(endereco, null, headers);
        StartCoroutine(OnRensponce(request));
        
    }
    private IEnumerator OnRensponce(WWW req)
    {
        yield return req;
        respostaText.text = req.text;
    }
}

public class Informacao
{
    public string nome;
    public int score;
    public int comboMaximo;

}
