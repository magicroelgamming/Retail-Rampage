using UnityEngine;

public class ProductSpeedScript : MonoBehaviour
{
    public float fallSpeed = 5f;
    void Update()
    {
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}