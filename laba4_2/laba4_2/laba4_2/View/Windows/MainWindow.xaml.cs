using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace laba4_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Simbol> _probabilities;
        private double HDouble = 0;
        private double HMaxDouble = 0;
        private double ReceiverEntropyDouble = 0;

        private ObservableCollection<ObservableCollection<double>> matrixChannel = new ObservableCollection<ObservableCollection<double>>()
        {
             new ObservableCollection<double> { 0, 0, 0, 0},
             new ObservableCollection<double> { 0, 0, 0, 0},
             new ObservableCollection<double> { 0, 0, 0, 0},
             new ObservableCollection<double> { 0, 0, 0, 0}
        };

        public MainWindow()
        {
            InitializeComponent();
            _probabilities = new ObservableCollection<Simbol>();
            DataGrid2.ItemsSource = matrixChannel;
        }

        private void InputText_TextChanged(object sender, TextChangedEventArgs e)
        {
            CalculateProbabilitiesSimbol();
            CreateMatrix(_probabilities.Count, _probabilities.Count);
            DataGrid2 = CreateDataGrid(DataGrid2, _probabilities.Count);
            CalculateReceiverEntropy();
        }

        private void CalculateProbabilitiesSimbol()
        {
            int totalCharacters = InputText.Text.Length;
            Dictionary<char, int> characterCounts = new Dictionary<char, int>();

            foreach (char character in InputText.Text)
            {
                if (characterCounts.ContainsKey(character))
                {
                    characterCounts[character]++;
                }
                else
                {
                    characterCounts[character] = 1;
                }
            }

            var newProbabilities = characterCounts.Select(kvp => new Simbol(kvp.Key, (double)kvp.Value / totalCharacters)).ToList();

            _probabilities.Clear();
            foreach (var symbol in newProbabilities)
            {
                _probabilities.Add(symbol);
            }

            DataGrid1.ItemsSource = _probabilities;

            CalculateH();
            CalculateHMax();
            CalculateUnderutilization();
        }

        private void CalculateH()
        {
            double result = 0;
            foreach (var symbol in _probabilities)
            {
                result += symbol.Probabilities * Math.Log2(symbol.Probabilities);
            }

            HDouble = -result;

            H.Text = HDouble.ToString();
        }

        private void CalculateHMax()
        {
            HMaxDouble = Math.Log2(_probabilities.Count);
            HMax.Text = HMaxDouble.ToString();
        }

        private void CalculateUnderutilization()
        {
            Underutilization.Text = (HMaxDouble - HDouble).ToString();
        }




        private DataGrid CreateDataGrid(DataGrid dgMatrix, int countColumn)
        {
            dgMatrix.Columns.Clear();
            if (countColumn > 0)
            {
                for (int i = 0; i < countColumn; i++)//столбец
                {
                    //формирование DataGrid
                    DataGridTextColumn column = new DataGridTextColumn
                    {
                        Header = (i + 1).ToString(),//заголовок стобца i
                        Binding = new Binding(String.Format("[{0}]", i))// биндинг (привязка) столбца
                    };// текстовый формат для столбца
                    dgMatrix.Columns.Add(column);//добавление столбца в DataGrid
                }
            }
            return dgMatrix;
        }

        private ObservableCollection<ObservableCollection<double>> CreateMatrix(int countRow, int countColumn)
        {
            matrixChannel.Clear();
            if (countRow > 0 && countColumn > 0)
            {
                Random random = new Random();

                for (int i = 0; i < countRow; i++) // Строки матрицы
                {
                    matrixChannel.Add(new ObservableCollection<double>()); // Добавили в матрицу строку
                }

                for (int j = 0; j < countColumn; j++) // Столбцы матрицы
                {
                    double columnSum = 0;

                    for (int i = 0; i < countRow - 1; i++) // Заполняем все элементы, кроме последнего
                    {
                        double randomValue = Math.Round(random.NextDouble() * (1 - columnSum), 2); // Округляем до 2 знаков после запятой
                        matrixChannel[i].Add(randomValue);
                        columnSum += randomValue;
                    }

                    // Последний элемент столбца рассчитывается так, чтобы сумма столбца была равна 1
                    double lastValue = Math.Round(1 - columnSum, 2);
                    matrixChannel[countRow - 1].Add(lastValue);

                    // Если в столбце есть хотя бы один ноль, пересоздаем столбец
                    while (matrixChannel.Any(row => row[j] == 0))
                    {
                        for (int i = 0; i < countRow - 1; i++)
                        {
                            double randomValue = Math.Round(random.NextDouble() * (1 - columnSum), 2);
                            matrixChannel[i][j] = randomValue;
                            columnSum += randomValue;
                        }

                        matrixChannel[countRow - 1][j] = Math.Round(1 - columnSum, 2);
                    }
                }
            }

            return matrixChannel;
        }

        private void DataGrid2_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;//заголовок строки 
        }

        private void CalculateReceiverEntropy()
        {
            double result = 0;
            for (int i = 0; i < matrixChannel.Count; i++)
            {
                double sum = 0;
                foreach (var item in matrixChannel)
                {
                    double qwe = item[i];
                    sum += item[i] * Math.Log2(item[i]);
                }
                result += sum;
            }

            ReceiverEntropyDouble = - result;
            ReceiverEntropy.Text = ReceiverEntropyDouble.ToString();
            Debug.WriteLine((-result).ToString());
        }
    }
}
