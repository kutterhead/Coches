using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisionNPC : MonoBehaviour
{
    // Start is called before the first frame update
    private int checkPointActual;

    private int ultimoAtravesado = 0;//indice

    public gameManager manager;
    public Transform lanzadorRay;

    private float tiempoFuera = 0;


    //byte posicionCarrera = 0;

    void Start()
    {
        checkPointActual = -1;

        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<gameManager>();
    }

    private void Update()
    {
        Vector3 direccionDelRayo = Vector3.down;
        Debug.DrawRay(lanzadorRay.position, direccionDelRayo * 2f);


        //rayo disparado para detectar la carretera
        if (Physics.Raycast(lanzadorRay.position, direccionDelRayo, out RaycastHit hit, 2f))
        {
            // Debug.Log("Hit: " + hit.collider.tag);
            if (hit.collider.CompareTag("OutRoad"))
            {
                //hemos salido
                tiempoFuera += Time.deltaTime;
                Debug.Log("Hit: " + tiempoFuera.ToString("F2"));


                if (tiempoFuera >= 5f)
                {
                    Debug.Log("resetea posición:");

                    vuelveACheckpoitUltimo();

                }

            }
            else if (hit.collider.CompareTag("Road"))
            {
                //estamos en pista
                tiempoFuera = 0;


            }
            else
            {

                Debug.Log("hemos volcado");
                Invoke("vuelveACheckpoitUltimo", 2f);

            }
            // Si el rayo golpea un objeto, imprimir el nombre del objeto
        }



    }

    public void vuelveACheckpoitUltimo()
    {

        transform.position = manager.checkPoints[ultimoAtravesado].position;
        transform.rotation = manager.checkPoints[ultimoAtravesado].rotation;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    

}
