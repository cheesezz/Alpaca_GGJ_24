using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slap : MonoBehaviour
{
    public float timeAlive = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timeAlive > 0f)
        {
            timeAlive -= Time.deltaTime;
        }
        else
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}
