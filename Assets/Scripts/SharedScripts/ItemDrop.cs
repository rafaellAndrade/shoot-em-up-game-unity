using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemDrop : MonoBehaviour
{
    public ItemEffect effect;
    public int score;
    public GameObject textScore;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se o objeto colidido era o player e se continha o GameObject textScore,
        // Caso sim, ele chama a função do efeito do item no script do player passando o efeito,
        // Caso haja um GameObject textScore ele pega o componente Canvas dentro dele, que por sua vez pega o Text dentro dele
        // alterando seu valor textual e logo após é instanciado no jogo na posição do item,
        // mostrando na tela o valor dos pontos que aquele item dá (score),
        // Depois incrementa a pontuação na tela e por fim destroi o item

        PlayerAttack player = other.GetComponent<PlayerAttack>();
        if(player != null)
        {
            player.SetItemEffect(effect);
            if(textScore != null)
            {
                textScore.GetComponent<Transform>().GetChild(0).GetComponent<Transform>().GetChild(0).GetComponent<TextMeshProUGUI>().text = score.ToString();                
                Instantiate(textScore, transform.position, Quaternion.identity);
                GameManager.Instance.SetScore(score);
            }
            Destroy(gameObject);
        }
    }
}
