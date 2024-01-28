using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeController : MonoBehaviour
{
    [SerializeField] private PipeController go;
    [SerializeField] private GameObject pos1, pos2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (this.gameObject == pos1)
        {
            if (other.gameObject.name == "BodyCollider" && 
                other.transform.GetComponentInParent<Rigidbody2D>().totalForce.x > pos2.transform.position.x - pos1.transform.position.x &&
                other.transform.GetComponentInParent<Rigidbody2D>().totalForce.y > pos2.transform.position.y - pos1.transform.position.y)
            {
                other.gameObject.transform.position = pos2.transform.position;
                Debug.Log("Using Pipe");
            }

        }
        else if (this.gameObject == pos2)
        {
            if (other.gameObject.name == "BodyCollider" && 
                other.transform.GetComponentInParent<Rigidbody2D>().totalForce.x > 
                pos1.transform.position.x - pos2.transform.position.x &&
                other.transform.GetComponentInParent<Rigidbody2D>().totalForce.y > 
                pos1.transform.position.y - pos2.transform.position.y)
            {
                other.gameObject.transform.position = pos1.transform.position;
                Debug.Log("Using Pipe");
            }
        }

    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (this.gameObject == pos1)
        {
            if (other.gameObject.name == "BodyCollider" && 
                other.transform.GetComponentInParent<Rigidbody2D>().totalForce.x > pos2.transform.position.x - pos1.transform.position.x &&
                other.transform.GetComponentInParent<Rigidbody2D>().totalForce.y > pos2.transform.position.y - pos1.transform.position.y)
            {
                other.gameObject.transform.position = pos2.transform.position;
                Debug.Log("Using Pipe");
            }

        }
        else if (this.gameObject == pos2)
        {
            if (other.gameObject.name == "BodyCollider" && 
                other.transform.GetComponentInParent<Rigidbody2D>().totalForce.x > 
                pos1.transform.position.x - pos2.transform.position.x &&
                other.transform.GetComponentInParent<Rigidbody2D>().totalForce.y > 
                pos1.transform.position.y - pos2.transform.position.y)
            {
                other.gameObject.transform.position = pos1.transform.position;
                Debug.Log("Using Pipe");
            }
        }

    }
}
