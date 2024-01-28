using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLifeController : MonoBehaviour
{
    [SerializeField] public bool isAlive = true;

    public float   deathTimerDuration = 5f,
                    deathTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive && deathTimer < deathTimerDuration)
        {
            deathTimer += Time.deltaTime;
        }
        else if (!isAlive && deathTimer >= deathTimerDuration)
        {
            isAlive = true;
            deathTimer = 0f;
        }

        if (!isAlive) gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Death") isAlive = false;
    }
}
