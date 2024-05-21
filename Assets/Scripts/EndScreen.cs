using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static BoardControl;

public class EndScreen : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _winnerText;

    [SerializeField]
    private TextMeshProUGUI _2ndtext;

    [SerializeField]
    private TextMeshProUGUI _3rdtext;

    [SerializeField]
    private TextMeshProUGUI _4thtext;
    
    private string _winners;

    // Start is called before the first frame update
    void Start()
    {

        StreamReader reader = new StreamReader("Assets/Resources/MessengerBoy.txt");

        _winners = reader.ReadLine();


        string[] winnersSplit = _winners.Split(':');

        string[] playerPlaces = winnersSplit[1].Split(',');

        _winnerText.text = "Player " + playerPlaces[0] + " Won!";
        _2ndtext.text = "2nd: Player " + playerPlaces[1];
        _3rdtext.text = "3rd: Player " + playerPlaces[2];
        _4thtext.text = "4th: Player " + playerPlaces[3];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("AButton1") || Input.GetButtonDown("AButton2") || Input.GetButtonDown("AButton3") || Input.GetButtonDown("AButton4"))
        {
            SceneManager.LoadScene("HomeScreen");
        }
    }

    
}
