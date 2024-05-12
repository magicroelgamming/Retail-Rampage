using UnityEngine;

public class BombScript : MonoBehaviour
{
    private float fallSpeed = 3f;
    void Update()
    {
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Basket/1") || other.CompareTag("Basket/2") || other.CompareTag("Basket/3") || other.CompareTag("Basket/4"))
        {
            Destroy(other.transform.parent.gameObject);
        }
    }
}
