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

        public MainWindow()
        {
            InitializeComponent();
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
                string s = Input.Text;
                byte[] p = System.Text.Encoding.Unicode.GetBytes(s);
                foreach (var item in p)
                final += Convert.ToString(item, 2).PadLeft(8, '0');
                while (final.Length > 0) 
                {
                    string newStringFourBit = final.Substring(0,4);
                    table newe = new table(newStringFourBit);
                    _table.Add(newe);
                    final = final.Substring(4);
                }
                Output.Text = "0";
                DG.ItemsSource = _table;
            }
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
                        // rowIndex has the row index
                        // bindingPath has the column's binding
                        // el.Text has the new, user-entered value
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
            }
        }


        private void stringLine_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) Run();
        }
    }
}
