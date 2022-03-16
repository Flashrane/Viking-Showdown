using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public Animator animator;
    string currentState;

    public readonly string PLAYER_IDLE = "Player_Idle";
    public readonly string PLAYER_WALK = "Player_Walk";
    public readonly string PLAYER_DODGE_LEFT = "Player_Dodge_Left";
    public readonly string PLAYER_DODGE_RIGHT = "Player_Dodge_Right";
    public readonly string PLAYER_ATTACK = "Player_Attack";

    public void ChangeAnimationState(string newState)
    {
        if (currentState == newState)
            return;

        animator.Play(newState);
        currentState = newState;
    }
}
