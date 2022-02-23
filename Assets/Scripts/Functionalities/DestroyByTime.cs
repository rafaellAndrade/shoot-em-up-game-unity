using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByTime : MonoBehaviour
{
    public float destroyTime;

    // Destroi o objeto com esse script depois do tempo estipulado
    void Start()
    {
        Destroy(gameObject, destroyTime);
    }    
}
