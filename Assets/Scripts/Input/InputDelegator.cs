using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputDelegator : MonoBehaviour
{
    Dictionary<short, PlayerMovementController> availableMovementControllers;
    // Start is called before the first frame update
    void Start()
    {
        var x = FindObjectsOfType<PlayerMovementController>();
        
        // get all controllers
        foreach (var controller in x)
        {
            if (!availableMovementControllers.ContainsKey(controller.playerControllerID))
            {
                availableMovementControllers.Add(controller.playerControllerID, controller);
            }
        }

        // Check how many controllers is there

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
