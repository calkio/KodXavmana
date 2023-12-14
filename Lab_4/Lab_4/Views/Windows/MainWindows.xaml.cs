using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Lab_4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region инициализация словарей

        Dictionary<char, int> charInt = new Dictionary<char, int>();
        Dictionary<char, float> dictionary = new Dictionary<char, float>();
        Dictionary<char, float> dictionaryFinally = new Dictionary<char, float>();
        Dictionary<char, float> entropy = new Dictionary<char, float>();
        Dictionary<char, float> entropyFinally = new Dictionary<char, float>();


        ObservableCollection<ObservableCollection<double>> matrixChannel
     = new ObservableCollection<ObservableCollection<double>>()
    {
     new ObservableCollection<double> { 0, 0, 0, 0},
     new ObservableCollection<double> { 0, 0, 0, 0},
     new ObservableCollection<double> { 0, 0, 0, 0},
     new ObservableCollection<double> { 0, 0, 0, 0}
     };
        #endregion


        public MainWindow()
        {
            InitializeComponent();
            channelMatrix.ItemsSource = matrixChannel;

            WindowStartupLocation = WindowStartupLocation.CenterScreen;//окно по центру
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

        private void DgMatrix_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;//заголовок строки
        }

        private ObservableCollection<ObservableCollection<double>> CreateMatrix(int countRow, int countColumn)
        {
            matrixChannel.Clear(); // Очистка матрицы, если без инициализации
            if (countRow > 0 && countColumn > 0)
            {
                Random random = new Random();

                for (int i = 0; i < countRow; i++) // Строки матрицы
                {
                    matrixChannel.Add(new ObservableCollection<double>()); // Добавили в матрицу строку

                    // Заполнение строки случайными числами (в интервале от 0 (включительно) до 1)
                    double rowSum = 0;
                    for (int j = 0; j < countColumn - 1; j++) // Заполняем все элементы, кроме последнего
                    {
                        double randomValue = Math.Round(random.NextDouble() * (1 - rowSum), 2); // Округляем до 2 знаков после запятой
                        matrixChannel[i].Add(randomValue);
                        rowSum += randomValue;
                    }

                    // Последний элемент строки рассчитывается так, чтобы сумма строки была равна 1
                    matrixChannel[i].Add(Math.Round(1 - rowSum, 2));

                    // Если в строке есть хотя бы один ноль, пересоздаем строку
                    while (matrixChannel[i].Contains(0))
                    {
                        matrixChannel[i].Clear();
                        rowSum = 0;
                        for (int j = 0; j < countColumn - 1; j++)
                        {
                            double randomValue = Math.Round(random.NextDouble() * (1 - rowSum), 2);
                            matrixChannel[i].Add(randomValue);
                            rowSum += randomValue;
                        }
                        matrixChannel[i].Add(Math.Round(1 - rowSum, 2));
                    }
                }
            }

            return matrixChannel;
        }


        private void stringLine_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Run();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Run();
        }


        private void dictionaryUpdate()
        {
            int N = stringLine.Text.Length;
            dictionary.Clear();
            dictionaryFinally.Clear();

            for (int i = 0; i < N; i++)
            {
                char symbol = stringLine.Text[i];
                if (!dictionary.ContainsKey(symbol))
                {
                    dictionary.Add(symbol, 1f);
                }
                else if (dictionary.ContainsKey(symbol))
                {
                    dictionary[symbol] += 1;
                }
            }

            foreach (var item in dictionary)
            {
                dictionaryFinally.Add(item.Key, item.Value);
            }

            double bE = 0;

            foreach (var item in dictionaryFinally)
            {
                dictionary[item.Key] = item.Value / (float)N;
                double probability = dictionary[item.Key];
                bE += probability * Math.Log(probability, 2); // Безусловная энтропия

            }

            besEntropyScore(bE);
            dgDic.ItemsSource = dictionary;
            dgDic.Items.Refresh();
        }

        private void entropyUpdate()
        {
            int N = stringLine.Text.Length;
            entropy.Clear();
            entropyFinally.Clear();

            for (int i = 0; i < N; i++)
            {
                char symbol = stringLine.Text[i];
                if (!entropy.ContainsKey(symbol))
                {
                    entropy.Add(symbol, 1f);
                }
                else if (entropy.ContainsKey(symbol))
                {
                    entropy[symbol] += 1;
                }
            }

            foreach (var item in entropy)
            {
                entropyFinally.Add(item.Key, item.Value);
            }

            double averageInformation = 0;

            foreach (var item in entropyFinally)
            {
                double probability = item.Value / (float)N;
                averageInformation = probability * Math.Log(probability, 2); // Безусловная энтропия
                entropy[item.Key] = (float)averageInformation;
            }

            dgEntropy.ItemsSource = entropy;
            dgEntropy.Items.Refresh();
        }



        private void besEntropyScore(double bE)
        {
            BE.Text = (bE * -1).ToString();
        }

        private void AverageConditionalEntropyScore()
        {
            CharIntCreate();
            double averageEntropy = 0;
            double summM = 0;

            int i = 0;
            foreach (var row in matrixChannel)
            {
                foreach (var cell in row)
                {
                    summM += cell * Math.Log2(cell);
                }

                char foundKey = charInt.FirstOrDefault(x => x.Value == i).Key;
                double Px = dictionary[foundKey];
                averageEntropy += summM * Px;
                i++;
            }

            AverageConditionalEntropy.Text = (-1*averageEntropy).ToString();
        }



        private void CharIntCreate()
        {
            int i = 0;
            charInt.Clear();

            foreach (var item in dictionary)
            {
                charInt.Add(item.Key, i);
                i++;
            }
        }


        private void Run()
        {
            dictionaryUpdate();
            entropyUpdate();

            if (dictionary.Count > 0)
            {
                channelMatrix = CreateDataGrid(channelMatrix, dictionary.Count);
                AverageConditionalEntropyScore();
            }
        }

        private void stringLine_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (stringLine.Text.Length > 0) 
            {
                dictionaryUpdate();
                entropyUpdate();
                CreateMatrix(dictionary.Count, dictionary.Count);
                channelMatrix = CreateDataGrid(channelMatrix, dictionary.Count);
            }
        }
    }
}
