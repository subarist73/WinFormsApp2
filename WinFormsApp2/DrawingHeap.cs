using System;
using System.Collections.Generic;
using System.Drawing;

namespace ProjectBinaryHeap;

public class DrawingHeap
{
    private const int NodeRadius = 20;
    private const int HorizontalSpacing = 50;
    private const int VerticalSpacing = 60;

    /// <summary>
    /// Рисует текущее состояние кучи на графике (Graphics).
    /// </summary>
    /// <param name="g">Графический контекст для рисования</param>
    /// <param name="heapData">Список элементов кучи</param>
    /// <param name="highlightedIndices">Индексы элементов, которые нужно подсветить</param>
    /// <param name="font">Шрифт для текста</param>
    public void DrawHeap(Graphics g, List<int> heapData, List<int> highlightedIndices, Font font)
    {
        if (heapData == null || heapData.Count == 0)
            return;

        var positions = CalculateNodePositions(heapData.Count);

        for (int i = 0; i < heapData.Count; i++)
        {
            bool isHighlighted = highlightedIndices.Contains(i);
            DrawNode(g, positions[i], heapData[i], isHighlighted, font);
        }

        // Нарисовать линии между узлами (ребра)
        for (int i = 0; i < heapData.Count; i++)
        {
            int leftChild = 2 * i + 1;
            int rightChild = 2 * i + 2;

            if (leftChild < heapData.Count)
            {
                DrawEdge(g, positions[i], positions[leftChild]);
            }

            if (rightChild < heapData.Count)
            {
                DrawEdge(g, positions[i], positions[rightChild]);
            }
        }
    }

    private List<Point> CalculateNodePositions(int count)
    {
        var positions = new List<Point>();

        for (int i = 0; i < count; i++)
        {
            int level = (int)Math.Floor(Math.Log(i + 1, 2));
            int maxNodesOnLevel = (int)Math.Pow(2, level);
            int indexInLevel = i - (maxNodesOnLevel - 1);

            int x = (HorizontalSpacing * (2 * indexInLevel + 1)) * (int)Math.Pow(2, (int)Math.Log(count, 2) - level);
            int y = VerticalSpacing * level + NodeRadius;

            positions.Add(new Point(x, y));
        }

        return positions;
    }

    private void DrawNode(Graphics g, Point center, int value, bool highlighted, Font font)
    {
        Color fillColor = highlighted ? Color.OrangeRed : Color.LightBlue;
        Color borderColor = Color.Black;

        using (Brush brush = new SolidBrush(fillColor))
        using (Pen pen = new Pen(borderColor, 2))
        {
            Rectangle rect = new Rectangle(center.X - NodeRadius, center.Y - NodeRadius, NodeRadius * 2, NodeRadius * 2);
            g.FillEllipse(brush, rect);
            g.DrawEllipse(pen, rect);

            string text = value.ToString();
            SizeF textSize = g.MeasureString(text, font);
            PointF textPos = new PointF(center.X - textSize.Width / 2, center.Y - textSize.Height / 2);
            g.DrawString(text, font, Brushes.Black, textPos);
        }
    }

    private void DrawEdge(Graphics g, Point parent, Point child)
    {
        using (Pen pen = new Pen(Color.Black, 2))
        {
            g.DrawLine(pen, parent, child);
        }
    }
}
