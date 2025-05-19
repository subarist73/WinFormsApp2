public class HeapState
{
    public List<int> HeapData { get; set; } = new List<int>();
    public string StepDescription { get; set; } = string.Empty;
    public List<int> HighlightedIndices { get; set; } = new List<int>();

    public HeapState() { }

    public HeapState(List<int> heapData, string stepDescription, List<int> highlightedIndices = null)
    {
        HeapData = heapData != null ? new List<int>(heapData) : new List<int>();
        StepDescription = stepDescription ?? string.Empty;
        HighlightedIndices = highlightedIndices != null ? new List<int>(highlightedIndices) : new List<int>();
    }
}
