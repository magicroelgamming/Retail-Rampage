using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private CharacterController _characterController;

    [SerializeField]
    private int _speed = 10;

    [SerializeField]
    private int _playerID;

    [SerializeField]
    ItemChanger _itemChanger;

    private int _score = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = new Vector3(-Input.GetAxis("LeftStickVertical" +  _playerID), 0, Input.GetAxis("LeftStickHorizontal" + _playerID));
        _characterController.Move(movement * Time.deltaTime * _speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == _itemChanger.__currentItem)
        {
            _score++;
            other.gameObject.SetActive(false);

            Debug.Log("Player" + _playerID + " Score: " + _score);
        }
    }
}