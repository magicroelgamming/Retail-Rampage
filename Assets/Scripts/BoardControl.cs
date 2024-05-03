using System;
using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class BoardControl : MonoBehaviour
{
    [SerializeField]
    private Camera _cameraMain;

    private bool _animRotation;

    private float _animRotationSpeed = 0.15f;

    private float _animRotationHeight = 1f;

    [SerializeField]
    public int PlayerCount;

    [SerializeField]
    private GameObject _prefabBasePlate;

    [SerializeField]
    private Material[] _playerColors = new Material[] { };

    //temp
    [SerializeField]
    private Material _selected;
    //

    float waitedtime;

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
    private ArrayList[] _PlayerInfo;

    [SerializeField]
    private GameObject[,] _tileSpots;

    [SerializeField]
    private ArrayList[,] _tiles;

    [SerializeField]
    private GameObject[] _shopPrefabs;

    private string[] _shopNames, _brandNames;


    private int _columns, _rows;

    System.Random rn = new System.Random();

    private int _rounds;

    private int _playerTurn;

    private int _currentPlayer;

    private int[] _selectedTile = new int[2];

    private int _tileView;

    private bool _allowedToMove;

    private bool _batteling;

    private bool _contest;

    private int _battleNumber;

    // Start is called before the first frame update
    void Start()
    {
        _brandNames = new string[] { "Garry's", "Aïki noodle", "Mike", "WcDonalds", "Jeff's", "Yuri's", "Roel's", "André's", "Evy's", "Sander's", "Jasper's", "Grigory's", "Finn's" };

        _shopNames = new string[] { "Stand", "Parking Lot", "Gas Station", "Shop", "Restaurant", "Super Market", "Electronics Store", "Mall", "Mega Mall", "Headquarters", "Headquarters", "Headquarters", "Headquarters"};

        _allowedToMove = true;

        _tiles = new ArrayList[0, 0];

        _tileSpots = new GameObject[0, 0];

        _PlayerInfo = new ArrayList[PlayerCount];

        switch (PlayerCount)
        {
            case 2:
                _columns = 4; _rows = 4;
                _tiles = new ArrayList[_columns, _rows];
                _tileSpots = new GameObject[_columns, _rows];
                break;

            default:
                _columns = 5; _rows = 5;
                _tiles = new ArrayList[_columns, _rows];
                _tileSpots = new GameObject[_columns, _rows];
                break;
        }


        MapGeneration();
        PlayerInitialization();
        GroundPlatePlacing();
        CameraStartPlacement();

        _TildeDetailDisplay.GetComponentInParent<CanvasGroup>().alpha = 0;
        

        PlayerTurn();
    }

    private void CameraStartPlacement()
    {
        float fieldWidth = _columns;
        float fieldHeight = _rows;
        int centralTileX = (int)Math.Ceiling(fieldWidth / 2) - 1;
        int centralTileZ = (int)Math.Ceiling(fieldHeight / 2) - 1;
        Debug.Log(centralTileX);
        Debug.Log(centralTileZ);
        Vector3 centralTilePosition = _tileSpots[centralTileZ, centralTileX].transform.position;
        _cameraMain.transform.parent.position = centralTilePosition;
        _cameraMain.transform.localPosition = new Vector3(0, 4, -5);
        _cameraMain.transform.localEulerAngles = new Vector3(45, 0, 0);
    }

    private void MapGeneration()
    {
        

        for (int i = 0; i < _columns; i++)
        {
            for (int j = 0; j < _rows; j++)
            {
                Debug.Log("-------------------------------------------------------------------------------");
                Debug.Log("Start");
                Debug.Log(i.ToString() + " _ " + j.ToString());
                switch (PlayerCount)
                {
                    case 2:

                        switch (i + (j * 10))
                        {
                            case 0:
                                ArrayList AllTileInfo = new ArrayList() { _playerColors[0], 9, 0 };
                                _tiles[i, j] = AllTileInfo;

                                break;

                            case 33:
                                AllTileInfo = new ArrayList() { _playerColors[3], 12, 0 };
                                _tiles[i, j] = AllTileInfo;
                                break;

                            case 1:
                            case 10:
                            case 11:
                            case 22:
                            case 23:
                            case 32:

                                AllTileInfo = new ArrayList();
                                AllTileInfo.Add(_playerColors[4]);
                                int TileLevel = rn.Next(1, 5);
                                AllTileInfo.Add(TileLevel);
                                int TileCost = rn.Next(((TileLevel * 100) - 20), (((TileLevel * 100) + 20) + 1));
                                AllTileInfo.Add(TileCost);
                                _tiles[i, j] = AllTileInfo;

                                break;

                            case 3:
                            case 30:

                                AllTileInfo = new ArrayList();
                                AllTileInfo.Add(_playerColors[4]);
                                TileLevel = rn.Next(7, 10);
                                AllTileInfo.Add(TileLevel);
                                TileCost = rn.Next(((TileLevel * 100) - 20), (((TileLevel * 100) + 20) + 1));
                                AllTileInfo.Add(TileCost);
                                _tiles[i, j] = AllTileInfo;

                                break;

                            default:

                                AllTileInfo = new ArrayList();
                                AllTileInfo.Add(_playerColors[4]);
                                TileLevel = rn.Next(5, 9);
                                AllTileInfo.Add(TileLevel);
                                TileCost = rn.Next(((TileLevel * 100) - 20), (((TileLevel * 100) + 20) + 1));
                                AllTileInfo.Add(TileCost);
                                _tiles[i, j] = AllTileInfo;

                                break;
                        }

                        break;

                    case 3:

                        switch (i + (j * 10))
                        {
                            case 0:
                                ArrayList AllTileInfo = new ArrayList() { _playerColors[0], 9, 0 };
                                _tiles[i, j] = AllTileInfo;

                                break;

                            case 22:
                                AllTileInfo = new ArrayList() { _playerColors[2], 11, 0 };
                                _tiles[i, j] = AllTileInfo;
                                break;



                            case 44:
                                AllTileInfo = new ArrayList() { _playerColors[3], 12, 0 };
                                _tiles[i, j] = AllTileInfo;
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
                                AllTileInfo.Add(_playerColors[4]);
                                int TileLevel = rn.Next(1, 5);
                                AllTileInfo.Add(TileLevel);
                                int TileCost = rn.Next(((TileLevel * 100) - 20), (((TileLevel * 100) + 20) + 1));
                                AllTileInfo.Add(TileCost);
                                string TileName = _brandNames[rn.Next(0,_brandNames.Length)] + " " + _shopNames[TileLevel-1];
                                AllTileInfo.Add(TileName);
                                _tiles[i, j] = AllTileInfo;

                                break;

                            case 4:
                            case 40:

                                AllTileInfo = new ArrayList();
                                AllTileInfo.Add(_playerColors[4]);
                                TileLevel = rn.Next(7, 10);
                                AllTileInfo.Add(TileLevel);
                                TileCost = rn.Next(((TileLevel * 100) - 20), (((TileLevel * 100) + 20) + 1));
                                AllTileInfo.Add(TileCost);
                                TileName = _brandNames[rn.Next(0, _brandNames.Length)] + " " + _shopNames[TileLevel - 1];
                                AllTileInfo.Add(TileName);
                                _tiles[i, j] = AllTileInfo;

                                break;

                            default:

                                AllTileInfo = new ArrayList();
                                AllTileInfo.Add(_playerColors[4]);
                                TileLevel = rn.Next(5, 9);
                                AllTileInfo.Add(TileLevel);
                                TileCost = rn.Next(((TileLevel * 100) - 20), (((TileLevel * 100) + 20) + 1));
                                AllTileInfo.Add(TileCost);
                                TileName = _brandNames[rn.Next(0, _brandNames.Length)] + " " + _shopNames[TileLevel - 1];
                                AllTileInfo.Add(TileName);
                                _tiles[i, j] = AllTileInfo;

                                break;
                        }

                        break;

                    case 4:

                        switch (i + (j * 10))
                        {
                            case 0:
                                ArrayList AllTileInfo = new ArrayList() { _playerColors[0], 10, 0, _brandNames[rn.Next(0, _brandNames.Length)] + " " + _shopNames[9] };
                                _tiles[i, j] = AllTileInfo;

                                break;

                            case 4:
                                AllTileInfo = new ArrayList() { _playerColors[2], 12, 0, _brandNames[rn.Next(0, _brandNames.Length)] + " " + _shopNames[11] };
                                _tiles[i, j] = AllTileInfo;
                                break;

                            case 40:
                                AllTileInfo = new ArrayList() { _playerColors[1], 11, 0, _brandNames[rn.Next(0, _brandNames.Length)] + " " + _shopNames[10] };
                                _tiles[i, j] = AllTileInfo;
                                break;

                            case 44:
                                AllTileInfo = new ArrayList() { _playerColors[3], 13, 0, _brandNames[rn.Next(0, _brandNames.Length)] + " " + _shopNames[12] };
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
                                AllTileInfo.Add(_playerColors[4]);
                                int TileLevel = rn.Next(1, 5);
                                AllTileInfo.Add(TileLevel);
                                int TileCost = rn.Next(((TileLevel * 100) - 20), (((TileLevel * 100) + 20) + 1));
                                AllTileInfo.Add(TileCost);
                                string TileName = _brandNames[rn.Next(0, _brandNames.Length)] + " " + _shopNames[TileLevel - 1];
                                AllTileInfo.Add(TileName);
                                _tiles[i, j] = AllTileInfo;

                                break;

                            case 22:

                                AllTileInfo = new ArrayList();
                                AllTileInfo.Add(_playerColors[4]);
                                TileLevel = rn.Next(7, 10);
                                AllTileInfo.Add(TileLevel);
                                TileCost = rn.Next(((TileLevel * 100) - 20), (((TileLevel * 100) + 20) + 1));
                                AllTileInfo.Add(TileCost);
                                TileName = _brandNames[rn.Next(0, _brandNames.Length)] + " " + _shopNames[TileLevel - 1];
                                AllTileInfo.Add(TileName);
                                _tiles[i, j] = AllTileInfo;

                                break;

                            default:

                                AllTileInfo = new ArrayList();
                                AllTileInfo.Add(_playerColors[4]);
                                TileLevel = rn.Next(5, 9);
                                AllTileInfo.Add(TileLevel);
                                TileCost = rn.Next(((TileLevel * 100) - 20), (((TileLevel * 100) + 20) + 1));
                                AllTileInfo.Add(TileCost);
                                TileName = _brandNames[rn.Next(0, _brandNames.Length)] + " " + _shopNames[TileLevel - 1];
                                AllTileInfo.Add(TileName);
                                _tiles[i, j] = AllTileInfo;

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

        for (int i = 0; i < PlayerCount; i++)
        {
            ArrayList NewPlayerIntialization = new ArrayList();
            NewPlayerIntialization.Add(i);
            NewPlayerIntialization.Add(500);
            _PlayerInfo[i] = NewPlayerIntialization;
            _playerMoneyDisplay.text = ((int)_PlayerInfo[_currentPlayer][1]).ToString();
        }
    }

    private void GroundPlatePlacing()
    {
        for (int i = 0; i < _columns; i++)
        {
            for (int j = 0; j < _rows; j++)
            {

                GameObject NewGroundPlate = GameObject.Instantiate(_prefabBasePlate, transform, true);
                NewGroundPlate.transform.position += new Vector3(i * 1.5f, 0, j * 1.5f);
                GameObject NewBuilding = GameObject.Instantiate(_shopPrefabs[(int)_tiles[i, j][1] -1], transform, true);
                NewBuilding.transform.localScale = NewBuilding.transform.localScale / 4;
                NewBuilding.transform.parent = NewGroundPlate.transform;
                NewBuilding.transform.position = NewGroundPlate.transform.position + new Vector3(0,0.1f,0);
                

                //Tell me te explain this if you don't get this part -M
                Material NewGroundPlateMaterial = (Material)((ArrayList)_tiles[i, j])[0];


                //This is to add the location of the HQs to the playerInfo -M
                if ((Material)((ArrayList)_tiles[i, j])[0] != _playerColors[4])
                {
                    for (int k = 0; k < _playerColors.Length-1; k++)
                    {
                        if (_playerColors[k] == (Material)((ArrayList)_tiles[i, j])[0])
                        {
                            _PlayerInfo[k].Add(i+(j*10));
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
                        
                        Debug.Log("skreeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeech");
                    }
                }
                NewBuilding.GetComponent<MeshRenderer>().materials = buildingMaterials;

                _tileSpots[i,j] = NewGroundPlate;

            }
        }
    }

    

    // Update is called once per frame
    void Update()
    {
        if (!_batteling && !_contest)
        {
            waitedtime += Time.deltaTime;

            if (_allowedToMove && (Input.GetAxis("LeftStickHorizontal1") != 0 || Input.GetAxis("LeftStickVertical1") != 0) && (waitedtime >= 0.25f))
            {
                waitedtime = 0;
                int HorizontalInput = 0;
                if (Input.GetAxis("LeftStickHorizontal1") > 0 && _selectedTile[0] < _columns - 1&& Input.GetAxis("LeftStickHorizontal1") > Mathf.Pow(Input.GetAxis("LeftStickVertical1"), 1)) HorizontalInput = 1;
                if (Input.GetAxis("LeftStickHorizontal1") < 0 && _selectedTile[0] > 0 && Input.GetAxis("LeftStickVertical1") > Mathf.Pow(Input.GetAxis("LeftStickHorizontal1"), 1)) HorizontalInput = -1;

                int VerticalInput = 0;
                if (Input.GetAxis("LeftStickVertical1") < 0 && _selectedTile[1] > 0 && Input.GetAxis("LeftStickHorizontal1") > Mathf.Pow(Input.GetAxis("LeftStickVertical1"), 1)) VerticalInput = -1;
                if (Input.GetAxis("LeftStickVertical1") > 0 && _selectedTile[1] < _rows - 1 && Input.GetAxis("LeftStickVertical1") > Mathf.Pow(Input.GetAxis("LeftStickHorizontal1"), 1)) VerticalInput = 1;

                Debug.Log("----------------------------------");
                Debug.Log(Input.GetAxis("LeftStickHorizontal1"));
                Debug.Log(HorizontalInput);
                Debug.Log(Input.GetAxis("LeftStickVertical1"));
                Debug.Log(VerticalInput);
                Debug.Log(_columns);
                Debug.Log(_rows);
                Debug.Log("----------------------------------");
                SelectedTileChanged(HorizontalInput, VerticalInput);
            }


            if (Input.GetButton("AButton1") && waitedtime >= 1f)
            {
                TheShowDetailAndBuyMethod(1);
                CameraIfTileSelected();
                TheShowDetailAndBuyMethod(0);
                waitedtime = 0f;
            }

            if (Input.GetButton("BButton1") && waitedtime >= 1f)
            {
                TheShowDetailAndBuyMethod(-1);
                waitedtime = 0f;
            }

            if (_animRotation)
            {
                _cameraMain.transform.parent.eulerAngles += Vector3.up * _animRotationSpeed;
                _cameraMain.transform.localPosition = new Vector3(_cameraMain.transform.localPosition.x,
                    _animRotationHeight + Mathf.Sin(Time.time * 1.5f) * 0.1f, _cameraMain.transform.localPosition.z);
            }
        }
        if (_batteling)
        {
            Battle();
        }
        if (_contest)
        {

        }
    }

    private void CameraIfTileSelected()
    {
        int row = _selectedTile[0];
        int column = _selectedTile[1];
        _cameraMain.transform.parent.position = _tileSpots[row, column].transform.position;
        _cameraMain.transform.localPosition = new Vector3(0, _animRotationHeight, -1);
        _cameraMain.transform.localEulerAngles = new Vector3(30, 0, 0);
        _animRotation = true;
    }


    private void PlayerTurn()
    {

        for (int i = 0; i < PlayerCount; i++)
        {
            if ((int)_PlayerInfo[i][0] == _playerTurn)
            {
                if (_PlayerInfo[i][0].ToString().Length == 1)
                {
                    _selectedTile[0] = int.Parse(_PlayerInfo[i][0].ToString());
                    _selectedTile[1] = 0;
                }
                else
                {
                    _selectedTile[1] = int.Parse(_PlayerInfo[i][0].ToString().Substring(0, 1));
                    _selectedTile[0] = int.Parse(_PlayerInfo[i][0].ToString().Substring(1, 1));
                }

                _currentPlayer = i;
            }
        }


        SelectedTileChanged(0,0);
    }

    private void SelectedTileChanged(int col, int row)
    {
        Debug.Log("");
        Debug.Log("Materials");
        Debug.Log("--------------------------------------------------------------");
        _tileSpots[_selectedTile[0], _selectedTile[1]].GetComponent<MeshRenderer>().material = (Material)((ArrayList)_tiles[_selectedTile[0], _selectedTile[1]])[0];
        Debug.Log((Material)((ArrayList)_tiles[_selectedTile[0], _selectedTile[1]])[0]);
        _selectedTile[0] += col;
        _selectedTile[1] += row;
        Debug.Log(_selectedTile[0].ToString() + " _ "+ _selectedTile[1].ToString());
        _tileSpots[_selectedTile[0], _selectedTile[1]].GetComponent<MeshRenderer>().material = _selected;
        Debug.Log("--------------------------------------------------------------");
    }

    private void TheShowDetailAndBuyMethod(int changeBy)
    {
        if (changeBy == 1 && _tileView !=2 )
        {
            _tileView++;
        }
        if (changeBy == -1 && _tileView != 0)
        {
            _tileView--;
        }

        Debug.Log(_tileView);

        switch (_tileView)
        {
            case 0:
                _allowedToMove = true;
                _TildeDetailDisplay.GetComponentInParent<CanvasGroup>().alpha = 0;

                break;
            case 1:
                _allowedToMove = false;
                _TildeDetailDisplay.GetComponentInParent<CanvasGroup>().alpha = 1;

                _ownerBuildingDisplay.text = "Owner: "+ (("Grey" == ((Material)_tiles[_selectedTile[0], _selectedTile[1]][0]).name.ToString())? "No One" : ((Material)_tiles[_selectedTile[0], _selectedTile[1]][0]).name.ToString());
                //Debug.Log(((Material)_tiles[_selectedTile[0], _selectedTile[1]][0]).name.ToString());
                _levelBuildingDisplay.text = "LVL: " + _tiles[_selectedTile[0], _selectedTile[1]][1].ToString();

                _costBuildingDisplay.text = "Cost: " + _tiles[_selectedTile[0], _selectedTile[1]][2].ToString();

                _nameBuildingDisplay.text = _tiles[_selectedTile[0], _selectedTile[1]][3].ToString();
                bool SomthingNextToIt = false;

                if (_selectedTile[0] != 0)
                {
                    if ((Material)_tiles[_selectedTile[0]-1, _selectedTile[1]][0] == (Material)_playerColors[_currentPlayer]) SomthingNextToIt = true;
                    //Debug.Log(((Material)_tiles[_selectedTile[0] - 1, _selectedTile[1]][0]).ToString() + " _ " + ((Material)_playerColors[_currentPlayer]).ToString());
                }

                if (_selectedTile[0] != _columns-1)
                {
                    if ((Material)_tiles[_selectedTile[0] + 1, _selectedTile[1]][0] == (Material)_playerColors[_currentPlayer]) SomthingNextToIt = true;
                    //Debug.Log(((Material)_tiles[_selectedTile[0], _selectedTile[1] + 1][0]).ToString() + " _ " + ((Material)_playerColors[_currentPlayer]).ToString());
                }

                if (_selectedTile[1] != 0)
                {
                    if ((Material)_tiles[_selectedTile[0], _selectedTile[1] - 1][0] == (Material)_playerColors[_currentPlayer]) SomthingNextToIt = true;
                    //Debug.Log(((Material)_tiles[_selectedTile[0], _selectedTile[1] - 1][0]).ToString() + " _ " + ((Material)_playerColors[_currentPlayer]).ToString());
                }

                if (_selectedTile[1] != _rows-1)
                {
                    if ((Material)_tiles[_selectedTile[0], _selectedTile[1] + 1][0] == (Material)_playerColors[_currentPlayer]) SomthingNextToIt = true;
                    //Debug.Log(((Material)_tiles[_selectedTile[0], _selectedTile[1] + 1][0]).ToString() + " _ " + ((Material)_playerColors[_currentPlayer]).ToString());
                }

                if ((_tiles[_selectedTile[0], _selectedTile[1]][0] != _playerColors[_currentPlayer]) && ((int)_tiles[_selectedTile[0], _selectedTile[1]][2] != 0) && (SomthingNextToIt))
                {
                    _BuyButtonDisplay.SetActive(true);
                }
                else
                {
                    _BuyButtonDisplay.SetActive(false);
                }

                break;
            case 2:
                if (_BuyButtonDisplay.active)
                {
                    BuyTile();
                    
                }
                else { _tileView = 1; }

                break;
        }
    }

    private void BuyTile()
    {
        if ((int)_PlayerInfo[_currentPlayer][1] >= (int)_tiles[_selectedTile[0], _selectedTile[1]][2]) 
        {
            _PlayerInfo[_currentPlayer][1] = (int)_PlayerInfo[_currentPlayer][1] - (int)_tiles[_selectedTile[0], _selectedTile[1]][2];
            _playerMoneyDisplay.text = ((int)_PlayerInfo[_currentPlayer][1]).ToString();
            if (_tiles[_selectedTile[0], _selectedTile[1]][0] != _playerColors[4])
            {
                _battleNumber = rn.Next(1, 5);
                _batteling = true;
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

        switch (_battleNumber)
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
            _tiles[_selectedTile[0], _selectedTile[1]][0] = _playerColors[_currentPlayer];
            _tileView = 0;
        }
        
    }

}
