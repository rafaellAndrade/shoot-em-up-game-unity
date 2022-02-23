using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Enumerados dos itens que podem ser dropados
public enum ItemEffect
{
    shield, levelUp, special, score
}

public class PlayerAttack : MonoBehaviour
{
    public GameObject bullet;
    public GameObject special;
    public GameObject shield;
    public Transform[] spawnPoints;
    public int shipLevel = 1;
    public Sprite[] shipSkin;
    private int specialValue;
    public AudioSource audioS;
    public AudioClip[] audiosC;
   

    void Update()
    {
        ChangeShipSkin();
        CheckPlayerInput();
    }

    public void CheckPlayerInput()
    {
        // Apenas verifica os inputs do player, para ele atirar e soltar o especial

        if (Input.GetButtonDown("Fire1"))
        {
            if (shipLevel >= 1)
            {
                Instantiate(bullet, spawnPoints[0].position, spawnPoints[0].rotation);
            }
            if (shipLevel >= 2)
            {
                Instantiate(bullet, spawnPoints[1].position, spawnPoints[1].rotation);
                Instantiate(bullet, spawnPoints[2].position, spawnPoints[2].rotation);
            }
            if (shipLevel >= 3)
            {
                Instantiate(bullet, spawnPoints[3].position, spawnPoints[3].rotation);
                Instantiate(bullet, spawnPoints[4].position, spawnPoints[4].rotation);
            }
        }

        if (Input.GetButtonDown("Jump") && specialValue > 1)
        {
            Instantiate(special, transform);
            specialValue = 0;
            GameManager.Instance.SetSpecial(specialValue);
        }
    }

    public void ChangeShipSkin()
    {
        // Troca a sprite da nave de acordo com o nivel do tiro dele

        if (shipLevel == 1)
        {
            GetComponent<SpriteRenderer>().sprite = shipSkin[0];

        }
        else if (shipLevel == 2)
        {
            GetComponent<SpriteRenderer>().sprite = shipSkin[1];

        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = shipSkin[2];
        }
    }

    public void SetShipLevel(int level)
    {
        // Seta o nivel de tiro dele
        shipLevel = level;
    }

    public void SetItemEffect(ItemEffect effect)
    {
        // Recebe um enum do item e verifica qual foi,
        // executando ações diferente para cada valor que recebeu
        // Ações como incremento , controle, instaciamento de objetos, play em diferentes sons e setar valores

        if(effect == ItemEffect.levelUp)
        {
            shipLevel++;
            audioS.clip = audiosC[1];
            audioS.Play();
            if (shipLevel >= 3)
            {                
                shipLevel = 3;
            }
        }
        else if(effect == ItemEffect.special)
        {
            specialValue++;
            audioS.clip = audiosC[2];
            audioS.Play();
            GameManager.Instance.SetSpecial(specialValue);
        }
        else if(effect == ItemEffect.shield)
        {
            Instantiate(shield, transform);
            audioS.clip = audiosC[0];
            audioS.Play();
        }
        else if (effect == ItemEffect.score)
        {            
            audioS.clip = audiosC[3];
            audioS.Play();
        }
    }
    
}
