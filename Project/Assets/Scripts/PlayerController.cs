using UnityEngine;

public class PlayerController : MonoBehaviour
{
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

    void Awake()
    {
        rbPlayer = GetComponent<Rigidbody2D>();
        combatInfo = GetComponent<PlayerCombat>();

        rbEnemy = null;

        playerAnimator.ChangeAnimationState(playerAnimator.PLAYER_IDLE);
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement = movement.normalized;

        if (movement.x != 0 || movement.y != 0)
        {
            if (!combatInfo.isAttacking && !isDodging)
                playerAnimator.ChangeAnimationState(playerAnimator.PLAYER_WALK);

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
                        Debug.Log("Not enough stamina");
                    else
                    {
                        Dodge();
                        staminaBar.UseStamina(StaminaBar.Cost.DODGE);
                    }
                }
            }
        }
        else if (!combatInfo.isAttacking && !isDodging)
        {
            playerAnimator.ChangeAnimationState(playerAnimator.PLAYER_IDLE);
        }

        if (combatInfo.isAttacking && isDodging)
        {
            CancelInvoke("DodgeCompleted");
            DodgeCompleted();
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
            playerAnimator.ChangeAnimationState(playerAnimator.PLAYER_DODGE_RIGHT);
            axeAnimator.ChangeAnimationState(axeAnimator.AXE_DODGE_RIGHT);
        }
        else if ((rbPlayer.rotation == 0f && movement.x < 0) ||
            (rbPlayer.rotation == -90f && movement.y > 0) ||
            (rbPlayer.rotation == -180f && movement.x > 0) ||
            (rbPlayer.rotation == 90f && movement.y < 0))
        {
            playerAnimator.ChangeAnimationState(playerAnimator.PLAYER_DODGE_LEFT);
            axeAnimator.ChangeAnimationState(axeAnimator.AXE_DODGE_LEFT);
        }
        
        rbPlayer.AddForce(movement * dodgeForce * (1 / slowingStrength), ForceMode2D.Impulse);

        Invoke("DodgeCompleted", dodgeCoolDown);
    }

    void DodgeCompleted()
    {
        isDodging = false;
        axeAnimator.ChangeAnimationState(axeAnimator.AXE_IDLE);
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
        if (rbEnemy != null)
        {
            rbEnemy.constraints = RigidbodyConstraints2D.None;
        }
    }
}