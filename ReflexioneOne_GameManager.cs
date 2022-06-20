using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class ReflexioneOne_GameManager : MonoBehaviour
{
    public GameObject objectToDisable;

    public void disableObject()
    {
        objectToDisable.SetActive(false);
    }

    //Timer
    private float time
    {
        get;    set;
    }
    public Text timerText;
    private int int_time;
    private void Update()
    {
        if(time > 0)
        {
            time -= Time.deltaTime;
        }
        int_time = (int)time;
        timerText.text = int_time.ToString();
    }
    //Initialize Board
    private int difficulty
    {
        get;    set;
    }
    public void getDifficulty(int _difficulty)
    {
        difficulty = _difficulty;
    }

    public GameObject enviroment;
    public GameObject prefab;
    public void startSettingUpEnviroment()
    {
        time = 60;
        while(enviroment.transform.childCount > 0)
        {
            DestroyImmediate(enviroment.transform.GetChild(0).gameObject);
        }
        enviroment.GetComponent<GridLayoutGroup>().constraintCount = difficulty;
        for (int i = 0; i < difficulty * difficulty; i++)
        {
            GameObject g = Instantiate(prefab, enviroment.transform);
            g.name = i.ToString();
        }
        StartCoroutine(chooseObjects());
    }

    private IEnumerator chooseObjects()
    {
        int objectCount = enviroment.transform.childCount;
        if(time > 0)
        {
            enviroment.transform.GetChild(Random.Range(0, enviroment.transform.childCount - 1)).GetComponent<box>().time = 1f;
            yield return new WaitForSeconds(1f);
            StartCoroutine(chooseObjects());
        }
    }
    public Text scoreText;
    public Text highScoreText;
    public void increaseScore()
    {
        int hscore = int.Parse(highScoreText.text);
        int sscore = int.Parse(scoreText.text) + 1;
        if(sscore > hscore)
        {
            highScoreText.text = sscore.ToString();
            string newline = "ReflexioneOne$" + highScoreText.text;
            File.AppendAllText("Assets/Resources/ReflexioneOneResources/SaveData.txt", newline + System.Environment.NewLine);
        }
        else
        {
            scoreText.text = sscore.ToString();
        }
    }
    public void decreaseScore()
    {
        scoreText.text = ((int.Parse(scoreText.text)) - 1).ToString();
    }
    public void ResetGame()
    {
        SceneManager.LoadScene(2);
    }
    private void Start()
    {
        string[] lines = File.ReadAllLines("Assets/Resources/ReflexioneOneResources/SaveData.txt");
        string[] splitted = lines[lines.Length - 1].Split('$');
        highScoreText.text = splitted[1];
    }
    public void ExitToHomeScreen()
    {
        SceneManager.LoadScene(0);
    }
}
