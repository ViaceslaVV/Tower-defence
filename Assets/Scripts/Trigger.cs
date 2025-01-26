using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public int damageAmount = 1; // Kiek gyvybi? atimti

    private void OnTriggerEnter(Collider other)
    {
        // Tikriname, ar objektas turi tag'? "Player"
        if (other.CompareTag("Enemy"))
        {
            // Bandome rasti „HealthManager“ skript? ?aid?jo objekte
            HealthManager healthManager = other.GetComponent<HealthManager>();
            if (healthManager != null)
            {
                healthManager.ReduceHealth(damageAmount); // Ma?iname gyvybes
            }
        }
    }
}
