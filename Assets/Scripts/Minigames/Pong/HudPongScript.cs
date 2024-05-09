using System.IO;
using Unity.VisualScripting;
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
    public int player1Score = 0;
    public int player2Score = 0;

    public bool startDelayBeforeMainBoard = false;
    private float delayTimer = 4f;
    private float delayTime = 0;
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
        MessengerBoy();
        if (startDelayBeforeMainBoard)
        {
            delayTime += Time.deltaTime;
            if (delayTime > delayTimer)
            {
                SceneManager.LoadScene("TheBoard");
            }
        }
    }
    void MessengerBoy()
    {
        StreamWriter writer = new StreamWriter("Assets/Ressources/MessengerBoy.txt");

        if (Player1Win)
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
    void ScoreText()
    {
        Player1.text = player1Score.ToString();
        Player2.text = player2Score.ToString();
        if (player1Score == 3)
        {
            Player1Win.enabled = true;
            Player1Win.text = "Player 1 WINS!";
            Player2Loose.enabled = true;
            Player2Loose.text = "Player 2 LOOSES!";
            GameOver();


        }
        else if (player2Score == 3)
        {
            Player2Win.enabled = true;
            Player2Win.text = "Player 2 WINS!";
            Player1Loose.enabled = true;
            Player1Loose.text = "Player 1 LOOSES!";
            GameOver();
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