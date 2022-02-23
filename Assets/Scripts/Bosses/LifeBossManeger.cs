using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LifeBossManeger : MonoBehaviour
{
    public Image bosslifeBar;
    private int maxBossLife;
    public CharacterLife bossLife;
    public TextMeshProUGUI textLife;

    private void Start()
    {
        maxBossLife = bossLife.health;
    }

    private void Update()
    {
        BossLifeBarUpdate();
    }  
    
    public void BossLifeBarUpdate()
    {
        // Atualiza a barra de preenchimento da vida do boss pelo fillAmount transformando o valor atual da vida dele em um valor entre 0 e 1
        // e escrevendo o valor total da vida dentro da barra, no final a destroi quando ele morre

        if (bossLife.health <= 0 && textLife != null)
        {
            textLife.text = "0";
            Destroy(gameObject, 3f);
        }
        else if (bossLife.health > 0 && textLife != null)
        {
            textLife.text = bossLife.health.ToString();
        }

        bosslifeBar.fillAmount = (bossLife.health * 1f / maxBossLife);
    }
}
