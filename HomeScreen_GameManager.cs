using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class HomeScreen_GameManager : MonoBehaviour
{
    public AudioClip[] audioClips;

    public GameObject homeScreenTopPart;
    public Text t;
    public Animator[] anims;

    private AudioSource source;
    private void Start()
    {
        source =  GetComponent<AudioSource>();
    }
    public void slide()
    {
        playsound();
        homeScreenTopPart.GetComponent<Image>().enabled = false;
        homeScreenTopPart.GetComponent<Button>().enabled = false;
        anims[0].SetTrigger("EntryButtonClicked");
        anims[1].SetTrigger("one");
        anims[2].SetTrigger("two");
        anims[3].SetTrigger("three");
        t.enabled = (false);
    }

    private void playsound()
    {
        source.PlayOneShot(audioClips[0]);
    }

    public void loadScene(int sceneIndex)
    {
        playsound();
        SceneManager.LoadScene(sceneIndex);
    }
    public void exitButtonClicked()
    {
        playsound();
        Application.Quit();
    }
}
