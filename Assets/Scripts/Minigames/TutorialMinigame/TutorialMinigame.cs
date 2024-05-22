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
    public string[] Scenes;
    public TextMeshProUGUI TitleText, ExplanationText;
    private int _minigameId;
    private string _message;
    private string _sceneName;

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
        //_player.source = Sources[_minigameId];


        switch (_minigameId)
        {
            case 1:
                TitleText.text = "Pong!";
                ExplanationText.text = "Move up and down with the Left Stick to move your paddle! \nWhen the ball hits the edge, the other player gets a point!";
                _sceneName = "Pong";
                break;
            case 2:
                TitleText.text = "Time Stop!";
                ExplanationText.text = "Press A to press the button first! \nHit it before the other player and you win!";
                break;
            case 3:
                TitleText.text = "Pull The Rope!";
                ExplanationText.text = "Repeatedly move the Left Stick back and forth to pull the rope! \nThe first one to pull the rope to their side, wins!";
                break;
            case 4:
                TitleText.text = "Button Mash!";
                ExplanationText.text = "Spam the B button! \nThe one who presses it the most, wins!";
                break;
            case 11:
                TitleText.text = "Find In Store";
                ExplanationText.text = "Move with the Left Stick to be the first to reach the aisle with the desired product!";
                break;
            case 12:
                TitleText.text = "Mash Race!";
                ExplanationText.text = "Spam the A button to climb the shelves! \nFirst one to climb to the top, wins!";
                break;
            case 13:
                TitleText.text = "Product Fall!";
                ExplanationText.text = "Move with the Left Stick to catch the items falling down! \nBe sure to avoid the bombs though! \nThe one with the most items wins!";
                break;
            case 14:
                TitleText.text = "Cross The Road!";
                ExplanationText.text = "Move with the Left Stick and cross the road! \nGather as many items from the other side and bring them back! \nThe one with the most items wins!";
                break;
        }
        

    }

    void Update()
    {
        if (Input.GetButtonDown("AButton1"))
        {
            StreamWriter writer = new StreamWriter("Assets/Resources/MessengerBoy.txt");
            writer.Write(_message.Split(";")[1]);
            writer.Close();
            SceneManager.LoadScene(Scenes[_minigameId]);
        }
    }
}
