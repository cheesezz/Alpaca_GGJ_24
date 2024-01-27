using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

public class InputRegistry : MonoBehaviour
{
    public enum InputType
    {
        Gamepad,
        Keyboard,
        TOTAL
    };

    public enum Player
    {
        Player_1,
        Player_2,
        Player_3,
        Player_4,
        TOTAL
    }

    Dictionary<Player, InputType> playerInputRegistry = new();
    Dictionary<Player, string> playerAttachedController = new();

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        
    }

    public void RegisterPlayerInput(Player player, InputType inputType)
    {
        if (playerInputRegistry.ContainsKey(player))
        {
            playerInputRegistry[player] = inputType;
            return;
        }
        playerInputRegistry.Add(player, inputType);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CheckControllerDisconnect()
    {
        // Check if disconnected controller is connected to a play
        foreach (var e in playerAttachedController)
        {
            if (connectedJoystics.Contains(e.Value))
            {
                // Notify of controller disconnect
            }
        }
    }
    public bool gameRunning = false;
    void CheckControllerConnect()
    {
        if (!gameRunning) return;
        // If game running, and controller disconnects, if controller connects again, check

    }

    List<string> connectedJoystics = new List<string>();

    IEnumerator CheckForControllers()
    {
        Debug.Log("Started checking for controllers...");
        while (true)
        {
            var controllers = Input.GetJoystickNames();

            if (controllers.Length == connectedJoystics.Count)
                yield return new WaitForSeconds(1);

            if (controllers.Length < connectedJoystics.Count)
            {
                Debug.LogWarning("Controller disconnected");
                CheckControllerDisconnect();
            }
            else
            {
                Debug.LogWarning("Controller connected!");
                CheckControllerConnect();
            }

            // Reset list
            connectedJoystics.Clear();

            foreach (var str in controllers)
            {
                connectedJoystics.Add(str);
            }

            yield return new WaitForEndOfFrame();
        }
    }

    private void Awake()
    {
        //StartCoroutine(CheckForControllers());
    }
}
