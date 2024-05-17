using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MashRace : MonoBehaviour
{
    [SerializeField] private GameObject FinishLineLoosePrefab;
    private float speed = 10f;
    private float distanceToMove = 7.5f;
    private float timerMove = 0.2f;

    private Vector3[] targetPositions = new Vector3[4];
    private bool[] playersFinished = new bool[4];
    private bool[] moveIsActive = new bool[4];

    private int playerPlacement = 1;

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

        Player[] players = (FindObjectsOfType<Player>()).OrderBy(i => i._score).Reverse().ToArray();

        string message = "234:";

        for (int i = 0; i < players.Length; i++)
        {
            message += i+1;
            if (i+1 != players.Length)
            {
                message += ",";
            }
        }
        writer.Write(message);
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
                targetPositions[i] = player.transform.position + Vector3.forward * distanceToMove;
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
                }
            }
        }
    }
    private void OnTriggerEnter(Collider hit)
    {
        for (int i = 0; i < 4; i++)
        {
            if (hit.CompareTag($"FinishLine/{i + 1}"))
            {
                string winText = $"Player {i + 1} Wins!";
                ShowWinText(i, winText);
                for (int j = 0; j < 4; j++)
                {
                    if (j + 1 != i + 1)
                    {
                        GameObject[] finishLines = GameObject.FindGameObjectsWithTag($"FinishLine/{j + 1}");
                        foreach (GameObject finishLine in finishLines)
                        {
                            Destroy(finishLine);
                        }
                    }
                }
            }
        }
        ShowPlacementText(hit);
    }
    private void ShowWinText(int playerIndex, string winText)
    {
        switch (playerIndex)
        {
            case 0:
                hudRaceScript.win1.text = winText;
                hudRaceScript.win1.enabled = true;
                hudRaceScript.loose1.enabled = false;
                break;
            case 1:
                hudRaceScript.win2.text = winText;
                hudRaceScript.win2.enabled = true;
                hudRaceScript.loose2.enabled = false;
                break;
            case 2:
                hudRaceScript.win3.text = winText;
                hudRaceScript.win3.enabled = true;
                hudRaceScript.loose3.enabled = false;
                break;
            case 3:
                hudRaceScript.win4.text = winText;
                hudRaceScript.win4.enabled = true;
                hudRaceScript.loose4.enabled = false;
                break;
        }
        playerPlacement++;
    }
    private void ShowPlacementText(Collider hit)
    {
        List<GameObject> nonFinishers = new List<GameObject>();
        for (int i = 0; i < 4; i++)
        {
            GameObject player = GameObject.FindGameObjectWithTag($"Player/{i + 1}");

            if (!playersFinished[i] && !hit.CompareTag($"FinishLine/{i + 1}"))
            {
                nonFinishers.Add(player);
            }
        }

        nonFinishers.Sort((a, b) => Vector3.Distance(a.transform.position, hit.transform.position).CompareTo(Vector3.Distance(b.transform.position, hit.transform.position)));

        for (int i = 0; i < nonFinishers.Count; i++)
        {
            string placementText = $"Player place {i + playerPlacement}";
            SetPlacementText(nonFinishers[i], placementText);
        }

        if (hit.CompareTag("FinishLine/1") || hit.CompareTag("FinishLine/2") || hit.CompareTag("FinishLine/3") || hit.CompareTag("FinishLine/4"))
        {
            string winText = $"Player {hit.gameObject.tag.Substring(11)} Wins!";
            ShowWinText(int.Parse(hit.gameObject.tag.Substring(11)) - 1, winText);
        }
    }
    private void SetPlacementText(GameObject player, string placementText)
    {
        switch (player.tag)
        {
            case "Player/1":
                hudRaceScript.loose1.text = placementText;
                hudRaceScript.loose1.enabled = true;
                break;
            case "Player/2":
                hudRaceScript.loose2.text = placementText;
                hudRaceScript.loose2.enabled = true;
                break;
            case "Player/3":
                hudRaceScript.loose3.text = placementText;
                hudRaceScript.loose3.enabled = true;
                break;
            case "Player/4":
                hudRaceScript.loose4.text = placementText;
                hudRaceScript.loose4.enabled = true;
                break;
        }
        Invoke("GameOver", 3f);
    }
}