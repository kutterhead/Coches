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
    public int vueltasActtuales;

    void Start()
    {
        checkActual = 0;
        vueltasActtuales = 0;
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
                checkActual++;
                if (checkActual== manager.checkPoints.Length)
                {
                    checkActual = 0;
                    vueltasActtuales++;
                    if (vueltasActtuales >= manager.vueltasTotales)
                    {
                        Destroy(this);//eliminamos este script
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
