using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MatrixApp
{
    public partial class MainWindow : Window
    {
        private int[,] matrix;
        private int rows;
        private int cols;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void CreateMatrix_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(RowsTextBox.Text, out rows) && int.TryParse(ColsTextBox.Text, out cols))
            {
                matrix = new int[rows, cols];
                MatrixItemsControl.Items.Clear();
                ResultTextBlock.Text = ""; // Очищаем результат
                Random random = new Random();

                for (int i = 0; i < rows; i++)
                {
                    var rowPanel = new StackPanel { Orientation = Orientation.Horizontal };
                    for (int j = 0; j < cols; j++)
                    {
                        var textBox = new TextBox
                        {
                            Width = 40,
                            Text = random.Next(1, 10).ToString()
                        };
                        rowPanel.Children.Add(textBox);
                    }
                    MatrixItemsControl.Items.Add(rowPanel);
                }
            }
            else
            {
                MessageBox.Show("Введите корректные размеры матрицы.");
            }
        }

        private void FindColumn_Click(object sender, RoutedEventArgs e)
        {
            if (matrix == null) return;

            int colsCount = matrix.GetLength(1);
            int rowsCount = matrix.GetLength(0);
            int firstColumnIndex = -1; // Индекс первого столбца с максимальным количеством одинаковых элементов

            for (int j = 0; j < colsCount; j++)
            {
                Dictionary<int, int> count = new Dictionary<int, int>();

                for (int i = 0; i < rowsCount; i++)
                {
                    int element = matrix[i, j];

                    // Используем TryGetValue для получения текущего счётчика
                    int currentCount = 0;
                    count.TryGetValue(element, out currentCount);
                    count[element] = currentCount + 1; // Увеличиваем счётчик
                }

                int maxCount = count.Values.Max();
                if (firstColumnIndex == -1 || maxCount > count[matrix[0, firstColumnIndex]])
                {
                    firstColumnIndex = j;
                }
            }

            if (firstColumnIndex >= 0)
            {
                ResultTextBlock.Text = $"Первый столбец с максимальным количеством одинаковых элементов: {firstColumnIndex + 1}";
            }
            else
            {
                ResultTextBlock.Text = "Нет одинаковых элементов.";
            }
        }
    }
}
