using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCart : MonoBehaviour
{
    private Vector3 _startPosition, _endPosition;
    public float Speed;
    public float Distance;
    private float _fraction;

    void Start()
    {
        _startPosition = transform.position;
        _endPosition = new Vector3(_startPosition.x + Distance, _startPosition.y, _startPosition.z);
    }

    
    void Update()
    {

        _fraction = Mathf.PingPong(Time.time * Speed, 1);
        transform.position = Vector3.Lerp(_startPosition, _endPosition, _fraction);
    }

}
