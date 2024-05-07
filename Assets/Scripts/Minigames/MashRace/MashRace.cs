using UnityEngine;

public class MashRace : MonoBehaviour
{
    private float speed = 10f;

    private bool moveIsActive = false;
    private bool moveIsActive1 = false;
    private bool moveIsActive2 = false;
    private bool moveIsActive3 = false;

    private float distanceToMove = 5f;
    private float timerMove = 0.5f;

    private Vector3 targetPosition;
    private Vector3 targetPosition1;
    private Vector3 targetPosition2;
    private Vector3 targetPosition3;

    void Update()
    {
        Movement();
    }
    private void Movement()
    {
        if (!moveIsActive && Input.GetMouseButtonDown(0))
        {
            GameObject cube = GameObject.FindGameObjectWithTag("Cube");
            {
                targetPosition = cube.transform.position + Vector3.up * distanceToMove;
                moveIsActive = true;
                Invoke("CanMove1", timerMove);
            }
        }
        else if (moveIsActive)
        {
            GameObject cube = GameObject.FindGameObjectWithTag("Cube");
            cube.transform.position = Vector3.MoveTowards(cube.transform.position, targetPosition, speed * Time.deltaTime);
            if (cube.transform.position == targetPosition)
            {
                moveIsActive = false;
            }
        }
        if (!moveIsActive1 && Input.GetKeyDown(KeyCode.Z))
        {
            GameObject sphere = GameObject.FindGameObjectWithTag("Sphere");
            targetPosition1 = sphere.transform.position + Vector3.up * distanceToMove;
            moveIsActive1 = true;
            Invoke("CanMove2", timerMove);
        }
        else if (moveIsActive1)
        {
            GameObject sphere = GameObject.FindGameObjectWithTag("Sphere");
            sphere.transform.position = Vector3.MoveTowards(sphere.transform.position, targetPosition1, speed * Time.deltaTime);
            if (sphere.transform.position == targetPosition1)
            {
                moveIsActive1 = false;
            }
        }
        if (!moveIsActive2 && Input.GetKeyDown(KeyCode.E))
        {
            GameObject capsule = GameObject.FindGameObjectWithTag("Capsule");
            targetPosition2 = capsule.transform.position + Vector3.up * distanceToMove;
            moveIsActive2 = true;
            Invoke("CanMove3", timerMove);
        }
        else if (moveIsActive2)
        {
            GameObject capsule = GameObject.FindGameObjectWithTag("Capsule");
            capsule.transform.position = Vector3.MoveTowards(capsule.transform.position, targetPosition2, speed * Time.deltaTime);
            if (capsule.transform.position == targetPosition2)
            {
                moveIsActive2 = false;
            }
        }
        if (!moveIsActive3 && Input.GetKeyDown(KeyCode.R))
        {
            GameObject cylinder = GameObject.FindGameObjectWithTag("Cylinder");
            targetPosition3 = cylinder.transform.position + Vector3.up * distanceToMove;
            moveIsActive3 = true;
            Invoke("CanMove4", timerMove);
        }
        else if (moveIsActive3)
        {
            GameObject cylinder = GameObject.FindGameObjectWithTag("Cylinder");
            cylinder.transform.position = Vector3.MoveTowards(cylinder.transform.position, targetPosition3, speed * Time.deltaTime);
            if (cylinder.transform.position == targetPosition3)
            {
                moveIsActive3 = false;
            }
        }
    }
    private void CanMove1()
    {
        moveIsActive = false;
    }
    private void CanMove2()
    {
        moveIsActive1 = false;
    }
    private void CanMove3()
    {
        moveIsActive2 = false;
    }
    private void CanMove4()
    {
        moveIsActive3 = false;
    }
}