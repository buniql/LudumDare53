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

    public Dictionary<Vector2Int, int> roomGrid = new Dictionary<Vector2Int, int>();
    private Dictionary<Vector2, int> streetGrid = new Dictionary<Vector2, int>();

    public int RoomPositionOffsetX;
    public int StreetPositionOffsetX;
    public int RoomPositionOffsetY;
    public int StreetPositionOffsetY;
    
    public int TotalAmountOfRooms;
    private int currentAmountOfRooms = 0;

    public MinimapUI MinimapUI;
    
    // Start is called before the first frame update
    void Start()
    {
        FillRoomGrid();

        List<Vector2Int> _roomIndexes = new List<Vector2Int>();
        
        Vector2Int bossRoomLocation = Vector2Int.zero;

        foreach (var room in roomGrid)
        {
            if(room.Value == 1 && Vector2.Distance(new Vector2Int(room.Key.x, room.Key.y), Vector2.zero) > Vector2.Distance(bossRoomLocation, bossRoomLocation)) bossRoomLocation = new Vector2Int(room.Key.x, room.Key.y);
        }
        
        roomGrid[bossRoomLocation] = 3;
        _roomIndexes.Remove(bossRoomLocation);
        
        Vector2Int lootRoomLocation = Vector2Int.zero;

        foreach (var room in roomGrid)
        {
            if(room.Value == 1 && Vector2.Distance(new Vector2Int(room.Key.x, room.Key.y), Vector2.zero) > Vector2.Distance(lootRoomLocation, lootRoomLocation)) lootRoomLocation = new Vector2Int(room.Key.x, room.Key.y);
        }
        
        roomGrid[lootRoomLocation] = 2;
        _roomIndexes = null;

        foreach (var room in roomGrid)
        {
            GameObject toSpawn = new GameObject();
            switch (room.Value)
            {
                case 0:
                    toSpawn = SpawnRoom;
                    break;
                case 1:
                    toSpawn = DefaultRooms[Random.Range(0, DefaultRooms.Count - 1)];
                    break;
                case 2:
                    toSpawn = ItemRoom[0];;
                    break;
                case 3:
                    toSpawn = BossRooms[0];;
                    break;
            }
            
            var spawnedRoom = Instantiate(toSpawn, new Vector3(room.Key.x * RoomPositionOffsetX, room.Key.y * RoomPositionOffsetY, 1), Quaternion.identity);
            CheckAdjecentStreets(new Vector2Int(room.Key.x, room.Key.y));
            spawnedRoom.transform.parent = transform;
            spawnedRoom.name = "Room["+room.Key.x+"|"+room.Key.y+"]";
        }
        
        MinimapUI.GenerateMinimap();

        foreach (var street in streetGrid)
        {
            var spawnedStreet = Instantiate(Street,
                new Vector3(street.Key.x * RoomPositionOffsetX + StreetPositionOffsetX - RoomPositionOffsetX * .5f,
                    street.Key.y * RoomPositionOffsetY + StreetPositionOffsetY - RoomPositionOffsetY * .5f, 1), Quaternion.identity);
            spawnedStreet.transform.parent = transform;
            spawnedStreet.name = "Street["+street.Key.x+"|"+street.Key.y+"]";
        }
    }

    private void FillRoomGrid()
    {
        roomGrid.Add(new Vector2Int(0, 0), 0);
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
