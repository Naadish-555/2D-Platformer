using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    [SerializeField] private float attackcooldown;
    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject[] bullets;
    private float cooldowntimer;

    [Header("sound")]
    [SerializeField] private AudioClip arrowsound;

    private void Attack()
    {
        cooldowntimer = 0;
        SoundManager.instance.PlaySound(arrowsound);
        bullets[findbullets()].transform.position = firepoint.position;
        bullets[findbullets()].GetComponent<Enemyprojectile>().ActivateProjectile();
    }

    private int findbullets()
    {
        for (int i = 0; i < bullets.Length; i++)
        {
            if (!bullets[i].activeInHierarchy)
                return i;
        }
        return 0;
    }

    private void Update()
    {

        cooldowntimer += Time.deltaTime;
        
        if (cooldowntimer >= attackcooldown)
            Attack();
    }
}
