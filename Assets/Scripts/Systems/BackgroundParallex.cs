using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class BackgroundParallex : MonoBehaviour
{
    Animator m_animator;

    public GameObject cameraLeftmostPos;
    public GameObject cameraRightmostPos;
    // Start is called before the first frame update
    void Start()
    {
        m_animator = GetComponent<Animator>();

    }

    float cameraProgressionThroughLevel = 0;
    // Update is called once per frame
    void FixedUpdate()
    {
        cameraProgressionThroughLevel =
            (transform.position.x - cameraLeftmostPos.transform.position.x) /
            (transform.position.x - cameraRightmostPos.transform.position.x);

        m_animator.SetFloat("ParallexValue", cameraProgressionThroughLevel);
        
    }


}
