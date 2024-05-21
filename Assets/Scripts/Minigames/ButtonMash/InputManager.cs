using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour
{
    private int _counterPlayer1, _counterPlayer2;
    public TextMeshProUGUI TextPlayer1, TextPlayer2, TextTimer, TextOutcome;
    public Canvas CanvasEnd, CanvasPlaying;
    private float _timer;
    public float TimerMaxTime = 10f;
    private bool _isPlaying = true;
    public GameObject ButtonP1, ButtonP2;
    private float _buttonY;

    private int[] _players;

    void Start()
    {
        _timer = TimerMaxTime;
        _buttonY = ButtonP1.transform.position.y;

        _players = new int[2];

        StreamReader Reader = new StreamReader("Assets/Resources/MessengerBoy.txt");
        string message = Reader.ReadToEnd();
        Reader.Close();
        string[] playerBuString = message.Split(':');
        for (int i = 0; i < playerBuString.Length; i++)
        {
            _players[i] = int.Parse(playerBuString[i]);
        }
    }

    void Update()
    {
        PlayerInput();

        _timer -= Time.deltaTime;
        TextTimer.text = _timer.ToString();
        if (_timer < 0)
            EndGame();
    }

    private void EndGame()
    {
        _isPlaying = false;
        if (_counterPlayer1 > _counterPlayer2)
            TextOutcome.text = "Player 1 wins";
        else
            TextOutcome.text = "Player 2 wins";
        CanvasPlaying.gameObject.SetActive(false);
        CanvasEnd.gameObject.SetActive(true);

        if (CanvasEnd.gameObject.activeSelf)
        {
            if (Input.GetButtonDown("AButton" + _players[0]) || Input.GetButtonDown("AButton" + _players[1]))
            {
                StreamWriter writer = new StreamWriter("Assets/Resources/MessengerBoy.txt");
                if (_counterPlayer1 > _counterPlayer2)
                {
                    writer.Write("1v1:true");
                }
                else
                {
                    writer.Write("1v1:false");
                }
                writer.Close();

                SceneManager.LoadScene("TheBoard");
            }
        }
        
    }

    private void PlayerInput()
    {
        if (!_isPlaying) return;
        if (Input.GetButtonDown("BButton" + _players[0]))
        {
            _counterPlayer1++;
            TextPlayer1.text = "Player 1 Score: " + _counterPlayer1;    
        }

        if (Input.GetButtonDown("BButton" + _players[1]))
        {
            _counterPlayer2++;
            TextPlayer2.text = "Player 2 Score: " + _counterPlayer2;
        }

        if (Input.GetButton("BButton" + _players[0]))
            ButtonP1.transform.position = ButtonP1.transform.position = new Vector3(ButtonP1.transform.position.x, _buttonY - 0.2f, ButtonP1.transform.position.z);
        else
            ButtonP1.transform.position = new Vector3(ButtonP1.transform.position.x, _buttonY, ButtonP1.transform.position.z);
        if (Input.GetButton("BButton" + _players[1]))
            ButtonP2.transform.position = ButtonP2.transform.position = new Vector3(ButtonP2.transform.position.x, _buttonY - 0.2f, ButtonP2.transform.position.z);
        else
            ButtonP2.transform.position = new Vector3(ButtonP2.transform.position.x, _buttonY, ButtonP2.transform.position.z);
    }
}
