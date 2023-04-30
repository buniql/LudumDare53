using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinimapUI : MonoBehaviour
{
    public CityGenerator CityGenerator;

    public List<GameObject> images;

    public Transform parent;
    
    private float distance = 64f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M)) GenerateMinimap();
        if (Input.GetKeyUp(KeyCode.M))
        {
            foreach (Transform child in parent) {
                GameObject.Destroy(child.gameObject);
            }
        }
    }

    // Start is called before the first frame update
    public void GenerateMinimap()
    {
        foreach (var room in CityGenerator.roomGrid)
        {
            //hier hin kommt er
            var uiElement = Instantiate(images[room.Value], new Vector3(room.Key.x * distance, room.Key.y * distance, 0), Quaternion.identity);
            uiElement.transform.SetParent(parent, false); 
            //uiElement.transform.position = new Vector3(room.Key.x * distance, room.Key.y * distance, 0);
        }
    }


}
