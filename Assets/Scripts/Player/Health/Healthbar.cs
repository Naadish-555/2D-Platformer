using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private HealthController Playerhealth;
    [SerializeField] private Image totalHealthBar;
    [SerializeField] private Image currentHealthBar;
    void Start()
    {
        totalHealthBar.fillAmount = Playerhealth.currenthealth / 10;
    }

    
    void Update()
    {
        currentHealthBar.fillAmount = Playerhealth.currenthealth / 10;
    }
}
