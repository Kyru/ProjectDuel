﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    [SerializeField] private GameObject blue_hearts;
    [SerializeField] private GameObject yellow_hearts;
    [SerializeField] private Sprite empty_heart;
    [SerializeField] private Image Blue_PBar;
    [SerializeField] private Image Yellow_PBar;
    [SerializeField] private Text victory_text;
    [SerializeField] private Button restart_Button;

    private List<GameObject> blue_hearts_list;
    private List<GameObject> yellow_hearts_list;


    void Start()
    {
        Messenger<int>.AddListener(GameEvent.BLUE_HURT, blue_remove_heart);
        Messenger<int>.AddListener(GameEvent.YELLOW_HURT, yellow_remove_heart);
        Messenger<double>.AddListener(GameEvent.BLUE_BAR, Blue_charge);
        Messenger<double>.AddListener(GameEvent.YELLOW_BAR, Yellow_charge);
        Messenger.AddListener(GameEvent.BLUE_DIES, Yellow_wins);
        Messenger.AddListener(GameEvent.YELLOW_DIES, Blue_wins);

        blue_hearts_list = new List<GameObject>();
        foreach (Transform child in blue_hearts.transform)
        {
            blue_hearts_list.Add(child.gameObject);
        }

        yellow_hearts_list = new List<GameObject>();
        foreach (Transform child in yellow_hearts.transform)
        {
            yellow_hearts_list.Add(child.gameObject);
        }
    }

    public void blue_remove_heart(int health)
    {
        blue_hearts_list[health].GetComponent<Image>().sprite = empty_heart;
    }
    public void yellow_remove_heart(int health)
    {
        yellow_hearts_list[health].GetComponent<Image>().sprite = empty_heart;
    }

    public void Blue_charge(double charge)
    {
        Blue_PBar.fillAmount = (float)charge;
    }

    public void Yellow_charge(double charge)
    {
        Yellow_PBar.fillAmount = (float)charge;
    }

    public void Blue_wins()
    {
        victory_text.text = "Blue Wins!";
        restart_Button.gameObject.SetActive(true);
    }

    public void Yellow_wins()
    {
        victory_text.text = "Yellow Wins!";
        restart_Button.gameObject.SetActive(true);
    }

    public void restart()
    {
        SceneManager.LoadScene("FirstScene");
    }
}
