# Cloudline Station 🌌☁️

**Cloudline Station** é um jogo 2D de gerenciamento narrativo e simulação de penhores desenvolvido na Unity. O projeto foi construído para fins acadêmicos, focando na implementação de estruturas de dados e algoritmos clássicos de Ciência da Computação, sem o uso de middlewares ou pacotes externos para os sistemas centrais.

---

## 📌 Sobre o Jogo

Você assume o balcão da *Cloudline Station*, um posto de trocas isolado e suspenso acima de um mar de nuvens densas. Com uma dívida massiva com a Máfia das Nuvens, seu objetivo é sobreviver por 3 dias comprando e revendendo relíquias trazidas por viajantes excêntricos. 

O jogo desafia o jogador através de um loop de jogabilidade dividido em duas áreas:
1. **O Balcão (Frente):** Onde ocorrem as negociações através de árvores de diálogos complexas e ramificadas.
2. **O Estoque (Fundos):** Um quebra-cabeça de espaço físico onde cada item comprado se torna um obstáculo físico na matriz do cenário, exigindo organização lógica para não travar seus caminhos.

---

## 🛠️ Destaques Técnicos & Implementações

O diferencial deste projeto é a ausência de bibliotecas prontas da Unity para as mecânicas principais, priorizando a lógica pura em C#:

### 1. Pathfinding A* (Busca Heurística)
Implementação do algoritmo **A*** sobre uma matriz de nós bidimensional dinâmica para a movimentação *point-and-click* do personagem no estoque.
*   **Heurística Utilizada:** Distância de Manhattan.
*   **Dinâmica de Grid:** Itens comprados alteram a propriedade `isWalkable` dos nós em tempo real com base em seu tamanho físico ($1\times1$, $2\times2$, etc.), forçando o algoritmo a recalcular novas rotas dinamicamente para desviar dos obstáculos.

### 2. Árvore de Diálogos como Grafos Direcionados
Sistema de conversação estruturado matematicamente como um **Grafo Direcionado** $G = (V, E)$, onde os vértices ($V$) representam as falas dos NPCs e as arestas ($E$) representam as escolhas do jogador.
*   **Condicionais de Estado:** Os nós do grafo avaliam variáveis ocultas em tempo real (como o nível de paciência do NPC ou se o item passou por testes de falsificação na bancada) para abrir ou fechar ramificações de negociação, blefes ou confrontos.

### 3. Inventário Baseado em ScriptableObjects
Arquitetura de dados otimizada separando os metadados dos itens (IDs, valores reais, estados de roubo/falsificação) da interface do usuário (UI). Utiliza o gerenciamento de estados globais para transitar itens entre o inventário virtual e o mundo físico do estoque.
