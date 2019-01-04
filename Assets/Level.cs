using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public LayerMask m_layerMask;
    public GameObject m_eyedropper;
    public Collider m_gameBoard;

    enum CellValue
    {
        Wall,
        Edible,
        Power_Up,
        Empty
    }

    struct Cell
    {
        public int xPos;
        public int yPos;
        public Vector3 center;
        public CellValue cellValue;
    }

    private Cell[,] m_grid;
    RaycastHit m_HitInfo = new RaycastHit();
    Color[] m_valueColors = {Color.gray, Color.blue, Color.green, Color.white};

    void Start()
    {
        Vector3 cellHalfExtents = Vector3.one * 0.5f;
        Vector3 extents = m_gameBoard.bounds.extents * 2;
        int width = (int)extents.x;
        int height = (int)extents.z;
        m_grid = new Cell[width, height];
        for (int row = 0; row < height; row++)
        {
            for (int column = 0; column < width; column++)
            {
                Vector3 center = m_gameBoard.bounds.min + new Vector3(column + 0.5f, 0.0f, row + 0.5f);
                Collider[] occupiers = Physics.OverlapBox(center, cellHalfExtents, Quaternion.identity, m_layerMask);
                CellValue cellValue = CellValue.Empty;
                foreach (Collider occupier in occupiers)
                {
                    bool isWall = occupier.gameObject.layer == LayerMask.NameToLayer("Interior Walls");
                    if (isWall)
                    {
                        cellValue = CellValue.Wall;
                        break;
                    }
                    bool isEdible = occupier.gameObject.layer == LayerMask.NameToLayer("Interior Walls");
                    if (isEdible)
                    {
                        // TODO: Check if power-up
                        cellValue = CellValue.Edible;
                    }
                }
                m_grid[row, column] = new Cell {xPos = column, yPos = row, cellValue = cellValue, center = center};
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.LeftShift))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out m_HitInfo))
            {
                int column = (int)(m_HitInfo.point.x - m_gameBoard.bounds.min.x);
                int row = (int)(m_HitInfo.point.z - m_gameBoard.bounds.min.z);
                m_eyedropper.transform.position = m_HitInfo.point;
                Cell cell = m_grid[row, column];
                m_eyedropper.GetComponent<Renderer>().material.color = m_valueColors[(int)cell.cellValue];

                Debug.Log($"Hit Point ({m_HitInfo.point.x}, {m_HitInfo.point.x}, {m_HitInfo.point.x}) which is cell ({row}, {column}) centered at {cell.center}");
            }
        }

    }
}
