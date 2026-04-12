using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovementState : MonoBehaviour
{
    public enum MoveState
        {
                Idle,
                Run,

                Jump,
                Fall
        }

        public MoveState CurrentMoveState {get; private set;}

    [SerializeField] private Animator animator;

    private const string idleAnim = "Idle";
    private const string runAnim = "Run";
    private const string jumpAnim = "Jump";
    private const string fallAnim = "Fall";
    public static Action<MoveState> OnPlayerMoveStateChanged;

    // Start is called before the first frame update
    public void SetMoveState(MoveState moveState)
{
    if (moveState == CurrentMoveState) return;

    CurrentMoveState = moveState;

    switch (moveState)
    {
        case MoveState.Idle:
            HandleIdle();
            break;

        case MoveState.Run:
            HandleRun();
            break;

        case MoveState.Jump:
            HandleJump();
            break;

        case MoveState.Fall:
            HandleFall();
            break;
        
        default:
                Debug.LogError("$Invalid movement state: {moveState}");
                break;
    }
    OnPlayerMoveStateChanged?.Invoke(moveState);
    CurrentMoveState = moveState;
        }


        private void HandleIdle()
        {
        animator.Play(idleAnim);
        }

        private void HandleRun()
        {
        animator.Play(runAnim);
        }

        private void HandleJump()
        {
        animator.Play(jumpAnim);
        }

        private void HandleFall()
        {
        animator.Play(fallAnim);
        }
}