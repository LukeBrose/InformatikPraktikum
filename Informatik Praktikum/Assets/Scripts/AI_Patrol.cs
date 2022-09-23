using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

    public class AI_Patrol : MonoBehaviour
    {
        //Quelle für Skripte und Ideenfindung: https://www.youtube.com/watch?v=rn3tCuGM688 sowie https://www.youtube.com/watch?v=8eWbSN2T8TE
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

        //Größe für Targetrange
        public float targetrange = 10f;

        //Bool, ob Player in Sicht ist
        public bool insight = false;

        public Material mat;

        

        // Start is called before the first frame update
        void Start()
        {
            distancepoint_right = transform.position.x + endpunkt_x;
            distancepoint_left = transform.position.x - endpunkt_x;
            Debug.Log(distancepoint_right);
            Debug.Log(distancepoint_left);
    }

        // Update is called once per frame
        void Update()
        {
            var cubeRenderer = this.GetComponent<Renderer>();

            player = GameObject.Find("Player");
            //Debug.Log(playerPos);

            //Unterscheidung zwischen 2 Methoden (links und rechts laufen bis zum Maximalwert) 
            if (mustPatrol == true && patrolright == true && insight == false)
            {
                Patrol();
            }
            if (mustPatrol == true && patrolright == false && insight == false)
            {
                PatrolOtherWay();
            }

            FindTarget();
        }

        void Patrol()
        {

            //Debug.Log("Patrol: " + transform.position.x);
            //Bei jedem Update bewegt sich der Gegner einen Schritt weiter zu endpunkt_x
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(distancepoint_right, transform.position.y, 0), walkSpeed * Time.deltaTime);

            //Überprüfe, ob das Ende erreicht wurde
            if (transform.position.x == distancepoint_right)
            {
                Debug.Log("Endpunkt erreicht (rechts)");
                //Boolscher Wert wird false, um die andere Methode im nächsten Update zu starten
                //Flip();
                patrolright = false;

            }
        }

        void PatrolOtherWay()
        {
            //Debug.Log("OtherPatrol: " + transform.position.x);
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(distancepoint_left, transform.position.y, 0), walkSpeed * Time.deltaTime);

            //Sobald es bei 0 ist, setze den Boolschen Wert auf True, sodass die erste Methode erneut ausgewählt wird 
            if (transform.position.x == distancepoint_left)
            {
                Debug.Log("Endpunkt erreicht (links)");
                //Flip();
                patrolright = true;

            }
        }

        /*void Flip()
        //Gegner soll sich immer Drehen, wenn die maximale Distanz erreicht ist 
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
        */


        //Schritt 2: Player auf Sicht (https://www.youtube.com/watch?v=db0KWYaWfeM) 
        private void FindTarget()
        {
            //Insight wird true, wenn sich der Player in der targetrange befindet
            // ist insight true, wird durch die Bedingungen in Zeile 51 / 55 die Update Methode des Skripts AIDestinationSetter genutz´t
            //Debug.Log(transform.position.x - player.transform.position.x);
            if (Vector3.Distance(transform.position, player.transform.position) < targetrange)
            {
                Color insightcol = new Color(0.7f, 0.1f, 0.1f, 0.2f);
                mat.SetColor("_Color", insightcol);
                insight = true;
            }

            if (Vector3.Distance(transform.position, player.transform.position) > targetrange)
            {
                Color outsightcol = new Color(0.8f, 0.7f, 0.1f, 0.2f);
                mat.SetColor("_Color", outsightcol);
                insight = false;

            }
        }
    }
