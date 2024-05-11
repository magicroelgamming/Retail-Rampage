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

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log("player1:" + Input.GetAxis("LeftStickHorizontal1"));
        Debug.Log("player2:" + Input.GetAxis("LeftStickHorizontal2"));

        if (Input.GetAxis("LeftStickHorizontal1") > 0 && _canPull1)
        {
            _rigidbody.AddForce(Vector3.one * _speed * Time.deltaTime, ForceMode.Impulse);
            _canPull1 = false;
        }
        if (Input.GetAxis("LeftStickHorizontal2") < 0 && _canPull2)
        {
            _rigidbody.AddForce(-Vector3.one * _speed * Time.deltaTime, ForceMode.Impulse);
            _canPull2 = false;
        }

        if (Input.GetAxis("LeftStickHorizontal1") <= 0)
        {
            _canPull1 = true;
        }
        if (Input.GetAxis("LeftStickHorizontal2") >= 0)
        {
            _canPull2 = true;
        }

        if (this.transform.position.z >= 1)
        {
            //player 1 wins
            MessengerBoy("1v1:true");
        }
        if (this.transform.position.z <= -1)
        {
            MessengerBoy("1v1:false");
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
