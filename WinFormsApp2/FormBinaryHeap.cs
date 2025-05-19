using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using ProjectBinaryHeap;
using System.Text.Json;
using static ProjectBinaryHeap.HeapManager;




namespace WinFormsApp2
{
    public partial class FormBinaryHeap : Form
    {
        private HeapManager heapManager;
        private HeapParameters parameters;
        private DrawingHeap drawingHeap;
        private int width;


        public FormBinaryHeap()
        {
            InitializeComponent();
            InitializeHeap();
        }
        private void InitializeHeap()

        {
            // Пример инициализации параметров - пустой список, мин-куча
            parameters = new HeapParameters(new List<int>(), isMinHeap: true);
            heapManager = new HeapManager(parameters);
            UpdateStatus("Куча инициализирована");
            RedrawHeap();
        }
        private void UpdateStatus(string message)
        {
            // Можно добавить метку статуса на форму и обновлять её здесь
            // Если нет, можно временно использовать MessageBox или Debug
            // Например, если есть label lblStatus:
            // lblStatus.Text = message;
            Console.WriteLine(message); // для примера
        }
        private void SaveFullStateToTxt(string path)
        {
            var storage = heapManager.GetStateStorage();
            var parametersToSave = this.parameters; // <--- ВАЖНО: исходные параметры
            var sb = new System.Text.StringBuilder();

            // Исходные параметры
            sb.AppendLine("=== Начальные параметры ===");
            sb.AppendLine($"IsMinHeap: {parametersToSave.IsMinHeap}");
            sb.AppendLine("InitialData: " + string.Join(", ", parametersToSave.InitialData));
            sb.AppendLine();

            // Все шаги
            sb.AppendLine("=== Шаги вставки ===");

            int stepNum = 1;
            foreach (var state in storage.GetAllStates())
            {
                sb.AppendLine($"-- Шаг {stepNum++} --");
                sb.AppendLine("Описание:");
                sb.AppendLine(state.StepDescription);
                sb.AppendLine("Куча:");
                sb.AppendLine(string.Join(", ", state.HeapData));
                sb.AppendLine("Подсвеченные индексы:");
                sb.AppendLine(state.HighlightedIndices.Count > 0 ? string.Join(", ", state.HighlightedIndices) : "нет");
                sb.AppendLine();
            }

            // Добавляем индекс текущего вставляемого элемента
            sb.AppendLine($"CurrentInsertIndex: {currentInsertIndex}");

            System.IO.File.WriteAllText(path, sb.ToString());
            UpdateStatus($"Полное состояние сохранено в файл: {path}");
        }





        private HeapParameters LoadParametersFromTxt(string[] lines, ref int lineIndex)
        {
            bool isMinHeap = false;
            List<int> initialData = new List<int>();

            while (lineIndex < lines.Length && !lines[lineIndex].StartsWith("InitialData:"))
            {
                if (lines[lineIndex].StartsWith("IsMinHeap:"))
                    isMinHeap = bool.Parse(lines[lineIndex].Split(":")[1].Trim());
                lineIndex++;
            }

            if (lineIndex < lines.Length && lines[lineIndex].StartsWith("InitialData:"))
            {
                var dataPart = lines[lineIndex].Substring("InitialData:".Length).Trim();
                if (!string.IsNullOrEmpty(dataPart))
                    initialData = dataPart.Split(',').Select(s => int.Parse(s.Trim())).ToList();
                lineIndex++;
            }

            return new HeapParameters(initialData, isMinHeap);
        }

        private List<HeapState> LoadStatesFromTxt(string[] lines, ref int lineIndex)
        {
            var states = new List<HeapState>();

            while (lineIndex < lines.Length)
            {
                if (lines[lineIndex].StartsWith("-- Шаг"))
                {
                    lineIndex++;
                    if (lines[lineIndex] == "Описание:")
                        lineIndex++;

                    var description = "";
                    while (lineIndex < lines.Length && lines[lineIndex] != "Куча:")
                    {
                        description += lines[lineIndex] + "\n";
                        lineIndex++;
                    }
                    description = description.Trim();

                    List<int> heapData = new List<int>();
                    if (lineIndex < lines.Length && lines[lineIndex] == "Куча:")
                    {
                        lineIndex++;
                        if (lineIndex < lines.Length)
                            heapData = lines[lineIndex].Split(',').Select(s => int.Parse(s.Trim())).ToList();
                        lineIndex++;
                    }

                    List<int> highlightedIndices = new List<int>();
                    if (lineIndex < lines.Length && lines[lineIndex] == "Подсвеченные индексы:")
                    {
                        lineIndex++;
                        if (lineIndex < lines.Length)
                        {
                            var highlighted = lines[lineIndex].Trim();
                            if (highlighted != "нет" && highlighted.Length > 0)
                                highlightedIndices = highlighted.Split(',').Select(s => int.Parse(s.Trim())).ToList();
                            lineIndex++;
                        }
                    }

                    states.Add(new HeapState(heapData, description, highlightedIndices));
                }
                else
                {
                    lineIndex++;
                }
            }

            return states;
        }
        private void LoadFullStateFromTxt(string path)
        {
            try
            {
                var (isMinHeap, initialData, steps, loadedInsertIndex) = ParseHeapLog(path);

                parameters = new HeapParameters(initialData, isMinHeap);

                heapManager = new HeapManager(parameters);

                var storage = heapManager.GetStateStorage();
                storage.Reset();
                foreach (var step in steps)
                    storage.AddState(step);
                storage.CurrentIndex = steps.Count - 1;

                heapManager.LoadOperatorStateFromStates(steps);

                heapManager.SetCurrentInsertIndex(loadedInsertIndex);

                // ВАЖНО: обновляем текущее значение для вставки в форме
                this.currentInsertIndex = loadedInsertIndex;

                UpdateUIAfterLoad(); // обновляем интерфейс
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки: " + ex.Message);
            }
        }
        private void UpdateUIAfterLoad()
        {
            // Перерисовать кучу
            RedrawHeap();

            // Обновить статус (если есть label или просто вывод в консоль)
            UpdateStatus("Состояние кучи загружено");
        }









        private void RedrawHeap()
        {
            if (heapManager == null)
                return;

            var states = heapManager.GetStateStorage()?.GetAllStates();
            if (states == null || states.Count == 0)
                return;

            var currentState = states[states.Count - 1];
            var heapData = currentState.HeapData;
            var highlightedIndices = currentState.HighlightedIndices ?? new List<int>();
            var description = currentState.StepDescription ?? "";

            if (label1 != null)
                label1.Text = description;

            using (Graphics g = pictureBox1.CreateGraphics())
            {
                g.Clear(Color.White);
                DrawHeap(g, heapData, highlightedIndices, this.Font, pictureBox1.Width);
            }
        }
        private (bool IsMinHeap, List<int> InitialData, List<HeapState> Steps, int CurrentInsertIndex) ParseHeapLog(string path)
        {
            var lines = File.ReadAllLines(path);
            bool isMinHeap = false;
            List<int> initialData = new List<int>();
            List<HeapState> steps = new List<HeapState>();
            int currentInsertIndex = -1;

            int i = 0;
            while (i < lines.Length)
            {
                var line = lines[i].Trim();

                if (line.StartsWith("IsMinHeap:"))
                {
                    var val = line.Substring("IsMinHeap:".Length).Trim();
                    isMinHeap = bool.Parse(val);
                    i++;
                }
                else if (line.StartsWith("InitialData:"))
                {
                    var dataStr = line.Substring("InitialData:".Length).Trim();
                    if (!string.IsNullOrEmpty(dataStr))
                        initialData = dataStr.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries)
                                             .Select(int.Parse).ToList();
                    i++;
                }
                else if (line.StartsWith("-- Шаг"))
                {
                    var step = new HeapState();

                    i += 2; // Пропускаем строку "Описание:" и переходим к описанию шага

                    step.StepDescription = lines[i].Trim();
                    i++;

                    i++; // Пропускаем строку "Куча:"

                    var heapDataLines = new List<string>();
                    while (i < lines.Length && !lines[i].Trim().StartsWith("Подсвеченные индексы:") && !string.IsNullOrEmpty(lines[i].Trim()))
                    {
                        heapDataLines.Add(lines[i].Trim());
                        i++;
                    }

                    var heapDataStr = string.Join(" ", heapDataLines);
                    if (!string.IsNullOrWhiteSpace(heapDataStr))
                        step.HeapData = heapDataStr.Split(new[] { ',', ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();

                    i++; // Строка "Подсвеченные индексы:"

                    if (i < lines.Length)
                    {
                        var hl = lines[i].Trim();
                        if (hl != "нет")
                            step.HighlightedIndices = hl.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
                        i++;
                    }

                    steps.Add(step);
                }
                else if (line.StartsWith("CurrentInsertIndex:"))
                {
                    var val = line.Substring("CurrentInsertIndex:".Length).Trim();
                    int.TryParse(val, out currentInsertIndex);
                    i++;
                }
                else
                {
                    i++;
                }
            }

            return (isMinHeap, initialData, steps, currentInsertIndex);
        }



        private void DrawHeap(Graphics g, List<int> heap, List<int> highlightedIndices, Font font, int width)
        {
            if (heap == null || heap.Count == 0)
                return;

            int radius = 20;
            int level = 0;
            int heightStep = 60;

            Queue<(int index, int x, int y)> nodes = new Queue<(int, int, int)>();
            nodes.Enqueue((0, width / 2, 30));

            while (nodes.Count > 0)
            {
                var (i, x, y) = nodes.Dequeue();
                if (i >= heap.Count)
                    continue;

                Brush fillBrush = highlightedIndices.Contains(i) ? Brushes.Red : Brushes.LightBlue;

                Rectangle rect = new Rectangle(x - radius, y - radius, radius * 2, radius * 2);
                g.FillEllipse(fillBrush, rect);
                g.DrawEllipse(Pens.Blue, rect);

                // Рисуем значение
                string val = heap[i].ToString();
                var valSize = g.MeasureString(val, font);
                g.DrawString(val, font, Brushes.Black, x - valSize.Width / 2, y - valSize.Height / 2);

                // Рисуем индекс под узлом
                string indexStr = $"[{i}]";
                var idxSize = g.MeasureString(indexStr, font);
                g.DrawString(indexStr, font, Brushes.Gray, x - idxSize.Width / 2, y + radius);

                int childY = y + heightStep;
                int offsetX = width / (int)Math.Pow(2, level + 2);

                int leftChild = 2 * i + 1;
                int rightChild = 2 * i + 2;

                if (leftChild < heap.Count)
                {
                    g.DrawLine(Pens.Black, x, y + radius, x - offsetX, childY - radius);
                    nodes.Enqueue((leftChild, x - offsetX, childY));
                }
                if (rightChild < heap.Count)
                {
                    g.DrawLine(Pens.Black, x, y + radius, x + offsetX, childY - radius);
                    nodes.Enqueue((rightChild, x + offsetX, childY));
                }

                if (nodes.Count > 0 && nodes.Peek().y > y)
                    level++;
            }
        }



        // Обработчик кнопки "Добавить элемент"

        private void button1_Click(object sender, EventArgs e)
        {

            FormInput inputForm = new FormInput();
            inputForm.ParametersSubmitted += OnParametersSubmitted;
            inputForm.ShowDialog(); // модально  
        }
        private void OnParametersSubmitted(object sender, HeapParameters parameters)
        {// Создаем HeapManager с пустым списком, но сохраняя флаг мин-кучи
            var emptyParams = new HeapParameters(new List<int>(), parameters.IsMinHeap);
            heapManager = new HeapManager(emptyParams);

            // Сохраняем все параметры, чтобы вставлять элементы по одному
            this.parameters = parameters;
            currentInsertIndex = 0;

            UpdateStatus("Параметры введены. Нажмите кнопку 'Выполнить шаг' для вставки элементов.");
            RedrawHeap();  // Показать пустую кучу
                           // Показываем кучу с текущими элементами (если они есть)
        }


        private int currentInsertIndex = 0;
        private void button2_Click(object sender, EventArgs e)
        {
            // Вставляем следующий элемент, если не началась вставка
            if (!heapManager.ExecuteStep())
            {
                if (currentInsertIndex < parameters.InitialData.Count)
                {
                    heapManager.Insert(parameters.InitialData[currentInsertIndex]);
                    currentInsertIndex++;
                    UpdateStatus($"Добавляется элемент: {parameters.InitialData[currentInsertIndex - 1]}");
                }
                else
                {
                    UpdateStatus("Все элементы добавлены");
                    return;
                }
            }

            // Выполняем шаг
            RedrawHeap();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            InitializeHeap();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Text Files (*.txt)|*.txt";
                sfd.Title = "Сохранить состояние кучи";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    SaveFullStateToTxt(sfd.FileName);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Text Files (*.txt)|*.txt";
                ofd.Title = "Загрузить состояние кучи";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    LoadFullStateFromTxt(ofd.FileName);
                }
            }
        }

        /*   private void button6_Click(object sender, EventArgs e)
           {
               using (SaveFileDialog sfd = new SaveFileDialog())
               {
                   sfd.Filter = "Text Files (*.txt)|*.txt";
                   sfd.Title = "Сохранить состояние кучи";
                   if (sfd.ShowDialog() == DialogResult.OK)
                   {
                       SaveFullStateToTxt(sfd.FileName);
                   }
               }

           }*/

        private void button7_Click(object sender, EventArgs e)
        {
            /*  using (OpenFileDialog ofd = new OpenFileDialog())
              {
                  ofd.Filter = "Text Files (*.txt)|*.txt";
                  ofd.Title = "Загрузить состояние кучи";
                  if (ofd.ShowDialog() == DialogResult.OK)
                  {
                      LoadFullStateFromTxt(ofd.FileName);
                  }
              }*/
            if (parameters == null)
            {
                MessageBox.Show("Сначала задайте параметры кучи.");
                return;
            }

            var infoForm = new InsertAlgorithmInfoForm(parameters.IsMinHeap);
            infoForm.ShowDialog();
        }

        /* private void button8_Click(object sender, EventArgs e)
         {
             if (parameters == null)
             {
                 MessageBox.Show("Сначала задайте параметры кучи.");
                 return;
             }

             var infoForm = new InsertAlgorithmInfoForm(parameters.IsMinHeap);
             infoForm.ShowDialog();
         }*/




        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {
            if (parameters == null)
            {
                MessageBox.Show("Сначала задайте параметры кучи.");
                return;
            }

            var infoForm = new InsertAlgorithmInfoForm(parameters.IsMinHeap);
            infoForm.ShowDialog();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void FormBinaryHeap_Load(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }



        private void toolStripComboBox2_Click(object sender, EventArgs e)
        {
            if (parameters == null)
            {
                MessageBox.Show("Сначала задайте параметры кучи.");
                return;
            }

            var infoForm = new InsertAlgorithmInfoForm(parameters.IsMinHeap);
            infoForm.ShowDialog();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void поставьтеМаксБаллToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Text Files (*.txt)|*.txt";
                sfd.Title = "Сохранить состояние кучи";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    SaveFullStateToTxt(sfd.FileName);
                }
            }
        }

        private void слЗагрToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Text Files (*.txt)|*.txt";
                ofd.Title = "Загрузить состояние кучи";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    LoadFullStateFromTxt(ofd.FileName);
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            {
                if (heapManager == null)
                {
                    UpdateStatus("Куча не инициализирована.");
                    return;
                }

                if (!heapManager.IsStepInProgress())// ❗Проверка, не в процессе ли вставки
                {
                    MessageBox.Show("Завершите текущую вставку перед добавлением нового элемента.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(textBox1.Text.Trim(), out int newValue))
                {
                    MessageBox.Show("Введите корректное целое число.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Вставка нового элемента
                heapManager.Insert(newValue);
                UpdateStatus($"Добавлен элемент: {newValue}");

                // Для отображения нового состояния
                RedrawHeap();

                textBox1.Clear();
            }
        }
    }
}



