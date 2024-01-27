using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject p1, p2, p3, p4;
    private int playerCount = 2;
    // Start is called before the first frame update
    void Start()
    {
        LevelManager lvlman = FindObjectOfType<LevelManager>();
        p1 = lvlman.p1;
        p2 = lvlman.p2;
        p3 = lvlman.p3;
        p4 = lvlman.p4;
    }

    // Update is called once per frame
    void Update()
    {
        switch (playerCount)
        {
            case 2:
                transform.position = new Vector3((p1.transform.position.x + p2.transform.position.x) / 2f,
                                        (p1.transform.position.y + p2.transform.position.y) / 2f, transform.position.z);

                break;
        }
    }
}
