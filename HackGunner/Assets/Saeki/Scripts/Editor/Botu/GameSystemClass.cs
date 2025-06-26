using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using static GameSystemClass;
using static UnityEngine.EventSystems.EventTrigger;

public class GameSystemClass : MonoBehaviour
{
    public Info info;
    public class Info
    {
        public Ypos[] ypos;
        public class Ypos
        {
            public Xpos[] xpos;
            public class Xpos
            {
                public Map_State state;
                public GameObject wallobj;
            }
        }
    }

    [SerializeField]
    MapData data;
    [SerializeField]
    GameObject nomal, sturdy;
    const int BOARD_MAX = 80;
    public int index = 0;
    int wallCount = 0;
    private void Start()
    {
        info = new Info();
        info.ypos = new Info.Ypos[BOARD_MAX];
        wallCount = data.preset[index].destValue;
        for (int y = 0; y < BOARD_MAX; y++)
        {
            info.ypos[y] = new Info.Ypos();
            info.ypos[y].xpos = new Info.Ypos.Xpos[BOARD_MAX];

            for (int x = 0; x < BOARD_MAX; x++)
            {              
                Vector3 vector3 = new Vector3(x,0,y);
                info.ypos[y].xpos[x] = new Info.Ypos.Xpos();

                info.ypos[y].xpos[x].state = data.GetState(index, y, x);
                switch (info.ypos[y].xpos[x].state)
                {
                    case Map_State.Destructible:
                        info.ypos[y].xpos[x].wallobj = Instantiate(nomal, vector3, Quaternion.identity);
                        break;

                    case Map_State.Indestructible:
                        info.ypos[y].xpos[x].wallobj = Instantiate(sturdy, vector3, Quaternion.identity);
                        break;

                    default:
                        info.ypos[y].xpos[x].wallobj = null;
                        break;
                }
            }
        }
    }
    public void ChengeObject(int y,int x)
    {
        info.ypos[y].xpos[x].state = Map_State.None;
        info.ypos[y].xpos[x].wallobj.SetActive(false);
        wallCount--;
        SarchObject(y, x);
    }

    void SarchObject(int Y, int X)
    {
        bool[] visit = new bool [BOARD_MAX * BOARD_MAX];//y * BOARD_MAX + x;
        bool startCheck = true;
        for (int i = 0; i < visit.Length; i++)
            visit[i] = false;

        Vector2Int[] posLIst = new Vector2Int[BOARD_MAX * BOARD_MAX];
        //Queue<Vector2Int> q = new Queue<Vector2Int>();
        //q.Enqueue(new Vector2Int(X,Y));
        posLIst[0] = new Vector2Int(X, Y);
        int listCount = 1;
        //while (q.Count <= qCount)
        for (int i = 0; i < listCount && listCount < BOARD_MAX * BOARD_MAX; i++)
        {
            Vector2Int dequeue = posLIst[i];

            if (startCheck && listCount == 2)
            {
                startCheck = false;
            }
            for (int y = -1; y <= 1; y++) 
            {
                if (startCheck && listCount == 2)
                    break;
                   
                for (int x = -1; x <= 1; x++)
                {
                    if (startCheck && listCount == 2)
                        break;
                    if (x == 0 && y == 0) continue;
                    Vector2Int enter = new Vector2Int(dequeue.x + x, dequeue.y + y);
                    if (enter.x < 0 || enter.y < 0 || enter.x >= BOARD_MAX || enter.y >= BOARD_MAX) continue;
                    
                    if (visit[enter.y * BOARD_MAX + enter.x])
                        continue;
                    else
                    {
                        visit[enter.y * BOARD_MAX + enter.x] = true;
                        if (info.ypos[enter.y].xpos[enter.x].state != Map_State.None)
                        {
                            posLIst[listCount] = new Vector2Int(enter.x, enter.y);
                            listCount++;
                        }
                    }
                }
            }
                
        }
        if(wallCount / 2 < listCount)
        {
            wallCount -= wallCount - listCount;

            for (int y = 0; y < BOARD_MAX; y++)
                for (int x = 0; x < BOARD_MAX; x++)
                    visit[y * BOARD_MAX + x] = false;

            for (int i = 0; i < listCount; i++)
                visit[posLIst[i].y * BOARD_MAX + posLIst[i].x] = true;

            for (int y = 0; y < BOARD_MAX; y++)
                for (int x = 0; x < BOARD_MAX; x++)
                   if(!visit[y * BOARD_MAX + x])
                    {
                        info.ypos[y].xpos[x].wallobj.SetActive(false);
                        info.ypos[y].xpos[x].state = Map_State.None;
                    }
        }
        else
        {
            wallCount -= listCount;
            for (int i = 0; i < listCount; i++)
            {
                info.ypos[posLIst[i].y].xpos[posLIst[i].x].wallobj.SetActive(false);
                info.ypos[posLIst[i].y].xpos[posLIst[i].x].state = Map_State.None;
            }      
        }
    }
}
