using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShoot : MonoBehaviour
{
    public GameObject bullet;
    public GameObject finalLaserBullet;
    public GameObject[] enemies;
    public GameObject[] traps;
    public float fireRate;
    public Transform[] shootSpawns;
    public bool isUfo;
    private float eixoy;



    void Start()
    {
        eixoy = transform.position.y;
        StartCoroutine(SequenceShot());
    }

    private void Update()
    {
        Vector2 position = new Vector2(transform.position.x, eixoy);
        transform.position = position;
    }

    IEnumerator UltimateLaser()
    {
        // Seleciona um ponto específico de tiro da nave e logo após é instanciado o laser,
        // espera o tempo da animação acabar e chama a função de sorter ataque novamente

        Quaternion newRotation = shootSpawns[4].rotation;
        newRotation.z = 180;
        Instantiate(finalLaserBullet, transform);
        yield return new WaitForSeconds(3f);       

        StartCoroutine(ChoosingAttack());
    }

    IEnumerator DropTraps()
    {
        // Cria um valor randômico que será usar para estipular a quantidade de armadilhas a serem instanciadas
        // Dentro do For sorteia uma armadilha e um ponto de spawn do boss e é instanciada, esperando um intervalo de
        // tempo para instanciar outra caso haja
        // Chama Sortear ataque
        int randomAmount = Random.Range(1, 4);

        for (int i = 0; i < randomAmount; i++)
        {
            int randomTrap = Random.Range(0, traps.Length);
            int randomIndex = Random.Range(0, shootSpawns.Length);
            Instantiate(traps[randomTrap], shootSpawns[randomIndex].position, shootSpawns[randomIndex].rotation);
            yield return new WaitForSeconds(0.5f);
        }     

        StartCoroutine(ChoosingAttack());
    }

    IEnumerator DropEnemies()
    {
        // Cria um valor randômico que será usar para estipular a quantidade de inimigos a serem instanciados
        // Dentro do For sorteia um inimigo e um ponto de spawn do boss e é instanciada, esperando um tempo para instanciar outro caso haja
        // Chama Sortear ataque
        int randomAmount = Random.Range(1, 4);

        for (int i = 0; i < randomAmount; i++)
        {
            int enemyRandom = Random.Range(0, enemies.Length);
            int randomIndex = Random.Range(0, shootSpawns.Length);
            Instantiate(enemies[enemyRandom], shootSpawns[randomIndex].position, Quaternion.identity);
            yield return new WaitForSeconds(1f);
        } 
        
        StartCoroutine(ChoosingAttack());
    }

    IEnumerator SequenceShot()
    {
        // Faz uma sequência de tiros em todos os pontos de spawn do boss esperando um intervalo de tempo entre cada disparo
        // Chama Sortear ataque
        for (int i = 0; i < shootSpawns.Length; i++)
        {
            Quaternion newRotation = shootSpawns[i].rotation;
            newRotation.z = 180;
            Instantiate(bullet, shootSpawns[i].position, newRotation);
            yield return new WaitForSeconds(0.5f);
        }

        StartCoroutine(ChoosingAttack());        
    }

    IEnumerator ChoosingAttack()
    {
        // Espera um tempo entre 1 e 2 segundos para sortear o próximo ataque
        // Sorteia/Escolhe um ataque aleatório atráves do Random Range
        yield return new WaitForSeconds(Random.Range(1, 3));

        int randomAttack = Random.Range(0, 4);        

        switch (randomAttack)
        {
            case 0:
                StartCoroutine(SequenceShot());
                break;
            case 1:
                StartCoroutine(UltimateLaser());
                break;
            case 2:
                StartCoroutine(DropEnemies());
                break;
            case 3:
                StartCoroutine(DropTraps());
                break;
        }             
    }
}
