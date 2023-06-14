using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : PlayerAbstract
{
    [SerializeField] private Rigidbody2D rb;

    [Range(0, 1)][SerializeField] private float m_SlideSpeed = .5f;   // How much to smooth out the movement
    [Range(0, .3f)][SerializeField] private float m_MovementSmoothing = .05f;   // How much to smooth out the movement
    [SerializeField] int totalJumps = 2; // Total jump player can perform
    [SerializeField] private bool m_AirControl = false;                         // Whether or not a player can steer while jumping;

    private float dirX = 0f; // X direction value
    private bool facingRight = true; // is player facing right
    private bool jumpPressed = false;
    private bool isRespawn = false;
    // public bool beingHit = false;
    private int availableJumps;
    private bool isDoubleJump;
    private bool coyoteJump;

    //
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float jumpHeight = 700f;
    // [SerializeField] private float hitForce = 150f;
    [SerializeField] private float gravityScale = 3f;

    // ground & wall attributes
    private bool grounded = true;
    private bool isSliding = false;
    const float k_WallRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
    const float k_GroundedRadius = .27f; // Radius of the overlap circle to determine if grounded
    [SerializeField] private Transform m_WallCheck;                          // A position marking where to check for ceilings
    [SerializeField] public Transform groundCheck;
    [SerializeField] private LayerMask m_WhatIsWall;                          // A mask determining what is ground to the character
    [SerializeField] private LayerMask m_WhatIsGround;

    private Vector3 m_Velocity = Vector3.zero;

    // [SerializeField] GameObject groundDust;
    // [SerializeField] GameObject wallDust;
    private Vector3 groundDustOffset = new Vector3(0f, -0.1f, 0f);
    private bool groundDustCoroutineAllow = false;
    private float groundDustEmissionRate = 0.1f;
    private bool wallDustCoroutineAllow = false;
    private float wallDustEmissionRate = 0.25f;

    protected override void Awake()
    {
        base.Awake();
        availableJumps = totalJumps;
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        this.rb = this.playerCtrl.GetComponent<Rigidbody2D>();
        StartCoroutine(Respawn());
    }

    protected bool CanMove()
    {
        return rb.bodyType == RigidbodyType2D.Dynamic && !GameManager.Ins.pause && !this.playerCtrl.PlayerStatus.IsDead() && !GameManager.Ins.levelCompleted;
    }

    IEnumerator Respawn()
    {
        rb.bodyType = RigidbodyType2D.Static;
        yield return new WaitForSeconds(.1f);
        this.playerCtrl.PlayerAnimation.ChangeAnimationState(PlayerAnimationState.PLAYER_APPEARING);
        isRespawn = true;
    }
    public void ApplyGravity()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = gravityScale;
        isRespawn = false;
    }

    // Update is called once per frame
    private void Update()
    {        
        if (!CanMove()) return;
        HandlePCInput();
    }

    protected void HandlePCInput()
    {
        dirX = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && totalJumps > 0)
        {
            jumpPressed = true;
        }

        if (Input.GetKeyDown(KeyCode.Escape)) GameManager.Ins.PauseGame();
    }
    private void Move()
    {
        if (jumpPressed)
        {
            Jump();
        }

        Vector2 targetVelocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        bool wasSliding = isSliding;
        isSliding = false;
        // Check if character is sliding on the wall
        if (Physics2D.OverlapCircle(m_WallCheck.position, k_WallRadius, m_WhatIsWall) && Mathf.Abs(dirX) > 0 && !grounded && rb.velocity.y < 0)
        {
            targetVelocity.y = -m_SlideSpeed;
            isSliding = true;
            if (!wasSliding) wallDustCoroutineAllow = true;
            availableJumps = totalJumps;
            isDoubleJump = false;
        }
        else if (wasSliding) wallDustCoroutineAllow = false;

        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
    }

    void Jump()
    {
        if (grounded || isSliding)
        {
            availableJumps = totalJumps;
            DoJump();
            grounded = false;

            StartCoroutine(CoyoteJumpDelay());
        }
        else if (availableJumps > 0)
        {
            if (coyoteJump)
            {
                isDoubleJump = true;
                coyoteJump = false;
                DoJump(1.2f);
            }
            else
            {
                DoJump();
            }
        }
        jumpPressed = false;
    }

    void DoJump(float alpha = 1f)
    {
        --availableJumps;
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(Vector2.up * jumpHeight * alpha);
        AudioManager.Ins.PlaySFX(EffectSound.JumpSound);
    }

    public void OnKillingEnemy()
    {
        if (!CanMove()) return;
        if (availableJumps > 1) --availableJumps;
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(Vector2.up * jumpHeight);
        AudioManager.Ins.PlaySFX(EffectSound.JumpSound);
        if (availableJumps >= 1)
            StartCoroutine(CoyoteJumpDelay());
    }

    // public void OnTrap()
    // {
    //     if (playerCtrl.PlayerStatus.IsDead()) return;
    //     // Vector2 dir = new Vector2(rb.velocity.x > 0 ? 1 : -1, grounded ? 0f : (rb.velocity.y > 0 ? 1 : -1));
    //     rb.velocity = new Vector2(0f, 0f);
    //     // rb.AddForce(dir * this.hitForce);
    // }

    IEnumerator CoyoteJumpDelay()
    {
        coyoteJump = true;
        yield return new WaitForSeconds(0.4f);
        coyoteJump = false;
    }

    private void FixedUpdate()
    {
        UpdateAnimationState();

        if (!CanMove()) return;

        bool wasGrounded = grounded;
        grounded = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, k_GroundedRadius, m_WhatIsGround);
        if (colliders.Length > 0)
        {
            grounded = true;
            if (!wasGrounded)
            {
                FXSpawner.Instance.Spawn(FXSpawner.GroundDust, groundCheck.position + groundDustOffset, Quaternion.Euler(0, 0, 0));
                groundDustCoroutineAllow = true;
            }
        }
        else if (wasGrounded)
        {
            groundDustCoroutineAllow = false;
        }

        HandleGroundEffect();
        HandleWallEffect();

        if (rb.velocity.y < 0 && !isSliding)
        {
            rb.gravityScale = gravityScale * 1.2f;
        }
        else
        {
            rb.gravityScale = gravityScale;
        }

        if (grounded || m_AirControl)
        {
            Move();
        }
    }

    private void HandleGroundEffect()
    {
        if (grounded && dirX != 0 && groundDustCoroutineAllow)
        {
            StartCoroutine("SpawnGroundCloud");
            groundDustCoroutineAllow = false;
        }

        if (dirX == 0 || !grounded)
        {
            if (SpawnGroundCloud() != null) StopCoroutine("SpawnGroundCloud");
            groundDustCoroutineAllow = true;
        }
    }

    private void HandleWallEffect()
    {
        if (isSliding && wallDustCoroutineAllow)
        {
            StartCoroutine("SpawnWallDust");
            wallDustCoroutineAllow = false;
        }

        if (!isSliding)
        {
            if (SpawnWallDust() != null) StopCoroutine("SpawnWallDust");
            wallDustCoroutineAllow = true;
        }
    }

    private void UpdateAnimationState()
    {
        if (isRespawn || playerCtrl.PlayerAnimation.beingHit) return;
        else if (playerCtrl.PlayerStatus.isOnTrap) return;
        else if (isSliding)
        {
            this.playerCtrl.PlayerAnimation.ChangeAnimationState(PlayerAnimationState.PLAYER_WALL_JUMP);
            return;
        }
        else if (!grounded)
        {
            if (isDoubleJump)
                this.playerCtrl.PlayerAnimation.ChangeAnimationState(PlayerAnimationState.PLAYER_DOUBLE_JUMP);
            else if (rb.velocity.y >= 0)
                this.playerCtrl.PlayerAnimation.ChangeAnimationState(PlayerAnimationState.PLAYER_JUMP);
            else if (rb.velocity.y < 0)
                this.playerCtrl.PlayerAnimation.ChangeAnimationState(PlayerAnimationState.PLAYER_FALL);
            if (dirX > 0)
            {
                if (!facingRight) Flip();
            }
            else if (dirX < 0)
            {
                if (facingRight) Flip();
            }
            return;
        }
        else if (grounded)
        {
            if (dirX > 0)
            {
                this.playerCtrl.PlayerAnimation.ChangeAnimationState(PlayerAnimationState.PLAYER_RUN);
                if (!facingRight) Flip();
            }
            else if (dirX < 0)
            {
                this.playerCtrl.PlayerAnimation.ChangeAnimationState(PlayerAnimationState.PLAYER_RUN);
                if (facingRight) Flip();
            }
            else
            {
                this.playerCtrl.PlayerAnimation.ChangeAnimationState(PlayerAnimationState.PLAYER_IDLE);
            }
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        transform.parent.Rotate(0f, 180f, 0f);
    }

    public void OnDoubleJumpEnd()
    {
        if (isDoubleJump) isDoubleJump = false;
    }

    IEnumerator SpawnGroundCloud()
    {
        while (grounded)
        {
            FXSpawner.Instance.Spawn(FXSpawner.GroundDust, groundCheck.position + groundDustOffset, Quaternion.identity);
            yield return new WaitForSeconds(groundDustEmissionRate);
        }
    }

    IEnumerator SpawnWallDust()
    {
        while (isSliding)
        {
            FXSpawner.Instance.Spawn(FXSpawner.WallDust, m_WallCheck.position, Quaternion.identity);
            yield return new WaitForSeconds(wallDustEmissionRate);
        }
    }

    public void AddForceToPlayer(Vector2 direction)
    {
        Debug.Log("addforce");
        this.rb.AddForce(direction);
    }
}
