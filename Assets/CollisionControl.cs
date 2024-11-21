using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionControl : MonoBehaviour
{
    // Start is called before the first frame update
    private int checkPointActual;
    
    public gameManager manager;


    void Start()
    {
        checkPointActual = -1;
        
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<gameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("checkPoint"))
        {
            int checkActual = other.gameObject.GetComponent<checkPoint>().numeroCheckPoint;

            print("CheckPoint atravesadp");

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
                    manager.vueltas++;
                }


            }

        }
        
    }
}
