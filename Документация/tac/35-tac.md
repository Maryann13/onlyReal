# Определение глубины CFG !heading

### Команда Roll

#### Постановка задачи
Написать метод определения глубины CFG.

#### Зависимости задач в графе задач

Задача зависит от:
* Построение CFG

#### Теория
Глубиной графа потока управления CFG называется наибольшее количество отступающих ребер по всем ациклическим путям. Если граф приводим, то глубина не зависит от глубинного остовного дерева.


#### Особенности реализации

Для определения глубины графа необходимо найти самый длинный путь, состоящий лишь из обратных дуг. В данной реализации для графа с правильной нумерацией дуга является обратной, если она ведет из узла с большим индексом в узел с меньшим индексом.
Алгоритм находит все обратные дуги, а затем для каждой из них ищет путь наибольшей длины.

``` csharp
public static int GetGraphDepth(ControlFlowGraph graph)
{
    int maxDepth = 0;
    var backwardArcs = new Dictionary<int, int>();

    for(int nodeId = 0; nodeId < graph.blocks.Count; nodeId++)
    {
        var successors = graph.cfg.GetOutputNodes(nodeId);
        foreach (var s in successors)
            if(s <= nodeId)                    
                backwardArcs.Add(nodeId, s);                
    }

    foreach(var arc in backwardArcs)
    {
        int currentDepth = 0;
        var currentNode = arc.Key;
        while(backwardArcs.ContainsKey(currentNode))
        {
            currentNode = backwardArcs[currentNode];
            currentDepth++;
        }
        maxDepth = currentDepth > maxDepth ? currentDepth : maxDepth; 
    }
    return maxDepth;
}
```
Для использования данной оптимизации необходимо:
1. Подключить пространство имен using SimpleLang.ControlFlowGraph;
2. Построить граф с правильной нумерацией вершин
5. Применить метод вычисления глубинй к графу

Ниже представлен код использования данного метода:
```
// построение CFG по блокам
CFG controlFlowGraph = new CFG(blocks);

```

#### Тесты
``` csharp
Исходный код программы
{
int a, b;
for(a=1 to 5)
{
while(a)
{
    b = 5;
}
}
a = 2;
}

Блоки трехадресного кода            a = 1
label_0:   temp_0 = a < 5
           if temp_0 goto label_1
           goto label_2
label_1:   temp_1 = a
           if temp_1 goto label_3
           goto label_4
label_3:   b = 5
           goto label_1
label_4:
           a = a + 1
           goto label_0
label_2:   a = 2
```
Глубина графа для данного трехадресного кода равна 1
![](CFG.png)

[Вверх](#содержание)