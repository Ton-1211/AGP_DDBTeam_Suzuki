using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScript : MonoBehaviour
{
    [SerializeField] GameObject redGrid;
    [SerializeField] GameObject blueGrid;
    [SerializeField] GameObject greenGrid;
    [SerializeField] GameObject yellowGrid;
    [Header("同じ色のグリッドがすべて落ちるまでの割合"), Range(0f, 1f), SerializeField] float allBreakRate = 0f;
    [Header("0は空気、1～4は色ごと"), SerializeField] string[] mapData;

    List<List<MapGrid>> map = new List<List<MapGrid>>();
    Transform gridParent;
    List<GameObject> redGrids = new List<GameObject>();
    List<GameObject> blueGrids = new List<GameObject>();
    List<GameObject> greenGrids = new List<GameObject>();
    List<GameObject> yellowGrids = new List<GameObject>();
    int redLimit;
    int blueLimit;
    int greenLimit;
    int yellowLimit;

    // Start is called before the first frame update
    void Start()
    {
        gridParent = GameObject.Find("Grids").transform;
        MapInstantiate();
        PlaceGrids();

        Debug.Log("redGrids" + redGrids.Count);
        Debug.Log("blueGrids" + blueGrids.Count);
        Debug.Log("greenGrids" + greenGrids.Count);
        Debug.Log("yellowGrids" + yellowGrids.Count);

        redLimit = (int)(redGrids.Count * allBreakRate);
        blueLimit = (int)(blueGrids.Count * allBreakRate);
        greenLimit = (int)(greenGrids.Count * allBreakRate);
        yellowLimit = (int)(yellowGrids.Count * allBreakRate);
    }

    public GameObject getGridParentObject() { return gridParent.gameObject; }

    public void BreakGrid(GameObject targetObject)
    {
        foreach (GameObject mapGrid in redGrids)
        {
            if (mapGrid == targetObject)
            {
                // リストから削除
                redGrids.Remove(mapGrid);
                // 指定されたオブジェクトを壊す
                BreakObject(targetObject);
                if (redGrids.Count < redLimit)
                {
                    BreakGridType(redGrids, MapGrid.State.RedFloor);// 同じ色をすべて壊す
                }
                return;
            }
        }
        foreach (GameObject mapGrid in blueGrids)
        {
            if (mapGrid == targetObject)
            {
                // リストから削除
                blueGrids.Remove(mapGrid);
                // 指定されたオブジェクトを壊す
                BreakObject(targetObject);
                if(blueGrids.Count < blueLimit)
                {
                    BreakGridType(blueGrids, MapGrid.State.BlueFloor);
                }
                return;
            }
        }
        foreach (GameObject mapGrid in greenGrids)
        {
            if (mapGrid == targetObject)
            {
                // リストから削除
                greenGrids.Remove(mapGrid);
                // 指定されたオブジェクトを壊す
                BreakObject(targetObject);
                if(greenGrids.Count < greenLimit)
                {
                    BreakGridType(greenGrids, MapGrid.State.GreenFloor);
                }
                return;
            }
        }
        foreach (GameObject mapGrid in yellowGrids)
        {
            if (mapGrid == targetObject)
            {
                // リストから削除
                yellowGrids.Remove(mapGrid);
                // 指定されたオブジェクトを壊す
                BreakObject(targetObject);
                if(yellowGrids.Count < yellowLimit)
                {
                    BreakGridType(yellowGrids, MapGrid.State.YellowFloor);
                }
                return;
            }
        }

    }

    private void BreakGridType(List<GameObject> targetObjects, MapGrid.State state)
    {
        switch (state)
        {
            case MapGrid.State.RedFloor:
                for(int i = 0; i < targetObjects.Count; i++)
                {
                    foreach (GameObject mapGrid in redGrids)
                    {
                        if (mapGrid == targetObjects[i])
                        {
                            // 指定されたオブジェクトを壊す
                            BreakObject(targetObjects[i]);
                            break;
                        }
                    }
                }
                redGrids.Clear();
                break;
            case MapGrid.State.BlueFloor:
                for (int i = 0; i < targetObjects.Count; i++)
                {
                    foreach (GameObject mapGrid in blueGrids)
                    {
                        if (mapGrid == targetObjects[i])
                        {
                            // 指定されたオブジェクトを壊す
                            BreakObject(targetObjects[i]);
                            break;
                        }
                    }
                }
                blueGrids.Clear();
                break;
            case MapGrid.State.GreenFloor:
                for (int i = 0; i < targetObjects.Count; i++)
                {
                    foreach (GameObject mapGrid in greenGrids)
                    {
                        if (mapGrid == targetObjects[i])
                        {
                            // 指定されたオブジェクトを壊す
                            BreakObject(targetObjects[i]);
                            break;
                        }
                    }
                }
                greenGrids.Clear();
                break;
            case MapGrid.State.YellowFloor:
                for (int i = 0; i < targetObjects.Count; i++)
                {
                    foreach (GameObject mapGrid in yellowGrids)
                    {
                        if (mapGrid == targetObjects[i])
                        {
                            // リストから削除
                            yellowGrids.Remove(mapGrid);
                            // 指定されたオブジェクトを壊す
                            BreakObject(targetObjects[i]);
                            break;
                        }
                    }
                }
                yellowGrids.Clear();
                break;
            default:
                break;
        }
    }

    private void BreakObject(GameObject gameObject)
    {
        Rigidbody rb;
        if (gameObject.TryGetComponent<Rigidbody>(out rb))
        {
            rb.isKinematic = false;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void MapInstantiate()
    {
        for (int i = 0; i < mapData.Length; i++)
        {
            map.Add(AssignmentStringToMapList(mapData[i], i));
        }
    }

    private void PlaceGrids()
    {
        GameObject obj;
        foreach (var mapRow in map)
        {
            foreach (MapGrid grid in mapRow)
            {
                switch (grid.GetState())
                {
                    case MapGrid.State.None:
                        break;
                    case MapGrid.State.RedFloor:
                        obj = Instantiate(redGrid, gridParent.position - new Vector3(-grid.GetX(), 0f, grid.GetY()), Quaternion.identity, gridParent);// マス作成
                        redGrids.Add(obj);
                        break;
                    case MapGrid.State.BlueFloor:
                        obj = Instantiate(blueGrid, gridParent.position - new Vector3(-grid.GetX(), 0f, grid.GetY()), Quaternion.identity, gridParent);
                        blueGrids.Add(obj);
                        break;
                    case MapGrid.State.GreenFloor:
                        obj = Instantiate(greenGrid, gridParent.position - new Vector3(-grid.GetX(), 0f, grid.GetY()), Quaternion.identity, gridParent);
                        greenGrids.Add(obj);
                        break;
                    case MapGrid.State.YellowFloor:
                        obj = Instantiate(yellowGrid, gridParent.position - new Vector3(-grid.GetX(), 0f, grid.GetY()), Quaternion.identity, gridParent);
                        yellowGrids.Add(obj);
                        break;
                    default:
                        break;
                }
            }
        }
    }

    private List<MapGrid> AssignmentStringToMapList(string str, int row)
    {
        List<MapGrid> grids = new List<MapGrid>();
        for (int i = 0; i < str.Length; i++)
        {
            MapGrid grid = new MapGrid(i + 1, row + 1, GetStateFromNum(str[i] - '0'));// 追加するグリッドの作成
            grids.Add(grid);
        }
        return grids;
    }

    private MapGrid.State GetStateFromNum(int num)
    {
        if (num < 0 || (MapGrid.State)num >= MapGrid.State.Max)// 指定したタイプの範囲外のとき
        {
            return MapGrid.State.None;
        }
        return (MapGrid.State)num;
    }
}
