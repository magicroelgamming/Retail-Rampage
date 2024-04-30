using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private Camera _cameraMain;
    [SerializeField]
    private GameObject _prefabBasePlate;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void CameraStartPlacement(int col, int row)
    {
        float fieldWidth = col;
        float fieldHeight = row;
        float cameraMainX = (float)Math.Ceiling(fieldWidth / 2) * _prefabBasePlate.transform.localScale.z;
        float cameraMainZ = (float)Math.Ceiling(fieldHeight / 2) * _prefabBasePlate.transform.localScale.x;
        _cameraMain.transform.position = new Vector3(cameraMainX, 7, cameraMainZ);
        _cameraMain.transform.rotation = Quaternion.AngleAxis(90, Vector3.right);
    }
}
