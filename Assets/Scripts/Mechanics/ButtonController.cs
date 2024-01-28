using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    [SerializeField] ButtonController go;
    [SerializeField] DoorController door;
    [SerializeField] Transform untriggeredPos, triggeredPos, originalPos;
    [SerializeField] GameObject buttonObject;
    [SerializeField] bool isTriggered = false;

    void Awake()
    {
        door = go.door;
        untriggeredPos = go.untriggeredPos;
        triggeredPos = go.triggeredPos;
        originalPos = go.originalPos;
        buttonObject = go.buttonObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 f = new Vector2();
        Vector2 mechanismDir = new Vector2(buttonObject.transform.position.x - originalPos.position.x,
            buttonObject.transform.position.y - originalPos.position.y);
        mechanismDir.Normalize();
        if (mechanismDir.x != 0f) 
        {
            buttonObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-mechanismDir.x * .1f, 0f));
            Debug.Log("Button Resetting X");
        }

        if (mechanismDir.y != 0f) 
        {
            buttonObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, -mechanismDir.y * .1f));
            Debug.Log("Button Resetting Y");
        }

        door.isTriggered = go.isTriggered;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 9) go.isTriggered = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == 9) go.isTriggered = false;
    }
}
