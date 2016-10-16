using UnityEngine;
using System.Collections.Generic;

public class NavigationKeyPoint : MonoBehaviour
{
    public List<NavigationKeyPoint> adjacentsKeyPoints;
    public string pointName;
    public string name;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(transform.position, new Vector3(0.5f,0.5f,0.5f));
    }
}
