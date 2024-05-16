using UnityEngine;
public class MashRaceCameraFollow : MonoBehaviour
{
    public Transform targetObject;
    void Update()
    {
        ObjectFollow();
    }
    private void ObjectFollow()
    {
        Vector3 newPosition = transform.position;
        newPosition.z = targetObject.position.z + 10f;
        transform.position = newPosition;
    }
}