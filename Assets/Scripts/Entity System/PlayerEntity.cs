using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerEntity : EntityBase
{
    public TextMeshPro playerText;

    // Start is called before the first frame update
    protected override void Start()
    {
        var input = GetComponent<PlayerInput>();

        playerText.text = "P" + (input.playerIndex + 1);

        FindObjectOfType<LevelManager>().players.Add(gameObject);

        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
