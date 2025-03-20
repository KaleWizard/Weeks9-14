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

            bool cellFull = placedGrid[gridPos.x + gridWidth / 2, gridPos.y + gridHeight / 2];
            if (!cellFull)
            {
                placedGrid[gridPos.x + gridWidth / 2, gridPos.y + gridHeight / 2] = true;
                Instantiate(building, grid.CellToWorld(gridPos) + Vector3.one, Quaternion.identity);
            }
        }
    }
}
