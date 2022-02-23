# Shoot'em Up game Unity PC :video_game: :alien:
Jogo estilo Shoot'em up desenvolvido na Unity conta com um boss que é spawnado quando atinge 200 pontos
* Pontuação para spawn do boss configurável direto no objeto GameManeger na unity

### Boss
* Boss com 4 tipos de ataques disparados aleatoriamente
* Barra de vida com o valor numérico dentro da barra

### Controle
* Movimentação com as teclas A, W, S, D ou as setas direcionais do teclado ou Pad Numerico
* Special é disparado com a barra de espaço
* Tiros são disparados com o Click Esquerdo do Mouse

### Nave do jogador
* Morre com 1 hit e dá respawn
* Sempre volta pro mesmo ponto quando morre
* 3 tipos de tiros com troca de skin

### Nave inimiga 4 tipos 
* x pontos de vida, configurável no prefab
* Vale y pontos de pontuação, configurável no prefab
* 1 tipo de tiro por tipo de inimigo
* Aparece fora da tela e passa por ela, sendo removido ao sair do outro lado ( tirando o disco voador que segue o player)

### 4 tipos de coletáveis que é dropado aleatoriamente pelos inimigos
* Special - Necessitar coletar dois para usar o especial
* Pontuação - Dá uma quantidade de pontuação ( configurável no prefab ), mostra na tela a quantidade de pontos que ele deu na posição do coletável
* Upgrade - Aumenta a quantidade de tiros e muda a skin da nave - Nivel máximo 3
* Shield - Protege o player de 1 hit ( tirando o Laser Final do boss ) 

#### Interface mostrando quantos pontos já fez
#### Sistema de geração aleatória dos inimigos e itens coletáveis.
