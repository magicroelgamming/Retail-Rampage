using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RopePull : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _rigidbody;

    [SerializeField]
    private float _speed = 3f; //this value might still need some works, so might the drag of the rigidbody

    [SerializeField]
    private TextMeshProUGUI _result;

    [SerializeField]
    private Image _attacker;

    [SerializeField]
    private Image _defender;

    private bool _canPull1 = true;
    private bool _canPull2 = true;

    private int[] players;

    // Start is called before the first frame update
    void Start()
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

        _result.gameObject.SetActive(false);

        GetColours();
    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log("player1:" + Input.GetAxis("LeftStickHorizontal1"));
        Debug.Log("player2:" + Input.GetAxis("LeftStickHorizontal2"));

        if (Input.GetAxis("LeftStickHorizontal" + players[0]) < 0 && _canPull1) //stick is left
        {
            _rigidbody.AddForce(Vector3.forward * _speed * Time.deltaTime, ForceMode.Impulse);
            _canPull1 = false;
        }
        if (Input.GetAxis("LeftStickHorizontal" + players[1]) > 0 && _canPull2) //stick is right
        {
            _rigidbody.AddForce(-Vector3.forward * _speed * Time.deltaTime, ForceMode.Impulse);
            _canPull2 = false;
        }

        if (Input.GetAxis("LeftStickHorizontal" + players[0]) >= 0) //stick is neutral 1
        {
            _canPull1 = true;
        }
        if (Input.GetAxis("LeftStickHorizontal" + players[1]) <= 0) //stick is neutral 2
        {
            _canPull2 = true;
        }

        if (this.transform.position.z >= 1)
        {
            //player 1 wins
            _result.gameObject.SetActive(true);
            _result.text = "Attacker wins!";
            MessengerBoy("1V1:true");
        }
        if (this.transform.position.z <= -1)
        {
            _result.gameObject.SetActive(true);
            _result.text = "Defender wins!";
            MessengerBoy("1V1:false");
        }



    }

    void MessengerBoy(string winner)
    {
        StreamWriter writer = new StreamWriter("Assets/Resources/MessengerBoy.txt");

        writer.Write(winner);

        writer.Close();

        Invoke("LoadBoard", 4);
    }

    void LoadBoard()
    {
        SceneManager.LoadScene("TheBoard");
    }

    void GetColours()
    {
        _defender.color = BoardControl.DataManager._playerColors[players[1] - 1].color;
        _attacker.color = BoardControl.DataManager._playerColors[players[0] - 1].color;
    }
}
