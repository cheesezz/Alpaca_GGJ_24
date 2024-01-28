using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public List<GameObject> players = new List<GameObject>();

    public GameObject spawn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject player in players)
        {
            PlayerLifeController life = player.GetComponent<PlayerLifeController>();
            if (!life.isAlive && life.deathTimer < life.deathTimerDuration)
            {
                life.deathTimer += Time.deltaTime;
            }
            else if (!life.isAlive && life.deathTimer >= life.deathTimerDuration)
            {
                life.isAlive = true;
                life.deathTimer = 0f;
                player.transform.position = spawn.transform.position;
                player.GetComponent<Rigidbody2D>().totalForce = Vector2.zero;
                player.SetActive(true);
            }
        }
    }
}
