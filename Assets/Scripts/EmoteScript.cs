using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class EmoteScript : MonoBehaviour
{
    [SerializeField]
    private AudioSource source;
    [SerializeField]
    private AudioClip[] _clipsGood,_clipsBad;

    private float waitTimePlayer1, waitTimePlayer2, waitTimePlayer3, waitTimePlayer4;

    private System.Random rn;
    // Start is called before the first frame update
    void Start()
    {
        rn = new System.Random();

        if (GameObject.FindGameObjectsWithTag("Emote").Length ==1)
        {
            DontDestroyOnLoad(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Camera.main.transform.position;
        waitTimePlayer1 += Time.deltaTime;
        waitTimePlayer2 += Time.deltaTime;
        waitTimePlayer3 += Time.deltaTime;
        waitTimePlayer4 += Time.deltaTime;

        if ((Input.GetButton("LeftBumper1")&& waitTimePlayer1 >= 5)|| (Input.GetButton("LeftBumper2") && waitTimePlayer2 >= 5) || (Input.GetButton("LeftBumper3") && waitTimePlayer3 >= 5) || (Input.GetButton("LeftBumper4") && waitTimePlayer4 >= 5))
        {
            
            
            source.PlayOneShot(_clipsBad[rn.Next(0, _clipsBad.Length)]);
        }
        if ((Input.GetButton("RightBumper1") && waitTimePlayer1 >= 5) || (Input.GetButton("RightBumper2") && waitTimePlayer2 >= 5) || (Input.GetButton("RightBumper3") && waitTimePlayer3 >= 5) || (Input.GetButton("RightBumper4") && waitTimePlayer4 >= 5))
        {
            source.PlayOneShot(_clipsGood[rn.Next(0, _clipsGood.Length)]);
        }

        if ((Input.GetButton("LeftBumper1") && waitTimePlayer1 >= 5)) waitTimePlayer1 = 0;
        if ((Input.GetButton("LeftBumper2") && waitTimePlayer2 >= 5)) waitTimePlayer2 = 0;
        if ((Input.GetButton("LeftBumper3") && waitTimePlayer3 >= 5)) waitTimePlayer3 = 0;
        if ((Input.GetButton("LeftBumper4") && waitTimePlayer4 >= 5)) waitTimePlayer4 = 0;
        if ((Input.GetButton("RightBumper1") && waitTimePlayer1 >= 5)) waitTimePlayer1 = 0;
        if ((Input.GetButton("RightBumper2") && waitTimePlayer2 >= 5)) waitTimePlayer2 = 0;
        if ((Input.GetButton("RightBumper3") && waitTimePlayer3 >= 5)) waitTimePlayer3 = 0;
        if ((Input.GetButton("RightBumper4") && waitTimePlayer4 >= 5)) waitTimePlayer4 = 0;
    }
}
