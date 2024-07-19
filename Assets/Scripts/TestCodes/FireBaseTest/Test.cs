using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Test : MonoBehaviour
{
    public IObjectPool<GameObject> pool;
    public GameObject item;
    private void Start()
    {
        pool = new ObjectPool<GameObject>(null, null, null, null, false, 10, 10000);
    }
}
