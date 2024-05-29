using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class HudProductFallGameScript : MonoBehaviour
{
    [SerializeField] private GameObject[] productprefabs;

    [SerializeField] public TextMeshProUGUI camera1Text;
    [SerializeField] public TextMeshProUGUI camera2Text;
    [SerializeField] public TextMeshProUGUI camera3Text;
    [SerializeField] public TextMeshProUGUI camera4Text;

    [SerializeField] public TextMeshProUGUI win1;
    [SerializeField] public TextMeshProUGUI win2;
    [SerializeField] public TextMeshProUGUI win3;
    [SerializeField] public TextMeshProUGUI win4;

    [SerializeField] public TextMeshProUGUI loose1;
    [SerializeField] public TextMeshProUGUI loose2;
    [SerializeField] public TextMeshProUGUI loose3;
    [SerializeField] public TextMeshProUGUI loose4;

    float[] productCollected = new float[4];

    public bool startDelayBeforeMainBoard = false;

    private int winLooseTime = 20;
    public bool _winIsActive = false;

    private float _timePassed;

    int[] PlayerEndingSpots;
    void Start()
    {
        win1.enabled = false;
        win2.enabled = false;
        win3.enabled = false;
        win4.enabled = false;
        loose1.enabled = false;
        loose2.enabled = false;
        loose3.enabled = false;
        loose4.enabled = false;
    }
    void Update()
    {
        _timePassed += Time.deltaTime;
        ProductsText();
        HudWinLoose();
        if(startDelayBeforeMainBoard)
        {
            MessengerBoy();
        }
    }
    void MessengerBoy()
    {
        
        StreamWriter writer = new StreamWriter("Assets/Resources/MessengerBoy.txt");
        string PlayerRanking = "234:";
        for (int i = 0; i < PlayerEndingSpots.Length; i++)
        {
            PlayerRanking += PlayerEndingSpots[i];
            if (i != (PlayerEndingSpots.Length-1))
            {
                PlayerRanking += ",";
            }
        }

        writer.Write(PlayerRanking);

        writer.Close();
        SceneManager.LoadScene("TheBoard");
    }
    public void GameOver()
    {
        startDelayBeforeMainBoard = true;
    }
    void ProductsText()
    {
        camera1Text.text = productCollected[0].ToString();
        camera1Text.color = Color.blue;

        camera2Text.text = productCollected[1].ToString();
        camera2Text.color = Color.blue;

        camera3Text.text = productCollected[2].ToString();
        camera3Text.color = Color.blue;

        camera4Text.text = productCollected[3].ToString();
        camera4Text.color = Color.blue;
    }
    void HudWinLoose()
    {/*
        if (productCollected[0] >= winLooseAmount)
        {
            win1.text = "Player 1 Wins!";
            win1.enabled = true;
            CameraTextDisabled();
            _winIsActive = true;
            Invoke("GameOver", 4f);
        }
        else if (productCollected[1] >= winLooseAmount)
        {
            win2.text = "Player 2 Wins!";
            win2.enabled = true;
            CameraTextDisabled();
            _winIsActive = true;
            Invoke("GameOver", 4f);
        }
        else if (productCollected[2] >= winLooseAmount)
        {
            win3.text = "Player 3 Wins!";
            win3.enabled = true;
            CameraTextDisabled();
            _winIsActive = true;
            Invoke("GameOver", 4f);
        }
        else if (productCollected[3] >= winLooseAmount)
        {
            win4.text = "Player 4 Wins!";
            win4.enabled = true;
            CameraTextDisabled();
            _winIsActive = true;
            Invoke("GameOver", 4f);
        }
        if (_winIsActive)
        {
            if (productCollected[0] < winLooseAmount)
            {
                loose1.text = "Player 1 Loses";
                CameraTextDisabled();
                loose1.enabled = true;
            }
            if (productCollected[1] < winLooseAmount)
            {
                loose2.text = "Player 2 Loses";
                CameraTextDisabled();
                loose2.enabled = true;
            }
            if (productCollected[2] < winLooseAmount)
            {
                loose3.text = "Player 3 Loses";
                CameraTextDisabled();
                loose3.enabled = true;
            }
            if (productCollected[3] < winLooseAmount)
            {
                loose4.text = "Player 4 Loses";
                CameraTextDisabled();
                loose4.enabled = true;
            }
        }
        */

        if (winLooseTime < _timePassed)
        {
            PlayerEndingSpots = new int[] { 1, 1, 1, 1 };
            for (int i = 0; i < productCollected.Length; i++)
            {
                for (int j = 0; j < productCollected.Length; j++)
                {
                    if (productCollected[i] < productCollected[j] && i != j)
                    {
                        PlayerEndingSpots[i] = PlayerEndingSpots[i] + 1;
                    }
                }
            }

            TextMeshProUGUI[] wintext = new TextMeshProUGUI[] {camera1Text,camera2Text,camera3Text,camera4Text };

            for (int i = 0; i < PlayerEndingSpots.Length; i++)
            {
                switch (PlayerEndingSpots[i])
                {
                    case 1:
                        wintext[i].text = "WINNER";
                        break;
                    default:
                        wintext[i].text = PlayerEndingSpots[i].ToString();
                        break;
                    case 4:
                        wintext[i].text = "LAST";
                        break;
                }
            }
            
            _winIsActive = true;
            Invoke("GameOver", 4f);
        }
    }
    void CameraTextDisabled()
    {
        camera1Text.enabled = false;
        camera2Text.enabled = false;
        camera3Text.enabled = false;
        camera4Text.enabled = false;
    }
    public void ProductCollected(int amount, string basketTag)
    {
        if (basketTag == "Basket/1")
        {
            productCollected[0] += amount;
        }
        if (basketTag == "Basket/2")
        {
            productCollected[1] += amount;
        }
        if (basketTag == "Basket/3")
        {
            productCollected[2] += amount;
        }
        if (basketTag == "Basket/4")
        {
            productCollected[3] += amount;
        }
    }
}