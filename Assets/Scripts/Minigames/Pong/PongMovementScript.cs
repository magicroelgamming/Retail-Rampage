using System.IO;
using UnityEngine;
public class PongMovementScript : MonoBehaviour
{
    [SerializeField] private Camera camera;
    private float speed = 20f;
    private int[] players;
    private void Start()
    {
        players = new int[2];

        StreamReader Reader = new StreamReader("Assets/Resources/MessengerBoy.txt");
        string message = Reader.ReadToEnd();
        Reader.Close();
        string[] playerBuString = message.Split(':');
        for (int i = 0; i < playerBuString.Length; i++)
        {
            players[i] = int.Parse(playerBuString[i]);
        }
    }
    void Update()
    {
        Movement();
    }
    void Movement()
    {
        Vector3 combinedMovement = Vector3.zero;
        float screenWidthOffset = 16f;
        for (int i = 1; i <= 2; i++)
        {
            if (CompareTag($"Player/{i}"))
            {
                float verticalInput = Input.GetAxis($"LeftStickVertical{players[i-1]}");
                combinedMovement += Vector3.forward * verticalInput * speed;

                float camHeight = camera.orthographicSize;
                float camWidth = camHeight * camera.aspect + screenWidthOffset;

                Vector3 characterPosition = transform.position;
                characterPosition += combinedMovement * Time.deltaTime;
                characterPosition.z = Mathf.Clamp(characterPosition.z, camera.transform.position.z - camWidth, camera.transform.position.z + camWidth);
                transform.position = characterPosition;
            }
        }
    }
}