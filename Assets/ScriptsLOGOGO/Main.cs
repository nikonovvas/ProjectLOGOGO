 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main : MonoBehaviour
{

    public Player player;
    public Text Booktext;
    public Image[] hearts;
    public Sprite isLive, nonLive;

    public GameObject PauseScreen;
    public GameObject WinScreen;
    public GameObject LoseScreen;


    public void Reload_Lvl()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
        player.enabled = true;
    }
    public void Update()
    {
        if(Booktext != null) {
            Booktext.text = player.Book_info().ToString();
        }
        
        

        for (int i = 0; i < hearts.Length;i++)
        {
            if (player.Hearts_info() > i)
                hearts[i].sprite = isLive;
            else
                hearts[i].sprite = nonLive;
        }
    }

    public void Pause_On()
    {
        Time.timeScale = 0f;
        player.enabled = false;
        PauseScreen.SetActive(true);
    }
    public void Pause_Off()
    {
        Time.timeScale =1f;
        player.enabled = true;
        PauseScreen.SetActive(false);
    }

    public void Win()
    {
        Time.timeScale = 0f;
        player.enabled = false;

        WinScreen.SetActive(true);

        if (!PlayerPrefs.HasKey("Lvl")|| PlayerPrefs.GetInt("Lvl") <SceneManager.GetActiveScene().buildIndex) {
            PlayerPrefs.SetInt("Lvl", SceneManager.GetActiveScene().buildIndex);
        }

        if (PlayerPrefs.HasKey("book"))
            PlayerPrefs.SetInt("book", PlayerPrefs.GetInt("book") + player.Book_info());
        else
            PlayerPrefs.SetInt("book", player.Book_info());
         
        print("Books:"+PlayerPrefs.GetInt("book"));


    }



    public void Lose()
    {
        Time.timeScale = 0f;
        player.enabled = false;
        LoseScreen.SetActive(true);
    }

    public void MenuLvl()
    {
        Time.timeScale = 1f;
        player.enabled = true;
        SceneManager.LoadScene("Menu");
    }
    public void NextLvl()
    {
        Time.timeScale = 1f;
        player.enabled = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

}
