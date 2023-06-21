using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float speed;
    public LayerMask groundLayer;
    public LayerMask wallLayer;
    public float jumpPower;
    private Rigidbody2D body;
    private Animator anim;
    private CapsuleCollider2D capsCollider;
    private float wallJumpCooldown;
    private float HorizontalInput;

    [Header("Player Movement Sounds")]
    [SerializeField] private AudioClip jumpsound;
    

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        capsCollider = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        HorizontalInput = Input.GetAxis("Horizontal");


        //player flipping code
        if (HorizontalInput > 0.01f)
            transform.localScale = new Vector3(5, 5, 1);
        else if (HorizontalInput < -0.01f)
            transform.localScale = new Vector3(-5, 5, 1);



        //Set animation parameters
        anim.SetBool("run", HorizontalInput != 0);
        anim.SetBool("grounded", isGrounded());

        //wall jump logic
        if (wallJumpCooldown > 0.2f)
        {
            body.velocity = new Vector2(HorizontalInput * speed, body.velocity.y);

            if (onWall() && !isGrounded())
            {
                body.gravityScale = 0;
                body.velocity = Vector2.zero;
            }
            else
            {
                body.gravityScale = 7f;
            }

            if (Input.GetKey(KeyCode.Space))
            {
                Jump();

                if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
                    SoundManager.instance.PlaySound(jumpsound);
            }

        }
        else
        {
            wallJumpCooldown += Time.deltaTime;
        }

    }

    void Jump()
    {
        if (isGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            anim.SetTrigger("jump");
        }
        else if (onWall() && !isGrounded())
        {
            if (HorizontalInput == 0)
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 8);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x) * 5, transform.localScale.y, transform.localScale.z);
            }
            else
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);
            }
            wallJumpCooldown = 0;

        }

    }

    bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(capsCollider.bounds.center, capsCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(capsCollider.bounds.center, capsCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    public bool canAttack()
    {
        return isGrounded() && !onWall() || !isGrounded() && !onWall(); ;
        
    }
}

