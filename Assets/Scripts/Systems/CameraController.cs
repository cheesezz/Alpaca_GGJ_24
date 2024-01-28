using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject p1, p2, p3, p4;
    private int playerCount = 2;
    // Start is called before the first frame update
    void Start()
    {
        LevelManager lvlman = FindObjectOfType<LevelManager>();
        p1 = lvlman.players[0];
        p2 = lvlman.players[1];
        p3 = lvlman.players[2];
        p4 = lvlman.players[3];

        StartCoroutine(DelayCheckForPlayers());
    }

    public GameObject cameraLeftmostPos;
    public GameObject cameraRightmostPos;

    IEnumerator DelayCheckForPlayers()
    {
        yield return new WaitForSeconds(0.1f);

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

    void CheckForPlayers()
    {
        var e = FindObjectsOfType<PlayerInput>();
        int index = 0;
        foreach (PlayerInput p in e)
        {
            switch (p.playerIndex)
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
        if (p1 == null)
        {
            CheckForPlayers();
            return;
        }
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
        // Clamp to 0
        if (newPosition.y < 0)
            newPosition.y = 0;

        transform.position = newPosition;
    }
}
