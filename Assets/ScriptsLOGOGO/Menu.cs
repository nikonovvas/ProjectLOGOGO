using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Menu : MonoBehaviour
{

    public Button[] lvls;
    public Text books;
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.HasKey("Lvl"))
            for (int i=0; i < lvls.Length; i++)
            {
                if (i <= PlayerPrefs.GetInt("Lvl"))
                    lvls[i].interactable = true;
                else
                    lvls[i].interactable = false;
            }
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerPrefs.HasKey("book"))
            books.text = PlayerPrefs.GetInt("book").ToString();
        else
            books.text = "0";
    }

   public void OpenScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void DelKeys()
    {
        PlayerPrefs.DeleteAll();
        print("Clear");
    }
}
