
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.U2D.Animation;
using UnityEngine.XR;

public class LevelGenerator : MonoBehaviour
{

    [SerializeField]
    GameObject trap;

    [SerializeField]
    GameObject environment;

    [SerializeField]
    GameObject background;

    [SerializeField]
    GameObject detail;

    [SerializeField]
    GameObject bossRoom;

    [SerializeField]
    GameObject platformX;     //Vertical platform

    [SerializeField]
    GameObject platformY;     //Horizontal platform

    [SerializeField]
    GameObject platformTopDetail;

    [SerializeField]
    GameObject mapBottomWall;

    [SerializeField]
    GameObject mapWall;

    [SerializeField]
    Vector2 mapsize;          //Size of map

    [SerializeField]
    Vector2 chunksize;        //Size of single chunk

    [SerializeField]
    Grid grid;                //Grid to Place objects into

    [SerializeField]
    List<GameObject> trapList;

    [SerializeField]
    List<GameObject> enemyList;

    [SerializeField]
    List<GameObject> fireFlyList;

    [SerializeField]
    List<GameObject> vineList;

    

    [SerializeField]
    int seed;                 //Map generator seed

    

    int numberOfBiomes = 4;  // min in max ex

    ///Map Generator Settings 
    int platformSpaceX              = 4;                ///  Forced [Empty Height] Space  bettwen X platforms
    int platformSpaceY              = 5;                ///  Forced [Empty Width ] Space  bettwen Y platforms
    int numberOfTrapsPerChunk       = 25;               ///  Number of check for traps spawning PER CHUNK
    int numberOfEnvironmentPerChunk = 100;              ///  Number of check for Environment type spawning PER CHUNK
    int numberOfEnemiesPerChunk     = 10;
    int numberOfCritters = 10;
    /// 

    ///Game Loop
    void Start()
    {
        
        

        platformSpaceX *= (int)grid.cellSize.x;
        platformSpaceY *= (int)grid.cellSize.y;

        CheckForPreferences(); /// It will overide existing settings, if player changed them 
        Random.InitState(seed);

        GenerateMap();
    }

    /// Functions
    void GenerateMap()
    {

        /// Map Chunking /////////////////////
        float xPoint = mapsize.x / chunksize.x;
        float yPoint = mapsize.y / chunksize.y;
        ///////////////////////////////////////

        float xCounter = 0f;        /// Inside Counter X

        for (int x = 0;x < xPoint;x++)
        {
            float yCounter = 0f;    /// Inside Counter Y

            for (int y = 0; y < yPoint; y++)
            {
                GenerateChunk(new Vector2(xCounter, yCounter));
                yCounter += chunksize.y;
            }
            xCounter += chunksize.x;
        }

        /// Create and place boss room
        GenerateBoss((int)mapsize.x, 0);

        GenerateWalls();
    }
    void GenerateChunk(Vector2 startPoint)
    {
        ///Randomly assign biome to chunk
        int biome = Random.Range(1, numberOfBiomes);

        ///Generate group for storing platforms gameobjects
        GameObject chunk = new GameObject("Chunk_X:" + startPoint.x + "Y:" + startPoint.y);

        ///Place chunk on grid
        PlaceOnGrid(chunk);

        ///Inside Counters/////////////////////////
        float insideCounterY = 0f + startPoint.x;// 
        float insideCounterX = 0f + startPoint.y;//
        ///////////////////////////////////////////
        
            

        /// Platforms
        for (int i = 0; i < (chunksize.x / platformSpaceX); i++)
        {
            AddToChunk(chunk, GenerateX(startPoint.x, insideCounterX, biome));
            insideCounterX += platformSpaceX;
        }
        for (int i = 0; i < (chunksize.y/platformSpaceY); i++)
        {
            AddToChunk(chunk, GenerateY(insideCounterY, startPoint.y,biome));
            insideCounterY += platformSpaceY;
        }

        /// Traps and enemy
        for (int i = 0;i < numberOfTrapsPerChunk; i++)
        {
            AddToChunk(chunk, GenerateTrap(startPoint.x, startPoint.y,biome));
        }

        /// Enviroment, Graphic details
        for (int i = 0; i < numberOfEnvironmentPerChunk; i++)
        {
            AddToChunk(chunk, GenerateEnvironment(startPoint.x, startPoint.y,biome));
        }


        for (int i = 0; i < numberOfEnvironmentPerChunk; i++)
        {
            AddToChunk(chunk, GeneratePlatformDetails(startPoint.x, startPoint.y,biome));
        }

        for (int i = 0; i < numberOfEnvironmentPerChunk; i++)
        {
            AddToChunk(chunk, GeneratePlatformTopDetail(startPoint.x, startPoint.y, biome));
        }

        for (int i = 0; i < numberOfEnvironmentPerChunk; i++)
        {
            AddToChunk(chunk, GenerateVine(startPoint.x, startPoint.y, biome));
        }

        for (int i = 0; i < numberOfEnemiesPerChunk; i++)
        {
            AddToChunk(chunk, GenerateEnemy(startPoint.x, startPoint.y, biome));
        }

        for (int i = 0; i < numberOfCritters; i++)
        {
            AddToChunk(chunk, GenerateFireFly(startPoint.x, startPoint.y, biome));
        }

        

        /// Chunk Background
        AddToChunk(chunk, GenerateBackground(startPoint.x, startPoint.y, biome));
        
        
        
    }

    /// All GameObject Generator functions
    GameObject GenerateX(float x, float y, int biome)
    {

        GameObject platform = Object.Instantiate(platformX, RandomCoordsChunkX(x, y), Quaternion.identity);
        platform.GetComponent<SpriteRenderer>().sprite = platform.GetComponent<SpriteLibrary>().GetSprite(biome.ToString(), Random.Range(1,numberOfBiomes).ToString());

        return platform;

    }
    GameObject GenerateY(float x, float y, int biome)
    {
        GameObject platform = Object.Instantiate(platformY, RandomCoordsChunkY(x, y), Quaternion.identity);
        platform.GetComponent<SpriteRenderer>().sprite = platform.GetComponent<SpriteLibrary>().GetSprite(biome.ToString(), Random.Range(1, numberOfBiomes).ToString());

        return platform;
    }
    GameObject GenerateTrap(float x, float y, int biome)
    {
        int xCoords;
        int yCoords;

        xCoords = (int)Random.Range(x,x+chunksize.x);  
        yCoords = (int)Random.Range(y,y+chunksize.y);
        
        RaycastHit2D ray = Physics2D.Raycast(new Vector2(xCoords,yCoords),Vector2.down,2f);

        if (     ray.collider     != null 
            &&   ray.collider.tag == "Ground" 
            && ( ray.distance > (grid.cellSize.y/2 - 0.5f) && ray.distance < grid.cellSize.y))
        {
          return Instantiate(trapList[biome-1],new Vector3(xCoords,yCoords,0),Quaternion.identity); //biome -1, because list start from 0
        }

        return null;
    }

    GameObject GenerateEnemy(float x, float y, int biome)
    {
        int xCoords;
        int yCoords;

        xCoords = (int)Random.Range(x, x + chunksize.x);
        yCoords = (int)Random.Range(y, y + chunksize.y);

        RaycastHit2D ray = Physics2D.Raycast(new Vector2(xCoords, yCoords), Vector2.down, 2f);

        if (ray.collider != null
            && ray.collider.tag == "Ground"
            && (ray.distance > (grid.cellSize.y / 2 - 0.5f) && ray.distance < grid.cellSize.y))
        {
            return Instantiate(enemyList[biome - 1], new Vector3(xCoords, yCoords, 0), Quaternion.identity); //biome -1, because list start from 0
        }

        return null;
    }
    GameObject GenerateFireFly(float x, float y, int biome)
    {
        int xCoords;
        int yCoords;

        xCoords = (int)Random.Range(x, x + chunksize.x);
        yCoords = (int)Random.Range(y, y + chunksize.y);

        RaycastHit2D ray = Physics2D.Raycast(new Vector2(xCoords, yCoords), Vector2.down, 2f);

        if (ray.collider != null
            && ray.collider.tag == "Ground"
            && (ray.distance > (grid.cellSize.y / 2 - 0.5f) && ray.distance < grid.cellSize.y))
        {
            return Instantiate(fireFlyList[biome - 1], new Vector3(xCoords, yCoords, 0), Quaternion.identity); //biome -1, because list start from 0
        }

        return null;
    }
    GameObject GenerateBackground(float x,float y,int biome)
    {
        GameObject gameObject;
        gameObject = Instantiate(background, new Vector3(x + chunksize.x / 2 - grid.cellSize.x / 2, y + chunksize.y / 2 - grid.cellSize.y / 2, 3), Quaternion.identity);
        gameObject.transform.localScale = chunksize;
        gameObject.GetComponent<SpriteRenderer>().sprite = gameObject.GetComponent<SpriteLibrary>().GetSprite(biome.ToString(), 1.ToString());
        return gameObject;
    }
    GameObject GenerateEnvironment(float x, float y, int biome) 
    {
        GameObject gameObject;

        int xCoords;
        int yCoords;

        xCoords = (int)Random.Range(x, x + chunksize.x);
        yCoords = (int)Random.Range(y, y + chunksize.y);

        RaycastHit2D ray = Physics2D.Raycast(new Vector2(xCoords, yCoords), Vector2.down, 2f);

        if (ray.collider != null
            && ray.collider.tag == "Ground"
            && (ray.distance > (grid.cellSize.y / 2 - 0.5f) && ray.distance < grid.cellSize.y))
        {
            gameObject = Instantiate(environment, new Vector3(xCoords, yCoords, 1), Quaternion.identity);
            gameObject.GetComponent<SpriteRenderer>().sprite = gameObject.GetComponent<SpriteLibrary>().GetSprite(biome.ToString(), Random.Range(1, numberOfBiomes).ToString());
            return gameObject;
        }

        return null;
    }
    GameObject GeneratePlatformDetails(float x, float y, int biome)
    {
        GameObject gameObject;

        int xCoords;
        int yCoords;

        xCoords = (int)Random.Range(x, x + chunksize.x);
        yCoords = (int)Random.Range(y, y + chunksize.y);


        RaycastHit2D ray = Physics2D.Raycast(new Vector2(xCoords, yCoords), Vector2.down, 0.2f);

        if(ray.collider != null && ray.collider.tag == "Ground" && ray.distance == 0)
        {
            gameObject = Instantiate(detail, new Vector3(xCoords, yCoords, -1), Quaternion.identity);
            return gameObject; 
        }
        return null;
    }
    GameObject GeneratePlatformTopDetail(float x,float y,int biome)
    {

        GameObject gameObject;

        int xCoords;
        int yCoords;

        xCoords = (int)Random.Range(x, x + chunksize.x);
        yCoords = (int)Random.Range(y, y + chunksize.y);

        RaycastHit2D ray = Physics2D.Raycast(new Vector2(xCoords, yCoords),-Vector2.down, 2f);

        if (ray.collider != null
            && ray.collider.tag == "Ground"
            && (ray.distance > (grid.cellSize.y / 2 - 0.5f) && ray.distance < grid.cellSize.y))
        {
            gameObject = Instantiate(platformTopDetail, new Vector3(xCoords, yCoords, 0), Quaternion.identity);
            gameObject.GetComponent<SpriteRenderer>().sprite = gameObject.GetComponent<SpriteLibrary>().GetSprite(biome.ToString(),1.ToString());
            return gameObject;
        }
        return null;
    }
    GameObject GenerateVine(float x, float y, int biome)
    {
        GameObject gameObject;

        int xCoords;
        int yCoords;

        xCoords = (int)Random.Range(x, x + chunksize.x);
        yCoords = (int)Random.Range(y, y + chunksize.y);

        RaycastHit2D ray = Physics2D.Raycast(new Vector2(xCoords, yCoords), -Vector2.down, 2f);

        if (ray.collider != null
            && ray.collider.tag == "Ground"
            && (ray.distance > (grid.cellSize.y / 2 - 0.5f) && ray.distance < grid.cellSize.y))
        {
            gameObject = Instantiate(vineList[biome-1], new Vector3(xCoords, yCoords, 2), Quaternion.identity); 
            ///new Vector3(x,y,z)  (if z = 2 then behind player   //  (if z = 2 0 = then infront player)

            return gameObject;
        }
        return null;

    }
    void GenerateBoss(float x, float y)
    {
        Instantiate(bossRoom, new Vector3(x, y, 0), Quaternion.identity);
    }
    
    void GenerateWalls() 
    {
        GameObject bottomWall;
        bottomWall = Instantiate(mapBottomWall,new Vector3(mapsize.x/2,-1,0), Quaternion.identity);
        bottomWall.transform.localScale *= new Vector2(mapsize.x , 1);
        bottomWall.transform.localScale += new Vector3(1, 0, 0);

        GameObject leftWall;
        leftWall = Instantiate(mapWall, new Vector3(0, mapsize.y / 2, 0), Quaternion.identity);
        leftWall.transform.localScale *= new Vector2(1, mapsize.y);
        leftWall.transform.localScale += new Vector3(0, 1,0);

        GameObject topWall;
        topWall = Instantiate(mapWall, new Vector3(mapsize.x / 2, mapsize.y, 0), Quaternion.identity);
        topWall.transform.localScale *= new Vector2(mapsize.x , 1);
        topWall.transform.localScale += new Vector3(1, 0, 0);

        GameObject rightWall; //Created with boss room in mind
        rightWall = Instantiate(mapWall, new Vector3(mapsize.x, mapsize.y / 2, 0), Quaternion.identity);
        rightWall.transform.localScale *= new Vector2(1, mapsize.y);

        rightWall.transform.localScale -= new Vector3(0, 25, 0);
        rightWall.transform.position += new Vector3(0, 12);

    }

   

    ///Wrapers
    void AddToChunk(GameObject chunk,GameObject gameObject)
    {
        if (gameObject != null)
        {
            gameObject.transform.parent = chunk.transform;
        }
    }
    void PlaceOnGrid(GameObject obj)
    {
        Vector3 gridCell = new Vector3(obj.transform.position.x * grid.cellSize.x, obj.transform.position.y * grid.cellSize.y, 0); //cell size jakby zmienic wysokosc
        obj.transform.position = gridCell;

        obj.transform.parent = grid.transform;

    }

    Vector3 RandomCoordsChunkX(float x, float y)
    {
        x = Mathf.Clamp(x, 0, 100000);     //Prevent value of x,and y from going out of range
        y = Mathf.Clamp(y, 0, 100000);

        return new Vector3((int)Random.Range(x, chunksize.x + x), y, 0);
    }
    Vector3 RandomCoordsChunkY(float x, float y)
    {
        x = Mathf.Clamp(x, 0, 100000);    //Prevent value of x,and y from going out of range
        y = Mathf.Clamp(y, 0, 100000);

        return new Vector3(x, (int)Random.Range(y, chunksize.y + y), 0);
    }


    private void CheckForPreferences()
    {
        if (PlayerPrefs.HasKey("MapSize"))
        {
            switch(PlayerPrefs.GetInt("MapSize"))
            {
                case 0: //Small
                    mapsize = new Vector2(100,100);
                    Debug.Log(mapsize);
                    break;
                case 1: //Medium
                    mapsize = new Vector2(200, 200);
                    Debug.Log(mapsize);
                    break;                    
                case 2: //Big
                    mapsize = new Vector2(300, 300);
                    Debug.Log(mapsize);
                    break;
            }
        }
        if (PlayerPrefs.HasKey("MapSeed"))
        {
           seed = PlayerPrefs.GetInt("MapSeed");
        }

        /*
        if(PlayerPrefs.HasKey("Difficulty"))
        {
            ///Maybe change number of traps per chunk enemies?
        }
        */
    }
}
