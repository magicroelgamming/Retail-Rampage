using System;
using System.Collections;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoardControl : MonoBehaviour
{
    [SerializeField]
    private GameObject _orbit;

    [SerializeField]
    private Camera _cameraMain;

    [SerializeField]
    private GameObject _prefabBasePlate;

    [SerializeField]
    private Material[] _playerColors;

    //temp
    [SerializeField]
    private Material _selected;
    //

    [SerializeField]
    private GameObject _TildeDetailDisplay;

    [SerializeField]
    private GameObject _BuyButtonDisplay;

    [SerializeField]
    private TextMeshProUGUI _nameBuildingDisplay;

    [SerializeField]
    private TextMeshProUGUI _ownerBuildingDisplay;

    [SerializeField]
    private TextMeshProUGUI _levelBuildingDisplay;

    [SerializeField]
    private TextMeshProUGUI _costBuildingDisplay;

    [SerializeField]
    private TextMeshProUGUI _playerMoneyDisplay;

    [SerializeField]
    private TextMeshProUGUI _playerNumberDisplay;

    [SerializeField]
    private GameObject[] _shopPrefabs;

    [SerializeField]
    private GameObject _4x4Map;

    [SerializeField]
    private GameObject _5x5Map;


    private bool _animRotation;

    private float _animRotationSpeed = 0.15f;

    private float _animRotationHeight = 1f;

    private bool _animOnBoardMovement;

    private float _animOnBoardSmoothness = 500;

    private float _animOnBoardFrame = 0;

    private float _tileToCameraX;

    private float _tileToCameraZ;

    private float _onBoardCameraOffset = -5;

    float waitedtime;



    System.Random rn = new System.Random();
    public static class DataManager
    {

        static public string MessangerBoy;

        static public Camera _cameraMain;


        static public int PlayerCount;

        static public GameObject _prefabBasePlate;

        static public Material[] _playerColors;

        //temp
        static public Material _selected;
        //

        static public GameObject _TildeDetailDisplay;

        static public GameObject _BuyButtonDisplay;

        static public TextMeshProUGUI _nameBuildingDisplay;

        static public TextMeshProUGUI _ownerBuildingDisplay;

        static public TextMeshProUGUI _levelBuildingDisplay;

        static public TextMeshProUGUI _costBuildingDisplay;

        static public TextMeshProUGUI _playerMoneyDisplay;

        static public TextMeshProUGUI _playerNumberDisplay;

        static public GameObject[] _shopPrefabs;

        [SerializeField]
        static public GameObject _4x4Map;

        [SerializeField]
        static public GameObject _5x5Map;


        static public GameObject _orbit;


        static public ArrayList[] _PlayerInfo;

        static public GameObject[,] _tilespots;

        static public ArrayList[,] _tiles;

        static public string[] _shopNames, _brandNames;

        static public int _columns, _rows;

        static public int _rounds;

        static public int _playerturn;

        static public int _currentPlayer;

        static public int[] _selectedTile = new int[2];

        static public int _tileView;

        static public bool _allowedToMove;

        static public bool _batteling;

        static public bool _contesting;

        static public int _battleNumber;

        static public int _contestNumber;

        static public int _roundNumber;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (DataManager.PlayerCount == 0)
        {
            StreamReader reader = new StreamReader("Assets/Resources/MessengerBoy.txt");

            DataManager.MessangerBoy = reader.ReadLine();

            reader.Close();

            DataManager.PlayerCount = int.Parse(DataManager.MessangerBoy);

            DataManager._cameraMain = _cameraMain;
            DataManager._prefabBasePlate = _prefabBasePlate;
            DataManager._playerColors = _playerColors;
            DataManager._selected = _selected;
            DataManager._TildeDetailDisplay = _TildeDetailDisplay;
            DataManager._BuyButtonDisplay = _BuyButtonDisplay;
            DataManager._nameBuildingDisplay = _nameBuildingDisplay;
            DataManager._ownerBuildingDisplay = _ownerBuildingDisplay;
            DataManager._levelBuildingDisplay = _levelBuildingDisplay;
            DataManager._costBuildingDisplay = _costBuildingDisplay;
            DataManager._playerMoneyDisplay = _playerMoneyDisplay;
            DataManager._playerNumberDisplay = _playerNumberDisplay;
            DataManager._shopPrefabs = _shopPrefabs;
            DataManager._4x4Map = _4x4Map;
            DataManager._5x5Map = _5x5Map;
            DataManager._orbit = _orbit;


            DontDestroyOnLoad(this);

            DataManager._brandNames = new string[] { "Garry's", "Irish", "WcDonalds", "Jeff's", "Roel's", "Andre's", "Evy's", "Sander's", "Jasper's", "Grigory's", "Mintendo", "OCircle", "Moist", "TrainConsole", "Baldur Studios", "Sun 8", "Goal", "Trottoir", "Adequate Purchase", "Taco Hut", "Pizza Bell", "Repairing Good", "6 Woman", "Out Of The Closet", "Forever 12", "M&H", "Belgian Tomorrow", "Forest", "Florida", "Webflex", "Ball Cluster", "1/77", "StarMeadow", "M.A.H.", "8008", "Nalop", "Calm Susan", "1 Minute", "Drop In", "" };

            DataManager._shopNames = new string[] { "Stand", "Parking Lot", "Gas Station", "Shop", "Restaurant", "Super Market", "Electronics Store", "Mall", "Mega Mall", "Headquarters", "Headquarters", "Headquarters", "Headquarters" };

            DataManager._allowedToMove = true;

            DataManager._tiles = new ArrayList[0, 0];

            DataManager._tilespots = new GameObject[0, 0];

            DataManager._PlayerInfo = new ArrayList[DataManager.PlayerCount];

            switch (DataManager.PlayerCount)
            {
                case 2:
                    DataManager._columns = 4; DataManager._rows = 4;
                    DataManager._tiles = new ArrayList[DataManager._columns, DataManager._rows];
                    DataManager._tilespots = new GameObject[DataManager._columns, DataManager._rows];
                    break;

                default:
                    DataManager._columns = 5; DataManager._rows = 5;
                    DataManager._tiles = new ArrayList[DataManager._columns, DataManager._rows];
                    DataManager._tilespots = new GameObject[DataManager._columns, DataManager._rows];
                    break;
            }


            MapGeneration();
            PlayerInitialization();
            GroundPlatePlacing();
            CameraStartPlacement();

            DataManager._TildeDetailDisplay.GetComponentInParent<CanvasGroup>().alpha = 0;

            PlayerTurn();
        }
        else
        {

            foreach (GameObject tile in DataManager._tilespots)
            {
                tile.SetActive(true);
            }

            GameObject[] allBoards = GameObject.FindGameObjectsWithTag("Board");
            foreach (var board in allBoards)
            {
                if (board.transform.childCount == 0)
                {
                    board.SetActive(false);
                }
                else
                {
                    board.SetActive(true);
                }
            }

            DataManager._cameraMain = Camera.main;
            DataManager._TildeDetailDisplay = GameObject.Find("TileDetailScreen");
            DataManager._BuyButtonDisplay = GameObject.Find("BuyButton");

            DataManager._nameBuildingDisplay = GameObject.Find("Name").GetComponent<TextMeshProUGUI>();
            DataManager._ownerBuildingDisplay = GameObject.Find("Owner").GetComponent<TextMeshProUGUI>();
            DataManager._levelBuildingDisplay = GameObject.Find("LVL").GetComponent<TextMeshProUGUI>();
            DataManager._costBuildingDisplay = GameObject.Find("Cost").GetComponent<TextMeshProUGUI>();
            DataManager._playerMoneyDisplay = GameObject.Find("Money").GetComponent<TextMeshProUGUI>();
            DataManager._playerNumberDisplay = GameObject.Find("PlayerNumber").GetComponent<TextMeshProUGUI>();

            DataManager._TildeDetailDisplay.GetComponentInParent<CanvasGroup>().alpha = 0;

            _animRotation = false;

            CameraStartPlacement();

            //Debug.Log(DataManager._playerturn);

            StreamReader reader = new StreamReader("Assets/Resources/MessengerBoy.txt");

            DataManager.MessangerBoy = reader.ReadLine();

            reader.Close();

            //Debug.Log(DataManager.MessangerBoy);

            string[] messangerBoySplit = DataManager.MessangerBoy.Split(':');

            if (messangerBoySplit[0] == "1V1")
            {
                if (messangerBoySplit[1] == "true")
                {
                    BattleConceeded(true);
                }
                else
                {
                    BattleConceeded(false);
                }
                DataManager._batteling = false;
                DataManager._contesting = false;
            }
            else
            {
                DataManager._playerturn = 0;
                ContestConceeded(messangerBoySplit[1].Split(','));
                DataManager._contesting = false;
            }
        }
    }
    private void CameraStartPlacement()
    {
        float _animStep = ((DataManager._columns - 1f) / 2f);
        _tileToCameraX = (float)DataManager._selectedTile[0] / _animStep - 1f;
        _tileToCameraZ = (float)DataManager._selectedTile[1] / _animStep;
        Vector3 centralTilePosition = Vector3.Lerp(DataManager._tilespots[0,0].transform.position, DataManager._tilespots[DataManager._columns -1, DataManager._rows -1].transform.position, 0.5f);
        DataManager._cameraMain.transform.parent.position = centralTilePosition;
        DataManager._cameraMain.transform.parent.localEulerAngles = Vector3.zero;
        DataManager._cameraMain.transform.localPosition = new Vector3(_tileToCameraX, 4, _tileToCameraZ + _onBoardCameraOffset);
        DataManager._cameraMain.transform.localEulerAngles = new Vector3(45, 0, 0);
        _animRotation = false;
    }

    private void MapGeneration()
    {
        for (int i = 0; i < DataManager._columns; i++)
        {
            for (int j = 0; j < DataManager._rows; j++)
            {
                //Debug.Log("-------------------------------------------------------------------------------");
                //Debug.Log("Start");
                //Debug.Log(i.ToString() + " _ " + j.ToString());
                switch (DataManager.PlayerCount)
                {
                    case 2:

                        switch (i + (j * 10))
                        {
                            case 0:
                                ArrayList AllTileInfo = new ArrayList() { DataManager._playerColors[0], 10, 0, DataManager._brandNames[rn.Next(0, DataManager._brandNames.Length)] + " " + DataManager._shopNames[9] };
                                DataManager._tiles[i, j] = AllTileInfo;

                                break;

                            case 33:
                                AllTileInfo = new ArrayList() { DataManager._playerColors[1], 13, 0, DataManager._brandNames[rn.Next(0, DataManager._brandNames.Length)] + " " + DataManager._shopNames[9] };
                                DataManager._tiles[i, j] = AllTileInfo;
                                break;

                            case 1:
                            case 10:
                            case 11:
                            case 22:
                            case 23:
                            case 32:

                                AllTileInfo = new ArrayList();
                                AllTileInfo.Add(DataManager._playerColors[4]);
                                int TileLevel = rn.Next(1, 5);
                                AllTileInfo.Add(TileLevel);
                                int TileCost = rn.Next(((TileLevel * 100) - 20), (((TileLevel * 100) + 20) + 1));
                                AllTileInfo.Add(TileCost);
                                string TileName = DataManager._brandNames[rn.Next(0, DataManager._brandNames.Length)] + " " + DataManager._shopNames[TileLevel - 1];
                                AllTileInfo.Add(TileName);
                                DataManager._tiles[i, j] = AllTileInfo;

                                break;

                            case 3:
                            case 30:

                                AllTileInfo = new ArrayList();
                                AllTileInfo.Add(DataManager._playerColors[4]);
                                TileLevel = rn.Next(7, 10);
                                AllTileInfo.Add(TileLevel);
                                TileCost = rn.Next(((TileLevel * 100) - 20), (((TileLevel * 100) + 20) + 1));
                                AllTileInfo.Add(TileCost);
                                TileName = DataManager._brandNames[rn.Next(0, DataManager._brandNames.Length)] + " " + DataManager._shopNames[TileLevel - 1];
                                AllTileInfo.Add(TileName);
                                DataManager._tiles[i, j] = AllTileInfo;

                                break;

                            default:

                                AllTileInfo = new ArrayList();
                                AllTileInfo.Add(DataManager._playerColors[4]);
                                TileLevel = rn.Next(5, 9);
                                AllTileInfo.Add(TileLevel);
                                TileCost = rn.Next(((TileLevel * 100) - 20), (((TileLevel * 100) + 20) + 1));
                                AllTileInfo.Add(TileCost);
                                TileName = DataManager._brandNames[rn.Next(0, DataManager._brandNames.Length)] + " " + DataManager._shopNames[TileLevel - 1];
                                AllTileInfo.Add(TileName);
                                DataManager._tiles[i, j] = AllTileInfo;
                                break;
                        }
                        break;
                    case 3:

                        switch (i + (j * 10))
                        {
                            case 0:
                                ArrayList AllTileInfo = new ArrayList() { DataManager._playerColors[0], 10, 0, DataManager._brandNames[rn.Next(0, DataManager._brandNames.Length)] + " " + DataManager._shopNames[9] };
                                DataManager._tiles[i, j] = AllTileInfo;
                                break;
                            case 22:
                                AllTileInfo = new ArrayList() { DataManager._playerColors[1], 12, 0, DataManager._brandNames[rn.Next(0, DataManager._brandNames.Length)] + " " + DataManager._shopNames[9] };
                                DataManager._tiles[i, j] = AllTileInfo;
                                break;
                            case 44:
                                AllTileInfo = new ArrayList() { DataManager._playerColors[2], 13, 0, DataManager._brandNames[rn.Next(0, DataManager._brandNames.Length)] + " " + DataManager._shopNames[9] };
                                DataManager._tiles[i, j] = AllTileInfo;
                                break;

                            case 1:
                            case 10:
                            case 11:
                            case 12:
                            case 21:
                            case 23:
                            case 32:
                            case 33:
                            case 34:
                            case 43:

                                AllTileInfo = new ArrayList();
                                AllTileInfo.Add(DataManager._playerColors[4]);
                                int TileLevel = rn.Next(1, 5);
                                AllTileInfo.Add(TileLevel);
                                int TileCost = rn.Next(((TileLevel * 100) - 20), (((TileLevel * 100) + 20) + 1));
                                AllTileInfo.Add(TileCost);
                                string TileName = DataManager._brandNames[rn.Next(0, DataManager._brandNames.Length)] + " " + DataManager._shopNames[TileLevel - 1];
                                AllTileInfo.Add(TileName);
                                DataManager._tiles[i, j] = AllTileInfo;

                                break;

                            case 4:
                            case 40:

                                AllTileInfo = new ArrayList();
                                AllTileInfo.Add(DataManager._playerColors[4]);
                                TileLevel = rn.Next(7, 10);
                                AllTileInfo.Add(TileLevel);
                                TileCost = rn.Next(((TileLevel * 100) - 20), (((TileLevel * 100) + 20) + 1));
                                AllTileInfo.Add(TileCost);
                                TileName = DataManager._brandNames[rn.Next(0, DataManager._brandNames.Length)] + " " + DataManager._shopNames[TileLevel - 1];
                                AllTileInfo.Add(TileName);
                                DataManager._tiles[i, j] = AllTileInfo;

                                break;

                            default:

                                AllTileInfo = new ArrayList();
                                AllTileInfo.Add(DataManager._playerColors[4]);
                                TileLevel = rn.Next(5, 9);
                                AllTileInfo.Add(TileLevel);
                                TileCost = rn.Next(((TileLevel * 100) - 20), (((TileLevel * 100) + 20) + 1));
                                AllTileInfo.Add(TileCost);
                                TileName = DataManager._brandNames[rn.Next(0, DataManager._brandNames.Length)] + " " + DataManager._shopNames[TileLevel - 1];
                                AllTileInfo.Add(TileName);
                                DataManager._tiles[i, j] = AllTileInfo;

                                break;
                        }

                        break;

                    case 4:

                        switch (i + (j * 10))
                        {
                            case 0:
                                ArrayList AllTileInfo = new ArrayList() { DataManager._playerColors[0], 10, 0, DataManager._brandNames[rn.Next(0, DataManager._brandNames.Length)] + " " + DataManager._shopNames[9] };
                                DataManager._tiles[i, j] = AllTileInfo;

                                break;

                            case 4:
                                AllTileInfo = new ArrayList() { DataManager._playerColors[1], 12, 0, DataManager._brandNames[rn.Next(0, DataManager._brandNames.Length)] + " " + DataManager._shopNames[11] };
                                DataManager._tiles[i, j] = AllTileInfo;
                                break;

                            case 40:
                                AllTileInfo = new ArrayList() { DataManager._playerColors[2], 11, 0, DataManager._brandNames[rn.Next(0, DataManager._brandNames.Length)] + " " + DataManager._shopNames[10] };
                                DataManager._tiles[i, j] = AllTileInfo;
                                break;

                            case 44:
                                AllTileInfo = new ArrayList() { DataManager._playerColors[3], 13, 0, DataManager._brandNames[rn.Next(0, DataManager._brandNames.Length)] + " " + DataManager._shopNames[12] };
                                DataManager._tiles[i, j] = AllTileInfo;
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
                                AllTileInfo.Add(DataManager._playerColors[4]);
                                int TileLevel = rn.Next(1, 5);
                                AllTileInfo.Add(TileLevel);
                                int TileCost = rn.Next(((TileLevel * 100) - 20), (((TileLevel * 100) + 20) + 1));
                                AllTileInfo.Add(TileCost);
                                string TileName = DataManager._brandNames[rn.Next(0, DataManager._brandNames.Length)] + " " + DataManager._shopNames[TileLevel - 1];
                                AllTileInfo.Add(TileName);
                                DataManager._tiles[i, j] = AllTileInfo;

                                break;

                            case 22:

                                AllTileInfo = new ArrayList();
                                AllTileInfo.Add(DataManager._playerColors[4]);
                                TileLevel = rn.Next(7, 10);
                                AllTileInfo.Add(TileLevel);
                                TileCost = rn.Next(((TileLevel * 100) - 20), (((TileLevel * 100) + 20) + 1));
                                AllTileInfo.Add(TileCost);
                                TileName = DataManager._brandNames[rn.Next(0, DataManager._brandNames.Length)] + " " + DataManager._shopNames[TileLevel - 1];
                                AllTileInfo.Add(TileName);
                                DataManager._tiles[i, j] = AllTileInfo;

                                break;
                            default:

                                AllTileInfo = new ArrayList();
                                AllTileInfo.Add(DataManager._playerColors[4]);
                                TileLevel = rn.Next(5, 9);
                                AllTileInfo.Add(TileLevel);
                                TileCost = rn.Next(((TileLevel * 100) - 20), (((TileLevel * 100) + 20) + 1));
                                AllTileInfo.Add(TileCost);
                                TileName = DataManager._brandNames[rn.Next(0, DataManager._brandNames.Length)] + " " + DataManager._shopNames[TileLevel - 1];
                                AllTileInfo.Add(TileName);
                                DataManager._tiles[i, j] = AllTileInfo;
                                break;
                        }
                        break;
                }
                //Debug.Log("Stop");
                //Debug.Log("-------------------------------------------------------------------------------");

                // Tile generation explained: sees what type of tile it is (HQ/low level/medium level/high level) after that creates an Arraylist with first the color of it (HQ gets a color normal ones get grey) then the level of the tile (1-9 so 0-8 in code and then you have the ones that go above which are the unique HQs) then The cost of that tile so you end with a Arraylist(Color,LVL,Cost) That araylist get's stored in a 2D ArrayList Array that get's that saves the row and colum the tile is on -M

                // tile info = Arraylist(Color,LVL,Cost) -M
            }
        }
    }

    private void PlayerInitialization()
    {
        // for now I just do the color order as who starts ask me to change this or change this yourself once we have a working started 4 player minigame -M

        for (int i = 0; i < DataManager.PlayerCount; i++)
        {
            ArrayList NewPlayerIntialization = new ArrayList();
            NewPlayerIntialization.Add(i);
            NewPlayerIntialization.Add(500);
            DataManager._PlayerInfo[i] = NewPlayerIntialization;
            DataManager._playerMoneyDisplay.text = ((int)DataManager._PlayerInfo[DataManager._currentPlayer][1]).ToString();
            DataManager._playerNumberDisplay.text = "Current Player: " + (DataManager._currentPlayer + 1);
        }
    }

    private void GroundPlatePlacing()
    {
        //places environment around tiles
        if (DataManager._columns * DataManager._rows == 16) //for 4x4 grid
        {
            GameObject map = Instantiate(DataManager._4x4Map, new Vector3(2.1f,-1f,8.1f), Quaternion.identity);
        }
        else if (DataManager._columns * DataManager._rows == 25) //for 5x5 grid
        {
            GameObject map = Instantiate(DataManager._5x5Map, new Vector3(3.1f, -1f, 8.25f), Quaternion.identity);
        }

        //places gound plates and buildings
        for (int i = 0; i < DataManager._columns; i++)
        {
            for (int j = 0; j < DataManager._rows; j++)
            {
                GameObject NewGroundPlate = GameObject.Instantiate(DataManager._prefabBasePlate, transform, true);
                NewGroundPlate.transform.position += new Vector3(i * 1.5f, 0, j * 1.5f);
                NewGroundPlate.transform.localScale = NewGroundPlate.transform.localScale / 4;
                GameObject NewBuilding = GameObject.Instantiate(_shopPrefabs[(int)DataManager._tiles[i, j][1] - 1], transform, true);
                NewBuilding.transform.localScale = NewBuilding.transform.localScale / 4;
                NewBuilding.transform.parent = NewGroundPlate.transform;
                NewBuilding.transform.position = NewGroundPlate.transform.position + new Vector3(0, 0.32f, 0);
                NewBuilding.transform.rotation = Quaternion.Euler(0,-90,0); //rotates the buildings to the camera

                //Tell me te explain this if you don't get this part -M
                Material NewGroundPlateMaterial = (Material)((ArrayList)DataManager._tiles[i, j])[0];


                //This is to add the location of the HQs to the playerInfo -M
                if ((Material)((ArrayList)DataManager._tiles[i, j])[0] != DataManager._playerColors[4])
                {
                    for (int k = 0; k < DataManager.PlayerCount; k++)
                    {
                        if (DataManager._playerColors[k] == (Material)((ArrayList)DataManager._tiles[i, j])[0])
                        {
                            DataManager._PlayerInfo[k].Add(i.ToString() + ',' + j.ToString());
                        }
                    }
                }

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

                DataManager._tilespots[i, j] = NewGroundPlate;

            }
        }
    }
    void Update()
    {
        if (!DataManager._batteling && !DataManager._contesting)
        {
            waitedtime += Time.deltaTime;
            //Debug.Log(DataManager._currentPlayer + 1);
            if (DataManager._allowedToMove && (Input.GetAxis($"LeftStickHorizontal{DataManager._currentPlayer + 1}") != 0 || Input.GetAxis($"LeftStickVertical{DataManager._currentPlayer + 1}") != 0) && (waitedtime >= 0.25f))
            {
                //Debug.Log("Do you work?");
                waitedtime = 0;
                int HorizontalInput = 0;
                if (Input.GetAxis($"LeftStickHorizontal{DataManager._currentPlayer + 1}") > 0 && DataManager._selectedTile[0] < DataManager._columns - 1 && Input.GetAxis($"LeftStickHorizontal{DataManager._currentPlayer + 1}") > Mathf.Pow(Input.GetAxis($"LeftStickVertical{DataManager._currentPlayer + 1}"), 1)) HorizontalInput = 1;
                if (Input.GetAxis($"LeftStickHorizontal{DataManager._currentPlayer + 1}") < 0 && DataManager._selectedTile[0] > 0 && Input.GetAxis($"LeftStickVertical{DataManager._currentPlayer + 1}") > Mathf.Pow(Input.GetAxis($"LeftStickHorizontal{DataManager._currentPlayer + 1}"), 1)) HorizontalInput = -1;


                int VerticalInput = 0;
                if (Input.GetAxis("LeftStickVertical" + (DataManager._currentPlayer + 1)) < 0 && DataManager._selectedTile[1] > 0 && Input.GetAxis("LeftStickHorizontal" + (DataManager._currentPlayer + 1)) > Mathf.Pow(Input.GetAxis($"LeftStickVertical{DataManager._currentPlayer + 1}"), 1)) VerticalInput = -1;
                if (Input.GetAxis("LeftStickVertical" + (DataManager._currentPlayer + 1)) > 0 && DataManager._selectedTile[1] < DataManager._rows - 1 && Input.GetAxis("LeftStickVertical" + (DataManager._currentPlayer + 1)) > Mathf.Pow(Input.GetAxis($"LeftStickHorizontal{DataManager._currentPlayer + 1}"), 1)) VerticalInput = 1;

                SelectedTileChanged(HorizontalInput, VerticalInput);

                float _animStep = ((DataManager._columns - 1f) / 2f);
                _tileToCameraX = (float)DataManager._selectedTile[0] / _animStep - 1f;
                _tileToCameraZ = (float)DataManager._selectedTile[1] / _animStep;
                _animOnBoardFrame = 0;
                _animOnBoardMovement = true;
            }
            //Debug.Log((float)DataManager._selectedTile[0] / 1.5f - 1);

            if (Input.GetButton($"AButton{DataManager._currentPlayer + 1}") && waitedtime >= 0.2f)
            {
                TheShowDetailAndBuyMethod(1);
                CameraIfTileSelected();
                TheShowDetailAndBuyMethod(0);
                waitedtime = 0f;
            }

            if (Input.GetButton($"BButton{DataManager._currentPlayer + 1}") && waitedtime >= 0.2f)
            {
                TheShowDetailAndBuyMethod(-1);
                TheShowDetailAndBuyMethod(0);
                waitedtime = 0f;
            }

            if (Input.GetButton($"YButton{DataManager._currentPlayer + 1}") && waitedtime >= 0.2f && DataManager._tileView == 0)
            {
                EndTurnMethod();
            }

            if (_animRotation)
            {
                DataManager._cameraMain.transform.parent.eulerAngles += Vector3.up * _animRotationSpeed;
                DataManager._cameraMain.transform.localPosition = new Vector3(DataManager._cameraMain.transform.localPosition.x,
                    _animRotationHeight + Mathf.Sin(Time.time * 1.5f) * 0.1f, DataManager._cameraMain.transform.localPosition.z);
            }

            
        }
        if (_animOnBoardMovement)
        {
            if (_animOnBoardFrame <= 1)
            {
                Vector3 cameraPosition = Vector3.Lerp(DataManager._cameraMain.transform.localPosition, new Vector3(_tileToCameraX, 4, _tileToCameraZ + _onBoardCameraOffset), _animOnBoardFrame);
                DataManager._cameraMain.transform.localPosition = cameraPosition;
                _animOnBoardFrame += 1f / _animOnBoardSmoothness;
            }
        }
    }

    private void EndTurnMethod()
    {
        if (DataManager._playerturn + 1 != DataManager.PlayerCount)
        {
            DataManager._playerturn++;
            DataManager._tilespots[DataManager._selectedTile[0], DataManager._selectedTile[1]].GetComponent<MeshRenderer>().material = (Material)((ArrayList)DataManager._tiles[DataManager._selectedTile[0], DataManager._selectedTile[1]])[0];
            PlayerTurn();
            CameraStartPlacement();
        }
        else
        {
            DataManager._playerturn = 0;
            DataManager._roundNumber++;
            if (DataManager._roundNumber <5)
            {
                DataManager._contesting = true;

                // You need to add the method ContestConceeded(int[]) to add the money and who goes when to each player. the int[] is to see which spot they ended in the contest spot 0 in the array is always player 1 the number you add on that spot is how well player 1 did and so on for the other players -M

                //change DataManager._contestNumber to whezre your code is and make DataManager._contesting = true to test your minigame;
                int minigame = rn.Next(1, 4);
                WriteForMiniGames((DataManager.PlayerCount+10).ToString(), minigame);
                SceneManager.LoadScene("TutorialMinigame");
                InvisibleBoard();
                DataManager._playerMoneyDisplay.text = DataManager._PlayerInfo[1].ToString();
            }
            else
            {
                //Game END -M

                StreamWriter write = new StreamWriter("Assets/Resources/MessengerBoy.txt");

                write.Write(CalculateWhoEndedWhere());
                 
                write.Close();

                SceneManager.LoadScene("EndScreen");
                DestroyObject(this);
            }
        }
    }

    private void WriteForMiniGames(string neededForMiniGame, int minigameNumber)
    {
        StreamWriter writer = new StreamWriter("Assets/Resources/MessengerBoy.txt");
        Debug.Log(neededForMiniGame);
        string toSend = minigameNumber + ";" + neededForMiniGame;
        writer.WriteLine(toSend);
        writer.Close();
    }

    private void InvisibleBoard()
    {
        foreach (GameObject tile in DataManager._tilespots)
        {
            tile.SetActive(false);
        }
    }
    private string CalculateWhoEndedWhere()
    {
        string EndOfGameSpots = DataManager.PlayerCount + ":";
        int[] CurrentPlayersNetWorths = new int[DataManager.PlayerCount];
        for (int i = 0; i < DataManager.PlayerCount; i++)
        {
            for (int j = 0; j < DataManager._columns; j++)
            {
                for (int k = 0; k < DataManager._rows; k++)
                {
                    if ((Material)DataManager._tiles[j, k][0] == DataManager._playerColors[i])
                    {
                        CurrentPlayersNetWorths[i] += (int)DataManager._tiles[j, k][2];
                    }
                }
            }
            CurrentPlayersNetWorths[i] += (int)DataManager._PlayerInfo[i][1];
        }
        for (int i = 0; i < DataManager.PlayerCount; i++)
        {
            int WherePlayerEnded = 1;
            for (int j = 0; j < DataManager.PlayerCount; j++)
            {
                if (CurrentPlayersNetWorths[i] < CurrentPlayersNetWorths[j]) WherePlayerEnded++;
                if (CurrentPlayersNetWorths[i] == CurrentPlayersNetWorths[j] && i != j)
                {
                    if (CurrentPlayersNetWorths[i] - (int)DataManager._PlayerInfo[i][1] < CurrentPlayersNetWorths[j] - (int)DataManager._PlayerInfo[j][1])
                    {
                        WherePlayerEnded++;
                    }
                    else
                    {
                        if (CurrentPlayersNetWorths[i] - (int)DataManager._PlayerInfo[i][1] == CurrentPlayersNetWorths[j] - (int)DataManager._PlayerInfo[j][1])
                        {
                            CurrentPlayersNetWorths[j]++;
                            WherePlayerEnded++;
                        }
                    }
                }
            }
            EndOfGameSpots += WherePlayerEnded;
            if (i + 1 < DataManager.PlayerCount)
            {
                EndOfGameSpots += ",";
            }
        }
        return EndOfGameSpots;
    }

    private void CameraIfTileSelected()
    {
        _animOnBoardMovement = false;
        int row = DataManager._selectedTile[0];
        int column = DataManager._selectedTile[1];
        DataManager._cameraMain.transform.parent.position = DataManager._tilespots[row, column].transform.position;
        DataManager._cameraMain.transform.localPosition = new Vector3(0, _animRotationHeight, -1);
        DataManager._cameraMain.transform.localEulerAngles = new Vector3(30, 0, 0);
        _animRotation = true;
    }
    private void PlayerTurn()
    {
        Debug.Log(DataManager._playerturn);
        Debug.Log(DataManager._currentPlayer);
        Debug.Log("start playerturn");
        DataManager._tilespots[DataManager._selectedTile[0], DataManager._selectedTile[1]].GetComponent<MeshRenderer>().material = (Material)((ArrayList)DataManager._tiles[DataManager._selectedTile[0], DataManager._selectedTile[1]])[0];
        //Debug.Log("---------------------");
        for (int i = 0; i < DataManager.PlayerCount; i++)
        {
            Debug.Log("---------------------");
            Debug.Log(DataManager._PlayerInfo[i][0].ToString());
            if ((int)DataManager._PlayerInfo[i][0] == DataManager._playerturn)
            {
                string[] LocationNumbers = DataManager._PlayerInfo[i][2].ToString().Split(',');
                for (int j = 0; j < LocationNumbers.Length; j++)
                {
                    DataManager._selectedTile[j] = int.Parse(LocationNumbers[j]);
                }
                Debug.Log(i);
                DataManager._currentPlayer = i;
                DataManager._playerNumberDisplay.text = "Current Player: " + (DataManager._currentPlayer + 1);
            }
            Debug.Log("---------------------");
        }
        //Debug.Log("---------------------");
        DataManager._playerMoneyDisplay.text = ((int)DataManager._PlayerInfo[DataManager._currentPlayer][1]).ToString();
        Debug.Log(DataManager._currentPlayer);
        Debug.Log("end playerturn");
        SelectedTileChanged(0, 0);
    }
    private void SelectedTileChanged(int col, int row)
    {
        //Debug.Log("");
        //Debug.Log("Materials");
        //Debug.Log("--------------------------------------------------------------");
        //Debug.Log(DataManager._selectedTile[0]);
        //Debug.Log(DataManager._selectedTile[1]);
        DataManager._tilespots[DataManager._selectedTile[0], DataManager._selectedTile[1]].GetComponent<MeshRenderer>().material = (Material)((ArrayList)DataManager._tiles[DataManager._selectedTile[0], DataManager._selectedTile[1]])[0];
        //Debug.Log((Material)((ArrayList)DataManager._tiles[DataManager._selectedTile[0], DataManager._selectedTile[1]])[0]);
        DataManager._selectedTile[0] += col;
        DataManager._selectedTile[1] += row;
        //Debug.Log(DataManager._selectedTile[0].ToString() + " _ "+ DataManager._selectedTile[1].ToString());
        DataManager._tilespots[DataManager._selectedTile[0], DataManager._selectedTile[1]].GetComponent<MeshRenderer>().material = _selected;
        CameraStartPlacement();
        //Debug.Log("--------------------------------------------------------------");
    }
    private void TheShowDetailAndBuyMethod(int changeBy)
    {
        if (changeBy == 1 && DataManager._tileView != 2)
        {
            DataManager._tileView++;
        }
        if (changeBy == -1 && DataManager._tileView != 0)
        {
            DataManager._tileView--;
        }
        //Debug.Log(DataManager._tileView);
        switch (DataManager._tileView)
        {
            case 0:
                DataManager._allowedToMove = true;
                DataManager._TildeDetailDisplay.GetComponentInParent<CanvasGroup>().alpha = 0;
                _animRotation = false;

                CameraStartPlacement();

                break;
            case 1:
                DataManager._allowedToMove = false;
                DataManager._TildeDetailDisplay.GetComponentInParent<CanvasGroup>().alpha = 1;

                DataManager._ownerBuildingDisplay.text = "Owner: " + (("Grey" == ((Material)DataManager._tiles[DataManager._selectedTile[0], DataManager._selectedTile[1]][0]).name.ToString()) ? "No One" : ((Material)DataManager._tiles[DataManager._selectedTile[0], DataManager._selectedTile[1]][0]).name.ToString());
                //Debug.Log(((Material)DataManager._tiles[DataManager._selectedTile[0], DataManager._selectedTile[1]][0]).name.ToString());
                DataManager._levelBuildingDisplay.text = "LVL: " + DataManager._tiles[DataManager._selectedTile[0], DataManager._selectedTile[1]][1].ToString();

                DataManager._costBuildingDisplay.text = "Cost: " + DataManager._tiles[DataManager._selectedTile[0], DataManager._selectedTile[1]][2].ToString();

                DataManager._nameBuildingDisplay.text = DataManager._tiles[DataManager._selectedTile[0], DataManager._selectedTile[1]][3].ToString();
                bool SomthingNextToIt = false;

                if (DataManager._selectedTile[0] != 0)
                {
                    if ((Material)DataManager._tiles[DataManager._selectedTile[0] - 1, DataManager._selectedTile[1]][0] == (Material)DataManager._playerColors[DataManager._currentPlayer]) SomthingNextToIt = true;
                    //Debug.Log(((Material)DataManager._tiles[DataManager._selectedTile[0] - 1, DataManager._selectedTile[1]][0]).ToString() + " _ " + ((Material)DataManager._playerColors[DataManager._currentPlayer]).ToString());
                }
                if (DataManager._selectedTile[0] != DataManager._columns - 1)
                {
                    if ((Material)DataManager._tiles[DataManager._selectedTile[0] + 1, DataManager._selectedTile[1]][0] == (Material)DataManager._playerColors[DataManager._currentPlayer]) SomthingNextToIt = true;
                    //Debug.Log(((Material)DataManager._tiles[DataManager._selectedTile[0], DataManager._selectedTile[1] + 1][0]).ToString() + " _ " + ((Material)DataManager._playerColors[DataManager._currentPlayer]).ToString());
                }
                if (DataManager._selectedTile[1] != 0)
                {
                    if ((Material)DataManager._tiles[DataManager._selectedTile[0], DataManager._selectedTile[1] - 1][0] == (Material)DataManager._playerColors[DataManager._currentPlayer]) SomthingNextToIt = true;
                    //Debug.Log(((Material)DataManager._tiles[DataManager._selectedTile[0], DataManager._selectedTile[1] - 1][0]).ToString() + " _ " + ((Material)DataManager._playerColors[DataManager._currentPlayer]).ToString());
                }
                if (DataManager._selectedTile[1] != DataManager._rows - 1)
                {
                    if ((Material)DataManager._tiles[DataManager._selectedTile[0], DataManager._selectedTile[1] + 1][0] == (Material)DataManager._playerColors[DataManager._currentPlayer]) SomthingNextToIt = true;
                    //Debug.Log(((Material)DataManager._tiles[DataManager._selectedTile[0], DataManager._selectedTile[1] + 1][0]).ToString() + " _ " + ((Material)DataManager._playerColors[DataManager._currentPlayer]).ToString());
                }
                if ((DataManager._tiles[DataManager._selectedTile[0], DataManager._selectedTile[1]][0] != DataManager._playerColors[DataManager._currentPlayer]) && ((int)DataManager._tiles[DataManager._selectedTile[0], DataManager._selectedTile[1]][2] != 0) && (SomthingNextToIt))
                {
                    DataManager._BuyButtonDisplay.SetActive(true);
                }
                else
                {
                    DataManager._BuyButtonDisplay.SetActive(false);
                }
                break;
            case 2:
                if (DataManager._BuyButtonDisplay.active)
                {
                    BuyTile();
                }
                else { DataManager._tileView = 1; }

                break;
        }
    }
    private void BuyTile()
    {
        if ((int)DataManager._PlayerInfo[DataManager._currentPlayer][1] >= (int)DataManager._tiles[DataManager._selectedTile[0], DataManager._selectedTile[1]][2])
        {
            DataManager._PlayerInfo[DataManager._currentPlayer][1] = (int)DataManager._PlayerInfo[DataManager._currentPlayer][1] - (int)DataManager._tiles[DataManager._selectedTile[0], DataManager._selectedTile[1]][2];
            DataManager._playerMoneyDisplay.text = ((int)DataManager._PlayerInfo[DataManager._currentPlayer][1]).ToString();
            if (DataManager._tiles[DataManager._selectedTile[0], DataManager._selectedTile[1]][0] != DataManager._playerColors[4])
            {
                _animRotation = false;
                DataManager._battleNumber = rn.Next(1, 5);
                DataManager._batteling = true;
                int otherPlayer = 0;
                for (int i = 0; i < _playerColors.Length; i++)
                {
                    if (DataManager._tiles[DataManager._selectedTile[0], DataManager._selectedTile[1]][0] == _playerColors[i])
                    {
                        otherPlayer = i +1;
                    }
                    
                }
                WriteForMiniGames((DataManager._currentPlayer + 1).ToString() + ":" + otherPlayer.ToString(), DataManager._battleNumber);
                SceneManager.LoadScene("TutorialMinigame");
                InvisibleBoard();

                
            }
            else
            {
                BattleConceeded(true);
            }
        }
        else
        {
            DataManager._tileView = 1;
        }
    }
    private void BattleConceeded(bool succeed)
    {
        if (succeed)
        {
            DataManager._tiles[DataManager._selectedTile[0], DataManager._selectedTile[1]][0] = DataManager._playerColors[DataManager._currentPlayer];
        }
        DataManager._playerMoneyDisplay.text = ((int)DataManager._PlayerInfo[DataManager._currentPlayer][1]).ToString();
        DataManager._tileView = 0;
        _animRotation = true;
        CameraStartPlacement();
        TheShowDetailAndBuyMethod(0);
    }
    private void ContestConceeded(string[] playerFinishingSpots)
    {

        int AddedNecesarie = 0;
        for (int i = 0; i < DataManager.PlayerCount; i++)
        {
            //Debug.Log("-------ContestConceeded-----");

            DataManager._PlayerInfo[i][1] = ((int)DataManager._PlayerInfo[i][1]) + (60 * (11 - int.Parse(playerFinishingSpots[i])));
        }
        DataManager._contesting = false;
        DataManager._playerturn = 0;
        DataManager._currentPlayer = 0;
        PlayerTurn();
    }
}