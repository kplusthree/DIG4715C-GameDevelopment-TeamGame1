using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public AudioClip menuMusic;
    public AudioSource musicSource;

    void Start()
    {
        musicSource.clip = menuMusic;
        musicSource.Play();
    }

    public void PlayGame(string LevelName)
    {
        SceneManager.LoadScene(LevelName);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
