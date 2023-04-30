using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinimapUI : MonoBehaviour
{
    public CityGenerator CityGenerator;

    public GameObject Player;
    public GameObject PlayerHeadImage;
    private GameObject playerHead;

    public GameObject ShopUI;

    public List<GameObject> images;

    public Transform parent;
    
    private float distance = 48f;

    private bool active;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M) && !active && !ShopUI.active) GenerateMinimap();
        if (Input.GetKeyUp(KeyCode.M))
        {
            active = false;
            foreach (Transform child in parent) {
                GameObject.Destroy(child.gameObject);
            }
        }

        if (active)
        {
            Debug.Log("moving");
            playerHead.GetComponent<RectTransform>().anchoredPosition =
                new Vector3(Player.transform.position.x / 52 * 48f, Player.transform.position.y / 36 * 48f, 0);
        }
    }

    // Start is called before the first frame update
    public void GenerateMinimap()
    {
        active = true;
        foreach (var room in CityGenerator.roomGrid)
        {
            //hier hin kommt er
            var uiElement = Instantiate(images[room.Value], new Vector3(room.Key.x * distance, room.Key.y * distance, 0), Quaternion.identity);
            uiElement.transform.SetParent(parent, false); 
            //uiElement.transform.position = new Vector3(room.Key.x * distance, room.Key.y * distance, 0);
        }
        playerHead = Instantiate(PlayerHeadImage, new Vector3(0,0,-1), Quaternion.identity);
        playerHead.transform.SetParent(parent, false);
    }


}
