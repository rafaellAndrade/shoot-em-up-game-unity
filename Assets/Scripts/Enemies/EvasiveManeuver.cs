using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvasiveManeuver : MonoBehaviour
{
    public float speed;
    public float dodge;
    private float target;
    private float objWidth; 
    public bool vertical = false;

    private Vector2 screenBounds;
    public Vector2 startWait;
    public Vector2 maneuverTime;
    public Vector2 maneuverWait;

    private Rigidbody2D rb;
   

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        objWidth = GetComponent<SpriteRenderer>().bounds.size.x / 2; 
        if(!vertical)
        {
            StartCoroutine(Evade());
        } else
        {
            StartCoroutine(EvadeVertical());
        }        
    }

    private void FixedUpdate()
    {
        MovementAndManeuver();
    }

    public void MovementAndManeuver()
    {
        // Verifica se é manobra vertical ou horizontal e
        // faz o controle de movimento e manobra do inimigo limitando para dentro dos limites da câmera, evitando que ele saia da tela.
        if (!vertical)
        {
            float newManeuver = Mathf.MoveTowards(rb.velocity.x, target, speed);
            rb.velocity = new Vector2(newManeuver, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, speed);
        }

        Vector2 viewPos = rb.position;
        viewPos.x = Mathf.Clamp(rb.position.x, screenBounds.x * -1 + objWidth, screenBounds.x - objWidth);
        viewPos.y = transform.position.y;

        rb.position = viewPos;
    }

    IEnumerator Evade()
    {
        // Sorteia uma distância alvo para o movimento controlando o tempo de manobra, parada
        // e próxima execução no eixo X
        yield return new WaitForSeconds(Random.Range(startWait.x, startWait.y));
        while (true)
        {
            
            target = Random.Range(1, dodge) * -Mathf.Sign(transform.position.x);           
            
            yield return new WaitForSeconds(Random.Range(maneuverTime.x, maneuverTime.y));
            target = 0;
            yield return new WaitForSeconds(Random.Range(maneuverWait.x, maneuverWait.y));
        }
    }

    IEnumerator EvadeVertical()
    {
        // Inverte a velocidade no eixo Y de acordo com o tempo estipulado, fazendo uma manobra nesse eixo,
        // Tomando cuidado com os valores, para ele descer mais do que sobe.
        yield return new WaitForSeconds(Random.Range(startWait.x, startWait.y));
        while (true)
        {
            speed = -speed;    
            yield return new WaitForSeconds(Random.Range(maneuverTime.x, maneuverTime.y));
            speed = -speed;
            yield return new WaitForSeconds(Random.Range(maneuverWait.x, maneuverWait.y));
        }
    }

}
