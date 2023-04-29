using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CityGenerator : MonoBehaviour
{
    public GameObject SpawnRoom;
    public GameObject Street;

    public List<GameObject> DefaultRooms;
    public List<GameObject> ItemRoom;
    public List<GameObject> BossRooms;

    private Dictionary<Vector2Int, int> roomGrid = new Dictionary<Vector2Int, int>();
    private Dictionary<Vector2, int> streetGrid = new Dictionary<Vector2, int>();

    public int RoomPositionOffsetX;
    public int StreetPositionOffsetX;
    public int RoomPositionOffsetY;
    public int StreetPositionOffsetY;
    
    public int TotalAmountOfRooms;
    private int currentAmountOfRooms = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        FillRoomGrid();
        foreach (var room in roomGrid)
        {
            var spawnedRoom = Instantiate(SpawnRoom, new Vector3(room.Key.x * RoomPositionOffsetX, room.Key.y * RoomPositionOffsetY, 0), Quaternion.identity);
            CheckAdjecentStreets(new Vector2Int(room.Key.x, room.Key.y));
            spawnedRoom.transform.parent = transform;
            spawnedRoom.name = "Room["+room.Key.x+"|"+room.Key.y+"]";
        }

        foreach (var street in streetGrid)
        {
            var spawnedStreet = Instantiate(Street,
                new Vector3(street.Key.x * RoomPositionOffsetX + StreetPositionOffsetX - RoomPositionOffsetX * .5f,
                    street.Key.y * RoomPositionOffsetY + StreetPositionOffsetY - RoomPositionOffsetY * .5f, 0), Quaternion.identity);
            spawnedStreet.transform.parent = transform;
            spawnedStreet.name = "Street["+street.Key.x+"|"+street.Key.y+"]";
        }
    }
    
    private void FillRoomGrid()
    {
        roomGrid.Add(new Vector2Int(0, 0), 1);
        roomGrid.Add(new Vector2Int(1, 0), 1);
        roomGrid.Add(new Vector2Int(-1, 0), 1);
        roomGrid.Add(new Vector2Int(0, 1), 1);
        roomGrid.Add(new Vector2Int(0, -1), 1);

        FillRoomGridFromBranch(new Vector2Int(1, 0), new Vector2Int(0, 1));
        FillRoomGridFromBranch(new Vector2Int(-1, 0), new Vector2Int(0, 1));
        FillRoomGridFromBranch(new Vector2Int(0, 1), new Vector2Int(1, 0));
        FillRoomGridFromBranch(new Vector2Int(0, -1), new Vector2Int(1, 0));
    }

    private void FillRoomGridFromBranch(Vector2Int direction, Vector2Int optional)
    {
        currentAmountOfRooms = 0;
        int failedTries = 0;
        Vector2Int position = direction;
        while (currentAmountOfRooms < TotalAmountOfRooms / 4 || failedTries == 20)
        {
            switch (Random.Range(0, 5))
            {
                case 0: position += optional;
                    break;
                case 1: position -= optional;
                    break;
                default: position += direction;
                    break;
            }

            if (!roomGrid.ContainsKey(position))
            {
                roomGrid.Add(position, 1);
                currentAmountOfRooms += 1;
                failedTries = 0;
            }
            else
            {
                failedTries += 1;
            }
        }
    }
    
    
    private void CheckAdjecentStreets(Vector2Int pos)
    {
        if (roomGrid.ContainsKey(new Vector2Int(pos.x + 1, pos.y)))
        {
            if (!streetGrid.ContainsKey(new Vector2(pos.x + .5f, pos.y)))
            {
                streetGrid.Add(new Vector2(pos.x + .5f, pos.y), 1);
            }
        }
        if (roomGrid.ContainsKey(new Vector2Int(pos.x - 1, pos.y)))
        {
            if (!streetGrid.ContainsKey(new Vector2(pos.x - .5f, pos.y)))
            {
                streetGrid.Add(new Vector2(pos.x - .5f, pos.y), 1);
            }
        }
        if (roomGrid.ContainsKey(new Vector2Int(pos.x, pos.y + 1)))
        {
            if (!streetGrid.ContainsKey(new Vector2(pos.x, pos.y + .5f)))
            {
                streetGrid.Add(new Vector2(pos.x, pos.y + .5f), 1);
            }
        }
        if (roomGrid.ContainsKey(new Vector2Int(pos.x, pos.y - 1)))
        {
            if (!streetGrid.ContainsKey(new Vector2(pos.x, pos.y - .5f)))
            {
                streetGrid.Add(new Vector2(pos.x, pos.y - .5f), 1);
            }
        }
    }
}
