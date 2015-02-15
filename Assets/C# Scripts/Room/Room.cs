using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Room : MonoBehaviour {
    public int sizeX, sizeY;
    public RoomCell cellPrefab;
    public RoomWall wallPrefab;
    public Door doorPrefab;
    public RoomObstacle obstaclePrefab;
    public float obstacleChance;

    private RoomCell[,] cells;
    private List<RoomWall> walls = new List<RoomWall>();
    private List<Door> doors = new List<Door>();
    private List<RoomObstacle> obstacles = new List<RoomObstacle>();

    public void GenerateNextRoom() {
        foreach (RoomObstacle o in obstacles)
        {
            Destroy(o.gameObject);
        }
        obstacles.Clear();
        Generate();
    }

    public void Generate() {
        cells = new RoomCell[sizeX, sizeY];
        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                CreateCell(x, y);
                int doorPos = shouldSpawnDoor(x, y);
                if(doorPos > -1)
                {
                    CreateDoor(x, y, doorPos);
                }
                else if(x == 0 || x == sizeX - 1 || y == 0 || y == sizeY - 1)
                {
                    CreateWall(x, y);
                }
                else if (shouldSpawnObstacle(x, y))
                {
                    SpawnObstacle(x, y);
                }
            }
        }
    }

    private void CreateCell(int x, int y) {
        RoomCell newCell = Instantiate(cellPrefab) as RoomCell;
        cells[x, y] = newCell;
        newCell.name = "RoomCell " + x + ", " + y;
        newCell.transform.parent = transform;
        newCell.transform.localPosition = new Vector3(x - sizeX * 0.5f, y - sizeY * 0.5f, 0);
    }

    private void CreateWall(int x, int y) {
        RoomWall newWall = Instantiate(wallPrefab) as RoomWall;
        walls.Add(newWall);
        newWall.name = "Room Wall " + x + ", " + y;
        newWall.transform.parent = transform;
        newWall.transform.localPosition = new Vector3(x - sizeX * 0.5f, y - sizeY * 0.5f, 0);
    }

    private void CreateDoor(int x, int y, int doorPos) {
        Door newDoor = Instantiate(doorPrefab) as Door;
        newDoor.setDoorPosition(doorPos);
        doors.Add(newDoor);
        newDoor.name = "New Door " + x + ", " + y;
        newDoor.transform.parent = transform;
        newDoor.transform.localPosition = new Vector3(x - sizeX * 0.5f, y - sizeY * 0.5f, 0);
    }


    private void SpawnObstacle(int x, int y) {
        RoomObstacle newObst = Instantiate(obstaclePrefab) as RoomObstacle;
        obstacles.Add(newObst);
        newObst.name = "Obstacle " + x + ", " + y;
        newObst.transform.parent = transform;
        newObst.transform.localPosition = new Vector3(x - sizeX * 0.5f, y - sizeY * 0.5f, 0);
    }

    private bool shouldSpawnObstacle(int x, int y) {
        bool xBounds = x < sizeX - 2 && x > 1;
        bool yBounds = y < sizeY - 2 && y > 1;
        bool chance = UnityEngine.Random.Range(0f, 100f) < obstacleChance;
        return xBounds && yBounds && chance;
    }

    private int shouldSpawnDoor(int x, int y) {
        if(x == (sizeX / 2) && y == sizeY - 1)
        {
            return DoorPositions.NORTH;
        }
        else if(x == (sizeX / 2) && y == 0)
        {
            return DoorPositions.SOUTH;
        }
        else if(x == sizeX - 1 && y == (sizeY / 2))
        {
            return DoorPositions.EAST;
        }
        else if(x == 0 && y == (sizeY / 2))
        {
            return DoorPositions.WEST;
        }
        else
        {
            return -1;
        }
    }

    public void SpawnCharacters(int direction, Character c) {
        float startX, startY;
        if (direction == DoorPositions.NORTH)
        {
            startX = 0;
            startY = (sizeY / 2) - Math.Min((sizeX * .15f), 3);
            c.setCharacterPosition(new Vector3((float)startX, (float)startY, 0f));
        }
        else if (direction == DoorPositions.EAST)
        {
            startX = (sizeX / 2) - Math.Min((sizeX  * .15f), 3);
            startY = 0;
            Debug.Log("X is " + startX);
            c.setCharacterPosition(new Vector3((float)startX, (float)startY, 0f));
        }
        else if (direction == DoorPositions.SOUTH)
        {
            startX = 0;
            startY = -(sizeY / 2) + Math.Min((sizeX * .15f), 3);
            c.setCharacterPosition(new Vector3((float)startX, (float)startY, 0f));
        }
        else if (direction == DoorPositions.WEST)
        {
            startY = 0;
            startX = -(sizeX / 2) + Math.Min((sizeX * .15f), 3);
            c.setCharacterPosition(new Vector3((float)startX, (float)startY, 0f));
        }
        else
        {
            Debug.Log("Invalid spawn characters direction " + direction);
        }

        Debug.Log("Character Position: " + c.getCharacterPosition());
    }
}
