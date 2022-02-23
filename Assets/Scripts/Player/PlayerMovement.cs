using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float spawnTime;
    public float invecibilityTime;

    private Vector2 movement;

    

    private Vector3 startPosition;

    public SpriteRenderer[] enginesFire;
    private SpriteRenderer playerSprite;

    public Sprite[] enginesFireSprite;

    private Rigidbody2D rb;
   
    void Start()
    {
        playerSprite = GetComponent<SpriteRenderer>();
        startPosition = transform.position;
        
        rb = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        movement = new Vector2(inputX * speed, inputY * speed);

        CheckScreenBounds();
    }

    private void FixedUpdate()
    {
        rb.velocity = movement;
    }


    public void CheckScreenBounds()
    {
        // Verifica o posicionamento do player em relação a câmera, limitando o espaço de movimento dele
        // Troca a skin do fogo dos jatos do player quando ele se move, para aumentar a sensação de aceleração

        var distanceZ = (transform.position - Camera.main.transform.position).z;

        var leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distanceZ)).x;

        var rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distanceZ)).x;

        var topBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distanceZ)).y;

        var bottomBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, distanceZ)).y;

        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, leftBorder, rightBorder),
            Mathf.Clamp(transform.position.y, topBorder, bottomBorder),
            transform.position.z);


        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            enginesFire[0].sprite = enginesFireSprite[1];
            enginesFire[1].sprite = enginesFireSprite[1];
        } else
        {
            enginesFire[0].sprite = enginesFireSprite[0];
            enginesFire[1].sprite = enginesFireSprite[0];
        }
    }

    public void Respaw()
    {
        StartCoroutine(Spawning());
    }

    IEnumerator Spawning()
    {
        // Quando o player morre desabilita componentes dele, faz ele retornar para sua posição inicial,
        // Pisca sua sprite, desativando e ativando ela a cada 0.1 segunds e depois ativa os componentes novamente

        enginesFire[0].enabled = false;
        enginesFire[1].enabled = false;
        playerSprite.enabled = false;
        PlayerAttack plyAttackSc = GetComponent<PlayerAttack>();
        plyAttackSc.SetShipLevel(1);
        plyAttackSc.enabled = false;
        gameObject.layer = 8;
        yield return new WaitForSeconds(spawnTime);
        transform.position = startPosition;
        plyAttackSc.enabled = true;
        enginesFire[0].enabled = true;
        enginesFire[1].enabled = true;

        for (float i = 0; i < invecibilityTime; i+= 0.1f)
        {
            playerSprite.enabled = !playerSprite.enabled;
            yield return new WaitForSeconds(0.1f);
        }
        gameObject.layer = 6;
        playerSprite.enabled = true;               

        CharacterLife characterLife = GetComponent<CharacterLife>();
        characterLife.isDead = false;

    }
}
