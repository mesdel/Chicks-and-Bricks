using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridHandler : MonoBehaviour
{
    [SerializeField]
    private Transform ghostTrans;
    private float defaultGhostY = 0.0f;
    private float cellSize = 1.0f;
    private float offSet = 0.5f;

    [SerializeField]
    private GameObject chickens;
    private Transform fullCellGroup;
    [SerializeField]
    private GameObject fullCellPrefab;

    private bool[][] grid;
    private int gridSize = 40;

    private void Awake()
    {
        fullCellGroup = transform.Find("Occupied Cells");
        InitGrid();
    }

    private void InitGrid()
    {
        grid = new bool[gridSize][];
        for(int i = 0; i < gridSize; i++)
        {
            grid[i] = new bool[gridSize];
        }

        foreach(Transform cellT in fullCellGroup)
        {
            FillCell(cellT);
        }
        foreach(Transform cellT in chickens.transform)
        {
            FillCell(cellT);
        }

        //todo: delete debug
        //PrintGrid();
    }

    private void FillCell(Transform worldT)
    {
        Vector2 gridCoords = WorldToCell(worldT.position.x, worldT.position.z);
        grid[(int)(gridCoords.x)][(int)(gridCoords.y)] = true;
    }

    private void PrintGrid()
    {
        for(int i = 0; i < gridSize; i++)
        {
            string row = "";
            for (int k = 0; k < gridSize; k++)
            {
                row += grid[i][k] + ", ";
            }
            Debug.Log(i + ": " + row);
        }
    }

    private Vector3 GridSnap(Vector3 point)
    {
        Vector3 newPoint = new Vector3();
        newPoint.y = defaultGhostY;

        newPoint.x = (int)(point.x) - offSet;
        newPoint.z = (int)(point.z) - offSet;

        return newPoint;
    }

    public void ProjectGhost(float range, Transform cameraTrans)
    {
        Ray viewRay = new Ray(cameraTrans.position, cameraTrans.forward);
        bool didHit = Physics.Raycast(viewRay, out RaycastHit hitData, range);

        if (didHit)
        {
            ghostTrans.position = GridSnap(hitData.point);
        }
        else
        {
            ghostTrans.position = GridSnap(cameraTrans.position + cameraTrans.forward * range);
        }
    }

    public void RotateGhost()
    {
        ghostTrans.Rotate(Vector3.up, 90.0f);
    }

    public void Activate(bool activeVal)
    {
        gameObject.SetActive(activeVal);
        foreach(Transform chickenT in chickens.transform)
        {
            chickenT.Find("Full Cell").gameObject.SetActive(activeVal);  
        }
    }

    // todo
    private Vector2 WorldToCell(float wx, float wy)
    {
        float cellX, cellY;

        cellX = wx - offSet + gridSize / 2.0f;
        cellY = wy - offSet + gridSize / 2.0f;

        return new Vector2(cellX, cellY);
    }

    private Vector2 CellToWorld(float cx, float cy)
    {
        return new Vector2();
    }

    public void PlaceOnGrid(float wx, float wy)
    {
        // translate world to cell, update grid, instantiate full cell
    }

    public void PickUpFromGrid(float wx, float wy)
    {
        // translate world to cell, update grid, delete full cell

        // problem to solve: link chicken with its full cell object?
        // possible fix: have every chicken have the object as child and act/deact on place/pickup
    }
}
