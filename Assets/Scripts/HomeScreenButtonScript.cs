using System.Collections;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static BoardControl;

public class HomeScreenButtonScript : MonoBehaviour
{
    private int _rotationCamera;

    [SerializeField]
    GameObject _center;
    [SerializeField]
    Material[] _playerColors;
    [SerializeField]
    GameObject _prefabBasePlate;

    ArrayList[,] _tiles = new ArrayList[5,5];
    GameObject[,] _tilespots = new GameObject[5,5];

    public TextMeshProUGUI PlayersText;
    private bool[] playerJoined = new bool[4];
    public bool startDelayBeforeMainBoard = false;
    private int playerReadyCount = 0;

    [SerializeField]
    private GameObject[] _shopPrefabs;

    private string[] _brandNames, _shopNames;

    System.Random rn = new System.Random();


    void Start()
    {
        _brandNames = new string[] { "Garry's", "Irish", "WcDonalds", "Jeff's", "Roel's", "Andre's", "Evy's", "Sander's", "Jasper's", "Grigory's", "Mintendo", "OCircle", "Moist", "TrainConsole", "Baldur Studios", "Sun 8", "Goal", "Trottoir", "Adequate Purchase", "Taco Hut", "Pizza Bell", "Repairing Good", "6 Woman", "Out Of The Closet", "Forever 12", "M&H", "Belgian Tomorrow", "Forest", "Florida", "Webflex", "Ball Cluster", "1/77", "StarMeadow", "M.A.H.", "8008", "Nalop", "Calm Susan", "1 Minute", "Drop In" };
        _shopNames = new string[] { "Stand", "Parking Lot", "Gas Station", "Shop", "Restaurant", "Super Market", "Electronics Store", "Mall", "Mega Mall", "Headquarters", "Headquarters", "Headquarters", "Headquarters" };

        MapGeneration();

        UpdatePlayersText();
        GroundPlatePlacing();
        CameraStartPlacement();
    }
    void Update()
    {
        ButtonPressToStart();
        ExitGame();
        if (startDelayBeforeMainBoard)
        {
            MessengerBoy();
        }
        
        _center.transform.eulerAngles += new Vector3(0,10,0)*Time.deltaTime;
    }
    void ExitGame()
    {
        if (Input.GetButtonDown("YButton1"))
        {
           Application.Quit();
        }
    }
    void MessengerBoy()
    {
        StreamWriter writer = new StreamWriter("Assets/Resources/MessengerBoy.txt");

        writer.Write(playerReadyCount);

        writer.Close();

        SceneManager.LoadScene("TheBoard");
    }
    void PlayersAreReady()
    {
        startDelayBeforeMainBoard = true;
    }
    void ButtonPressToStart()
    {
        if (Input.GetButtonDown("AButton1") && !playerJoined[0])
        {
            playerJoined[0] = true;
            playerReadyCount++;
            UpdatePlayersText();
        }

        if (Input.GetButtonDown("AButton2") && !playerJoined[1])
        {
            playerJoined[1] = true;
            playerReadyCount++;
            UpdatePlayersText();
        }

        if (Input.GetButtonDown("AButton3") && !playerJoined[2])
        {
            playerJoined[2] = true;
            playerReadyCount++;
            UpdatePlayersText();
        }

        if (Input.GetButtonDown("AButton4") && !playerJoined[3])
        {
            playerJoined[3] = true;
            playerReadyCount++;
            UpdatePlayersText();
        }
        if (playerReadyCount >= 2 && Input.GetButtonDown("BButton1"))
        {
            PlayersAreReady();
        }
    }
    void UpdatePlayersText()
    {
        string players = "Players: [";
        bool firstPlayerAdded = false;
        for (int i = 0; i < playerJoined.Length; i++)
        {
            if (playerJoined[i])
            {
                if (firstPlayerAdded)
                    players += ",";
                players += (i + 1).ToString();
                firstPlayerAdded = true;
            }
        }
        if (playerJoined[3])
            players += "]";
        PlayersText.text = players;
    }

    private void MapGeneration()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                switch (i + (j * 10))
                {
                    case 0:
                        ArrayList AllTileInfo = new ArrayList() {  _playerColors[0], 10, 0,  _brandNames[rn.Next(0,  _brandNames.Length)] + " " +  _shopNames[9] };
                         _tiles[i, j] = AllTileInfo;

                        break;

                    case 4:
                        AllTileInfo = new ArrayList() {  _playerColors[1], 12, 0,  _brandNames[rn.Next(0,  _brandNames.Length)] + " " +  _shopNames[11] };
                         _tiles[i, j] = AllTileInfo;
                        break;

                    case 40:
                        AllTileInfo = new ArrayList() {  _playerColors[2], 11, 0,  _brandNames[rn.Next(0,  _brandNames.Length)] + " " +  _shopNames[10] };
                         _tiles[i, j] = AllTileInfo;
                        break;

                    case 44:
                        AllTileInfo = new ArrayList() {  _playerColors[3], 13, 0,  _brandNames[rn.Next(0,  _brandNames.Length)] + " " +  _shopNames[12] };
                         _tiles[i, j] = AllTileInfo;
                        break;

                    case 1:
                    case 3:
                    case 10:
                    case 11:
                    case 13:
                    case 14:
                    case 30:
                    case 31:
                    case 33:
                    case 34:
                    case 41:
                    case 43:

                        AllTileInfo = new ArrayList();
                        AllTileInfo.Add( _playerColors[4]);
                        int TileLevel = rn.Next(1, 5);
                        AllTileInfo.Add(TileLevel);
                        int TileCost = rn.Next(((TileLevel * 100) - 20), (((TileLevel * 100) + 20) + 1));
                        AllTileInfo.Add(TileCost);
                        string TileName =  _brandNames[rn.Next(0,  _brandNames.Length)] + " " +  _shopNames[TileLevel - 1];
                        AllTileInfo.Add(TileName);
                         _tiles[i, j] = AllTileInfo;

                        break;

                    case 22:

                        AllTileInfo = new ArrayList();
                        AllTileInfo.Add( _playerColors[4]);
                        TileLevel = rn.Next(7, 10);
                        AllTileInfo.Add(TileLevel);
                        TileCost = rn.Next(((TileLevel * 100) - 20), (((TileLevel * 100) + 20) + 1));
                        AllTileInfo.Add(TileCost);
                        TileName =  _brandNames[rn.Next(0,  _brandNames.Length)] + " " +  _shopNames[TileLevel - 1];
                        AllTileInfo.Add(TileName);
                         _tiles[i, j] = AllTileInfo;

                        break;
                    default:

                        AllTileInfo = new ArrayList();
                        AllTileInfo.Add( _playerColors[4]);
                        TileLevel = rn.Next(5, 9);
                        AllTileInfo.Add(TileLevel);
                        TileCost = rn.Next(((TileLevel * 100) - 20), (((TileLevel * 100) + 20) + 1));
                        AllTileInfo.Add(TileCost);
                        TileName =  _brandNames[rn.Next(0,  _brandNames.Length)] + " " +  _shopNames[TileLevel - 1];
                        AllTileInfo.Add(TileName);
                         _tiles[i, j] = AllTileInfo;
                        break;
                }
            }
        }
    }

    private void GroundPlatePlacing()
    {
        for (int i = 0; i <  5; i++)
        {
            for (int j = 0; j <  5; j++)
            {
                GameObject NewGroundPlate = GameObject.Instantiate( _prefabBasePlate, transform, true);
                NewGroundPlate.transform.position += new Vector3(i * 1.5f, 0, j * 1.5f);
                NewGroundPlate.transform.localScale = NewGroundPlate.transform.localScale / 4;
                GameObject NewBuilding = GameObject.Instantiate(_shopPrefabs[(int) _tiles[i, j][1] - 1], transform, true);
                NewBuilding.transform.localScale = NewBuilding.transform.localScale / 4;
                NewBuilding.transform.parent = NewGroundPlate.transform;
                NewBuilding.transform.position = NewGroundPlate.transform.position + new Vector3(0, 0.32f, 0);

                //Tell me te explain this if you don't get this part -M
                Material NewGroundPlateMaterial = (Material)((ArrayList) _tiles[i, j])[0];


                NewGroundPlate.GetComponent<MeshRenderer>().material = NewGroundPlateMaterial;

                Material[] buildingMaterials = NewBuilding.GetComponent<MeshRenderer>().materials;

                for (int k = 0; k < NewBuilding.GetComponent<MeshRenderer>().materials.Length; k++)
                {
                    if (NewBuilding.GetComponent<MeshRenderer>().materials[k].name == "PlayerColour (Instance)")
                    {

                        buildingMaterials[k] = NewGroundPlateMaterial;

                    }
                }
                NewBuilding.GetComponent<MeshRenderer>().materials = buildingMaterials;

                 _tilespots[i, j] = NewGroundPlate;

            }
        }
    }
    private void CameraStartPlacement()
    {
        Camera theCamera = Camera.main;
        _center.transform.position = _tilespots[2,2].transform.position;

        theCamera.transform.localPosition = new Vector3(0f, 3f, -6f);
        theCamera.transform.localEulerAngles = new Vector3(33,0,0);
    }
}