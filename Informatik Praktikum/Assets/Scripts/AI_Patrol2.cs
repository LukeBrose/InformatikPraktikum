using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

    public class AI_Patrol2 : MonoBehaviour
    {
        //Abwandlung des AI_Patrol Skripts. Siehe dieses für Kommentare 
        //Dieses Skript ist für das Verhalten der Krabbe zuständig.
        //Sie nutzt keinen A*, sondern soll sich von dem Spieler/der Spielerin wegbewegen, wenn diese/r in Sicht ist 
        
        public GameObject player;
        public GameObject enemy;
        public Material thisMaterial;
        public bool mustPatrol = true;
        public float walkSpeed;
        public float runSpeed; 

        public float endpunkt_x;
        private float distancepoint_right;
        private float distancepoint_left;
        public bool patrolright = true;
        public float targetrange = 10f;
        public bool insight = false;
        public Material mat;

        void Start()
        {
            distancepoint_right = transform.position.x + endpunkt_x;
            distancepoint_left = transform.position.x - endpunkt_x;
            //Debug.Log(distancepoint_right);
            //Debug.Log(distancepoint_left);
    }

        void Update()
        {
            player = GameObject.Find("Player");
            //Debug.Log(playerPos);

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
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(distancepoint_right, transform.position.y, 0), walkSpeed * Time.deltaTime);
            if (transform.position.x == distancepoint_right)
            {
                Flip();
                patrolright = false;
            }
        }

        void PatrolOtherWay()
        {
        //Debug.Log("OtherPatrol: " + transform.position.x);
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(distancepoint_left, transform.position.y, 0), walkSpeed * Time.deltaTime);
            if (transform.position.x == distancepoint_left)
            {
                Flip();
                patrolright = true;

            }
        }

        void Flip()
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }



        //Schritt 2: Player auf Sicht. Quelle für erste Ideenfindung: https://www.youtube.com/watch?v=db0KWYaWfeM) 
        private void FindTarget()
        {
            // Erweiterung des Falls, wenn der Spieler/die Spielerin in der targetrange ist
            // Die Krabbe soll den Player zusätzlich nur sehen, wenn dieser nicht zu weit oben (bspw. versteckt auf einer Plattform) ist
            // Dabei wird überprüft, ob die Differenz des Betrages der Positonen von Gegner und Spieler/in kleiner als 1 ist, nur dann soll sich das Verhalten ändern
            if (Vector3.Distance(transform.position, player.transform.position) <= targetrange && Mathf.Abs(transform.position.y) - Mathf.Abs(player.transform.position.y) < 1)
            {
                //Ändere auch hier die Farbe des Kreises
                Color insightcol = new Color(0.7f, 0.1f, 0.1f, 0.2f);
                mat.SetColor("_Color", insightcol);
                insight = true;
                //Rufe die neue Methode RunAway() auf, wenn sich der Player in Sicht befindet 
                RunAway();
            }

            if (Vector3.Distance(transform.position, player.transform.position) > targetrange)
            {
                Color outsightcol = new Color(0.8f, 0.7f, 0.1f, 0.2f);
                mat.SetColor("_Color", outsightcol);
                //Ruft die Patroullie auf 
                insight = false;
            }
        }


    private void RunAway()
    {
        //Einfache Implementation, um sich vom dem/der Spieler/in wegzubewegen. Auch hier wird MoveTowards genutzt wie bei der Patroullie
        //Wenn der x-Wer vom Gegner größer ist als vom Spieler, ist der Gegner rechts vom Spieler 
        if (transform.position.x > player.transform.position.x)
        //Um zu entkommen muss somit der x-Wert des Gegners erhöht werden
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x + 1, transform.position.y, 0), runSpeed * Time.deltaTime);

        //Wenn der x-Wert vom Gegner kleiner ist als vom Spieler, ist der Gegner links vom Spieler 
        //Um zu entkommen muss somit der x-Wert des Gegners verringert
        else
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x - 1, transform.position.y, 0), runSpeed * Time.deltaTime);
    }
}
