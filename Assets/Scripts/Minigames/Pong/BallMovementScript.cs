using System.Linq;
using UnityEngine;

public class BallMovementScript : MonoBehaviour
{
    private float speed = 25f;
    private float speedIncreaseInterval = 1f;
    private float speedIncreaseAmount = 3f;
    private float speedIncreaseTimer = 0f;
    private float currentSpeed;
    private Vector3 moveDirection;
    [SerializeField] private HudPongScript hudPong;
    private int[] randomSign = {1, -1};
    private int sign;
    private bool onResetDelay = true;
    private float resetTimer;
    private float resetDelay = 1.5f;


    void Start()
    {
        currentSpeed = speed;
        int randomIndex = Random.Range(0, randomSign.Count());
        moveDirection = new Vector3(Random.Range(0.7f, 1f) * randomSign[randomIndex], 0f, Random.Range(-0.3f, 0.3f)).normalized * currentSpeed;
    }
    void Update()
    {
        if (!hudPong.startDelayBeforeMainBoard)
        {
            resetTimer += Time.deltaTime;
            if (resetTimer >= resetDelay)
            {

                resetTimer = 0f;
                onResetDelay = false;
            }
            if (!onResetDelay)
            {
                speedIncreaseTimer += Time.deltaTime;
                if (speedIncreaseTimer >= speedIncreaseInterval)
                {
                    currentSpeed += speedIncreaseAmount;
                    speedIncreaseTimer = 0f;
                }
                moveDirection = moveDirection.normalized * currentSpeed;
                InScreen();
                Scoring();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player/1") || other.CompareTag("Player/2"))
        {
            ReverseDirection();
        }
    }
    private void ReverseDirection()
    {
        moveDirection.x *= -1;
    }
    void InScreen()
    {
        transform.Translate(moveDirection * Time.deltaTime);
        if (transform.position.z > 27f || transform.position.z < -27f)
        {
            moveDirection.z *= -1;
        }
    }
    void Scoring()
    {
        if (transform.position.x < -55f)
        {
            hudPong.Scored(1, transform.position.x);
            sign = -1;
            ResetBall();
        }
        if (transform.position.x > 55f)
        {
            hudPong.Scored(1, transform.position.x);
            sign = 1;
            ResetBall();
        }
    }
    void ResetBall()
    {
        currentSpeed = speed;
        transform.position = Vector3.zero;
        moveDirection = new Vector3(Random.Range(0.7f, 1f) * sign, 0f, Random.Range(-0.3f, 0.3f)).normalized;
        onResetDelay = true;
    }
}
