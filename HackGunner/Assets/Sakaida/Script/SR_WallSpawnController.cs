using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SR_WallSpawnController : MonoBehaviour
{

    [SerializeField] GameObject WallObj;
    [SerializeField] GameObject SturdyWallObj;

    /*
    int[,] map = new int[,]
    {
    {0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0 },
    {0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0 },
    {0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0 },
    {1,1,1,1,1,2,2,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
    {1,1,1,1,1,2,2,2,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
    {1,1,1,1,1,2,2,2,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
    {0,1,1,1,1,2,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
    {0,1,2,2,1,2,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
    {0,1,1,1,1,2,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
    {1,1,1,1,1,2,2,1,1,1,2,2,1,1,1,1,1,1,1,1,1,1,1,1,1 },
    {1,1,1,1,1,2,2,1,1,1,2,2,2,1,1,1,1,1,1,1,1,1,1,1,1 },
    {1,1,1,1,1,2,2,1,1,1,1,2,2,1,1,1,1,1,1,1,1,1,1,1,0 },
    {1,1,1,1,1,2,2,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0 },
    {1,1,1,1,1,2,2,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0 },
    {1,1,1,1,1,2,2,2,2,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0 },
    {1,1,1,1,1,2,2,2,2,2,2,1,1,1,1,1,1,1,1,1,1,1,1,0,0 },
    {0,1,1,1,1,1,1,2,2,2,2,1,1,1,1,1,1,1,1,1,1,1,1,0,0 },
    {0,1,2,2,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0 },
    {0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0 },
    {0,0,1,1,1,1,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,0,0,0,0 },

    };*/
    int[,] map = new int[,]
   {
    {1,1,1,1,1,1,1,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0 },
    {1,1,1,1,1,1,1,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0 },
    {1,1,1,1,1,1,1,0,0,1,1,0,0,0,0,0,0,0,1,1,0,0,1,1,0 },
    {0,0,0,0,0,1,1,0,0,1,1,0,0,0,0,0,0,0,1,1,0,0,1,1,1 },
    {0,0,0,0,0,1,1,0,0,1,1,1,1,1,1,1,1,1,1,1,0,0,1,1,1 },
    {0,0,0,0,0,1,1,0,0,1,1,0,0,0,0,0,0,0,1,1,0,0,1,1,1 },
    {0,1,1,1,1,1,1,0,0,1,1,0,0,0,0,0,0,0,1,1,0,0,1,1,1 },
    {0,1,1,1,1,1,1,0,0,1,1,0,0,0,0,0,0,0,1,1,0,0,1,1,1 },
    {0,1,1,1,1,1,1,0,0,1,1,0,0,0,0,0,0,0,1,1,0,0,1,1,1 },
    {0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
    {0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
    {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0 },
    {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0 },
    {1,1,1,1,1,2,2,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0 },
    {1,1,1,1,1,2,2,2,2,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0 },
    {1,1,1,1,1,2,2,2,2,2,2,1,1,1,1,1,1,1,1,1,1,1,1,0,0 },
    {1,1,1,1,1,1,1,2,2,2,2,1,1,1,1,1,1,1,1,1,1,1,1,0,0 },
    {1,1,2,2,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0 },
    {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0 },
    {1,0,1,1,1,1,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,0,0,0,0 },

   };

    // Start is called before the first frame update
    void Start()
    {
        isGenerateMap();
    }

    // Update is called once per frame
    void Update()
    {
        //isBlob();
    }

    void isGenerateMap() 
    {

        for (int x = 0; x < map.GetLength(0); x++) 
        {
            for (int y = 0; y < map.GetLength(1); y++) 
            {
                
                if (map[x, y] == 1) 
                {//壊れる壁を指定された座標へ設置
                    CloneWall(WallObj,y,x);
                }
                if (map[x, y] == 2)
                {//壊れない壁を指定された座標へ設置
                    CloneWall(SturdyWallObj, y, x);
                    
                }
            }
        }

    }

    void CloneWall(GameObject wallname,int x,int y) 
    { //送られたゲームオブジェクトをクローンする
        Vector3 WallPos = new Vector3(x, y, 0);
    GameObject CL_Wall = GameObject.Instantiate(wallname, WallPos,Quaternion.identity);
    }
    void isBlob() 
    {
        isProcessBlobs(map);
        isPrintMap(map);

        
    }

     public int FloodFill(int[,] map, int x, int y, int targetValue, int replacementValue) 
    {
        if (x < 0 || x >= map.GetLength(0) || y < 0 || y >= map.GetLength(1)) 
        { 
        return 0;
        }
        if (map[x, y] != targetValue) 
        {
            return 0;
        }

        map[x,y] = replacementValue;

        int size = 1;
        size += FloodFill(map, x + 1, y, targetValue, replacementValue);
        size += FloodFill(map, x - 1, y, targetValue, replacementValue);
        size += FloodFill(map, x, y + 1, targetValue, replacementValue);
        size += FloodFill(map, x, y - 1, targetValue, replacementValue);

        return size;
    }

    void isProcessBlobs(int[,] map) 
    { 
    Dictionary<int,int>blobSizes = new Dictionary<int, int>();
        int blobId = 1;

        for (int i = 0; i < map.GetLength(0); i++) 
        {
            for (int j = 0; j < map.GetLength(1); j++) 
            {
                int size = FloodFill(map, i, j, map[i, j], blobId);
                blobSizes[blobId] = size;
                blobId++;
            }
        }
        // 最小の塊を見つける
        int minBlobId = -1;
        int minSize = Int32.MaxValue;
        foreach (var blobSize in blobSizes)
        {
            if (blobSize.Value < minSize)
            {
                minSize = blobSize.Value;
                minBlobId = blobSize.Key;
            }
        }

        // 最小の塊を0で置き換える
        ReplaceBlobWithZero(map, minBlobId);
    }
    void ReplaceBlobWithZero(int[,] map, int blobId)
    {
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                if (map[i, j] == blobId)
                {
                    map[i, j] = 0;
                }
            }
        }
    }
    void isPrintMap(int[,] map)
    {
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                Console.Write(map[i, j] + " ");
            }
            Console.WriteLine();
        }
    }
}
