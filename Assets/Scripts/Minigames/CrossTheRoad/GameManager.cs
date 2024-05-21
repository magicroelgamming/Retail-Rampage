using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    private float _timer;
    private float _maxTimerTime = 60f;
    public TextMeshProUGUI TimerText, OutcomeText, TimeText;
    public Canvas PlayingCanvas, EndCanvas;
    public GameObject[] ArrayPlayers;
    public int[] ArrayPlayersScores;
    public GameObject Player;
    public Material MatRed, MatGreen, MatYellow, MatBlue;
    private float _timeValue = 3;

    void Start()
    {
        //ArrayPlayers = new GameObject[Input.GetJoystickNames().Length];
        ArrayPlayers = new GameObject[4];
        ArrayPlayersScores = new int[ArrayPlayers.Length];
        for (int i = 0; i < ArrayPlayers.Length; i++)
        {
            GameObject player = Player;
            switch (i)
            {
                case 0:
                    player.GetComponent<Renderer>().material = MatRed;
                    break;
                case 1:
                    player.GetComponent<Renderer>().material = MatGreen;
                    break;
                case 2:
                    player.GetComponent<Renderer>().material = MatYellow;
                    break;
                case 3:
                    player.GetComponent<Renderer>().material = MatBlue;
                    break;
            }
            player.GetComponent<CrossRoadPlayerInput>().PlayerId = i + 1;
            Instantiate(player);
        }
        
        _timer = _maxTimerTime;
    }


    public void ToDisplayTimer(float timeToDisplay)
    {
        if (timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }
        int seconds = Mathf.CeilToInt(timeToDisplay);

        TimeText.color = GetColorForSecond(seconds);

        TimeText.text = string.Format("{0}", seconds);
        if (timeToDisplay == 0)
        {
            TimeText.enabled = false;
            foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
            {
                player.GetComponent<CrossRoadPlayerInput>().IsPlaying = true;
            }
        }
    }
    private void Timer()
    {
        if (_timeValue > 0)
        {
            _timeValue -= Time.deltaTime;
        }
    }
    private Color GetColorForSecond(int second)
    {
        switch (second)
        {
            case 3:
                return Color.red;
            case 2:
                return Color.green;
            case 1:
                return Color.blue;
            case 0:
                return Color.yellow;
            default:
                return Color.white;
        }
    }
    void Update()
    {
        
        Timer();
        ToDisplayTimer(_timeValue);
        if (TimeText.enabled == false)
        {
            _timer -= Time.deltaTime;
            if (_timer < 0)
                EndGame();
            TimerText.text = "Timer: " + _timer;

            if (EndCanvas.gameObject.activeSelf)
            {
                Invoke("MessengerBoy", 4f);
            }
        }
        
    }
    private void MessengerBoy()
    {
        StreamWriter writer = new StreamWriter("Assets/Resources/MessengerBoy.txt");
        string writerMessage = "234:" + ArrayPlayers[0].GetComponent<CrossRoadPlayerInput>().PlayerId + 
            "," + ArrayPlayers[1].GetComponent<CrossRoadPlayerInput>().PlayerId + 
            "," + ArrayPlayers[2].GetComponent<CrossRoadPlayerInput>().PlayerId +
            "," + ArrayPlayers[3].GetComponent<CrossRoadPlayerInput>().PlayerId;
        writer.Write(writerMessage);

        writer.Close();

        SceneManager.LoadScene("TheBoard");
    }

    private void EndGame()
    {
        
        PlayingCanvas.gameObject.SetActive(false);
        EndCanvas.gameObject.SetActive(true);
        ArrayPlayers = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < ArrayPlayers.Length; i++)
        {
            ArrayPlayersScores[i] = ArrayPlayers[i].GetComponent<CrossRoadPlayerInput>().Score;
        }
        Array.Sort(ArrayPlayersScores, ArrayPlayers);
        Array.Reverse(ArrayPlayers);
        OutcomeText.text = "Ranking: \n";
        for (int i = 0;i < ArrayPlayers.Length;i++)
        {
            OutcomeText.text += "Player " + ArrayPlayers[i].GetComponent<CrossRoadPlayerInput>().PlayerId + ": "+ ArrayPlayers[i].GetComponent<CrossRoadPlayerInput>().Score + " Points\n";
        }

        foreach(GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            player.GetComponent<CrossRoadPlayerInput>().IsPlaying = false;
        }
    }
}
