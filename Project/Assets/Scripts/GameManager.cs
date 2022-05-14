using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject player;

    public static bool GameIsPaused = false;
    public static bool GameHasEnded = false;

    public void DisableGameplay()
    {
        player.GetComponent<AnimationManager>().ChangeAnimationState(AnimationManager.PLAYER_IDLE);
        player.GetComponent<PlayerController>().enabled = false;
        player.GetComponent<PlayerCombat>().enabled = false;
        player.GetComponent<CapsuleCollider2D>().enabled = false;
    }

    public void EnableGameplay()
    {
        player.GetComponent<PlayerController>().enabled = true;
        player.GetComponent<PlayerCombat>().enabled = true;
        player.GetComponent<CapsuleCollider2D>().enabled = true;
    }
}
