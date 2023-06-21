using UnityEngine;

public class Sawmovement : MonoBehaviour
{
    [SerializeField] private string direction;
    [SerializeField] private float sawdamage;
    [SerializeField] private float sawspeed;
    [SerializeField] private float sawtraveldist;
    private bool movingleft;
    private bool movingdown;
    private float leftEdge;
    private float rightEdge;
    private float upEdge;
    private float downEdge;

    private void Awake()
    {
        leftEdge = transform.position.x - sawtraveldist;
        rightEdge = transform.position.x + sawtraveldist;
        upEdge = transform.position.y + sawtraveldist;
        downEdge = transform.position.y - sawtraveldist;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<HealthController>().TakeDamage(sawdamage);
        }
    }


    void Update()
    {
        if (direction == "horizontal")
        {
            if (movingleft)
            {
                if (transform.position.x > leftEdge)
                {
                    transform.position = new Vector3(transform.position.x - sawspeed * Time.deltaTime, transform.position.y, transform.position.z);
                }
                else
                {
                    movingleft = false;
                }
            }
            else
            {
                if (transform.position.x < rightEdge)
                {
                    transform.position = new Vector3(transform.position.x + sawspeed * Time.deltaTime, transform.position.y, transform.position.z);
                }
                else
                {
                    movingleft = true;
                }
            }
        }
        if (direction == "vertical")
        {
            if(movingdown)
            {
                if (transform.position.y > downEdge)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y - sawspeed * Time.deltaTime, transform.position.z);
                }
                else
                {
                    movingdown = false;
                }
            }
            else
            {
                if (transform.position.y < upEdge)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y + sawspeed * Time.deltaTime, transform.position.z);
                }
                else
                {
                    movingdown = true;
                }
            }
            
        }
    }
        

        
    }
