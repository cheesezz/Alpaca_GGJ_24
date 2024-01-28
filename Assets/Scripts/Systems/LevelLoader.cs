using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Loader.LoadAdditiveScene(Loader.Scene.JoinRoom);
    }
}
