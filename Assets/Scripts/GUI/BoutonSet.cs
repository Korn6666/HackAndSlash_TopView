using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoutonSet : MonoBehaviour
{
    [SerializeField] private Sprite normalButton;
    [SerializeField] private Sprite clickedButton;

    [SerializeField] private GameObject button1;
    [SerializeField] private GameObject button2;
    [SerializeField] private GameObject button3;

    // Start is called before the first frame update
    void Start()
    {
        SetButton1();
    }


    //Pas très jolie comme façon de faire, mais je n'ai pas trop le temps de l'optimiser
    public void SetButton1()
    {
        button1.GetComponent<Image>().sprite = clickedButton;
        button2.GetComponent<Image>().sprite = normalButton;
        button3.GetComponent<Image>().sprite = normalButton;
    }

    public void SetButton2()
    {
        button1.GetComponent<Image>().sprite = normalButton;
        button2.GetComponent<Image>().sprite = clickedButton;
        button3.GetComponent<Image>().sprite = normalButton;
    }

    public void SetButton3()
    {
        button1.GetComponent<Image>().sprite = normalButton;
        button2.GetComponent<Image>().sprite = normalButton;
        button3.GetComponent<Image>().sprite = clickedButton;
    }
}
