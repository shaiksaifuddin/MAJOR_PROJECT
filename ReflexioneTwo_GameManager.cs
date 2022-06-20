using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class ReflexioneTwo_GameManager : MonoBehaviour
{
    public GameObject entryCanvas;
    public void beginGame()
    {
        entryCanvas.SetActive(false);
    }

    
    public GameObject[] containers;
    public Text levelText;
    public Text highScoreText;
    public Text playerturn;

    private int level
    {
        get;    set;
    }
    private int highScore
    {
        get;    set;
    }
    private float duration
    {
        get;    set;
    }
    private GameObject[] gameChoosedContainers;
    private List<GameObject> playerChoosedContainers = new List<GameObject>();
    void Start()
    {
        level = 1;
        string[] lines = File.ReadAllLines("Assets/Resources/ReflexioneTwoResources/SavedData.txt");
        string[] splitted = lines[lines.Length - 1].Split('$');
        highScore = int.Parse(splitted[1]);
        highScoreText.text = highScore.ToString();
        levelText.text = level.ToString();
        duration = 1f;
    }
    public void choose(bool t)
    {
        if (t)
        {
            level = 1;
        }
        levelText.text = level.ToString();
        gameChoosedContainers = new GameObject[level];
        for (int i = 0; i < level; i++)
        {
            GameObject a = containers[Random.Range(0, 5)];
            gameChoosedContainers[i] = a;
        }
        StartCoroutine(changeColors());
    }
    IEnumerator changeColors()
    {
        for (int i = 0; i < level; i++)
        {
            playerturn.enabled = false;
            yield return new WaitForSeconds(1f);
            GameObject g = gameChoosedContainers[i];
            g.GetComponent<Container>().time = duration;
            yield return new WaitForSeconds(duration);
        }
        playerturn.enabled = true;

    }
    public void takeInput(GameObject gObject)
    {
        if(playerChoosedContainers.Count < level)
        {
            playerChoosedContainers.Add(gObject);
        }
        if(playerChoosedContainers.Count == level)
        {
            compare();
        }
    }
    void compare()
    {
        bool levelup = false;
        string _gameChoosed;
        for (int i = 0; i < playerChoosedContainers.Count; i++)
        {
            _gameChoosed = gameChoosedContainers[i].name;
            if(_gameChoosed == playerChoosedContainers[i].name)
            {
                levelup = true;
            }
            else
            {
                levelup = false;
            }
        }
        if (levelup)
        {
            level += 1;
            checkHighScore();
        }
        else if (!levelup)
        {
            level = 1;
        }
        choose(false);
        playerChoosedContainers.Clear();
    }

    void checkHighScore()
    {
        if(level > highScore)
        {
            highScore = level;
            print(highScore);
            highScoreText.text = highScore.ToString();
            string newline = "ReflexioneTwo$" + highScore.ToString();
            File.AppendAllText("Assets/Resources/ReflexioneTwoResources/SavedData.txt", newline + System.Environment.NewLine);
        }
    }
    public GameObject exitDialogueBox;
    public void ExitButtonClicked()
    {
        exitDialogueBox.SetActive(true);
    }
    public void cancelExit()
    {
        exitDialogueBox.SetActive(false);
    }
    public void exitToDesktop()
    {
        Application.Quit();
    }
    public void exitToHomeScreen()
    {
        SceneManager.LoadScene(0);
    }
}
