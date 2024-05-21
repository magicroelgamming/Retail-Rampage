using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.Video;

public class TutorialMinigame : MonoBehaviour
{
    private VideoPlayer _player;
    public VideoSource[] Sources;
    public Scene[] Scenes;
    public TextMeshProUGUI TitleText, ExplanationText;
    private int _minigameId;
    private string _message;

    void Start()
    {
        _player = transform.GetChild(0).GetComponent<VideoPlayer>();
        ReadMessangerBoy();
    }

    private void ReadMessangerBoy()
    {
        StreamReader reader = new StreamReader("Assets/Resources/MessengerBoy.txt");
        _message = reader.ReadToEnd();
        
        reader.Close();

        _minigameId = Convert.ToInt32(_message.Split(";")[0]);
        _player.source = Sources[_minigameId];


        switch (_minigameId)
        {
            case 0:
                TitleText.text = "Pong!";
                ExplanationText.text = "";
                break;
            case 1:
                TitleText.text = "Time Stop!";
                ExplanationText.text = "";
                break;
            case 2:
                TitleText.text = "Pull The Rope!";
                ExplanationText.text = "";
                break;
            case 3:
                TitleText.text = "Button Mash!";
                ExplanationText.text = "";
                break;
            case 4:
                TitleText.text = "Find In Store";
                ExplanationText.text = "";
                break;
            case 5:
                TitleText.text = "Mash Race!";
                ExplanationText.text = "";
                break;
            case 6:
                TitleText.text = "Product Fall!";
                ExplanationText.text = "";
                break;
            case 7:
                TitleText.text = "Cross The Road!";
                ExplanationText.text = "";
                break;
        }
        

    }

    void Update()
    {
        if (Input.GetButtonDown("AButton1"))
        {
            StreamWriter writer = new StreamWriter("Assets/Resources/MessengerBoy.txt");
            writer.Write(_message.Split(";"[1]));
            writer.Close();
            SceneManager.LoadScene(Scenes[_minigameId].name);
        }
    }
}
