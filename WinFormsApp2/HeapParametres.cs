namespace ProjectBinaryHeap;

public class HeapParameters
{
    public List<int> InitialData { get; set; }
    public bool IsMinHeap { get; set; }

    public HeapParameters(List<int> data, bool isMinHeap)
    {
        InitialData = data;
        IsMinHeap = isMinHeap;
    }
}