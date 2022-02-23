using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDamage : MonoBehaviour
{
    public int damage = 1;

    public bool destroyByContact = true;

    public GameObject bullet;
    public Transform[] shotSpawns;
    public float timeSelfDestruction = 4f;


    
    private void Start()
    {
        Invoke("Fire", timeSelfDestruction);   
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // A armadilha pode disparar e ser destruida de 2 formas, ou por contato com o player ou esperando o tempo de auto destruição
        CharacterLife character = other.GetComponent<CharacterLife>();
        if (character != null)
        {
            character.TakeDamage(damage);
            if (destroyByContact)
            {
                Fire();                
            }
        }       
    }
    private void Fire()
    {
        // Função que atira por todos os pontos de spawn da armadilha e logo depois o objeto é destruido

        for (int i = 0; i < shotSpawns.Length; i++)
        {
            Instantiate(bullet, shotSpawns[i].position, shotSpawns[i].rotation);
        }

        Destroy(gameObject);
    }
}
