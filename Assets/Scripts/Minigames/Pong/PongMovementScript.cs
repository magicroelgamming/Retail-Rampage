using System.IO;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
public class PongMovementScript : MonoBehaviour
{
    [SerializeField] private Camera camera;
    private float speed = 40f;
    private int[] _players;

    private void Start()
    {
        _players = new int[2];

        StreamReader Reader = new StreamReader("Assets/Resources/MessengerBoy.txt");
        string message = Reader.ReadToEnd();
        Reader.Close();
        string[] playerBuString = message.Split(':');
        for (int i = 0; i < playerBuString.Length; i++)
        {
            _players[i] = int.Parse(playerBuString[i]);
        }
        SetColour();
    }
    void Update()
    {
        Movement();
    }
    void Movement()
    {
        Vector3 combinedMovement = Vector3.zero;
        float screenWidthOffset = 16f;
        for (int i = 0; i < 2; i++)
        {
            Debug.Log(_players[i]);
            if (CompareTag($"Player/{i+1}")) //what the actual hot diggity fuck is going on here? -S
            {
                float verticalInput = Input.GetAxis($"LeftStickVertical{_players[i]}");
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

    private void SetColour()
    {
        for (int i = 0; i < 2; i++) //has to also write it like this, just a very werid way to do movement... -S
        {
            this.GetComponent<Renderer>().material.color = BoardControl.DataManager._playerColors[_players[i] - 1].color;
        }
    }
}