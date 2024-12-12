using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionControl : MonoBehaviour
{
    // Start is called before the first frame update
    private int checkPointActual;

    private int ultimoAtravesado = 0;//indice
    
    public gameManager manager;
    public Transform lanzadorRay;

    private float tiempoFuera = 0;


    byte posicionCarrera = 0;

    void Start()
    {
        checkPointActual = -1;
        
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<gameManager>();
    }

    private void Update()
    {
        Vector3 direccionDelRayo = Vector3.down;
        Debug.DrawRay(lanzadorRay.position, direccionDelRayo*2f);


        //rayo disparado para detectar la carretera
        if (Physics.Raycast(lanzadorRay.position, direccionDelRayo, out RaycastHit hit, 2f))
        {
           // Debug.Log("Hit: " + hit.collider.tag);
            if (hit.collider.CompareTag("OutRoad"))
            {
                //hemos salido
                tiempoFuera+=Time.deltaTime;
                Debug.Log("Hit: " + tiempoFuera.ToString("F2"));


                if (tiempoFuera>=5f)
                {
                    Debug.Log("resetea posición:");

                    transform.position = manager.checkPoints[ultimoAtravesado].position;
                    transform.rotation = manager.checkPoints[ultimoAtravesado].rotation;
                    GetComponent<Rigidbody>().velocity = Vector3.zero;


                }


            }
            else
            {
                //estamos en pista
                tiempoFuera = 0;


            }
            // Si el rayo golpea un objeto, imprimir el nombre del objeto
        }


        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("checkPoint"))
        {
            int checkActual = other.gameObject.GetComponent<checkPoint>().numeroCheckPoint;
            ultimoAtravesado = checkActual;
            print("CheckPoint atravesado");

            if (checkActual == checkPointActual + 1)
            {


                checkPointActual++;
                print("CheckPoint número: " + checkPointActual);


                //en este caso ha llegado al último
                if (checkActual == manager.checkPoints.Length-1)
                {
                    print("Fin Vuelta");
                   

                    //dejamos preparado para que pueda volver a empezar
                    checkPointActual = -1;



                    manager.registraTiempo();
                    manager.vueltas++;
                    if (manager.vueltas >= manager.vueltasTotales)
                    {


                        manager.detieneTiempo();
                        manager.numeroGanador++;
                        posicionCarrera = manager.numeroGanador;

                        //deshabilita controles
                        gameObject.GetComponent<UserControl>().enabled = false;
                        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                        
                        print("Fin carrera, posición: " + posicionCarrera);
                    }



                }


            }

        }
        
    }
    //private void OnCollisionStay(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("outRoad"))
    //    {
    //    }
    //}

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Colision con :" + collision.gameObject.name);
        if (collision.gameObject.CompareTag("OutRoad"))
        {

            Debug.Log("hemos salido");
        }
    }
}
