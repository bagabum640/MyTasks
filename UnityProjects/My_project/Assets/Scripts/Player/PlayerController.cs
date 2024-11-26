using System;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement),
                  typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    private PlayerMovement _movement;
    private PlayerInput _input;

    private void Awake()
    {
        _input = GetComponent<PlayerInput>();
        _movement = GetComponent<PlayerMovement>();
    }

    private void FixedUpdate()
    {
        _movement.Move(_input.Direction);
    }
}