using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class timeControl : MonoBehaviour
{
    // Start is called before the first frame update


    public TMPro.TextMeshProUGUI textoTiempo;
    public float tiempoActual = 0;



    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        //tiempoActual += Time.deltaTime;
        //print("Tiempo:" + tiempoActual.ToString("F2"));
    }



    public void iniciaTime()
    {

        StartCoroutine(tiempoCarrera());

    }

    public void detieneTime()
    {
        StopAllCoroutines();

        print("tiempo detenido" + tiempoActual.ToString("F2"));
    }



        IEnumerator tiempoCarrera()
    {
        while (true)
        {
            tiempoActual += Time.deltaTime;



            //print("Tiempo:" + tiempoActual.ToString("F2"));
            yield return null;
        }

        
    }


}
