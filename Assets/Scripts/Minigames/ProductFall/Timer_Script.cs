using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerTextCamera1;
    public Text timerTextCamera2;
    public Text winText1;
    public Text winText2;
    public Text tieText;
    public Text looseText1;
    public Text looseText2;

    public float minTime = 5f;
    public float maxTime = 10f;
    private float currentTimeCamera1;
    private float currentTimeCamera2;

    private bool timerRunningCamera1;
    private bool timerRunningCamera2;
    private bool timerStarted;

    void Start()
    {
        timerTextCamera1.enabled = false;
        timerTextCamera2.enabled = false;
        winText1.enabled = false;
        winText2.enabled = false;
        tieText.enabled = false;
        looseText1.enabled = false; 
        looseText2.enabled = false;
    }

    void Update()
    {
        TimerManagment();
    }
    private void TimerManagment()
    {
        if (!timerStarted)
        {
            StartTimer();
            timerStarted = true;
        }

        if (Input.GetMouseButtonDown(0) && GameObject.FindGameObjectWithTag("Camera1"))
        {
            StopTimer("Camera1");
        }

        if (Input.GetMouseButtonDown(1) && GameObject.FindGameObjectWithTag("Camera2"))
        {
            StopTimer("Camera2");
        }

        if (timerRunningCamera1)
        {
            currentTimeCamera1 -= Time.deltaTime;
            if (currentTimeCamera1 <= 0)
            {
                currentTimeCamera1 = 0;
                timerRunningCamera1 = false;
            }
            UpdateTimerDisplay("Camera1");
        }

        if (timerRunningCamera2)
        {
            currentTimeCamera2 -= Time.deltaTime;
            if (currentTimeCamera2 <= 0)
            {
                currentTimeCamera2 = 0;
                timerRunningCamera2 = false;
            }
            UpdateTimerDisplay("Camera2");
        }
        if (!timerRunningCamera1 && !timerRunningCamera2)
        {
            CheckWinCondition();
        }
    }

    public void StartTimer()
    {
        timerRunningCamera1 = true;
        timerRunningCamera2 = true;
        currentTimeCamera1 = Random.Range(minTime, maxTime);
        currentTimeCamera2 = currentTimeCamera1;
    }

    public void StopTimer(string cameraTag)
    {
        if (cameraTag == "Camera1")
        {
            timerRunningCamera1 = false;
            timerTextCamera1.enabled = true;
        }
        else if (cameraTag == "Camera2")
        {
            timerRunningCamera2 = false;
            timerTextCamera2.enabled = true;
        }
    }

    private void UpdateTimerDisplay(string cameraTag)
    {
        if (cameraTag == "Camera1")
        {
            timerTextCamera1.text = currentTimeCamera1.ToString("F1");
        }
        else if (cameraTag == "Camera2")
        {
            timerTextCamera2.text = currentTimeCamera2.ToString("F1");
        }
    }
    private void CheckWinCondition()
    {
        float camera1Time = currentTimeCamera1;
        float camera2Time = currentTimeCamera2;

        if (camera1Time < camera2Time)
        {
            winText1.text = "Camera 1 Wins!";
            winText1.enabled = true;
        }
        else if (camera2Time < camera1Time)
        {
            winText2.text = "Camera 2 Wins!";
            winText2.enabled = true;
        }
        else if (camera2Time == camera1Time)
        {
            tieText.text = "It's a tie!";
            tieText.enabled = true;
        }
        if (camera2Time <= 0 || camera2Time < camera1Time)
        {
            looseText1.text = "Camera 2 Lost!";
            looseText1.enabled = true;
        }
        if (camera1Time <= 0 || camera1Time < camera2Time)
        {
            looseText2.text = "Camera 1 Lost!";
            looseText2.enabled = true;
        }
    }
}
