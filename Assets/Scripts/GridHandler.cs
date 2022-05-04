using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridHandler : MonoBehaviour
{
    public Transform ghostTrans;
    private float defaultGhostY = 0.0f;
    private float offSet = 0.5f;

    public GameObject chickens;
    private List<Vector3> oldChickenPos;
    private Transform fullCellGroup;
    [SerializeField]
    private GameObject fullCellPrefab;

    private bool[][] grid;
    private int gridSize = 40;

    [SerializeField]
    private Material defaultGhostMat;
    [SerializeField]
    private Material redGhostMat;
    private SkinnedMeshRenderer ghostRenderer;
    public bool validGhost { private set; get; }

    // todo: give ghost chicken its own class

    private void Awake()
    {
        fullCellGroup = transform.Find("Occupied Cells");
        ghostTrans = transform.Find("Ghost Chicken");
        InitGrid();
        ghostRenderer = ghostTrans.gameObject.GetComponentInChildren<SkinnedMeshRenderer>();
        ghostRenderer.material = defaultGhostMat;
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
            SetCell(cellT.position, true);
        }

        // init chicken cells and save to old list
        InitChickenGrid();

        //PrintGrid();
    }

    private void InitChickenGrid()
    {
        oldChickenPos = new List<Vector3>();
        foreach (Transform cellT in chickens.transform)
        {
            SetCell(cellT.position, true);
            oldChickenPos.Add(cellT.transform.position);
        }
    }

    public void UpdateGrid()
    {
        foreach(Vector3 worldPos in oldChickenPos)
        {
            // reset all old chicken cells
            SetCell(worldPos, false);
        }

        // update chicken cells and save to old list
        InitChickenGrid();
    }

    private void SetCell(Vector3 worldPos, bool value)
    {
        Vector2 gridCoords = WorldToCell(worldPos.x, worldPos.z);
        grid[(int)(gridCoords.x)][(int)(gridCoords.y)] = value;
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

        Vector2 ghostsCell = WorldToCell(ghostTrans.position.x, ghostTrans.position.z);
        if(grid[(int)ghostsCell.x][(int)ghostsCell.y])
        // cell is full
        {
            ghostRenderer.material = redGhostMat;
            validGhost = false;
        }
        else // cell is empty
        {
            ghostRenderer.material = defaultGhostMat;
            validGhost = true;
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
}
