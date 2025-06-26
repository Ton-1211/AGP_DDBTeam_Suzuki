using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTrailController : MonoBehaviour
{
    [SerializeField] GameObject trailObject;

    private void OnDestroy()
    {
        trailObject.transform.parent = null;
    }
}
