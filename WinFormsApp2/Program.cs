namespace ProjectBinaryHeap;
using System;
using System.Windows.Forms;

internal static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new WinFormsApp2.FormBinaryHeap());
    }
}