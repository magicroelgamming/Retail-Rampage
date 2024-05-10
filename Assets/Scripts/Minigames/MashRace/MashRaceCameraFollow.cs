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
        newPosition.y = targetObject.position.y + 12f;
        transform.position = newPosition;
    }
}