namespace ProjectBinaryHeap;

using System;
using System.Collections.Generic;

[Serializable]
public class StateStorage
{
    public List<HeapState> States { get; private set; }
    private int currentIndex;

    public StateStorage()
    {
        States = new List<HeapState>();
        currentIndex = 0;
    }

    public void AddState(HeapState state)
    {
        States.Add(state);
    }

    public HeapState GetNextState()
    {
        return currentIndex < States.Count ? States[currentIndex++] : null;
    }

    public void Reset()
    {
        currentIndex = 0;
        States.Clear();
    }

    public List<HeapState> GetAllStates() => States;

    public HeapState GetLastState() => States.Count > 0 ? States[^1] : null;
    public int CurrentIndex
    {
        get => currentIndex;
        set => currentIndex = value;
    }


}

