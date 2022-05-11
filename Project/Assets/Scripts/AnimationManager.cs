using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public Animator animator;
    string currentState;

    public static string PLAYER_IDLE = "Player_Idle";
    public static string PLAYER_WALK = "Player_Walk";
    public static string PLAYER_DODGE_LEFT = "Player_Dodge_Left";
    public static string PLAYER_DODGE_RIGHT = "Player_Dodge_Right";
    public static string PLAYER_ATTACK = "Player_Attack";

    public static string AXE_IDLE = "Axe_Idle";
    public static string AXE_ATTACK = "Axe_Attack";
    public static string AXE_DODGE_LEFT = "Axe_Dodge_Left";
    public static string AXE_DODGE_RIGHT = "Axe_Dodge_Right";

    public static string BOSS_IDLE = "Boss_Idle";
    public static string BOSS_WALK = "Boss_Walk";
    public static string BOSS_ATTACK = "Boss_Attack";

    public void ChangeAnimationState(string newState)
    {
        if (currentState == newState)
            return;

        animator.Play(newState);
        currentState = newState;
    }
}
