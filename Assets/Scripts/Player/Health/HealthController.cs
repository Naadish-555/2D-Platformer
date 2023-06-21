using System.Collections;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float startinghealth;
    public float currenthealth { get; private set; }
    private Animator anim;
    private bool dead;

    [Header("iframes")]
    [SerializeField] private float iframesDuration;
    [SerializeField] private float numberofflashes;
    private SpriteRenderer spriteRenderer;

    [Header("Death")]
    [SerializeField] private AudioClip deathsound;
    [SerializeField] private AudioClip hurtsound;


    void Awake()
    {
        currenthealth = startinghealth;
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    public void TakeDamage(float dmg)
    {
        currenthealth = Mathf.Clamp(currenthealth - dmg, 0, startinghealth);
        if (currenthealth > 0)
        {
            anim.SetTrigger("hurt");
            SoundManager.instance.PlaySound(hurtsound);
            //StartCoroutine(Immortal());
        }
        else
        {
            if(!dead)
            {
                anim.SetTrigger("die");
                //Player
                if (GetComponent<MovementController>() != null) 
                    GetComponent<MovementController>().enabled = false;

                //Enemy
                if (GetComponentInParent<EnemyPatrol>() != null)
                    GetComponentInParent<EnemyPatrol>().enabled = false;

                if (GetComponent<MeleeEnemy>() != null)
                    GetComponent<MeleeEnemy>().enabled = false;

                if (GetComponentInParent<GoblinPatrol>() != null)
                    GetComponentInParent<GoblinPatrol>().enabled = false;
                
                if (GetComponent<GoblinMeele>() != null)
                    GetComponent<GoblinMeele>().enabled = false;
                
                dead = true;
                SoundManager.instance.PlaySound(deathsound);
            }
             
        }
    }
    public void AddHealth(float val)
    {
        currenthealth = Mathf.Clamp(currenthealth + val, 0, startinghealth);
    }

    private IEnumerator Immortal()
    {
        Physics2D.IgnoreLayerCollision(7, 8, true);
        //immortal duration
        for (int i = 0; i < numberofflashes; i++)
        {
            spriteRenderer.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iframesDuration / numberofflashes * 2);
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(iframesDuration / numberofflashes * 2);
        }
        Physics2D.IgnoreLayerCollision(7, 8, false);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
