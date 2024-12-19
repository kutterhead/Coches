using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class agente1 : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player;
    public gameManager manager;
    public int checkActual;
    public int vueltasActuales;
    public byte posicionCarrera = 0;
    public float[] tiempos;
    void Start()
    {

       player = GameObject.FindGameObjectWithTag("Car").transform;
       manager = FindObjectOfType<gameManager>();

        checkActual = 0;
        vueltasActuales = 0;
        GetComponent<NavMeshAgent>().SetDestination(manager.checkPoints[checkActual].position);
    }

    // Update is called once per frame
    void Update()
    {
       // GetComponent<NavMeshAgent>().SetDestination(player.position);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("checkPoint")) {

            if (other.gameObject.GetComponent<checkPoint>().numeroCheckPoint == checkActual)
            {
                System.Array.Resize(ref tiempos, tiempos.Length + 1);
                tiempos[tiempos.Length-1] = manager.gameObject.GetComponent<timeControl>().tiempoActual;


                checkActual++;
                if (checkActual== manager.checkPoints.Length)
                {
                    checkActual = 0;
                    vueltasActuales++;


                    //fin carrera para agente
                    if (vueltasActuales >= manager.vueltasTotales)
                    {
                        manager.numeroGanador++;
                        posicionCarrera = manager.numeroGanador;
                        GetComponent<NavMeshAgent>().enabled = false;
                        this.enabled = false;
                    }

                }



                GetComponent<NavMeshAgent>().SetDestination(manager.checkPoints[checkActual].position);


            }


            Debug.Log("Colision trigger Agente");




        }
        
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log("Colision normal Agente");
    //}
}
