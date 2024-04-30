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
    private ArrayList[] _PlayerInfo;

    [SerializeField]
    private GameObject[,] _tileSpots;

    [SerializeField]
    private ArrayList[,] _tiles;

    [SerializeField]
    private GameObject[] _shopPrefabs;

    private string[] _shopNames, _brandNames;


    private int _rows, _columns;

    System.Random rn = new System.Random();

    private int _rounds;

    private int _playerTurn;

    private int _currentPlayer;

    private int[] _selectedTile = new int[2];

    private int _tileView;

    private bool _allowedToMove;
    

    // Start is called before the first frame update
    void Start()
    {
        _brandNames = new string[] { "Garry's", "Aïki noodle", "Mike", "WcDonalds", "Jeff's", "Yuri's", "Roel's", "André's", "Evy's", "Sander's", "Jasper's", "Grigory's", "Finn's" };

        _shopNames = new string[] { "Stand", "Parking Lot", "Gas Station", "Shop", "Restaurant", "Super Market", "Electronics Store", "Mall", "Mega Mall"};

        _allowedToMove = true;

        _tiles = new ArrayList[0, 0];

        _tileSpots = new GameObject[0, 0];

        _PlayerInfo = new ArrayList[PlayerCount];

        switch (PlayerCount)
        {
            case 2:
                _rows = 4; _columns = 4;
                _tiles = new ArrayList[_rows, _columns];
                _tileSpots = new GameObject[_rows, _columns];
                break;

            default:
                _rows = 5; _columns = 5;
                _tiles = new ArrayList[_rows, _columns];
                _tileSpots = new GameObject[_rows, _columns];
                break;
        }


        MapGeneration();
        PlayerInitialization();
        GroundPlatePlacing();

        _TildeDetailDisplay.GetComponentInParent<CanvasGroup>().alpha = 0;

        PlayerTurn();
    }

    private void MapGeneration()
    {
        

        for (int i = 0; i < _rows; i++)
        {
            for (int j = 0; j < _columns; j++)
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

                    case 4:

                        switch (i + (j * 10))
                        {
                            case 0:
                                ArrayList AllTileInfo = new ArrayList() { _playerColors[0], 9, 0 };
                                _tiles[i, j] = AllTileInfo;

                                break;

                            case 4:
                                AllTileInfo = new ArrayList() { _playerColors[2], 11, 0 };
                                _tiles[i, j] = AllTileInfo;
                                break;

                            case 40:
                                AllTileInfo = new ArrayList() { _playerColors[1], 10, 0 };
                                _tiles[i, j] = AllTileInfo;
                                break;

                            case 44:
                                AllTileInfo = new ArrayList() { _playerColors[3], 12, 0 };
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
                                _tiles[i, j] = AllTileInfo;

                                break;

                            case 22:

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
        }
    }

    private void GroundPlatePlacing()
    {
        for (int i = 0; i < _rows; i++)
        {
            for (int j = 0; j < _columns; j++)
            {

                GameObject NewGroundPlate = GameObject.Instantiate(_prefabBasePlate, transform, true);
                NewGroundPlate.transform.localPosition += new Vector3(i * 1.5f, 0, j * 1.5f);

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
                _tileSpots[i,j] = NewGroundPlate;

            }
        }
    }

    

    // Update is called once per frame
    void Update()
    {
        

        waitedtime += Time.deltaTime;

        if (_allowedToMove && (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) && ((Input.GetAxis("Horizontal") != 0) && (Input.GetAxis("Vertical") != 0))==false &&(waitedtime >= 0.25f))
        {
            waitedtime = 0;
            int HorizontalInput = 0;
            if (Input.GetAxis("Horizontal") > 0 && _selectedTile[1] < _rows-1) HorizontalInput = 1;
            if (Input.GetAxis("Horizontal") < 0 && _selectedTile[1] > 0) HorizontalInput = -1;

            int VerticalInput = 0;
            if (Input.GetAxis("Vertical") < 0 && _selectedTile[0] > 0) VerticalInput = -1;
            if (Input.GetAxis("Vertical") > 0 && _selectedTile[0] < _columns-1) VerticalInput = 1;

            Debug.Log("----------------------------------");
            Debug.Log(Input.GetAxis("Horizontal"));
            Debug.Log(HorizontalInput);
            Debug.Log(Input.GetAxis("Vertical"));
            Debug.Log(VerticalInput);
            Debug.Log("----------------------------------");
            SelectedTileChanged(VerticalInput, HorizontalInput);
        }


        if (Input.GetButton("Jump") && waitedtime >= 1f)
        {
            TheShowDetailAndBuyMethod(1);
        }
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

    private void SelectedTileChanged(int row, int col)
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

        switch (_tileView)
        {
            case 0:

                break;
            case 1:
                _allowedToMove = false;
                _TildeDetailDisplay.GetComponentInParent<CanvasGroup>().alpha = 1;

                _ownerBuildingDisplay.text = "Owner: "+ (("Grey" == ((Material)_tiles[_selectedTile[0], _selectedTile[1]][0]).name.ToString())? "No One" : ((Material)_tiles[_selectedTile[0], _selectedTile[1]][0]).name.ToString());
                Debug.Log(((Material)_tiles[_selectedTile[0], _selectedTile[1]][0]).name.ToString());
                _levelBuildingDisplay.text = "LVL: " + _tiles[_selectedTile[0], _selectedTile[1]][1].ToString();

                _costBuildingDisplay.text = "Cost: " + _tiles[_selectedTile[0], _selectedTile[1]][2].ToString();

                bool SomthingNextToIt = false;

                if (_selectedTile[0] != 0)
                {
                    if ((Material)_tiles[_selectedTile[0]-1, _selectedTile[1]][0] == (Material)_playerColors[_currentPlayer]) SomthingNextToIt = true;
                    Debug.Log(((Material)_tiles[_selectedTile[0] - 1, _selectedTile[1]][0]).ToString() + " _ " + ((Material)_playerColors[_currentPlayer]).ToString());
                }

                if (_selectedTile[0] != _rows-1)
                {
                    if ((Material)_tiles[_selectedTile[0] + 1, _selectedTile[1]][0] == (Material)_playerColors[_currentPlayer]) SomthingNextToIt = true;
                    Debug.Log(((Material)_tiles[_selectedTile[0], _selectedTile[1] + 1][0]).ToString() + " _ " + ((Material)_playerColors[_currentPlayer]).ToString());
                }

                if (_selectedTile[1] != 0)
                {
                    if ((Material)_tiles[_selectedTile[0], _selectedTile[1] - 1][0] == (Material)_playerColors[_currentPlayer]) SomthingNextToIt = true;
                    Debug.Log(((Material)_tiles[_selectedTile[0], _selectedTile[1] - 1][0]).ToString() + " _ " + ((Material)_playerColors[_currentPlayer]).ToString());
                }

                if (_selectedTile[1] != _columns-1)
                {
                    if ((Material)_tiles[_selectedTile[0], _selectedTile[1] + 1][0] == (Material)_playerColors[_currentPlayer]) SomthingNextToIt = true;
                    Debug.Log(((Material)_tiles[_selectedTile[0], _selectedTile[1] + 1][0]).ToString() + " _ " + ((Material)_playerColors[_currentPlayer]).ToString());
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
            _tiles[_selectedTile[0], _selectedTile[1]][0] = _playerColors[_currentPlayer];
            _tileView = 0;
        }
    }
}
