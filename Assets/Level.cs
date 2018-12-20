using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public LayerMask m_layerMask;

    struct Cell
    {
        public int xPos;
        public int yPos;
        public int cellValue;
    }

    private Cell[,] m_grid;

    void Start()
    {
        Transform gameBoard = this.transform.Find("Board");
        Collider collider = gameBoard.GetComponent<Collider>();

        Vector3 cellHalfExtents = Vector3.one * 0.5f;
        Vector3 extents = collider.bounds.extents * 2;
        int width = (int)extents.x;
        int height = (int)extents.y;
        m_grid = new Cell[width, height];
        for (int row = 0; row < height; row++)
        {
            for (int column = 0; column < width; column++)
            {
                Vector3 center = collider.bounds.min + new Vector3(column + 0.5f, row + 0.5f, 0.0f);
                Collider[] occupiers = Physics.OverlapBox(center, cellHalfExtents, Quaternion.identity, m_layerMask);
                int cellValue = 0;
                foreach (Collider occupier in occupiers)
                {
                    
                }
                m_grid[row, column] = new Cell {xPos = column, yPos = row, cellValue = 0};
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
