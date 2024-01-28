using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public bool isTriggered = false;

    [SerializeField] private Transform closePos, openPos;

    [SerializeField] private GameObject go;

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        Vector2 dir = new Vector2();
        if (isTriggered)
        {
            dir = new Vector2(openPos.position.x - closePos.position.x, openPos.position.y - closePos.position.y);
            
        }
        else
        {
            dir = new Vector2(closePos.position.x - openPos.position.x, closePos.position.y - openPos.position.y);
        }

        go.GetComponent<Rigidbody2D>().AddForce(dir);
    }
}
