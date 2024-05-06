using UnityEngine;
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
        }
        else if (player2Score == 3)
        {
            Player2Win.enabled = true;
            Player2Win.text = "Player 2 WINS!";
            Player1Loose.enabled = true;
            Player1Loose.text = "Player 1 LOOSES!";
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
}