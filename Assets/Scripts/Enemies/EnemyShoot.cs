using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public GameObject bullet;
    public float fireRate;
    public Transform[] shootSpawns;
    public bool isUfo;
    
    

    void Start()
    {
        // Verifica se é o disco voador ou não chamar o tipo de ataque certo
        
        if (!isUfo)
        {
            InvokeRepeating("Fire", fireRate, fireRate);

        }else
        {
            StartCoroutine(UfoFire());
        }      
    }    


    private void Fire()
    {    
        // Faz os tiros do inimigo sairem de todos os seus pontos de spawn ao mesmo tempo

        for(int i = 0; i < shootSpawns.Length; i++)
        {           
            Instantiate(bullet, shootSpawns[i].position, shootSpawns[i].rotation);
        }
    }

    IEnumerator UfoFire()
    {
        // Faz os tiros do inimigo sairem de todos os seus pontos de spawn esperando um intervalo de tempo entre cada tiro, fazendo assim
        // sair uma sequência de tiro.

        for (int i = 0; i < shootSpawns.Length; i++)
        {
            Instantiate(bullet, shootSpawns[i].position, shootSpawns[i].rotation);
            yield return new WaitForSeconds(0.1f);            
        }

        yield return new WaitForSeconds(fireRate);
        yield return UfoFire();
    }
}
