using UnityEngine;
using UnityEngine.UI;
public class HudProductFallGameScript : MonoBehaviour
{
    [SerializeField] private GameObject[] productprefabs;

    [SerializeField] public Text camera1Text;
    [SerializeField] public Text camera2Text;
    [SerializeField] public Text camera3Text;
    [SerializeField] public Text camera4Text;

    [SerializeField] public Text win1;
    [SerializeField] public Text win2;
    [SerializeField] public Text win3;
    [SerializeField] public Text win4;

    [SerializeField] public Text loose1;
    [SerializeField] public Text loose2;
    [SerializeField] public Text loose3;
    [SerializeField] public Text loose4;

    private float productCollected1 = 0f;
    private float productCollected2 = 0f;
    private float productCollected3 = 0f;
    private float productCollected4 = 0f;

    private int winLooseAmount = 3;
    public bool _winIsActive = false;
    void Start()
    {
        productCollected1 = 0;
        productCollected2 = 0;
        productCollected3 = 0;
        productCollected4 = 0;
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
        ProductsText();
        HudWinLoose();
    }

    void ProductsText()
    {
        camera1Text.text = productCollected1.ToString();
        camera1Text.color = Color.blue;

        camera2Text.text = productCollected2.ToString();
        camera2Text.color = Color.blue;

        camera3Text.text = productCollected3.ToString();
        camera3Text.color = Color.blue;

        camera4Text.text = productCollected4.ToString();
        camera4Text.color = Color.blue;
    }
    void HudWinLoose()
    {
        if (productCollected1 == winLooseAmount)
        {
            win1.text = "Player 1 Wins!";
            win1.enabled = true;
            CameraTextDisabled();
            _winIsActive = true;
        }
        else if (productCollected2 == winLooseAmount)
        {
            win2.text = "Player 2 Wins!";
            win2.enabled = true;
            CameraTextDisabled();
            _winIsActive = true;
        }
        else if (productCollected3 == winLooseAmount)
        {
            win3.text = "Player 3 Wins!";
            win3.enabled = true;
            camera1Text.enabled = false;
            camera2Text.enabled = false;
            camera3Text.enabled = false;
            camera4Text.enabled = false;
            _winIsActive = true;
        }
        else if (productCollected4 == winLooseAmount)
        {
            win4.text = "Player 4 Wins!";
            win4.enabled = true;
            CameraTextDisabled();
            _winIsActive = true;
        }
        if (_winIsActive)
        {
            if (productCollected1 < winLooseAmount)
            {
                loose1.text = "Player 1 Looses";
                CameraTextDisabled();
                loose1.enabled = true;
            }
            if (productCollected2 < winLooseAmount)
            {
                loose2.text = "Player 2 Looses";
                CameraTextDisabled();
                loose2.enabled = true;
            }
            if (productCollected3 < winLooseAmount)
            {
                loose3.text = "Player 3 Looses";
                CameraTextDisabled();
                loose3.enabled = true;
            }
            if (productCollected4 < winLooseAmount)
            {
                loose4.text = "Player 4 Looses";
                CameraTextDisabled();
                loose4.enabled = true;
            }
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
            productCollected1 += amount;
        }
        if (basketTag == "Basket/2")
        {
            productCollected2 += amount;
        }
        if (basketTag == "Basket/3")
        {
            productCollected3 += amount;
        }
        if (basketTag == "Basket/4")
        {
            productCollected4 += amount;
        }
    }
}