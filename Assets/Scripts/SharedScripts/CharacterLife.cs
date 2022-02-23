using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLife : MonoBehaviour
{
    public int health;
    public int scorePoints;
    public GameObject explosion;
    public Color damageColor;

    [HideInInspector]
    public bool isDead = false;

    private SpriteRenderer sprite;
    public GameObject[] dropItems;
    private static int chanceToDropItem = 0;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(int damage)
    {
        // Verifica se o objeto está "morto" ou não, diminui sua barra de vida, verifica se sua vida é menor ou igual 0,
        // verfica se contem o prefab de explosão, caso sim a explosão é instanciada
        if (!isDead)
        {
            health -= damage;
            if(health <= 0)
            {
                isDead = true;
                if(explosion != null)
                {
                    Instantiate(explosion, transform.position, transform.rotation);
                }

                // Verifica se o objeto é o boss, caso sim seta a variavel do isBoss do GameManeger como falsa e ativa os spawns
                // de inimigos novamente
                if(this.GetComponent<BossShoot>() != null)
                {
                    GameManager.Instance.SetIsBoss();
                }

                // Verifica se o objeto é o player, caso sim ativa a função de respaw do Player
                if (this.GetComponent<PlayerMovement>() != null)
                {
                    GetComponent<PlayerMovement>().Respaw();
                }
                else
                {
                    // Caso não seja o player ele aumenta a chance de dropar um item do objeto
                    // Soteia um numero aleatório verifica a condição de drop, se for atendida o item é instanciado, a pontuação é incrementada
                    // e o objeto é destruido
                    // Caso não seja atendida apenas incrementa a pontuação e destroi o objeto
                    chanceToDropItem++;
                    int random = Random.Range(0, 20);
                    if (random < chanceToDropItem && dropItems.Length > 0)
                    {
                        Instantiate(dropItems[Random.Range(0, dropItems.Length)], transform.position, Quaternion.identity);
                        chanceToDropItem = 0;
                    }
                    GameManager.Instance.SetScore(scorePoints);                    
                    Destroy(gameObject);
                }                
            }
            else
            {
                StartCoroutine(TakingDamage());
            }
        }
    }

    IEnumerator TakingDamage()
    {
        // Troca a cor da sprite e volta ao normal logo em seguida, para reforçar a sensação de dano no objeto
        sprite.color = damageColor;
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;
    }
}
