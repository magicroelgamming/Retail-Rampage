using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private CharacterController _characterController;

    [SerializeField]
    private int _speed = 10;

    [SerializeField]
    public int _playerID;

    [SerializeField]
    private TextMeshPro _scoreDisplay;

    [SerializeField]
    private ItemChanger _itemChanger;


    public int _score = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_itemChanger.__currentItem != null)
        {
            Vector3 movement = new Vector3(-Input.GetAxis("LeftStickVertical" + _playerID), 0, Input.GetAxis("LeftStickHorizontal" + _playerID));
            _characterController.Move(movement * Time.deltaTime * _speed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == _itemChanger.__currentItem)
        {
            _score++;
            other.gameObject.SetActive(false);

            Debug.Log("Player" + _playerID + " Score: " + _score);

            _scoreDisplay.text = "Score: " + _score;
        }
    }
}
