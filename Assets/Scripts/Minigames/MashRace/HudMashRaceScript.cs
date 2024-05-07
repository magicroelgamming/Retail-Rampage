using UnityEngine;
using UnityEngine.UI;

public class HudMashRaceScript : MonoBehaviour
{
    [SerializeField] public Text win1;
    [SerializeField] public Text win2;
    [SerializeField] public Text win3;
    [SerializeField] public Text win4;

    [SerializeField] public Text loose1;
    [SerializeField] public Text loose2;
    [SerializeField] public Text loose3;
    [SerializeField] public Text loose4;

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
}
