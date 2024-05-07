using UnityEngine;

public class MashRace : MonoBehaviour
{
    private float speed = 10f;

    private bool moveIsActive = false;
    private bool moveIsActive1 = false;
    private bool moveIsActive2 = false;
    private bool moveIsActive3 = false;

    private float distanceToMove = 5f;
    private float timerMove = 0.5f;

    private Vector3 targetPosition;
    private Vector3 targetPosition1;
    private Vector3 targetPosition2;
    private Vector3 targetPosition3;
    private bool _winIsActive = false;

    [SerializeField] private HudMashRaceScript hudRaceScript;
    void Update()
    {
        if(hudRaceScript.win1.enabled || hudRaceScript.win2.enabled || hudRaceScript.win3.enabled|| hudRaceScript.win4.enabled)
        {
            return;
        }
        Movement();
    }
    private void Movement()
    {
        if (!moveIsActive && Input.GetButtonDown("AButton1"))
        {
            GameObject player1 = GameObject.FindGameObjectWithTag("Player/1");
            {
                targetPosition = player1.transform.position + Vector3.up * distanceToMove;
                moveIsActive = true;
                Invoke("CanMove1", timerMove);
            }
        }
        else if (moveIsActive)
        {
            GameObject player1 = GameObject.FindGameObjectWithTag("Player/1");
            player1.transform.position = Vector3.MoveTowards(player1.transform.position, targetPosition, speed * Time.deltaTime);
            if (player1.transform.position == targetPosition)
            {
                moveIsActive = false;
            }
        }
        if (!moveIsActive1 && Input.GetButtonDown("AButton2"))
        {
            GameObject player2 = GameObject.FindGameObjectWithTag("Player/2");
            targetPosition1 = player2.transform.position + Vector3.up * distanceToMove;
            moveIsActive1 = true;
            Invoke("CanMove2", timerMove);
        }
        else if (moveIsActive1)
        {
            GameObject player2 = GameObject.FindGameObjectWithTag("Player/2");
            player2.transform.position = Vector3.MoveTowards(player2.transform.position, targetPosition1, speed * Time.deltaTime);
            if (player2.transform.position == targetPosition1)
            {
                moveIsActive1 = false;
            }
        }
        if (!moveIsActive2 && Input.GetButtonDown("AButton3"))
        {
            GameObject player3 = GameObject.FindGameObjectWithTag("Player/3");
            targetPosition2 = player3.transform.position + Vector3.up * distanceToMove;
            moveIsActive2 = true;
            Invoke("CanMove3", timerMove);
        }
        else if (moveIsActive2)
        {
            GameObject player3 = GameObject.FindGameObjectWithTag("Player/3");
            player3.transform.position = Vector3.MoveTowards(player3.transform.position, targetPosition2, speed * Time.deltaTime);
            if (player3.transform.position == targetPosition2)
            {
                moveIsActive2 = false;
            }
        }
        if (!moveIsActive3 && Input.GetButtonDown("AButton4"))
        {
            GameObject player4 = GameObject.FindGameObjectWithTag("Player/4");
            targetPosition3 = player4.transform.position + Vector3.up * distanceToMove;
            moveIsActive3 = true;
            Invoke("CanMove4", timerMove);
        }
        else if (moveIsActive3)
        {
            GameObject player4 = GameObject.FindGameObjectWithTag("Player/4");
            player4.transform.position = Vector3.MoveTowards(player4.transform.position, targetPosition3, speed * Time.deltaTime);
            if (player4.transform.position == targetPosition3)
            {
                moveIsActive3 = false;
            }
        }
    }
    private void CanMove1()
    {
        moveIsActive = false;
    }
    private void CanMove2()
    {
        moveIsActive1 = false;
    }
    private void CanMove3()
    {
        moveIsActive2 = false;
    }
    private void CanMove4()
    {
        moveIsActive3 = false;
    }
    private void OnTriggerEnter(Collider hit)
    {
        if (hit.CompareTag("FinishLine/1"))
        {
            hudRaceScript.win1.text = "Player 1 Wins!";
            hudRaceScript.win1.enabled = true;
            _winIsActive = true;
        }
        else if (hit.CompareTag("FinishLine/2"))
        {
            hudRaceScript.win2.text = "Player 2 Wins!";
            hudRaceScript.win2.enabled = true;
            _winIsActive = true;
        }
        else if (hit.CompareTag("FinishLine/3"))
        {
            hudRaceScript.win3.text = "Player 3 Wins!";
            hudRaceScript.win3.enabled = true;
            _winIsActive = true;
        }
        else if (hit.CompareTag("FinishLine/4"))
        {
            hudRaceScript.win4.text = "Player 4 Wins!";
            hudRaceScript.win4.enabled = true;
            _winIsActive = true;
        }
        if (_winIsActive)
        {
            if (hudRaceScript.win2.enabled == true || hudRaceScript.win3.enabled == true || hudRaceScript.win4.enabled == true)
            {
                hudRaceScript.loose1.text = "Player 1 Looses";
                hudRaceScript.loose1.enabled = true;
            }
            if (hudRaceScript.win1.enabled == true || hudRaceScript.win3.enabled == true || hudRaceScript.win4.enabled == true)
            {
                hudRaceScript.loose2.text = "Player 2 Looses";
                hudRaceScript.loose2.enabled = true;
            }
            if (hudRaceScript.win2.enabled == true || hudRaceScript.win1.enabled == true || hudRaceScript.win4.enabled == true)
            {
                hudRaceScript.loose3.text = "Player 3 Looses";
                hudRaceScript.loose3.enabled = true;
            }
            if (hudRaceScript.win2.enabled == true || hudRaceScript.win3.enabled == true || hudRaceScript.win1.enabled == true)
            {
                hudRaceScript.loose4.text = "Player 4 Looses";
                hudRaceScript.loose4.enabled = true;
            }
        }
    }

}