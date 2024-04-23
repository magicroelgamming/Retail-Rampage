using System;
using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    private ArrayList[] _PlayerInfo;

    private GameObject[,] _tileSpots;

    private ArrayList[,] _tiles;

    private int _rows, _columns;

    System.Random rn = new System.Random();

    private int _rounds;

    private int _playerTurn;

    private int[] _selectedTile = new int[2];

    

    // Start is called before the first frame update
    void Start()
    {
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
                                int TileLevel = rn.Next(0, 5);
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
                                int TileLevel = rn.Next(0, 5);
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
                                int TileLevel = rn.Next(0, 5);
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
        if (_playerTurn == 0) PlayerTurn();


        if ((Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) && !(Input.GetAxis("Horizontal") != 0 && Input.GetAxis("Vertical") != 0))
        {
            SelectedTileChanged((int)Input.GetAxis("Horizontal"), (int)Input.GetAxis("Vertical"));
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
                
                
                break;
            }
        }

        _playerTurn++;

        SelectedTileChanged(0,0);
    }

    private void SelectedTileChanged(int row, int col)
    {
        _tileSpots[_selectedTile[0], _selectedTile[1]].GetComponent<MeshRenderer>().material = (Material)_tiles[_selectedTile[0], _selectedTile[1]][0];
        _selectedTile[0] += row;
        _selectedTile[1] += col;
        _tileSpots[_selectedTile[0], _selectedTile[1]].GetComponent<MeshRenderer>().material = _selected;
    }
}
