using UnityEngine;

public class HealthColllectible : MonoBehaviour
{

    [SerializeField] private float healthvalue;


    [SerializeField] private AudioClip pickuphealth;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            SoundManager.instance.PlaySound(pickuphealth);
            collision.GetComponent<HealthController>().AddHealth(healthvalue);
            gameObject.SetActive(false);
        }
             
    }

    void Update()
    {
        
    }
}
