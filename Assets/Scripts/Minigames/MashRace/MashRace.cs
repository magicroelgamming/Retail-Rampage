using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MashRace : MonoBehaviour
{
    private float speed = 10f;

    private float distanceToMove = 7.5f;
    private float timerMove = 0.2f;

    private Vector3[] targetPositions = new Vector3[4];
    private bool[] playersFinished = new bool[4];
    private bool[] moveIsActive = new bool[4];
    private bool _winIsActive = false;

    public bool startDelayBeforeMainBoard = false;

    [SerializeField] private HudMashRaceScript hudRaceScript;
    void Update()
    {
        if (startDelayBeforeMainBoard)
        {
            MessengerBoy();
        }
        if (hudRaceScript.win1.enabled || hudRaceScript.win2.enabled || hudRaceScript.win3.enabled || hudRaceScript.win4.enabled)
        {
            return;
        }
        Movement();
    }
    void MessengerBoy()
    {
        StreamWriter writer = new StreamWriter("Assets/Resources/MessengerBoy.txt");

        writer.Write("234:2,1,3,4");

        writer.Close();
        SceneManager.LoadScene("TheBoard");
    }
    public void GameOver()
    {
        startDelayBeforeMainBoard = true;
    }
    private void Movement()
    {
        for (int i = 0; i < 4; i++)
        {
            if (!moveIsActive[i] && Input.GetButtonDown($"AButton{i + 1}"))
            {
                GameObject player = GameObject.FindGameObjectWithTag($"Player/{i + 1}");
                targetPositions[i] = player.transform.position + Vector3.up * distanceToMove;
                moveIsActive[i] = true;
                Invoke($"CanMove{i}", timerMove);
            }
            else if (moveIsActive[i])
            {
                GameObject player = GameObject.FindGameObjectWithTag($"Player/{i + 1}");
                player.transform.position = Vector3.MoveTowards(player.transform.position, targetPositions[i], speed * Time.deltaTime);
                if (player.transform.position == targetPositions[i])
                {
                    moveIsActive[i] = false;
                    CanMove0();
                    CanMove1();
                    if (!playersFinished[i])
                    {
                        playersFinished[i] = true;
                    }
                }
            }
        }
    }
    private void CanMove0()
    {
        moveIsActive[0] = false;
    }
    private void CanMove1()
    {
        moveIsActive[1] = false;
    }
    private void OnTriggerEnter(Collider hit)
    {
        if (_winIsActive) return;

        for (int i = 0; i < 4; i++)
        {
            if (hit.CompareTag($"FinishLine/{i + 1}"))
            {
                string winText = $"Player {i + 1} Wins!";
                switch (i)
                {
                    case 0:
                        hudRaceScript.win1.text = winText;
                        hudRaceScript.win1.enabled = true;
                        break;
                    case 1:
                        hudRaceScript.win2.text = winText;
                        hudRaceScript.win2.enabled = true;
                        break;
                    case 2:
                        hudRaceScript.win3.text = winText;
                        hudRaceScript.win3.enabled = true;
                        break;
                    case 3:
                        hudRaceScript.win4.text = winText;
                        hudRaceScript.win4.enabled = true;
                        break;
                }
                _winIsActive = true;
                Invoke("GameOver", 3f);
            }
            else
            {
                string loseText = $"Player {i + 1} Looses!";
                switch (i)
                {
                    case 0:
                        hudRaceScript.loose1.text = loseText;
                        hudRaceScript.loose1.enabled = true;
                        break;
                    case 1:
                        hudRaceScript.loose2.text = loseText;
                        hudRaceScript.loose2.enabled = true;
                        break;
                    case 2:
                        hudRaceScript.loose3.text = loseText;
                        hudRaceScript.loose3.enabled = true;
                        break;
                    case 3:
                        hudRaceScript.loose4.text = loseText;
                        hudRaceScript.loose4.enabled = true;
                        break;
                }
            }
        }
    }
}