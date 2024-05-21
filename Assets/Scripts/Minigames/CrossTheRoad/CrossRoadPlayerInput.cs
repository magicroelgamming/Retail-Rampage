using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UIElements;

public class CrossRoadPlayerInput : MonoBehaviour
{
    private CharacterController _charController;
    public float Speed;
    public int PlayerId;
    [SerializeField]
    public int Score;
    private bool _hasItem;
    public bool IsPlaying = false;

    private TextMeshProUGUI _playerScore;

    void Start()
    {    
        _charController = GetComponent<CharacterController>();
        _playerScore = GameObject.Find("ScorePlayer" + PlayerId).GetComponent<TextMeshProUGUI>();
        ReturnToStart();
    }

    void Update()
    {
        if (IsPlaying)
            _charController.Move(new Vector3(Input.GetAxis("LeftStickHorizontal" + PlayerId) * Speed * Time.deltaTime, 0f, Input.GetAxis("LeftStickVertical" + PlayerId) * Speed * Time.deltaTime));
        transform.position = new Vector3(transform.position.x, 0.25f, transform.position.z);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Cart")
        {
            ReturnToStart();
        }
        if (other.tag == "Checkpoint")
        {
            _hasItem = true;
            transform.GetChild(0).gameObject.SetActive(true);
        }
        if (other.tag == "PlayerStart" && other.GetComponent<PlayerStartPoint>().ID == PlayerId && _hasItem)
        {
            Score++;
            _playerScore.text = "Score Player " + (PlayerId) + ": " + Score;
            _hasItem = false;
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    private void ReturnToStart()
    {
        foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("PlayerStart"))
        {
            if (gameObject.GetComponent<PlayerStartPoint>().ID == PlayerId)
            {
                transform.GetChild(0).gameObject.SetActive(false);
                _charController.enabled = false;
                transform.position = new Vector3(gameObject.transform.position.x, 0.25f, gameObject.transform.position.z);
                _charController.enabled = true;
                _hasItem = false;
            }
        }

    }
}
