using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float rotateMin, rotateMax;

    private float rotateSpeed;


    // Sorteia uma rota��o entre os valores passados e rotaciona o objeto com o script
    void Start()
    {
        rotateSpeed = Random.Range(rotateMin, rotateMax);
    }

    
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, rotateSpeed) * Time.deltaTime);
    }
}
