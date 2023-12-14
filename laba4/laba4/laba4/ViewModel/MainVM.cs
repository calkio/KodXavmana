using laba4.Model;
using laba4.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;
using static System.Net.Mime.MediaTypeNames;

namespace laba4.ViewModel
{
    internal class MainVM : BaseVM
    {
        private string _inputText;
        public string InputText
        {
            get 
            {
                return _inputText;
            }
            set
            {
                Set(ref _inputText, value);

                CalculateProbabilitiesSimbol();
                CreateSymbolTable();
            }
        }

        private ObservableCollection<Simbol> _probabilities;
        public ObservableCollection<Simbol> Probabilities { get => _probabilities; set => Set(ref _probabilities, value); }


        private ObservableCollection<ObservableCollection<double>> _symbolTable;
        public ObservableCollection<ObservableCollection<double>> SymbolTable { get => _symbolTable; set => Set(ref _symbolTable, value); }




        private double _h;
        public double H { get => _h; set => Set(ref _h, value); }

        private double _hMax;
        public double HMax { get => _hMax; set => Set(ref _hMax, value); }

        private double _underutilization;
        public double Underutilization { get => _underutilization; set => Set(ref _underutilization, value); }


        private MainWindow _mainWindow;

        public MainVM(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            Probabilities = new ObservableCollection<Simbol>();
            SymbolTable = new ObservableCollection<ObservableCollection<double>>();
        }



        private void CalculateProbabilitiesSimbol()
        {
            int totalCharacters = _inputText.Length;
            Dictionary<char, int> characterCounts = new Dictionary<char, int>();

            foreach (char character in _inputText)
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

            Probabilities.Clear();
            foreach (var symbol in newProbabilities)
            {
                Probabilities.Add(symbol);
            }

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

            H = -result;
        }

        private void CalculateHMax()
        {
            HMax = Math.Log2(Probabilities.Count);
        }

        private void CalculateUnderutilization()
        {
            Underutilization = HMax - H;
        }



        private void CreateSymbolTable()
        {
            SymbolTable.Clear();

            List<char> symbols = _probabilities.Select(x => x.Name).ToList();

            var random = new Random();

            for (int i = 0; i < symbols.Count; i++)
            {
                var rowValues = new ObservableCollection<double>();
                double remaining = 1.0;

                for (int j = 0; j < symbols.Count - 1; j++)
                {
                    double value = random.NextDouble() * remaining;
                    rowValues.Add(value);
                    remaining -= value;
                }

                rowValues.Add(remaining); // Добавляем оставшееся значение, чтобы сумма стала 1
                SymbolTable.Add(rowValues);
                DataGridTextColumn column = new DataGridTextColumn
                {
                    Header = (i + 1).ToString(),//заголовок стобца i
                    Binding = new Binding(String.Format("[{0}]", i))// биндинг (привязка) столбца
                };
                _mainWindow.dataGrid.Columns.Add(column);
            }
        }
    }
}
