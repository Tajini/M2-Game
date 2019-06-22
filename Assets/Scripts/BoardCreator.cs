using System;
using System.Collections;
using UnityEngine;

public class BoardCreator : MonoBehaviour
{
    // The type of tile that will be laid in a specific position.
    public enum TileType
    {
        Wall, Floor, EnnemySpawn,
    }
    public bool cfini;
    public bool gridUp = false;
    public int columns;
    public int rows;
    public int currentLevel = 1;
    public IntRange numEnnemy = new IntRange(30, 50);
    public IntRange numRooms = new IntRange(10, 20);
    public IntRange roomWidth = new IntRange(4, 9);
    public IntRange roomHeight = new IntRange(3, 10);
    public GameObject[] floorTiles;
    public GameObject[] floorTilesEnnemy;
    public GameObject[] wallTiles1;
    public GameObject[] wallTiles2;
    public GameObject[] wallTiles3;
    public GameObject[] wallTiles4;
    public GameObject[] outerWallTiles;
    public GameObject player;
    public GameObject[] PrefabsEnnemy;
    public GameObject[] PrefabsMiniBoss;
    public SoundManager soundManager;

    private TileType[][] tiles;                               // A jagged array of tile types representing the board, like a grid.
    private Room[] rooms;                                     // Toutes les chambres créer.
    private Corridor[] corridors;                             // Tableau des couloirs connecter au chambreAll the corridors that connect the rooms.
    private GameObject boardHolder;
    private GameObject[] wallTiles;
    private GameObject[] spawnTilesEnnemy;
    private int Ennemy;
    private IntRange corridorLength = new IntRange(5, 10);

    AStarGrid asg;

    public static BoardCreator instance = null;


    private void Start()
    {
        // Permet de créer un gameObject qui va récupérer toutes les tiles généré dans le niveau.

        boardHolder = new GameObject("BoardHolder");
        asg = new AStarGrid();
        switch (currentLevel)
        {
            case 1:
               // soundManager.MakeLevel1Sound();
                columns = 50;
                rows = 50;
                numEnnemy = new IntRange(5, 15);
                numRooms = new IntRange(3, 5);
                roomWidth = new IntRange(10, 10);
                roomHeight = new IntRange(10, 10);
                NewLevel();
                break;

            case 2:
              //  soundManager.MakeLevel1Sound();
                columns = 100;
                rows = 100;
                numEnnemy = new IntRange(15, 30);
                numRooms = new IntRange(7, 8);
                roomWidth = new IntRange(10, 10);
                roomHeight = new IntRange(10, 10);
                NewLevel();
                break;

            case 3:
               // soundManager.MakeLevel1Sound();
                columns = 500;
                rows = 500;
                numEnnemy = new IntRange(35, 55);
                numRooms = new IntRange(10, 12);
                roomWidth = new IntRange(5, 9);
                roomHeight = new IntRange(4, 8);
                NewLevel();
                break;

            case 4:
              //  soundManager.MakeLevel1Sound();
                columns = 700;
                rows = 700;
                numEnnemy = new IntRange(55, 65);
                numRooms = new IntRange(5, 10);
                roomWidth = new IntRange(6, 10);
                roomHeight = new IntRange(5, 9);
                NewLevel();
                break;

            case 5:
             //   soundManager.MakeLevel1Sound();
                columns = 800;
                rows = 800;
                numEnnemy = new IntRange(65, 80);
                numRooms = new IntRange(8, 12);
                roomWidth = new IntRange(9, 13);
                roomHeight = new IntRange(7, 10);
                NewLevel();
                break;

            case 6:
             //   soundManager.MakeLevel1Sound();
                columns = 1000;
                rows = 1000;
                numEnnemy = new IntRange(80, 90);
                numRooms = new IntRange(9, 11);
                roomWidth = new IntRange(8, 15);
                roomHeight = new IntRange(6, 10);
                NewLevel();
                break;

            case 7:
             //   soundManager.MakeLevel1Sound();
                columns = 1500;
                rows = 1500;
                numEnnemy = new IntRange(90, 100);
                numRooms = new IntRange(10, 13);
                roomWidth = new IntRange(9, 14);
                roomHeight = new IntRange(6, 9);
                NewLevel();
                break;
        }


    }

    private void Update()
    {
        //Debug.Log(gridUp);
        if (gridUp == true)
        {
            //Debug.Log(gridUp);
            SetupEnnemy();
            gridUp = false;
        }
    }
    public void NewLevel()
    {

        SetupTilesArray();

        CreateRoomsAndCorridors();

        SetTilesValuesForRooms();
        SetTilesValuesForCorridors();

        InstantiateTiles(currentLevel);
        InstantiateOuterWalls();
    }

    public void SetupEnnemy()
    {

        spawnTilesEnnemy = GameObject.FindGameObjectsWithTag("SpawnEnnemy");
        Ennemy = numEnnemy.Random;
        //Debug.Log(Ennemy);
        int miniBoss = UnityEngine.Random.Range(0, 3); // 1 chance sur 3 d'avoir un mini boss dans le niveau
        int indexMB = UnityEngine.Random.Range(0, PrefabsMiniBoss.Length);
        //Debug.Log(indexMB);
        int indexSpawn = UnityEngine.Random.Range(0, spawnTilesEnnemy.Length);
        //Debug.Log(miniBoss);

        if (miniBoss.Equals(0))
        {
            //Debug.Log(PrefabsMiniBoss[indexMB]);
            //Debug.Log(spawnTilesEnnemy[indexSpawn].gameObject.transform.position);
            //Debug.Log(spawnTilesEnnemy[indexSpawn].gameObject.transform.rotation);
            Instantiate(PrefabsMiniBoss[indexMB], spawnTilesEnnemy[indexSpawn].gameObject.transform.position, spawnTilesEnnemy[indexSpawn].gameObject.transform.rotation);

        }
        for (int i = 0; i < Ennemy; i++)
        {
            int numSpawn = UnityEngine.Random.Range(0, spawnTilesEnnemy.Length);
            int IndexEnnemy = UnityEngine.Random.Range(0, PrefabsEnnemy.Length);

            //Debug.Log(PrefabsEnnemy[IndexEnnemy]);
            //Debug.Log(spawnTilesEnnemy[numSpawn].gameObject.transform.position);
            //Debug.Log(spawnTilesEnnemy[numSpawn].gameObject.transform.rotation);
            //Instancie le prefabs d'un ennemi aléatoirement dans les rooms
            Instantiate(PrefabsEnnemy[IndexEnnemy], spawnTilesEnnemy[numSpawn].gameObject.transform.position, spawnTilesEnnemy[numSpawn].gameObject.transform.rotation);


        }
    }

    void SetupTilesArray()
    {
        // Set the tiles jagged array to the correct width.
        tiles = new TileType[columns][];

        // Go through all the tile arrays...
        for (int i = 0; i < tiles.Length; i++)
        {
            // ... and set each tile array is the correct height.
            tiles[i] = new TileType[rows];
        }
    }


    void CreateRoomsAndCorridors()
    {
        // Create the rooms array with a random size.
        rooms = new Room[numRooms.Random];

        // There should be one less corridor than there is rooms.
        corridors = new Corridor[rooms.Length - 1];

        // Create the first room and corridor.
        rooms[0] = new Room();
        corridors[0] = new Corridor();

        // Setup the first room, there is no previous corridor so we do not use one.
        rooms[0].SetupRoom(roomWidth, roomHeight, columns, rows);

        // Setup the first corridor using the first room.
        corridors[0].SetupCorridor(rooms[0], corridorLength, roomWidth, roomHeight, columns, rows, true);

        for (int i = 1; i < rooms.Length; i++)
        {
            // Create a room.
            rooms[i] = new Room();

            // Setup the room based on the previous corridor.
            rooms[i].SetupRoom(roomWidth, roomHeight, columns, rows, corridors[i - 1]);

            // If we haven't reached the end of the corridors array...
            if (i < corridors.Length)
            {
                // ... create a corridor.
                corridors[i] = new Corridor();

                // Setup the corridor based on the room that was just created.
                corridors[i].SetupCorridor(rooms[i], corridorLength, roomWidth, roomHeight, columns, rows, false);
            }

            // Instancie le héros
            if (cfini != true)
            {
                Vector3 playerPos = new Vector3(rooms[0].xPos, rooms[0].yPos, 0);
                GameObject heros = Instantiate(player, playerPos, Quaternion.identity);
                heros.name = "Heros";
                cfini = true;
            }
        }

    }


    void SetTilesValuesForRooms()
    {
        // Go through all the rooms...s
        for (int i = 0; i < rooms.Length; i++)
        {

            Room currentRoom = rooms[i];
            // ... and for each room go through it's width.
            for (int j = 0; j < currentRoom.roomWidth; j++)
            {

                int xCoord = currentRoom.xPos + j;

                // For each horizontal tile, go up vertically through the room's height.
                for (int k = 0; k < currentRoom.roomHeight; k++)
                {
                    int yCoord = currentRoom.yPos + k;
                    // The coordinates in the jagged array are based on the room's position and it's width and height.
                    tiles[xCoord][yCoord] = TileType.EnnemySpawn;
                }
            }
        }
    }


    void SetTilesValuesForCorridors()
    {

        // Go through every corridor...
        for (int i = 0; i < corridors.Length; i++)
        {

            Corridor currentCorridor = corridors[i];

            // and go through it's length.
            for (int j = 0; j < currentCorridor.corridorLength; j++)
            {

                // Start the coordinates at the start of the corridor.
                int xCoord = currentCorridor.startXPos;
                int yCoord = currentCorridor.startYPos;

                // Depending on the direction, add or subtract from the appropriate
                // coordinate based on how far through the length the loop is.
                switch (currentCorridor.direction)
                {

                    case Direction.North:
                        yCoord += j;
                        break;
                    case Direction.East:
                        xCoord += j;
                        break;
                    case Direction.South:
                        yCoord -= j;
                        break;
                    case Direction.West:
                        xCoord -= j;
                        break;
                }

                // Set the tile at these coordinates to Floor.
                tiles[xCoord][yCoord] = TileType.Floor;
            }
        }
    }



    void InstantiateTiles(int Level)
    {

        switch (Level)
        {
            case 1:
                wallTiles = wallTiles1;
                break;
            case 2:
                wallTiles = wallTiles2;
                break;

            case 3:
                wallTiles = wallTiles3;
                break;

            case 4:
                wallTiles = wallTiles4;
                break;

            case 5:
                wallTiles = wallTiles1;
                break;
            case 6:
                wallTiles = wallTiles2;
                break;

            case 7:
                wallTiles = wallTiles3;
                break;
        }

        // Go through all the tiles in the jagged array...
        for (int i = 0; i < tiles.Length; i++)
        {

            for (int j = 0; j < tiles[i].Length; j++)
            {

                // ... and instantiate a floor tile for it.
                InstantiateFromArray(floorTiles, i, j);

                // If the tile type is Wall...
                if (tiles[i][j] == TileType.Wall)
                {

                    // ... instantiate a wall over the top.
                    InstantiateFromArray(wallTiles, i, j);
                }

                if (tiles[i][j] == TileType.EnnemySpawn)
                {
                    InstantiateFromArray(floorTilesEnnemy, i, j);
                }
            };
        }

    }


    void InstantiateOuterWalls()
    {

        // The outer walls are one unit left, right, up and down from the board.
        float leftEdgeX = -1f;
        float rightEdgeX = columns + 0f;
        float bottomEdgeY = -1f;
        float topEdgeY = rows + 0f;

        // Instantiate both vertical walls (one on each side).
        InstantiateVerticalOuterWall(leftEdgeX, bottomEdgeY, topEdgeY);
        InstantiateVerticalOuterWall(rightEdgeX, bottomEdgeY, topEdgeY);

        // Instantiate both horizontal walls, these are one in left and right from the outer walls.
        InstantiateHorizontalOuterWall(leftEdgeX + 1f, rightEdgeX - 1f, bottomEdgeY);
        InstantiateHorizontalOuterWall(leftEdgeX + 1f, rightEdgeX - 1f, topEdgeY);
    }


    void InstantiateVerticalOuterWall(float xCoord, float startingY, float endingY)
    {

        // Start the loop at the starting value for Y.
        float currentY = startingY;

        // While the value for Y is less than the end value...
        while (currentY <= endingY)
        {

            // ... instantiate an outer wall tile at the x coordinate and the current y coordinate.
            InstantiateFromArray(outerWallTiles, xCoord, currentY);

            currentY++;
        }
    }


    void InstantiateHorizontalOuterWall(float startingX, float endingX, float yCoord)
    {

        // Start the loop at the starting value for X.
        float currentX = startingX;

        // While the value for X is less than the end value...
        while (currentX <= endingX)
        {

            // ... instantiate an outer wall tile at the y coordinate and the current x coordinate.
            InstantiateFromArray(outerWallTiles, currentX, yCoord);

            currentX++;
        }
    }


    void InstantiateFromArray(GameObject[] prefabs, float xCoord, float yCoord)
    {

        // Create a random index for the array.
        int randomIndex = UnityEngine.Random.Range(0, prefabs.Length);

        // The position to be instantiated at is based on the coordinates.
        Vector3 position = new Vector3(xCoord, yCoord, 0f);

        // Create an instance of the prefab from the random index of the array.
        GameObject tileInstance = Instantiate(prefabs[randomIndex], position, Quaternion.identity) as GameObject;

        // Set the tile's parent to the board holder.
        tileInstance.transform.parent = boardHolder.transform;
    }
}