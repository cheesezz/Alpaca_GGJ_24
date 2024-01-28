using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slap : MonoBehaviour
{
    public float timeAlive = 0.5f;

    [Range(0f, 0.95f)]
    public float minTweenSize = 0.5f;

    float originalTimeAlive;
    Vector3 originalSize;
    // Start is called before the first frame update
    void Start()
    {
        originalSize = transform.localScale;
        originalTimeAlive = timeAlive;
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

        float currentTweenSize = minTweenSize + ((1.0f - minTweenSize) * Mathf.Lerp(1, 0, timeAlive / originalTimeAlive));
        Vector3 tweenScale = currentTweenSize * originalSize;

        transform.localScale = tweenScale;
    }
}
