using System;
using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardControl : MonoBehaviour
{
    public int PlayerCount;

    private ArrayList[,] Tiles;

    private Color[] PlayerColors = new Color[] {Color.red, Color.blue, Color.green, Color.yellow, Color.gray };

    private int _rows, _columns;

    private const int _tileInfo = 3;

    System.Random rn = new System.Random();

    // Start is called before the first frame update
    void Start()
    {
        Tiles = new ArrayList[0, 0];

        switch (PlayerCount)
        {
            case 2:
                _rows = 4; _columns = 4;
               Tiles = new ArrayList[4, 4];
                break;

            default:
                _rows = 5; _columns = 5;
                Tiles = new ArrayList[5, 5];
                break;
        }


        MapGeneration();
    }

    private void MapGeneration()
    {
        

        for (int i = 0; i < _rows; i++)
        {
            for (int j = 0; j < _columns; j++)
            {
                switch (PlayerCount)
                {
                    case 3:

                        break;

                    case 4:

                        switch (_rows + (_columns * 10))
                        {
                            case 0:
                                ArrayList AllTileInfo = new ArrayList() { PlayerColors[0], 9, 0 };
                                Tiles[_rows,_columns] = AllTileInfo;

                                break;

                            case 4:
                                AllTileInfo = new ArrayList() { PlayerColors[2], 11, 0 };
                                Tiles[_rows, _columns] = AllTileInfo;
                                break;

                            case 40:
                                AllTileInfo = new ArrayList() { PlayerColors[1], 10, 0 };
                                Tiles[_rows, _columns] = AllTileInfo;
                                break;

                            case 44:
                                AllTileInfo = new ArrayList() { PlayerColors[3], 12, 0 };
                                Tiles[_rows, _columns] = AllTileInfo;
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
                                AllTileInfo.Add(PlayerColors[4]);
                                int TileLevel = rn.Next(0, 5);
                                AllTileInfo.Add(TileLevel);
                                int TileCost = rn.Next(((TileLevel*100)-20), (((TileLevel * 100) + 20)+1));
                                AllTileInfo.Add(TileCost);
                                Tiles[_rows, _columns] = AllTileInfo;

                                break;

                            case 22:

                                AllTileInfo = new ArrayList();
                                AllTileInfo.Add(PlayerColors[4]);
                                TileLevel = rn.Next(7, 10);
                                AllTileInfo.Add(TileLevel);
                                TileCost = rn.Next(((TileLevel * 100) - 20), (((TileLevel * 100) + 20) + 1));
                                AllTileInfo.Add(TileCost);
                                Tiles[_rows, _columns] = AllTileInfo;

                                break;

                            default:

                                AllTileInfo = new ArrayList();
                                AllTileInfo.Add(PlayerColors[4]);
                                TileLevel = rn.Next(5, 9);
                                AllTileInfo.Add(TileLevel);
                                TileCost = rn.Next(((TileLevel * 100) - 20), (((TileLevel * 100) + 20) + 1));
                                AllTileInfo.Add(TileCost);
                                Tiles[_rows, _columns] = AllTileInfo;

                                break;
                        }

                        break;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
