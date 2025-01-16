using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisionNPC : MonoBehaviour
{
    // Start is called before the first frame update

    CarController ControlledCar;

    public int checkPointActual;//el check point que busca

    private int ultimoAtravesado = 0;//el último check atravesado

    gameManager manager;//privado pues se captura en Start
    public Transform lanzadorRay;

    private float tiempoFuera { get; set; }
    public float Horizontal { get; private set; }
    public float Vertical { get; private set; }
    public bool Brake { get; private set; }

    public Transform sensorL;
    public Transform sensorR;
    //byte posicionCarrera = 0;


    public bool SL;
    public bool SR;

    //Control proporcional------------------------------------------------------

    public Transform puntero;

    void Start()
    {

        
        ControlledCar = GetComponent<CarController>();

        checkPointActual = 0;//inicialización de check

        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<gameManager>();//captura de manager

        Horizontal = 0f;
        Vertical = 0.05f;
        Brake = false;

    }


    public void Update()
    {
        //Vertical += Time.deltaTime / 5;
        ControlledCar.UpdateControls(Horizontal, Vertical, Brake);
        puntero.LookAt(manager.checkPoints[3]);

     
        Debug.Log(puntero.localEulerAngles.y);

        
        //control binario
        //updateControl();
        //UpdateSensorsRoadLR();
        //UpdateSensorRoad();
    }



    private void updateControl()
    {
        if (SL == false && SR== false)
        {
            Horizontal = 0f;
            Vertical = 0.2f;
            Brake = false;

            //avanza
        }
        else if (SL == false && SR == true)
        {
            Horizontal = -0.4f;
            Vertical = 0.1f;
            Brake = false;

            //gira I
        }
        else if (SL == true && SR == false)
        {
            Horizontal = 0.4f;
            Vertical = 0.1f;
            Brake = false;

            //gira D
        }
        else//caso true true
        {
            Horizontal = 0f;
            Vertical = 0.5f;
            Brake = true;

            //avanza
        }

    }

    private void UpdateSensorsRoadLR()//intentar añadir sensor central 8 combinaciones
    {
        Vector3 direccionDelRayo = Vector3.down;
        Debug.DrawRay(sensorL.position, direccionDelRayo * 2f);


        //rayo disparado para detectar la carretera
        if (Physics.Raycast(sensorL.position, direccionDelRayo, out RaycastHit hit, 2f))
        {
            // Debug.Log("Hit: " + hit.collider.tag);
            if (hit.collider.CompareTag("OutRoad"))
            {

                SL = true;
                           }
            else if (hit.collider.CompareTag("Road"))
            {
              
                SL = false;
            }
            
        }

        //proyectoa segundo ray
        if (Physics.Raycast(sensorR.position, direccionDelRayo, out RaycastHit hit2, 2f))
        {
            // Debug.Log("Hit: " + hit.collider.tag);
            if (hit2.collider.CompareTag("OutRoad"))
            {
                //Debug.Log("HitR: " + tiempoFuera.ToString("F2"));

                SR = true;
               
            }
            else if (hit2.collider.CompareTag("Road"))
            {
                //estamos en pista
                //tiempoFuera = 0;
                //Debug.Log("HitR: " + tiempoFuera.ToString("F2"));
                SR = false;
            }

        }


    }


    private void UpdateSensorRoad()
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("checkPoint"))
        {
            int checkActual = other.gameObject.GetComponent<checkPoint>().numeroCheckPoint;
            ultimoAtravesado = checkActual;
            print("NPC CheckPoint atravesado");

            if (checkActual == checkPointActual)
            {


                checkPointActual++;
                print("NPC CheckPoint número: " + checkPointActual);


                //en este caso ha llegado al último
                if (checkActual == manager.checkPoints.Length - 1)
                {
                    print("Fin Vuelta");


                    //dejamos preparado para que pueda volver a empezar
                    checkPointActual = -1;



                    //manager.registraTiempo();
                    //manager.vueltas++;
                    if (manager.vueltas >= manager.vueltasTotales)
                    {


                        manager.detieneTiempo();
                        manager.numeroGanador++;
                        //posicionCarrera = manager.numeroGanador;

                        //deshabilita controles
                        //gameObject.GetComponent<UserControl>().enabled = false;
                        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;

                       // print("Fin carrera, posición: " + posicionCarrera);
                    }



                }


            }

        }

    }


}
