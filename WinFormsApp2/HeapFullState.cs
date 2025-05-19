using ProjectBinaryHeap;

public class HeapFullState
{
    public HeapParameters Parameters { get; set; }
    public List<HeapState> States { get; set; }
    public int CurrentStateIndex { get; set; }
    public int CurrentInsertIndex { get; set; }  // добавь сюда
}
