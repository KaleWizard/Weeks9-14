using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPainter : MonoBehaviour
{
    public GameObject building;

    public Grid grid;
    static int gridWidth = 10;
    static int gridHeight = 6;
    bool[,] placedGrid = new bool[gridWidth, gridHeight];

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < gridWidth; i++)
        {
            for (int j = 0; j < gridHeight; j++)
            {
                placedGrid[i, j] = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int gridPos = grid.WorldToCell(pos);

            int myX = gridPos.x + gridWidth / 2;
            int myY = gridPos.y + gridHeight / 2;

            if (myX < 0 || myX >= gridWidth || myY < 0 || myY >= gridHeight)
            {
                print("Out of bounds!!!");
                return;
            }

            bool cellFull = placedGrid[myX, myY];
            if (!cellFull)
            {
                placedGrid[myX, myY] = true;
                Instantiate(building, grid.CellToWorld(gridPos) + Vector3.one, Quaternion.identity);
            }
        }
    }
}
