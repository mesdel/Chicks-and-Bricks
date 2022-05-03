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
    }
}
