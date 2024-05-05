using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemChanger : MonoBehaviour
{
    public string __currentItem;

    [SerializeField]
    private float _gameCountdown = 60;

    [SerializeField]
    private int _baseCountdown = 6;
    private float _countdown;

    [SerializeField]
    private int _itemCount;

    // Start is called before the first frame update
    void Start()
    {
        _countdown = _baseCountdown;
    }

    // Update is called once per frame
    void Update()
    {
        _gameCountdown -= Time.deltaTime;
        _countdown -= Time.deltaTime;

        if (_gameCountdown <= 0)
        {
            Debug.Log("Finish!");
        }


        

        if (_countdown <= 0)
        {
            _countdown = _baseCountdown;
            System.Random random = new System.Random();
            int rand = random.Next(0, _itemCount);
            switch (rand)
            {
                case 0:
                    __currentItem = ("Banana");
                    break;
                case 1:
                    __currentItem = ("Flour");
                    break;
                case 2:
                    __currentItem = ("Cheese");
                    break;
                case 3:
                    __currentItem = ("Milk");
                    break;
                case 4:
                    __currentItem = ("Sugar");
                    break;
                case 5:
                    __currentItem = ("Fish");
                    break;
                case 6:
                    __currentItem = ("Steak");
                    break;
                case 7:
                    __currentItem = ("Cabbage");
                    break;
                case 8:
                    __currentItem = ("Bread");
                    break;
                case >= 9:
                    __currentItem = ("Untagged");
                    break;
            }

            //setall all items back to active
            Collider[] childrenItems = GetComponentsInChildren<Collider>(true);
            foreach (Collider child in childrenItems)
            {
                child.gameObject.SetActive(true);
            }

            Debug.Log(__currentItem);
        }
    }
}
