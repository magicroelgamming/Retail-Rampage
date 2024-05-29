using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UIElements;

public class CrossRoadPlayerInput : MonoBehaviour
{
    private Rigidbody _rb;
    public float Speed;
    public int PlayerId;
    [SerializeField]
    public int Score;
    private bool _hasItem;
    public bool IsPlaying = false;

    private TextMeshProUGUI _playerScore;

    void Start()
    {
        //_rb.velocity = Vector3.zero;
        ReturnToStart();
        _rb = GetComponent<Rigidbody>();
        _rb.constraints = RigidbodyConstraints.FreezeRotation;
        _playerScore = GameObject.Find("ScorePlayer" + PlayerId).GetComponent<TextMeshProUGUI>();
        
    }

    void Update()
    {
        Vector3 movement = new Vector3(Input.GetAxis("LeftStickHorizontal" + PlayerId), 0f, Input.GetAxis("LeftStickVertical" + PlayerId)).normalized;
        if (IsPlaying)
            _rb.velocity += (movement * Speed * Time.deltaTime) + new Vector3(0,-0.1f,0);
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
            _playerScore.text = "Player " + (PlayerId) + " \nScore: " + Score;
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
                transform.position = new Vector3(gameObject.transform.position.x, 0.10f, gameObject.transform.position.z);
                _hasItem = false;
            }
        }

    }
}
