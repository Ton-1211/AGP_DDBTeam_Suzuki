using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SR_Wall : MonoBehaviour
{

    public enum WallType 
    { 
    None,
    NomalWall,
    SturdyWall
    }
    public WallType wallType;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        isWallTypeChange();
    }

    void isWallTypeChange() 
    {
        if (wallType == WallType.None) 
        { 
        gameObject.SetActive(false);
        }
        if (wallType == WallType.NomalWall)
        {
            gameObject.SetActive(true);
        }
        if (wallType == WallType.SturdyWall)
        {
            gameObject.SetActive(true);
        }
    }
}
