using UnityEngine;
public class ProductFall : MonoBehaviour
{
    public GameObject[] objectPrefabs;
    public float spawnInterval = 2f;
    public float spawnHeight = 1f;
    private float timer;

    public Camera[] cameras;

    public HudProductFallGameScript hudScript;

    void Update()
    {
        if (hudScript._winIsActive == true)
        {
            return;
        }
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnObject();
            timer = 0f;
        }
    }
    void SpawnObject()
    {
        int randomIndex = Random.Range(0, objectPrefabs.Length);

        foreach (Camera camera in cameras)
        {
            float zPos = 18f;
            float yPos = 10f;
            float minX = camera.ViewportToWorldPoint(new Vector3(0.1f, yPos, zPos)).x;
            float maxX = camera.ViewportToWorldPoint(new Vector3(0.9f, yPos, zPos)).x;

            float randomX = Random.Range(minX, maxX);

            Vector3 spawnPosition = new Vector3(randomX, 25, spawnHeight);
            Instantiate(objectPrefabs[randomIndex], spawnPosition, Quaternion.identity);
        }
    }
}