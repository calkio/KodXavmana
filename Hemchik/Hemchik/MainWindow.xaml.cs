using Hemchik.ViewModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hemchik
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<table> _table = new();
        private Dictionary<char, string> _huffmanDictionary;

        public MainWindow(string text, Dictionary<char, string> huffmanDictionary)
        {
            InitializeComponent();
            _huffmanDictionary = huffmanDictionary;
            Input.Text = text;
            DG.ItemsSource = _table;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Run();
        }

        private void Run()
        {
            string final = "";
            _table.Clear();
            Output.Clear();
            if (Input.Text.Length > 0) 
            {
                Output.Text = "";
                final = AddZero(Input.Text);
                Input.Text = final;

                for (int i = 0; i < final.Length; i += 4)
                {
                    // Получаем подстроку из 4 символов
                    string subString = final.Substring(i, 4);

                    table newe = new table(subString);
                    _table.Add(newe);
                }

                DG.ItemsSource = _table;
            }

            CalculeteDecoder();
        }

        private string AddZero(string text)
        {
            int difference = text.Length % 4;

            if (difference == 0)
            {
                return text;
            }

            for (int i = 0; i < 4 - difference; i++)
            {
                text += "0";
            }
            return text;
        }

        void myDG_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                var column = e.Column as DataGridBoundColumn;
                if (column != null)
                {
                    var bindingPath = (column.Binding as Binding).Path.Path;
                    if (bindingPath == "eightBit")
                    {
                        int rowIndex = e.Row.GetIndex();
                        var el = e.EditingElement as TextBox;
                        _table[rowIndex].Refresh(el.Text);
                    }

                    int hammingBoundary = 0;
                    foreach (var item in _table)
                    {
                        if (item.complited != "ошибок нет")
                        {
                            hammingBoundary++;
                        }
                    }

                    Output.Text = hammingBoundary.ToString();
                }

                CalculeteDecoder();
            }
        }


        private void CalculeteDecoder()
        {
            string encodedData = "";
            foreach (var item in _table)
            {
                if (item.CheackEightBit != null)
                {
                    var a = item.CheackEightBit.Substring(1, 4);
                    encodedData += item.CheackEightBit.Substring(1, 4);
                }
            }

            HuffmanDecoder decoder = new HuffmanDecoder(_huffmanDictionary);
            Decoder.Text = decoder.Decode(encodedData);
        }

        private void stringLine_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) Run();
        }
    }
}
