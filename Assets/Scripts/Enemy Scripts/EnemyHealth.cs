using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyHealth : MonoBehaviour {

    //initialises variables
    [SerializeField]
    private int Health = 100;
    public TextMeshPro healthText;
    public Powerup PowerupScript;

    //Reduces enemy health by the specified amount in the parameter
    public void takenHealthDamage(int takenDamage) {
        Health -= takenDamage;

        //Destroys object enemy on death and creates powerup object, also increases score by 10
        if (Health <= 0) {
            PointScript.pointValue +=10;
            PowerupScript.createPowerup();
            Destroy(gameObject);
        }
        //Updates enemy health text as they have been damaged
        setHealthText();
    }

    //Function used to set the health of the enemy above their head
    private void setHealthText() {
        healthText.text = Health.ToString();
    }
}
