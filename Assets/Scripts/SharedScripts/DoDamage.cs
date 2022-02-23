using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoDamage : MonoBehaviour
{
    public int damage = 1;

    public bool destroyByContact = true;
    public bool destroyShoots = false;    
    public int bySeconds = 1;    

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se o objeto colidido era o player, armadilha, chefe e se continha um script DoDamage
        // Dependendo da condiçao, ele destrói o objeto com o qual se colidiu ou se auto destrói
        CharacterLife character = other.GetComponent<CharacterLife>();
        if(character != null)
        {           
            StartCoroutine(DamagePerSecond(other, character));                       

            if (destroyByContact)
            {
                Destroy(gameObject);
            }           
        }

        DoDamage shoot = other.GetComponent<DoDamage>();
        TrapDamage shootTrap = other.GetComponent<TrapDamage>();
        BossShoot bossShot = other.GetComponent<BossShoot>();
        if (shoot != null && destroyShoots && bossShot == null || shootTrap != null && destroyShoots && bossShot == null)
        {
            Destroy(other.gameObject);
        }
    }

    IEnumerator DamagePerSecond(Collider2D other, CharacterLife character)
    {
        // Realiza um dano a cada segundo no objeto colidido, como se fosse um burn ou posion
        for (int i = 0; i < bySeconds; i++)
        {
            if (other != null)
            {
                character.TakeDamage(damage);
                yield return new WaitForSeconds(1f);
            }
        }        
    }
}
