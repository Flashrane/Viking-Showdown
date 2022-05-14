using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    new AudioManager audio;
    Rigidbody2D rbPlayer;
    PlayerCombat combatInfo;
    public AnimationManager playerAnimator;
    public AnimationManager axeAnimator;
    public StaminaBar staminaBar;

    [SerializeField] float movementSpeed = 330.0f;
    public float slowingStrength = 1f; // 1 is neutral in multiplications
    [SerializeField] float dodgeForce = 3f;
    float dodgeCoolDown = 0.3f;
    Vector2 movement;
    Rigidbody2D rbEnemy;

    public bool isDodging = false;
    bool isRunwayPadTriggered = false;
    public bool hasReachedShore = false;

    [SerializeField] Objective objective;
    [SerializeField] SpriteRenderer runwayPad;
    [SerializeField] Transform boat;
    [SerializeField] Boat boatScript;
    [SerializeField] PolygonCollider2D seaCollider;

    [SerializeField] Collider2D bossFightCollider;

    void Awake()
    {
        audio = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        rbPlayer = GetComponent<Rigidbody2D>();
        combatInfo = GetComponent<PlayerCombat>();

        rbEnemy = null;

        playerAnimator.ChangeAnimationState(AnimationManager.PLAYER_IDLE);
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement = movement.normalized;

        if (movement.x != 0 || movement.y != 0)
        {
            if (!audio.IsPlaying("RunningOnGrass"))
                audio.Play("RunningOnGrass");
            if (!combatInfo.isAttacking && !isDodging)
            {
                playerAnimator.ChangeAnimationState(AnimationManager.PLAYER_WALK);
            }

            if (movement.x == 0 && movement.y > 0)
                rbPlayer.rotation = 0f;
            else if (movement.x > 0 && movement.y == 0)
                rbPlayer.rotation = -90f;
            else if (movement.x == 0 && movement.y < 0)
                rbPlayer.rotation = -180f;
            else if (movement.x < 0 && movement.y == 0)
                rbPlayer.rotation = 90f;
            
            if (Input.GetKeyDown(KeyCode.Z))
            {
                if (!isDodging)
                {
                    if (staminaBar.stamina < StaminaBar.Cost.DODGE)
                    {
                        Debug.Log("Not enough stamina");
                        audio.Play("StaminaDrained");
                    }
                    else
                    {
                        Dodge();
                        staminaBar.UseStamina(StaminaBar.Cost.DODGE);

                        audio.Play("PlayerDodge");
                    }
                }
            }
        }
        else if (!combatInfo.isAttacking && !isDodging)
        {
            playerAnimator.ChangeAnimationState(AnimationManager.PLAYER_IDLE);
        }

        if (movement.x == 0 && movement.y == 0)
        {
            audio.Stop("RunningOnGrass");
        }

        if (combatInfo.isAttacking && isDodging)
        {
            CancelInvoke("DodgeCompleted");
            DodgeCompleted();
        }

        if (Objective.sceneIndex == 2)
        {
            if (isRunwayPadTriggered)
            {
                if (Input.GetKeyDown(KeyCode.X))
                {
                    gameManager.DisableGameplay();
                    transform.position = new Vector3(boat.position.x, boat.position.y - 0.7f, boat.position.z);
                    rbPlayer.rotation = 0f;

                    objective.NextObjective();

                    boatScript.enabled = true;
                }
            }
        }
        else if (Objective.sceneIndex == 3)
        {
            if (hasReachedShore)
            {
                if (Input.GetKeyDown(KeyCode.X))
                {
                    transform.position = new Vector3(boat.position.x + 1.5f, boat.position.y + 1f, boat.position.z);
                    hasReachedShore = false;
                    seaCollider.enabled = true;

                    BoxCollider2D[] boatColliders = boat.GetComponents<BoxCollider2D>();
                    foreach(BoxCollider2D collider in boatColliders)
                        Destroy(collider);

                    objective.NextObjective();
                }
            }
        }
    }

    void FixedUpdate()
    {
        // move based on input
        rbPlayer.AddForce(movement * movementSpeed * (1 / slowingStrength) * Time.fixedDeltaTime, ForceMode2D.Force);
    }

    void Dodge()
    {
        isDodging = true;
        if ((rbPlayer.rotation == 0f && movement.x > 0) ||
            (rbPlayer.rotation == -90f && movement.y < 0) ||
            (rbPlayer.rotation == -180f && movement.x < 0) ||
            (rbPlayer.rotation == 90f && movement.y > 0) ||
            (movement.x == 0 || movement.y == 0))
        {
            playerAnimator.ChangeAnimationState(AnimationManager.PLAYER_DODGE_RIGHT);
            axeAnimator.ChangeAnimationState(AnimationManager.AXE_DODGE_RIGHT);
        }
        else if ((rbPlayer.rotation == 0f && movement.x < 0) ||
            (rbPlayer.rotation == -90f && movement.y > 0) ||
            (rbPlayer.rotation == -180f && movement.x > 0) ||
            (rbPlayer.rotation == 90f && movement.y < 0))
        {
            playerAnimator.ChangeAnimationState(AnimationManager.PLAYER_DODGE_LEFT);
            axeAnimator.ChangeAnimationState(AnimationManager.AXE_DODGE_LEFT);
        }
        
        rbPlayer.AddForce(movement * dodgeForce * (1 / slowingStrength), ForceMode2D.Impulse);

        Invoke("DodgeCompleted", dodgeCoolDown);
    }

    void DodgeCompleted()
    {
        isDodging = false;
        axeAnimator.ChangeAnimationState(AnimationManager.AXE_IDLE);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            rbEnemy = collision.gameObject.GetComponent<Rigidbody2D>();
            rbEnemy.constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (rbEnemy != null)
                rbEnemy.constraints = RigidbodyConstraints2D.None;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Objective.sceneIndex == 2)
        {
            if (Objective.objectiveIndex == 1 && collision.name == "objective-1")
            {
                objective.NextObjective();
                runwayPad.enabled = true;
            }
            if (Objective.objectiveIndex == 2 && collision.name == "objective-2")
            {
                isRunwayPadTriggered = true;
                Color color = Color.green;
                color.a = 0.45f;
                runwayPad.color = color;
            }
        }
        else if (Objective.sceneIndex == 3)
        {
            if (Objective.objectiveIndex == 1 && collision.name == "brokenFence")
            {
                objective.NextObjective();
                Destroy(collision.gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (Objective.sceneIndex == 2)
        {
            if (Objective.objectiveIndex == 2 && collision.name == "objective-2")
            {
                isRunwayPadTriggered = false;
                Color color = Color.yellow;
                color.a = 0.45f;
                runwayPad.color = color;
            }
        }
        else if (Objective.sceneIndex == 3)
        {
            if (Objective.objectiveIndex == 3 && collision.name == "bossFightCollider")
            {
                bossFightCollider.isTrigger = false;
            }
        }
    }
}