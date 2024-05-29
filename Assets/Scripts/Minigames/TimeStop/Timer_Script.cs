using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public GameObject Button1;
    public GameObject Button2;
    public TextMeshProUGUI explanationText;
    public TextMeshProUGUI timerTextCamera1;
    public TextMeshProUGUI timerTextCamera2;
    public TextMeshProUGUI resultText1;
    public TextMeshProUGUI resultText2;
    public TextMeshProUGUI tieText;
    //public Text loseText1;
    //public Text loseText2;
    public Image FlashLight;

    private float minRedTime = 2f;
    private float maxRedTime = 10f;
    private float currentTimeCamera1;
    private float currentTimeCamera2;

    private bool timerRunningCamera1;
    private bool timerRunningCamera2;
    private bool timerStarted;

    public float delayBefotreMainBoard;
    public bool startDelayBeforeMainBoard;

    private float redDuration;
    private bool timerRed;
    private float timerBeforeTie;
    private bool gameOver;

    private float timer;

    private int[] players;
    void Start()
    {
        FlashLight.color = Color.red;
        timerTextCamera1.enabled = false;
        timerTextCamera2.enabled = false;
        resultText1.enabled = false;
        resultText2.enabled = false;
        tieText.enabled = false;
        resultText1.enabled = false;
        resultText2.enabled = false;
        explanationText.text = "Don't Push!";

        delayBefotreMainBoard = 5f;
        startDelayBeforeMainBoard = false;
        timerRed = false;
        timerBeforeTie = 1f;
        gameOver = false;

        players = new int[2];

        StreamReader Reader = new StreamReader("Assets/Resources/MessengerBoy.txt");
        string message = Reader.ReadToEnd();
        Reader.Close();
        string[] playerBuString = message.Split(':');
        for (int i = 0; i < playerBuString.Length; i++)
        {
            players[i] = int.Parse(playerBuString[i]);
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        Debug.Log(timer);
        if (!gameOver)
        {
            TimerManagment();
        }
        if (startDelayBeforeMainBoard)
        {
            MessengerBoy();
        }
    }
    void MessengerBoy()
    {
        StreamWriter writer = new StreamWriter("Assets/Resources/MessengerBoy.txt");

        if (resultText1.text.Contains("Wins"))
        {
            writer.Write("1V1:true");
        }
        else
        {
            writer.Write("1V1:false");
        }
        writer.Close();

        SceneManager.LoadScene("TheBoard");
    }
    private void TimerManagment()
    {
        if (!timerStarted)
        {
            StartTimer();
            timerStarted = true;
        }

        if (Input.GetButtonDown("AButton" + players[0]) && GameObject.FindGameObjectWithTag("Camera/1"))
        {
            if (!timerRed)
            {
                StopTimer("Camera/1");
                Button1.transform.position -= Vector3.up;
            }
            else
            {
                FalseStart("Camera/1");
            }
            Debug.Log("Player 1 Pressed");
        }

        if (Input.GetButtonDown("AButton" + players[1]) && GameObject.FindGameObjectWithTag("Camera/2"))
        {
            if (!timerRed)
            {
                StopTimer("Camera/2");
                Button2.transform.position -= Vector3.up;
            }
            else
            {
                FalseStart("Camera/2");
            }
            Debug.Log("Player 2 Pressed");
        }

        if (timerRed)
        {
            redDuration -= Time.deltaTime;
            if (redDuration <= 0)
            {
                FlashLight.color = Color.green;
                explanationText.text = "Push!";
                timerRed = false;
            }
        }
        else
        {
            if (timerRunningCamera1)    
            {
                currentTimeCamera1 -= Time.deltaTime;
                if (currentTimeCamera1 <= 0)
                {
                    currentTimeCamera1 = 0;
                    timerRunningCamera1 = false;
                }
                //UpdateTimerDisplay("Camera/1"); //this timer is not neccesary for players
            }

            if (timerRunningCamera2)
            {
                currentTimeCamera2 -= Time.deltaTime;
                if (currentTimeCamera2 <= 0)
                {
                    currentTimeCamera2 = 0;
                    timerRunningCamera2 = false;
                }
                //UpdateTimerDisplay("Camera/2"); //this timer is not neccesary for players
            }
        }
        if (!timerRunningCamera1 && !timerRunningCamera2)
        {
            CheckWinCondition();
        }
    }

    public void StartTimer()
    {
        redDuration = Random.Range(minRedTime, maxRedTime);
        timerRed = true;
        timerRunningCamera1 = true;
        timerRunningCamera2 = true;
        currentTimeCamera1 = timerBeforeTie;
        currentTimeCamera2 = currentTimeCamera1;
    }

    public void StopTimer(string cameraTag)
    {
        if (cameraTag == "Camera/1")
        {
            timerRunningCamera1 = false;
            timerTextCamera1.enabled = true;
        }
        else if (cameraTag == "Camera/2")
        {
            timerRunningCamera2 = false;
            timerTextCamera2.enabled = true;
        }
    }

    private void UpdateTimerDisplay(string cameraTag)
    {
        if (cameraTag == "Camera/1")
        {
            timerTextCamera1.text = currentTimeCamera1.ToString("F1");
        }
        else if (cameraTag == "Camera/2")
        {
            timerTextCamera2.text = currentTimeCamera2.ToString("F1");
        }
    }
    public void FalseStart(string cameraTag)
    {
        gameOver = true;
        timerRed = false;
        if (cameraTag == "Camera/1")
        {
            resultText1.text = "Attacker\nFalse Start!";
            resultText1.enabled = true;
            resultText2.text = "Defender Wins!";
            resultText2.enabled = true;
        }
        else if (cameraTag == "Camera/2")
        {
            resultText2.text = "Attacker\nFalse Start!";
            resultText2.enabled = true;
            resultText1.text = "Defender Wins!";
            resultText1.enabled = true;
        }
        Invoke("GameOver", delayBefotreMainBoard);
    }
    private void CheckWinCondition()
    {
        gameOver = true;
        float camera1Time = currentTimeCamera1;
        float camera2Time = currentTimeCamera2;

        if (camera2Time < camera1Time)
        {
            resultText1.text = "Attacker Wins! ";// + (timerBeforeTie - camera1Time);
            resultText1.enabled = true;
            resultText2.text = "Defender Lost! ";// + (timerBeforeTie - camera2Time);
            resultText2.enabled = true;
        }
        else if (camera1Time < camera2Time)
        {
            resultText2.text = "Attacker Wins! ";// + (timerBeforeTie - camera2Time);
            resultText2.enabled = true;
            resultText1.text = "Defender Lost! ";// + (timerBeforeTie - camera1Time);
            resultText1.enabled = true;
        }
        else if (camera2Time == camera1Time)
        {
            tieText.text = "It's a tie!";
            tieText.enabled = true;
        }
        if (camera2Time <= 0)
        {
            resultText2.text = "Defender Lost!";
            resultText2.enabled = true;
        }
        if (camera1Time <= 0)
        {
            resultText1.text = "Attacker Lost!";
            resultText1.enabled = true;
        }

        Invoke("GameOver", delayBefotreMainBoard);
    }
    void GameOver()
    {
        startDelayBeforeMainBoard = true;
    }
}
