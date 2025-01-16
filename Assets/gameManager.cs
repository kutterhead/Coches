using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    // Start is called before the first frame update


    public GameObject CheckPointContainer;

    public GameObject[] checkPointsObjects;
    public Transform[] checkPoints;
    public int vueltas;//estas son las vueltas actuales

    public int vueltasTotales = 3;
    public float[] tiempoVueltas;

    timeControl tc;
    public byte numeroGanador = 0; 

    private void Awake()
    {

        checkPointsObjects = GameObject.FindGameObjectsWithTag("checkPoint");

        System.Array.Resize(ref checkPoints, checkPointsObjects.Length);

        numeroGanador = 0;
        for (int i = 0; i < checkPoints.Length;i++)
        {
            checkPointsObjects[i].GetComponent<checkPoint>().numeroCheckPoint = i;
            checkPoints[i] = checkPointsObjects[i].transform;
        }
        tc = gameObject.GetComponent<timeControl>();
    }
    public void registraTiempo()
    {


        tiempoVueltas[vueltas] = tc.tiempoActual;
    }

    void Start()
    {
    vueltas = 0;
        tc.iniciaTime();

        System.Array.Resize(ref tiempoVueltas, vueltasTotales);


    }

    public void detieneTiempo(){

        tc.detieneTime();
    }

    // Update is called once per frame
  
}
