using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.Video;

public class TutorialMinigame : MonoBehaviour
{
    private VideoPlayer _player;
    public VideoSource[] Sources;
    public TextMeshProUGUI TitleText, ExplanationText;

    public Image _pong;
    public Image _timeStop;
    public Image _ropePull;
    public Image _buttonMash;
    public Image _findInStore;
    public Image _mashRace;
    public Image _productFall;
    public Image _crossTheRoad;

    private int _minigameId;
    private string _message;
    private string _sceneName;

    void Start()
    {
        _player = transform.GetChild(0).GetComponent<VideoPlayer>();

        _pong.gameObject.SetActive(false);
        _timeStop.gameObject.SetActive(false);
        _ropePull.gameObject.SetActive(false);
        _buttonMash.gameObject.SetActive(false);
        _findInStore.gameObject.SetActive(false);
        _mashRace.gameObject.SetActive(false);
        _productFall.gameObject.SetActive(false);
        _crossTheRoad.gameObject.SetActive(false);

        ReadMessangerBoy();
    }

    private void ReadMessangerBoy()
    {
        StreamReader reader = new StreamReader("Assets/Resources/MessengerBoy.txt");
        _message = reader.ReadToEnd();
        
        reader.Close();

        _minigameId = Convert.ToInt32(_message.Split("!")[0]);
        //_player.source = Sources[_minigameId];


        switch (_minigameId)
        {
            case 1:
                TitleText.text = "Pong!";
                ExplanationText.text = "Move up and down with the Left Stick to move your paddle! \nWhen the ball hits the edge, the other player gets a point!";
                _pong.gameObject.SetActive(true);
                _sceneName = "Pong";
                break;
            case 2:
                TitleText.text = "Time Stop!";
                ExplanationText.text = "Press A to press the button first! \nWhen the light turns green, hit the button before the other player and you win!";
                _timeStop.gameObject.SetActive(true);
                _sceneName = "TimeStop";
                break;
            case 3:
                TitleText.text = "Pull The Rope!";
                ExplanationText.text = "Repeatedly move the Left Stick back and forth to pull the rope! \nThe first one to pull the rope to their side, wins!";
                _ropePull.gameObject.SetActive(true);
                _sceneName = "PullTheRope";
                break;
            case 4:
                TitleText.text = "Button Mash!";
                ExplanationText.text = "Spam the B button! \nThe one who presses it the most, wins!";
                _buttonMash.gameObject.SetActive(true);
                _sceneName = "ButtonMashingMinigame";
                break;
            case 11:
                TitleText.text = "Find In Store";
                ExplanationText.text = "Move with the Left Stick to be the first to reach the aisle with the desired product!";
                _findInStore.gameObject.SetActive(true);
                _sceneName = "Game_FindInStore";
                break;
            case 14:
                TitleText.text = "Mash Race!";
                ExplanationText.text = "Spam the A button to climb the shelves! \nFirst one to climb to the top, wins!";
                _mashRace.gameObject.SetActive(true);
                _sceneName = "MashRace";
                break;
            case 13:
                TitleText.text = "Product Fall!";
                ExplanationText.text = "Move with the Left Stick to catch the items falling down! \nBe sure to avoid the bombs though! \nThe one with the most items wins!";
                _productFall.gameObject.SetActive(true);
                _sceneName = "ProductFall";
                break;
            case 12:
                TitleText.text = "Cross The Road!";
                ExplanationText.text = "Move with the Left Stick and cross the road! \nGather as many items from the other side and bring them back! \nThe one with the most items wins!";
                _crossTheRoad.gameObject.SetActive(true);
                _sceneName = "CrossTheRoad";
                break;
        }
        

    }

    void Update()
    {
        if (Input.GetButtonDown("AButton1") || Input.GetButtonDown("AButton2") || Input.GetButtonDown("AButton3") || Input.GetButtonDown("AButton4"))
        {
            StreamWriter writer = new StreamWriter("Assets/Resources/MessengerBoy.txt");
            writer.Write(_message.Split("!")[1]);
            writer.Close();
            SceneManager.LoadScene(_sceneName);
        }
    }
}
