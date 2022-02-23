using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public float startWait;
    public float spawnWaitMin;
    public float waveWait;
    public float waveWaitMin;    
    private float initialWaveWait;
    

    private int enemyCount = 1;
    public int enemyCountMax = 10;
    private int score;
    public int amountBossToAppear = 500;

    private bool isBoss = false;
    private bool bossAlreadyAppeared = false;
    
    public GameObject textPressSpace;
    public Image imageSpecial; 

    public GameObject[] enemies;
    public GameObject boss;
    public Transform bossPosition;

    private Vector2 screenBounds;
    public Vector2 spawnWait;
    private Vector2 InitialspawnWait;

    public TextMeshProUGUI scoreText;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {        
        initialWaveWait = waveWait;
        InitialspawnWait = spawnWait;

        
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        StartCoroutine(SpawnWaves());
    }

    public void ResetSpawnValues()
    {
        // Reseta os valores de spawn para quando o boss morrer, não nascer um monte de uma vez e sim começar a incrementar
        // a velocidade e quantidade novamente

        waveWait = initialWaveWait;
        spawnWait = InitialspawnWait;
        enemyCount = 1;
    }
  

    IEnumerator SpawnWaves()
    {
        // Verifica se o boss está na tela, caso sim, o spawn do inimigo é interrompido
        // Caso não, sorteia um inimigo presente no array e ele é instanciado na parte de cima da tela, usando as medidas da camera do jogo
        // Diminui o tempo de espera entre os instanciamentos e aumenta a quantidade de inimigos instanciados ao mesmo tempo

        
        yield return new WaitForSeconds(startWait);
        while (!isBoss)
        {
            for(int i = 0; i < enemyCount; i++)
            {
                GameObject enemy = enemies[Random.Range(0, enemies.Length)];
                float enemyWidth = enemy.GetComponent<SpriteRenderer>().bounds.size.x / 2;

                Vector3 spawnPosition = new Vector3(Random.Range(screenBounds.x * -1 + enemyWidth, screenBounds.x), screenBounds.y + 1, 0);
                Instantiate(enemy, spawnPosition, Quaternion.identity);
                yield return new WaitForSeconds(Random.Range(spawnWait.x, spawnWait.y));
            }

            enemyCount++;
            if(enemyCount >= enemyCountMax)
            {
                enemyCount = 1;
            }

            spawnWait.x -= 0.1f;
            spawnWait.y -= 0.1f;
            if(spawnWait.y <= spawnWaitMin)
            {
                spawnWait.y = spawnWaitMin;
            }
            if (spawnWait.x <= spawnWaitMin)
            {
                spawnWait.x = spawnWaitMin;
            }

            yield return new WaitForSeconds(waveWait);
            waveWait -= 0.1f;
            if(waveWait <= waveWaitMin)
            {
                waveWait = waveWaitMin;
            }
        }
    }

    IEnumerator BossComing()
    {    
        // O boss é instanciado no jogo
        yield return new WaitForSeconds(3f);
        Instantiate(boss, bossPosition.position, bossPosition.rotation);
    }

    public void SetScore(int scorePoints)
    {
        // Função para atualizar a pontuação do jogo e verificar se o boss pode ser instanciado ou não
        score += scorePoints;
        scoreText.text = score.ToString();

        if(score >= amountBossToAppear && !isBoss && !bossAlreadyAppeared)
        {            
            isBoss = true;
            bossAlreadyAppeared = true;
            StartCoroutine(BossComing());
            ResetSpawnValues();
        }
    }

    public void ReloadScene()
    {
        // Usado no botão do canto superior esquerdo do jogo apenas para reiniciar a cena atual
        // Reload Button
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        // Usado no botão Quit Button para fechar a aplicação

        Application.Quit();
    }

    public void SetSpecial(int value)
    {
        // Preenche a barra de especial do player na parte superior da tela, e ativa o texto Pressione espaço quando a barra está completa
        // desativando quando não
        imageSpecial.fillAmount = value / 2f;

       if(value > 1)
        {
            textPressSpace.SetActive(true);
        }else
        {
            textPressSpace.SetActive(false);
        }
    }

    public void SetIsBoss()
    {
        // Usado para setar que o boss não está no jogo, reiniciando os spawns do inimigo
        isBoss = false;
        StartCoroutine(SpawnWaves());
    }
}
