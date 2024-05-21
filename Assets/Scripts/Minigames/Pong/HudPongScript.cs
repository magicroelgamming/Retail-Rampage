using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class HudPongScript : MonoBehaviour
{
    [SerializeField] private Text Player1;
    [SerializeField] private Text Player2;
    [SerializeField] private Text Player1Win;
    [SerializeField] private Text Player2Win;
    [SerializeField] private Text Player1Loose;
    [SerializeField] private Text Player2Loose;
    [SerializeField] private Text timeText;

    private Color[] colors = {Color.red, Color.green, Color.blue, Color.yellow};

    private float timeValue = 3;

    public int player1Score = 0;
    public int player2Score = 0;

    public bool startDelayBeforeMainBoard = false;
    void Start()
    {
        Player1Loose.enabled = false;
        Player1Win.enabled = false;
        Player2Loose.enabled = false;
        Player2Win.enabled = false;
        player1Score = 0;
        player2Score = 0;
    }
    void Update()
    {
        ScoreText();
        Timer();
        ToDisplayTimer(timeValue);
        if (startDelayBeforeMainBoard)
        {
            MessengerBoy();
        }
    }
    void MessengerBoy()
    {
        StreamWriter writer = new StreamWriter("Assets/Resources/MessengerBoy.txt");

        if (player1Score != 0)
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
    private void Timer()
    {
        if (timeValue > 0)
        {
            timeValue -= Time.deltaTime;
        }
    }
        public void ToDisplayTimer(float timeToDisplay)
    {
        if (timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }
        int seconds = Mathf.CeilToInt(timeToDisplay);

        timeText.color = GetColorForSecond(seconds);

        timeText.text = string.Format("{0}", seconds);
        if (timeToDisplay == 0)
        {
            timeText.enabled = false;
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
    
    void ScoreText()
    {
        Player1.text = player1Score.ToString();
        Player2.text = player2Score.ToString();
        if (player1Score == 1)
        {
            Player1Win.enabled = true;
            Player1Win.text = "Player 1 WINS!";
            Player2Loose.enabled = true;
            Player2Loose.text = "Player 2 LOOSES!";
            Invoke("GameOver", 4f);
        }
        else if (player2Score == 1)
        {
            Player2Win.enabled = true;
            Player2Win.text = "Player 2 WINS!";
            Player1Loose.enabled = true;
            Player1Loose.text = "Player 1 LOOSES!";
            Invoke("GameOver", 4f);
        }
    }
    public void Scored(int amount, float ballPositionZ)
    {
        if (ballPositionZ < -55f)
        {
            player2Score += amount;
        }
        else if (ballPositionZ > 55f)
        {
            player1Score += amount;
        }
    }
    public void GameOver()
    {
        startDelayBeforeMainBoard = true;
    }
}