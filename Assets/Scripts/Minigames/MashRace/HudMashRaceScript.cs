using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class HudMashRaceScript : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI win1;
    [SerializeField] public TextMeshProUGUI win2;
    [SerializeField] public TextMeshProUGUI win3;
    [SerializeField] public TextMeshProUGUI win4;

    [SerializeField] public TextMeshProUGUI loose1;
    [SerializeField] public TextMeshProUGUI loose2;
    [SerializeField] public TextMeshProUGUI loose3;
    [SerializeField] public TextMeshProUGUI loose4;
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