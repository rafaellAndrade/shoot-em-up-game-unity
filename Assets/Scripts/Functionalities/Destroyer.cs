using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{

    // Destroi todas as coisas que sairem de dentro do collider do objeto
    // Usado para distruir todas as balas e naves inimigas

    private void OnTriggerExit2D(Collider2D other)
    {
        Destroy(other.gameObject);
    }
}
