using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using UnityEngine.SceneManagement;
using TMPro;

public class ItemChanger : MonoBehaviour
{
    public string __currentItem;

    private string _prevItem;

    [SerializeField]
    private TextMeshProUGUI _currentItemDisplay;

    [SerializeField]
    private TextMeshProUGUI _counterDisplay;

    [SerializeField]
    private TextMeshProUGUI _results;

    [SerializeField]
    private float _gameCountdown = 60;

    [SerializeField]
    private int _baseCountdown = 6;
    private float _countdown;

    [SerializeField]
    private int _itemCount;


    [SerializeField]
    private AudioScript _audioScript;
    
    // Start is called before the first frame update
    void Start()
    {
        __currentItem = null;
        
        _countdown = _baseCountdown;

        _currentItemDisplay.text = "Ready?";

        _results.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        _gameCountdown -= Time.deltaTime;
        _countdown -= Time.deltaTime;

        if (_gameCountdown <= 0)
        {
            MessengerBoy();
        }

        if (__currentItem == null && _gameCountdown > 0)
        {
            _counterDisplay.text = ((int)(_countdown)).ToString();
        }
        else
        {
            _counterDisplay.gameObject.SetActive(false);
        }


        if (_countdown <= 0 && _gameCountdown > 0)
        {
            _prevItem = __currentItem;

            RandomizeItem();
        }
    }

    void RandomizeItem()
    {
        _countdown = _baseCountdown;
        System.Random random = new System.Random();
        int rand = random.Next(0, _itemCount);
        switch (rand)
        {
            case 0:
                __currentItem = ("Banana");
                break;
            case 1:
                __currentItem = ("Flour");
                break;
            case 2:
                __currentItem = ("Cheese");
                break;
            case 3:
                __currentItem = ("Milk");
                break;
            case 4:
                __currentItem = ("Sugar");
                break;
            case 5:
                __currentItem = ("Fish");
                break;
            case 6:
                __currentItem = ("Steak");
                break;
            case 7:
                __currentItem = ("Cabbage");
                break;
            case 8:
                __currentItem = ("Bread");
                break;
            case >= 9:
                __currentItem = ("Untagged");
                break;
        }
        if (_prevItem == __currentItem)
        {
            RandomizeItem();
        }
        _audioScript.PlaySound(rand);

        //setall all items back to active
        Collider[] childrenItems = GetComponentsInChildren<Collider>(true);
        foreach (Collider child in childrenItems)
        {
            child.gameObject.SetActive(true);
        }

        if (_gameCountdown > 0)
        {
            _currentItemDisplay.text = "Gather " + __currentItem + "!";
            Debug.Log(__currentItem);
        }
        else
        {
            _currentItemDisplay.gameObject.SetActive(false);
        }
    }

    void MessengerBoy()
    {
        StreamWriter writer = new StreamWriter("Assets/Resources/MessengerBoy.txt");

        //gets winning players
        Player[] players = (FindObjectsOfType<Player>()).OrderBy(i => i._score).Reverse().ToArray();

        writer.Write("234:" + players[0]._playerID + "," + players[1]._playerID + "," + players[2]._playerID + "," + players[3]._playerID);

        _results.gameObject.SetActive(true);
        _results.text = "Winner: Player " + players[0]._playerID +
            "\n2nd: Player " + players[1]._playerID +
            "\n3rd: Player " + players[2]._playerID +
            "\nLast: Player " + players[3]._playerID;

        Debug.Log(writer.ToString());
        writer.Close();

        Invoke("SwitchScene", 4);
    }

    void SwitchScene()
    {
        SceneManager.LoadScene("TheBoard");
    }
}


