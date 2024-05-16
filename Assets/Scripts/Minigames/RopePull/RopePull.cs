using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RopePull : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _rigidbody;

    [SerializeField]
    private float _speed = 3f; //this value might still need some works, so might the drag of the rigidbody

    private bool _canPull1 = true;
    private bool _canPull2 = true;

    private int[] players;

    // Start is called before the first frame update
    void Start()
    {
        StreamReader Reader = new StreamReader("Assets/Resources/MessengerBoy.txt");
        string message = Reader.ReadToEnd();
        Reader.Close();
        string[] playerBuString = message.Split(':');
        for (int i = 0; i < playerBuString.Length; i++)
        {
            players[i] = int.Parse(playerBuString[i]);
        }

    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log("player1:" + Input.GetAxis("LeftStickHorizontal1"));
        Debug.Log("player2:" + Input.GetAxis("LeftStickHorizontal2"));

        if (Input.GetAxis("LeftStickHorizontal" + players[0]) > 0 && _canPull1)
        {
            _rigidbody.AddForce(Vector3.one * _speed * Time.deltaTime, ForceMode.Impulse);
            _canPull1 = false;
        }
        if (Input.GetAxis("LeftStickHorizontal" + players[1]) < 0 && _canPull2)
        {
            _rigidbody.AddForce(-Vector3.one * _speed * Time.deltaTime, ForceMode.Impulse);
            _canPull2 = false;
        }

        if (Input.GetAxis("LeftStickHorizontal" + players[0]) <= 0)
        {
            _canPull1 = true;
        }
        if (Input.GetAxis("LeftStickHorizontal" + players[1]) >= 0)
        {
            _canPull2 = true;
        }

        if (this.transform.position.z >= 1)
        {
            //player 1 wins
            MessengerBoy("1V1:true");
        }
        if (this.transform.position.z <= -1)
        {
            MessengerBoy("1V1:false");
        }



    }

    void MessengerBoy(string winner)
    {
        StreamWriter writer = new StreamWriter("Assets/Resources/MessengerBoy.txt");

        writer.Write(winner);

        writer.Close();

        SceneManager.LoadScene("TheBoard");
    }
}
