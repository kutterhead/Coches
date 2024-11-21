using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform[] checkPoints;
    public int vueltas;
    private void Awake()
    {
        for (int i = 0; i < checkPoints.Length;i++)
        {
            checkPoints[i].gameObject.GetComponent<checkPoint>().numeroCheckPoint = i;
        }
        
    }


    void Start()
    {
    vueltas = 0;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
