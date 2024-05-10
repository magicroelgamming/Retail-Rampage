using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class HomeScreenButtonScript : MonoBehaviour
{
    public Text PlayersText;
    private bool[] playerJoined = new bool[4];
    public bool startDelayBeforeMainBoard = false;

    void Start()
    {
        UpdatePlayersText();
    }
    void Update()
    {
        ButtonPressToStart();
        if (startDelayBeforeMainBoard)
        {
            MessengerBoy();
        }
    }
    void MessengerBoy()
    {
        SceneManager.LoadScene("TheBoard");
    }
    void PlayersAreReady()
    {
        startDelayBeforeMainBoard = true;
    }
    void ButtonPressToStart()
    {
        if (Input.GetButtonDown("AButton1") && !playerJoined[0])
        {
            playerJoined[0] = true;
            UpdatePlayersText();
        }

        if (Input.GetButtonDown("AButton2") && !playerJoined[1])
        {
            playerJoined[1] = true;
            UpdatePlayersText();
        }

        if (Input.GetButtonDown("AButton3") && !playerJoined[2])
        {
            playerJoined[2] = true;
            UpdatePlayersText();
        }

        if (Input.GetButtonDown("AButton4") && !playerJoined[3])
        {
            playerJoined[3] = true;
            UpdatePlayersText();
        }
        if (playerJoined[0] && playerJoined[1] && playerJoined[2] && playerJoined[3])
        {
            Invoke("PlayersAreReady", 4f);
        }
    }
    void UpdatePlayersText()
    {
        string players = "Players: [";
        bool firstPlayerAdded = false;
        for (int i = 0; i < playerJoined.Length; i++)
        {
            if (playerJoined[i])
            {
                if (firstPlayerAdded)
                    players += ",";
                players += (i + 1).ToString();
                firstPlayerAdded = true;
            }
        }
        if (playerJoined[3])
            players += "]";
        PlayersText.text = players;
    }
}