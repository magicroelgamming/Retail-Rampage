using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterMovementBehaviourScript : MonoBehaviour
{
    [SerializeField] private HudProductFallGameScript hudScript;
    [SerializeField] private Camera[] cameraBasket;
    void Update()
    {
        Movement();
    }
    void Movement()
    {
        Vector3 combinedMovement = Vector3.zero;

        for (int i = 1; i <= 4; i++)
        {
            if (CompareTag($"Basket/{i}"))
            {
                float horizontalInput = Input.GetAxis($"LeftStickHorizontal{i}");
                combinedMovement += Vector3.right * horizontalInput * 3f;

                Camera basketCamera = cameraBasket[i - 1];

                float camHeight = basketCamera.orthographicSize;
                float camWidth = camHeight * basketCamera.aspect + 2.5f;

                Vector3 characterPosition = transform.position;
                characterPosition += combinedMovement * Time.deltaTime;
                characterPosition.x = Mathf.Clamp(characterPosition.x, basketCamera.transform.position.x - camWidth, basketCamera.transform.position.x + camWidth);
                transform.position = characterPosition;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Product"))
        {
            hudScript.ProductCollected(1, tag);
            Destroy(other.gameObject);
        }
        for (int i = 1; i <= 4; i++)
        {
            if (CompareTag($"Basket/{i}"))
            {
                if (other.CompareTag("Bomb"))
                {
                    switch (i)
                    {
                        case 1:
                            hudScript.loose1.text = "Player 1 Loses";
                            hudScript.loose1.enabled = true;
                            TextEnabled();
                            break;
                        case 2:
                            hudScript.loose2.text = "Player 2 Loses";
                            hudScript.loose2.enabled = true;
                            TextEnabled();
                            break;
                        case 3:
                            hudScript.loose3.text = "Player 3 Loses";
                            hudScript.loose3.enabled = true;
                            TextEnabled();
                            break;
                        case 4:
                            hudScript.loose4.text = "Player 4 Loses";
                            hudScript.loose4.enabled = true;
                            TextEnabled();
                            break;
                    }
                    if (hudScript.loose1.enabled && hudScript.loose2.enabled && hudScript.loose3.enabled)
                    {
                        hudScript.win4.text = "Player 4 Wins";
                        hudScript.win4.enabled = true;
                        TextEnabled();
                    }
                    else if (hudScript.loose1.enabled && hudScript.loose2.enabled && hudScript.loose4.enabled)
                    {
                        hudScript.win3.text = "Player 3 Wins";
                        hudScript.win3.enabled = true;
                        TextEnabled();
                    }
                    else if (hudScript.loose1.enabled && hudScript.loose3.enabled && hudScript.loose4.enabled)
                    {
                        hudScript.win2.text = "Player 2 Wins";
                        hudScript.win2.enabled = true;
                        TextEnabled();
                    }
                    else if (hudScript.loose2.enabled && hudScript.loose3.enabled && hudScript.loose4.enabled)
                    {
                        hudScript.win1.text = "Player 1 Wins";
                        hudScript.win1.enabled = true;
                        TextEnabled();
                    }
                }
            }
        }
    }
    void TextEnabled()
    {
        hudScript.camera1Text.enabled = false;
        hudScript.camera2Text.enabled = false;
        hudScript.camera3Text.enabled = false;
        hudScript.camera4Text.enabled = false;
    }
}
