using System.Collections.Generic;

namespace ProjectBinaryHeap;

public class HeapOperator
{
    private HeapParameters parameters;
    private List<int> heapData;
    private StateStorage stateStorage;
    private int insertIndex;  // индекс вставки текущего элемента
    private bool isInserting;
    private bool isMinHeap;
    private int newElement;

    public HeapOperator(HeapParameters parameters)
    {
        this.parameters = parameters;
        this.heapData = new List<int>(parameters.InitialData);
        this.stateStorage = new StateStorage();
        this.isMinHeap = parameters.IsMinHeap;
        ResetState();
    }

    public bool Insert(int element)
    {
        if (isInserting) return false; // Уже идет вставка

        newElement = element;
        heapData.Add(newElement);
        insertIndex = heapData.Count - 1;
        isInserting = true;

        SaveState($"Добавлен элемент {element} в конец кучи", new List<int> { insertIndex });
        return true;
    }

    public bool Step()
    {
        if (!isInserting)
            return false;

        if (insertIndex == 0)
        {
            isInserting = false;
            SaveState("Вставка завершена", new List<int>());
            return false;
        }

        int parentIndex = (insertIndex - 1) / 2;
        bool needSwap = isMinHeap
            ? heapData[insertIndex] < heapData[parentIndex]
            : heapData[insertIndex] > heapData[parentIndex];

        if (needSwap)
        {
            int fromIndex = insertIndex;
            int toIndex = parentIndex;

            Swap(fromIndex, toIndex);
            insertIndex = toIndex;

            SaveState($"Элемент перемещается вверх с индекса {fromIndex} на индекс {toIndex}",
                new List<int> { fromIndex, toIndex });

            return true;
        }
        else
        {
            isInserting = false;
            SaveState("Вставка завершена", new List<int>());
            return false;
        }
    }


    public void SetHeapType(bool isMinHeap)
    {
        this.isMinHeap = isMinHeap;
    }

    public StateStorage GetStateStorage()
    {
        return stateStorage;
    }

    public void ResetState()
    {
        isInserting = false;
        stateStorage.Reset();
    }

    private void Swap(int i, int j)
    {
        int temp = heapData[i];
        heapData[i] = heapData[j];
        heapData[j] = temp;
    }

    private void SaveState(string description, List<int> highlightedIndices)
    {
        stateStorage.AddState(new HeapState(new List<int>(heapData), description, highlightedIndices));
    }

    public void LoadFromStates(List<HeapState> states)
    {
        if (states == null || states.Count == 0)
        {
            ResetState();
            return;
        }

        var lastState = states[states.Count - 1];
        this.heapData = new List<int>(lastState.HeapData);

        // Определяем, идёт ли вставка, по описанию шага
        if (lastState.StepDescription.Contains("Вставка завершена"))
        {
            isInserting = false;
            insertIndex = -1;
        }
        else if (lastState.HighlightedIndices != null && lastState.HighlightedIndices.Count > 0)
        {
            isInserting = true;
            // Обычно последний подсвеченный индекс — это индекс вставляемого элемента
            insertIndex = lastState.HighlightedIndices.Last();
        }
        else
        {
            // Если нет подсветки — вставка не идёт
            isInserting = false;
            insertIndex = -1;
        }
    }
    public void RestoreInsertState(int insertIndex, List<int> heapData)
    {
        this.heapData = new List<int>(heapData);

        if (insertIndex >= 0 && insertIndex < heapData.Count)
        {
            this.insertIndex = insertIndex;
            this.isInserting = true;
            this.newElement = heapData[insertIndex];
        }
        else
        {
            this.isInserting = false;
            this.insertIndex = -1;
        }
    }



}

