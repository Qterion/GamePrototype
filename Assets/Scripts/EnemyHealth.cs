using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyHealth : MonoBehaviour {

    [SerializeField]
    private int Health = 100;
    public TextMeshPro healthText;

    public void takenHealthDamage(int takenDamage) {
        Health -= takenDamage;
        if (Health <= 0) {
            Destroy(gameObject);
        }
        setHealthText();
    }

    private void setHealthText() {
        healthText.text = Health.ToString();
    }
}
