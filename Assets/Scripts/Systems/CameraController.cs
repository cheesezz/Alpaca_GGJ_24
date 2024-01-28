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

        StartCoroutine(DelayCheckForPlayers());
    }

    public GameObject cameraLeftmostPos;
    public GameObject cameraRightmostPos;

    IEnumerator DelayCheckForPlayers()
    {
        yield return new WaitForEndOfFrame();

        var e = FindObjectsOfType<PlayerEntity>();
        int index = 0;
        foreach ( PlayerEntity p in e )
        {
            switch(index)
            {
                case 0:
                    p1 = p.gameObject;
                    break;
                case 1:
                    p2 = p.gameObject;
                    break;
                case 2:
                    p3 = p.gameObject;
                    break;
                case 3:
                    p4 = p.gameObject;
                    break;

            }
            index++;
        }

        playerCount = index;
    }

    Vector3 newPosition = new Vector3(0,0,-1f);
    bool cameraPositionOnLeftWall = false;
    // Update is called once per frame
    void Update()
    {
        switch (playerCount)
        {
            case 1:
                newPosition = new Vector3(p1.transform.position.x, transform.position.y, transform.position.z);
                break;
            case 2:
                newPosition = new Vector3((p1.transform.position.x + p2.transform.position.x) / 2f,
                                        (p1.transform.position.y + p2.transform.position.y) / 2f, transform.position.z);

                break;
        }
        cameraPositionOnLeftWall = newPosition.x < cameraLeftmostPos.transform.position.x;
        if (cameraPositionOnLeftWall || newPosition.x > cameraRightmostPos.transform.position.x)
        {
            if (cameraPositionOnLeftWall)
            {
                newPosition.x = cameraLeftmostPos.transform.position.x;
            }
            else
                newPosition.x = cameraRightmostPos.transform.position.x;
        }    

        transform.position = newPosition;
    }
}
