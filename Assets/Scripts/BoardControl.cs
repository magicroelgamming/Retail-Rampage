using System;
using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class BoardControl : MonoBehaviour
{
    [SerializeField]
    private GameObject _orbit;

    [SerializeField]
    private Camera _cameraMain;

    [SerializeField]
    public int PlayerCount;

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
    private GameObject[] _shopPrefabs;


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
    public static class DataManager {



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

        static public GameObject[] _shopPrefabs;


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
    }

    // Start is called before the first frame update
    void Start()
    {
        if (DataManager.PlayerCount == 0)
        {
            DataManager._cameraMain = _cameraMain;
            DataManager.PlayerCount = PlayerCount;
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
            DataManager._shopPrefabs = _shopPrefabs;
            DataManager._orbit = _orbit;

            DontDestroyOnLoad(this);

            DataManager._brandNames = new string[] { "Garry's", "Irish", "WcDonalds", "Jeff's", "Roel's", "Andr�'s", "Evy's", "Sander's", "Jasper's", "Grigory's", "Mintendo", "OCircle", "Moist", "TrainConsole", "Baldur Studios" };

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
            DataManager._cameraMain = Camera.main;
            DataManager._TildeDetailDisplay = _TildeDetailDisplay;
            DataManager._BuyButtonDisplay = _BuyButtonDisplay;
            DataManager._nameBuildingDisplay = _nameBuildingDisplay;
            DataManager._ownerBuildingDisplay = _ownerBuildingDisplay;
            DataManager._levelBuildingDisplay = _levelBuildingDisplay;
            DataManager._costBuildingDisplay = _costBuildingDisplay;
            DataManager._playerMoneyDisplay = _playerMoneyDisplay;

            DataManager._batteling = false;
            DataManager._contesting = false;

            DataManager._TildeDetailDisplay.GetComponentInParent<CanvasGroup>().alpha = 0;
            PlayerTurn();
        }
    }

    private void CameraStartPlacement()
    {
        _tileToCameraX = (float)DataManager._selectedTile[0] / 2 - 1;
        _tileToCameraZ = (float)DataManager._selectedTile[1] / 2;
        Vector3 centralTilePosition = Vector3.Lerp(DataManager._tilespots[0,0].transform.position, DataManager._tilespots[DataManager._columns -1, DataManager._rows -1].transform.position, 0.5f);
        _cameraMain.transform.parent.position = centralTilePosition;
        _cameraMain.transform.parent.localEulerAngles = Vector3.zero;
        _cameraMain.transform.localPosition = new Vector3(_tileToCameraX, 4, _tileToCameraZ + _onBoardCameraOffset);
        _cameraMain.transform.localEulerAngles = new Vector3(45, 0, 0);
        _animRotation = false;
    }

    private void MapGeneration()
    {
        

        for (int i = 0; i < DataManager._columns; i++)
        {
            for (int j = 0; j < DataManager._rows; j++)
            {
                Debug.Log("-------------------------------------------------------------------------------");
                Debug.Log("Start");
                Debug.Log(i.ToString() + " _ " + j.ToString());
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
                                string TileName = DataManager._brandNames[rn.Next(0,DataManager._brandNames.Length)] + " " + DataManager._shopNames[TileLevel-1];
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

                Debug.Log("Stop");
                Debug.Log("-------------------------------------------------------------------------------");

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
            _playerMoneyDisplay.text = ((int)DataManager._PlayerInfo[DataManager._currentPlayer][1]).ToString();
        }
    }

    private void GroundPlatePlacing()
    {
        for (int i = 0; i < DataManager._columns; i++)
        {
            for (int j = 0; j < DataManager._rows; j++)
            {

                GameObject NewGroundPlate = GameObject.Instantiate(DataManager._prefabBasePlate, transform, true);
                NewGroundPlate.transform.position += new Vector3(i * 1.5f, 0, j * 1.5f);
                NewGroundPlate.transform.localScale = NewGroundPlate.transform.localScale / 4;
                GameObject NewBuilding = GameObject.Instantiate(_shopPrefabs[(int)DataManager._tiles[i, j][1] -1], transform, true);
                NewBuilding.transform.localScale = NewBuilding.transform.localScale / 4;
                NewBuilding.transform.parent = NewGroundPlate.transform;
                NewBuilding.transform.position = NewGroundPlate.transform.position + new Vector3(0,0.32f,0);
                

                //Tell me te explain this if you don't get this part -M
                Material NewGroundPlateMaterial = (Material)((ArrayList)DataManager._tiles[i, j])[0];


                //This is to add the location of the HQs to the playerInfo -M
                if ((Material)((ArrayList)DataManager._tiles[i, j])[0] != DataManager._playerColors[4])
                {
                    for (int k = 0; k < DataManager.PlayerCount; k++)
                    {
                        if (DataManager._playerColors[k] == (Material)((ArrayList)DataManager._tiles[i, j])[0])
                        {
                            DataManager._PlayerInfo[k].Add(i+(j*10));
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

                DataManager._tilespots[i,j] = NewGroundPlate;

            }
        }
    }

    

    // Update is called once per frame
    void Update()
    {
        if (!DataManager._batteling && !DataManager._contesting)
        {
            waitedtime += Time.deltaTime;

            if (DataManager._allowedToMove && (Input.GetAxis($"LeftStickHorizontal{DataManager._playerturn+1}") != 0 || Input.GetAxis($"LeftStickVertical{DataManager._playerturn+1}") != 0) && (waitedtime >= 0.25f))
            {
                Debug.Log("Do you work?");
                waitedtime = 0;
                int HorizontalInput = 0;
                if (Input.GetAxis($"LeftStickHorizontal{DataManager._playerturn+1}") > 0 && DataManager._selectedTile[0] < DataManager._columns - 1&& Input.GetAxis($"LeftStickHorizontal{DataManager._playerturn+1}") > Mathf.Pow(Input.GetAxis($"LeftStickVertical{DataManager._playerturn+1}"), 1)) HorizontalInput = 1;
                if (Input.GetAxis($"LeftStickHorizontal{DataManager._playerturn+1}") < 0 && DataManager._selectedTile[0] > 0 && Input.GetAxis($"LeftStickVertical{DataManager._playerturn+1}") > Mathf.Pow(Input.GetAxis($"LeftStickHorizontal{DataManager._playerturn+1}"), 1)) HorizontalInput = -1;


                int VerticalInput = 0;
                if (Input.GetAxis("LeftStickVertical" + (DataManager._playerturn + 1)) < 0 && DataManager._selectedTile[1] > 0 && Input.GetAxis("LeftStickHorizontal" + (DataManager._playerturn + 1)) > Mathf.Pow(Input.GetAxis($"LeftStickVertical{DataManager._playerturn + 1}"), 1)) VerticalInput = -1;
                if (Input.GetAxis("LeftStickVertical" + (DataManager._playerturn + 1)) > 0 && DataManager._selectedTile[1] < DataManager._rows - 1 && Input.GetAxis("LeftStickVertical" + (DataManager._playerturn + 1)) > Mathf.Pow(Input.GetAxis($"LeftStickHorizontal{DataManager._playerturn + 1}"), 1)) VerticalInput = 1;

                SelectedTileChanged(HorizontalInput, VerticalInput);

                float _animStep = ((DataManager._columns - 1f) / 2f);
                _tileToCameraX = (float)DataManager._selectedTile[0] / _animStep - 1f;
                _tileToCameraZ = (float)DataManager._selectedTile[1] / _animStep;
                _animOnBoardFrame = 0;
                _animOnBoardMovement = true;
              
            }
            Debug.Log((float)DataManager._selectedTile[0] / 1.5f - 1);

            if (Input.GetButton($"AButton{DataManager._playerturn+1}") && waitedtime >= 0.2f)
            {
                TheShowDetailAndBuyMethod(1);
                CameraIfTileSelected();
                TheShowDetailAndBuyMethod(0);
                waitedtime = 0f;
            }

            if (Input.GetButton($"BButton{DataManager._playerturn+1}") && waitedtime >= 0.2f)
            {
                TheShowDetailAndBuyMethod(-1);
                waitedtime = 0f;
            }

            if (Input.GetButton($"YButton{DataManager._playerturn+1}") && waitedtime >= 0.2f && DataManager._tileView == 0)
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
                Vector3 cameraPosition = Vector3.Lerp(_cameraMain.transform.localPosition, new Vector3(_tileToCameraX, 4, _tileToCameraZ + _onBoardCameraOffset), _animOnBoardFrame);
                _cameraMain.transform.localPosition = cameraPosition;
                _animOnBoardFrame += 1f / _animOnBoardSmoothness;
            }
        }

        if (DataManager._batteling)
        {
            Battle();
        }

        if (DataManager._contesting)
        {
            Contest();
        }
    }

    private void Contest()
    {
        // You need to add the method ContestConceeded(int[]) to add the money and who goes when to each player. the int[] is to see which spot they ended in the contest spot 0 in the array is always player 1 the number you add on that spot is how well player 1 did and so on for the other players -M

        //change DataManager._contestNumber to whezre your code is and make DataManager._contesting = true to test your minigame;
        switch (DataManager._contestNumber)
        {
            case 1:
                // you can put your 2-4 player minigame in here -M
                SceneManager.LoadScene("Pong");
                DataManager._contesting = false;
                DataManager._currentPlayer = 0;
                DataManager._battleNumber = 1;
                DataManager._batteling = true;
                break;

            case 2:
                // you can put your 2-4 player minigame in here -M
                break;

            case 3:
                // you can put your 2-4 player minigame in here -M
                break;

            case 4:
                // you can put your 2-4 player minigame in here -M
                break;
        }
    }

    private void EndTurnMethod()
    {
        if (DataManager._playerturn+1 != DataManager.PlayerCount)
        {
            DataManager._playerturn++;
            DataManager._tilespots[DataManager._selectedTile[0], DataManager._selectedTile[1]].GetComponent<MeshRenderer>().material = (Material)((ArrayList)DataManager._tiles[DataManager._selectedTile[0], DataManager._selectedTile[1]])[0];
            PlayerTurn();
        }
        else
        {
            DataManager._contesting = true;
            DataManager._contestNumber = rn.Next(1, 2);
        }
        
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

        for (int i = 0; i < DataManager.PlayerCount; i++)
        {
            if ((int)DataManager._PlayerInfo[i][0] == DataManager._playerturn)
            {
                if (DataManager._PlayerInfo[i][0].ToString().Length == 1)
                {
                    DataManager._selectedTile[0] = int.Parse(DataManager._PlayerInfo[i][0].ToString());
                    DataManager._selectedTile[1] = 0;
                }
                else
                {
                    DataManager._selectedTile[1] = int.Parse(DataManager._PlayerInfo[i][0].ToString().Substring(0, 1));
                    DataManager._selectedTile[0] = int.Parse(DataManager._PlayerInfo[i][0].ToString().Substring(1, 1));
                }

                DataManager._currentPlayer = i;
            }
        }


        SelectedTileChanged(0,0);
    }

    private void SelectedTileChanged(int col, int row)
    {
        Debug.Log("");
        Debug.Log("Materials");
        Debug.Log("--------------------------------------------------------------");
        DataManager._tilespots[DataManager._selectedTile[0], DataManager._selectedTile[1]].GetComponent<MeshRenderer>().material = (Material)((ArrayList)DataManager._tiles[DataManager._selectedTile[0], DataManager._selectedTile[1]])[0];
        Debug.Log((Material)((ArrayList)DataManager._tiles[DataManager._selectedTile[0], DataManager._selectedTile[1]])[0]);
        DataManager._selectedTile[0] += col;
        DataManager._selectedTile[1] += row;
        Debug.Log(DataManager._selectedTile[0].ToString() + " _ "+ DataManager._selectedTile[1].ToString());
        DataManager._tilespots[DataManager._selectedTile[0], DataManager._selectedTile[1]].GetComponent<MeshRenderer>().material = _selected;
        Debug.Log("--------------------------------------------------------------");
    }

    private void TheShowDetailAndBuyMethod(int changeBy)
    {
        if (changeBy == 1 && DataManager._tileView !=2 )
        {
            DataManager._tileView++;
        }
        if (changeBy == -1 && DataManager._tileView != 0)
        {
            DataManager._tileView--;
        }

        Debug.Log(DataManager._tileView);

        switch (DataManager._tileView)
        {
            case 0:
                DataManager._allowedToMove = true;
                DataManager._TildeDetailDisplay.GetComponentInParent<CanvasGroup>().alpha = 0;

                break;
            case 1:
                DataManager._allowedToMove = false;
                DataManager._TildeDetailDisplay.GetComponentInParent<CanvasGroup>().alpha = 1;

                DataManager._ownerBuildingDisplay.text = "Owner: "+ (("Grey" == ((Material)DataManager._tiles[DataManager._selectedTile[0], DataManager._selectedTile[1]][0]).name.ToString())? "No One" : ((Material)DataManager._tiles[DataManager._selectedTile[0], DataManager._selectedTile[1]][0]).name.ToString());
                //Debug.Log(((Material)DataManager._tiles[DataManager._selectedTile[0], DataManager._selectedTile[1]][0]).name.ToString());
                DataManager._levelBuildingDisplay.text = "LVL: " + DataManager._tiles[DataManager._selectedTile[0], DataManager._selectedTile[1]][1].ToString();

                DataManager._costBuildingDisplay.text = "Cost: " + DataManager._tiles[DataManager._selectedTile[0], DataManager._selectedTile[1]][2].ToString();

                DataManager._nameBuildingDisplay.text = DataManager._tiles[DataManager._selectedTile[0], DataManager._selectedTile[1]][3].ToString();
                bool SomthingNextToIt = false;

                if (DataManager._selectedTile[0] != 0)
                {
                    if ((Material)DataManager._tiles[DataManager._selectedTile[0]-1, DataManager._selectedTile[1]][0] == (Material)DataManager._playerColors[DataManager._currentPlayer]) SomthingNextToIt = true;
                    //Debug.Log(((Material)DataManager._tiles[DataManager._selectedTile[0] - 1, DataManager._selectedTile[1]][0]).ToString() + " _ " + ((Material)DataManager._playerColors[DataManager._currentPlayer]).ToString());
                }

                if (DataManager._selectedTile[0] != DataManager._columns-1)
                {
                    if ((Material)DataManager._tiles[DataManager._selectedTile[0] + 1, DataManager._selectedTile[1]][0] == (Material)DataManager._playerColors[DataManager._currentPlayer]) SomthingNextToIt = true;
                    //Debug.Log(((Material)DataManager._tiles[DataManager._selectedTile[0], DataManager._selectedTile[1] + 1][0]).ToString() + " _ " + ((Material)DataManager._playerColors[DataManager._currentPlayer]).ToString());
                }

                if (DataManager._selectedTile[1] != 0)
                {
                    if ((Material)DataManager._tiles[DataManager._selectedTile[0], DataManager._selectedTile[1] - 1][0] == (Material)DataManager._playerColors[DataManager._currentPlayer]) SomthingNextToIt = true;
                    //Debug.Log(((Material)DataManager._tiles[DataManager._selectedTile[0], DataManager._selectedTile[1] - 1][0]).ToString() + " _ " + ((Material)DataManager._playerColors[DataManager._currentPlayer]).ToString());
                }

                if (DataManager._selectedTile[1] != DataManager._rows-1)
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
                DataManager._battleNumber = rn.Next(1, 5);
                DataManager._batteling = true;
                Battle();
            }
            else
            {
                BattleConceeded(true);
            }
        }
    }

    private void Battle()
    {
        // if you want to send who won the fight you do BattleConceeded(bool) the bool will be true if the attacker wins falls if the attacker looses -M

        switch (DataManager._battleNumber)
        {
            case 1:
                // you can put your minigames 1v1's in here -M
                break;

            case 2:
                // you can put your minigames 1v1's in here -M
                break;

            case 3:
                // you can put your minigames 1v1's in here -M
                break;

            case 4:
                // you can put your minigames 1v1's in here -M
                break;
        }
    }

    private void BattleConceeded(bool succeed)
    {
        if (succeed)
        {
            DataManager._tiles[DataManager._selectedTile[0], DataManager._selectedTile[1]][0] = DataManager._playerColors[DataManager._currentPlayer];
            DataManager._tileView = 0;
        }
        
    }

    private void ContestConceeded(int[] playerFinishingSpots)
    {
        for (int i = 0; i < DataManager.PlayerCount; i++)
        {
            DataManager._PlayerInfo[i][0] = playerFinishingSpots[i];
            DataManager._PlayerInfo[i][1] = ((int)DataManager._PlayerInfo[i][1]) + (30*(11- playerFinishingSpots[i]));
        }
        DataManager._contesting = false;
        DataManager._playerturn = 0;
        PlayerTurn();
    }

}
