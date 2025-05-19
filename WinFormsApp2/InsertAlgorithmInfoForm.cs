using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp2
{
    public partial class InsertAlgorithmInfoForm : Form
    {
        public InsertAlgorithmInfoForm(bool isMinHeap)
        {
            InitializeComponent();
            ShowInsertAlgorithmInfo(isMinHeap);
        }

        private void ShowInsertAlgorithmInfo(bool isMinHeap)
        {
            string text;

            if (isMinHeap)
            {
                text =
                    "Алгоритм вставки в бинарную кучу (Min-Heap):\n\n" +
                    "1. Добавить элемент в конец массива (в конец кучи).\n" +
                    "2. Пока добавленный элемент меньше своего родителя:\n" +
                    "   a. Обменять элемент с родителем.\n" +
                    "   b. Повторить проверку выше по дереву.\n" +
                    "3. Если родитель меньше или равен — остановиться.";
            }
            else
            {
                text =
                    "Алгоритм вставки в бинарную кучу (Max-Heap):\n\n" +
                    "1. Добавить элемент в конец массива (в конец кучи).\n" +
                    "2. Пока добавленный элемент больше своего родителя:\n" +
                    "   a. Обменять элемент с родителем.\n" +
                    "   b. Повторить проверку выше по дереву.\n" +
                    "3. Если родитель больше или равен — остановиться.";
            }

            richTextBox1.Text = text; // Добавь RichTextBox на форму
        }
    }

}
