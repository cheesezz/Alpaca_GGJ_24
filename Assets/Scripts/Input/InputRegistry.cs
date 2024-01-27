using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;


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

    public List<GameObject> uiJoinList = new List<GameObject>();

    Dictionary<Player, InputType> playerInputRegistry = new();
    Dictionary<Player, int> playerAttachedController = new();

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        var playerInputManager = FindObjectOfType<PlayerInputManager>();
        int index = 0;

        Debug.Log("Detected " + InputSystem.devices.Count + " devices");
        foreach (var device in InputSystem.devices)
        {
            playerInputManager.JoinPlayer(index);
            index++;
        }
    }

    /*
    public void RegisterPlayerInput(Player player, InputType inputType)
    {
        if (playerInputRegistry.ContainsKey(player))
        {
            playerInputRegistry[player] = inputType;
            return;
        }
        playerInputRegistry.Add(player, inputType);
    }

    public void AssignDevicesToPlayers()
    {
        int maxPlayers = Math.Min(playerInputRegistry.Count, connectedDevices.Count);

        for (int index = 0; index < maxPlayers; index++)
        {
            playerAttachedController[(Player)index] = connectedDevices[index];
            Debug.Log("Player " + (index + 1) + " using controller " + connectedDevices[index]);
        }
    }
    */

    // Update is called once per frame
    void Update()
    {
        // Manual initilallise for testing
        if (Input.GetKeyDown(KeyCode.U))
        {
        }
    }

    void CheckControllerDisconnect(int ID)
    {
        // Check if disconnected controller is connected to a player
        
    }
    public bool gameRunning = false;
    void CheckControllerConnect(int ID)
    {
        if (!gameRunning) return;
        // If game running, and controller disconnects, if controller connects again, check

    }

    public List<int> connectedDevices = new();

    private void Awake()
    {
        InputSystem.onDeviceChange += CheckForControllerChange;
    }

    private void CheckForControllerChange(InputDevice device, InputDeviceChange change)
    {
        switch (change)
        {
            case InputDeviceChange.Added:
                Debug.Log("Added Controller " + device.deviceId);
                connectedDevices.Add(device.deviceId);
                break;

            case InputDeviceChange.Disconnected:
                //Debug.LogWarning("Controller Disconnected " + device.deviceId);
                if (connectedDevices.Contains(device.deviceId))
                    CheckControllerDisconnect(device.deviceId);

                break;

            case InputDeviceChange.Reconnected:
                //Debug.Log("Controller Reconnected " + device.deviceId);
                if (connectedDevices.Contains(device.deviceId))
                    CheckControllerConnect(device.deviceId);

                break;

            case InputDeviceChange.Removed:
                Debug.LogWarning("Controller Removed " + device.deviceId);
                connectedDevices.Remove(device.deviceId);
                break;

            default:
                break;
        }
        Debug.Log("concurrent controllers connected: " + connectedDevices.Count);
    }
}
