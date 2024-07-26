using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    public Transform hand;

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, hand.eulerAngles.y, transform.eulerAngles.z);
    }
}
