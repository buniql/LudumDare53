using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCityGenerator : MonoBehaviour
{
    public Dictionary<Vector2Int, int> roomGrid = new Dictionary<Vector2Int, int>();

    void Start()
    {
        roomGrid.Add(Vector2Int.zero, 0);
        roomGrid.Add(Vector2Int.right, 1);
        roomGrid.Add(Vector2Int.right * 2, 1);
        roomGrid.Add(Vector2Int.right * 3, 1);
    }
}
