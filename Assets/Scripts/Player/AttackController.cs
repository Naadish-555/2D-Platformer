using UnityEngine;

public class AttackController : MonoBehaviour
{

    private MovementController movementController;
    [SerializeField] private float rngAttackCooldown;
    [SerializeField] private Transform fireballpoint;
    [SerializeField] private GameObject[] fireball;
    [SerializeField] private AudioClip fireballSound;

    private Animator anim;
    private float cooldownTimer = Mathf.Infinity;
    void Awake()
    {
        anim = GetComponent<Animator>();
        movementController = GetComponent<MovementController>();

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && cooldownTimer > rngAttackCooldown && movementController.canAttack())
        {
            RangeAttack();
        }
        cooldownTimer += Time.deltaTime;
    }

    void RangeAttack()
    {
        SoundManager.instance.PlaySound(fireballSound);
        cooldownTimer = 0;


        //object pulling insted of instantiate
        fireball[FindFireball()].transform.position = fireballpoint.position;
        fireball[FindFireball()].GetComponent<ProjectileController>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private int FindFireball()
    {
        for (int i=0 ; i < fireball.Length; i++)
        {   
            if (!fireball[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
}
