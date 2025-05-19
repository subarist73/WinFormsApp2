using System.Text.Json;

namespace ProjectBinaryHeap
{
    public class HeapManager
    {
        private HeapOperator heapOperator;
        private DrawingHeap drawingHeap;
        private HeapParameters parameters;  // поле для хранения параметров
        private int currentInsertIndex;     // добавим поле для текущего индекса вставки

        public HeapManager(HeapParameters parameters)
        {
            this.parameters = parameters;
            heapOperator = new HeapOperator(parameters);
            drawingHeap = new DrawingHeap();
            currentInsertIndex = 0; // по умолчанию
        }

        // Возвращает параметры кучи
        public HeapParameters GetParameters()
        {
            return parameters;
        }

        public bool ExecuteStep()
        {
            return heapOperator.Step();
        }

        public bool Insert(int newElement)
        {
            return heapOperator.Insert(newElement);
        }

        public void SetExtractionMode(bool isMinHeap)
        {
            heapOperator.SetHeapType(isMinHeap);
        }

        public StateStorage GetStateStorage()
        {
            return heapOperator.GetStateStorage();
        }

        public DrawingHeap GetVisualiser()
        {
            return drawingHeap;
        }

        public void ResetExtractionState()
        {
            heapOperator.ResetState();
            currentInsertIndex = 0; // сброс индекса вставки при сбросе состояния
        }

        public void LoadOperatorStateFromStates(List<HeapState> states)
        {
            heapOperator.LoadFromStates(states);
        }

        public void RestoreInsertState(int insertIndex, List<int> heapData)
        {
            heapOperator.RestoreInsertState(insertIndex, heapData);
            currentInsertIndex = insertIndex + 1; // обновляем индекс вставки, следующий после последнего
        }

        public int GetCurrentInsertIndex()
        {
            return currentInsertIndex;
        }

        public void SetCurrentInsertIndex(int index)
        {
            currentInsertIndex = index;
        }

        internal bool IsStepInProgress()
        {
            throw new NotImplementedException();
        }

        // Теперь внутренний статический класс HeapPersistence без ошибок
        public static class HeapPersistence
        {
            public static void SaveToFile(string path, HeapManager heapManager)
            {
                var fullState = new HeapFullState
                {
                    Parameters = heapManager.GetParameters(),
                    States = heapManager.GetStateStorage().GetAllStates(),
                    CurrentStateIndex = heapManager.GetStateStorage().CurrentIndex,
                    CurrentInsertIndex = heapManager.GetCurrentInsertIndex()
                };

                var options = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(fullState, options);
                File.WriteAllText(path, json);
            }

            public static HeapManager LoadFromFile(string path)
            {
                string json = File.ReadAllText(path);
                var fullState = JsonSerializer.Deserialize<HeapFullState>(json);

                var heapManager = new HeapManager(fullState.Parameters);

                var storage = heapManager.GetStateStorage();
                storage.Reset();
                foreach (var state in fullState.States)
                {
                    storage.AddState(state);
                }

                storage.CurrentIndex = fullState.CurrentStateIndex;

                // Восстанавливаем состояние оператора из шагов
                heapManager.LoadOperatorStateFromStates(fullState.States);

                // Восстанавливаем состояние вставки и текущий индекс вставки
                int lastInsertedIndex = fullState.States.LastOrDefault()?.HeapData.Count - 1 ?? -1;
                heapManager.RestoreInsertState(lastInsertedIndex, fullState.States.LastOrDefault()?.HeapData ?? new List<int>());

                heapManager.SetCurrentInsertIndex(fullState.CurrentInsertIndex);

                return heapManager;
            }
        }
    }

    // Класс состояния с новым полем CurrentInsertIndex
    public class HeapFullState
    {
        public HeapParameters Parameters { get; set; }
        public List<HeapState> States { get; set; }
        public int CurrentStateIndex { get; set; }
        public int CurrentInsertIndex { get; set; }
    }
}
 
  