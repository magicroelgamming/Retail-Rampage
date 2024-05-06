using UnityEngine;

public class BallMovementScript : MonoBehaviour
{
    private float speed = 25f;
    private float speedIncreaseInterval = 5f;
    private float speedIncreaseAmount = 5f;
    private float currentSpeed;
    private Vector3 moveDirection;
    private float timer;
    [SerializeField] private HudPongScript hudPong;
    void Start()
    {
        currentSpeed = speed;
        moveDirection = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-0.5f, 0.5f)).normalized * currentSpeed;
        timer = 0f;
    }
    void Update()
    {
        if (hudPong.player1Score == 3 || hudPong.player2Score == 3)
        {
            return;
        }
        timer += Time.deltaTime;
        if (timer >= speedIncreaseInterval)
        {
            currentSpeed += speedIncreaseAmount;
            timer = 0f;
        }
        moveDirection = moveDirection.normalized * currentSpeed;
        InScreen();
        Scoring();
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
            ResetBall();
        }
        if (transform.position.x > 55f)
        {
            hudPong.Scored(1, transform.position.x);
            ResetBall();
        }
    }
    void ResetBall()
    {
        transform.position = Vector3.zero;
        moveDirection = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-0.5f, 0.5f)).normalized;
    }
}
