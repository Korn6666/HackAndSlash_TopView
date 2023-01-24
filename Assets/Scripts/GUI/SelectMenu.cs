using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectMenu : MonoBehaviour
{
    [SerializeField] GameObject warrior;
    [SerializeField] GameObject mage;
    [SerializeField] GameObject inge;

    private int arenaIndex;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        GameManager.instance.isPaused = false;
        SetArena(1);
        SetClasses("warrior");
    }

    public void SetClasses(string name)
    {
        GameManager.instance.playerClasse = name;

        if(name == "warrior")
        {
            warrior.SetActive(true);
            mage.SetActive(false);
            inge.SetActive(false);
        }

        if (name == "mage")
        {
            warrior.SetActive(false);
            mage.SetActive(true);
            inge.SetActive(false);
        }

        if (name == "inge")
        {
            warrior.SetActive(false);
            mage.SetActive(false);
            inge.SetActive(true);
        }
    }


    public void SetArena(int index)
    {
        arenaIndex = index;
    }

    
    public void OnePlay()
    {
        SceneManager.LoadScene(arenaIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
