using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public GameObject mainobject;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        // Gegner soll verschwinden, wenn der Spieler/die Spielerin auf diesen springt
        // Simuliert hierbei die Lebenspunkte des Gegners. Später erweiterbar für mehrere notwendige Angriffe.
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(mainobject);
        }
    }
}
