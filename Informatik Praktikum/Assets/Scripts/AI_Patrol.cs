using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

    public class AI_Patrol : MonoBehaviour
    {
        //Quelle für Skripte und Ideenfindung: https://www.youtube.com/watch?v=rn3tCuGM688 sowie https://www.youtube.com/watch?v=8eWbSN2T8TE
        //Skript für die Patroullie in Verbindung mit dem A*-Algorithmus 
        public GameObject player;
        public GameObject enemy;
        public Material thisMaterial;

        //Bool um die Patroullienstatus zu stoppen und fortzuführen
        public bool mustPatrol = true;

        //Laufgeschwindigkeit der Gegner 
        public float walkSpeed;

        //x-Koordinate des Gegners, wird im späteren Verlauf negiert 
        public float endpunkt_x;

        //Berechne die maximalen Punkte rechts und links, sodass der Code flexibel für verschiedene Distanzen und Startpunkte der Gegner ist
        private float distancepoint_right;
        private float distancepoint_left;

        //Überprüfung ob Gegner gerade nach rechts Laufen soll 
        public bool patrolright = true;

        //Größe für Targetrange (Ab wann sieht der Gegner den Spieler) 
        public float targetrange = 10f;

        //Bool, ob Player in Sicht ist
        public bool insight = false;

        //Matieral des Sichtfeldes (verfärbt sich später) 
        public Material mat;


        void Start()
        {
            //Setzt bei Start die Endpunkte der Patroullie (flexibler als fest gesetzte Zahlen) 
            distancepoint_right = transform.position.x + endpunkt_x;
            distancepoint_left = transform.position.x - endpunkt_x;
            //Debug.Log(distancepoint_right);
            //Debug.Log(distancepoint_left);
    }
        void Update()
        {
            player = GameObject.Find("Player");

            //Unterscheidung zwischen 2 Methoden (links und rechts laufen bis zum Maximalwert) 
            if (mustPatrol == true && patrolright == true && insight == false)
            {
                Patrol();
            }
            if (mustPatrol == true && patrolright == false && insight == false)
            {
                PatrolOtherWay();
            }
            // Wenn insight == true, wurde ein Ziel gefunden. Dann wird FindTarget() ausgeführt 
            FindTarget();
        }

        void Patrol()
        {
            //Debug.Log("Patrol: " + transform.position.x);
            //Bei jedem Update bewegt sich der Gegner einen Schritt weiter zum rechten Endpunkt der Patroullie
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(distancepoint_right, transform.position.y, 0), walkSpeed * Time.deltaTime);

            //Überprüfe, ob das Ende erreicht wurde
            if (transform.position.x == distancepoint_right)
            {
                //Debug.Log("Endpunkt erreicht (rechts)");
                //Boolscher Wert wird false, um die andere Methode (Patroullie in andere Richtung) im nächsten Update zu starten
                patrolright = false;
                Flip();
            }
        }

        void PatrolOtherWay()
        {
            //Debug.Log("OtherPatrol: " + transform.position.x);
            //Bei jedem Update bewegt sich der Gegner einen Schritt weiter zum linken Endpunkt der Patroullie
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(distancepoint_left, transform.position.y, 0), walkSpeed * Time.deltaTime);

            //Sobald es beim Zielpunkt ist, setze den Boolschen Wert auf True, sodass die erste Methode erneut ausgewählt wird 
            if (transform.position.x == distancepoint_left)
            {
                //Debug.Log("Endpunkt erreicht (links)");
                patrolright = true;
                Flip();
            }
        }

        void Flip()
        //Gegner soll sich immer Drehen, wenn die maximale Distanz der Patroullie erreicht ist 
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
  
        //Schritt 2: Player auf Sicht. Quelle für erste Ideenfindung: https://www.youtube.com/watch?v=db0KWYaWfeM
        private void FindTarget()
        {
            //Insight wird true, wenn sich der Player in der targetrange befindet
            //Ist insight true, wird durch die Bedingungen in Zeile 51 / 55 die Update Methode des Skripts CustomAIDestinationSetter genutzt
            //Debug.Log(transform.position.x - player.transform.position.x);
            if (Vector3.Distance(transform.position, player.transform.position) < targetrange)
            {
                //Ändert die Farbe des Sichtkreises für bessere Visualisierung
                //Ausgeführt, wenn Gegner in Sichtbereich ist
                Color insightcol = new Color(0.7f, 0.1f, 0.1f, 0.2f);
                mat.SetColor("_Color", insightcol);
                insight = true;
            }

            if (Vector3.Distance(transform.position, player.transform.position) > targetrange)
            {
                //Ändert die Farbe des Sichtkreises für bessere Visualisierung
                //Ausgeführt, wenn Gegner nicht in Sichtbereich ist
                Color outsightcol = new Color(0.8f, 0.7f, 0.1f, 0.2f);
                mat.SetColor("_Color", outsightcol);
                insight = false;
            }
        }
    }
