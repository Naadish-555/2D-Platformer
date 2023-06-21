using UnityEngine;

public class Enemydamage : MonoBehaviour
{
    [Header("Enemy/Trap Damage")]
    [SerializeField] protected float damage;

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            collision.GetComponent<HealthController>().TakeDamage(damage);
    }

}
