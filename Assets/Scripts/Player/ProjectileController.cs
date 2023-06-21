using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private float speed;
    private float direction;
    private bool hit;
    private float lifetime;


    private CapsuleCollider2D fbcollider;
    private Animator anim;

    private void Awake()
    {
        fbcollider = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if (hit)
            return;
        float movementspeed = speed * Time.deltaTime * direction;
        transform.Translate(movementspeed, 0, 0);

        lifetime += Time.deltaTime;
        if (lifetime > 2)
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        fbcollider.enabled = false;
        anim.SetTrigger("Explode");

        if (collision.tag == "Enemy")
            collision.GetComponent<HealthController>().TakeDamage(1);
            
    }

    public void SetDirection(float Dxn)
    {
        lifetime = 0;
        direction = Dxn;
        gameObject.SetActive(true);
        hit = false;
        fbcollider.enabled = true;

        float localscaleX = transform.localScale.x;
        if(Mathf.Sign(localscaleX) != Dxn)
        {
            localscaleX = -localscaleX;
        }
        transform.localScale = new Vector3(localscaleX , transform.localScale.y, transform.localScale.z);

    }

    void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
