using SearchSpring.Infrastructure.Commands;
using SearchSpring.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SearchSpring.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase
    {
        #region Title window

        private string _title = "4 Лаба";

        /// <summary>Заголовок окна</summary>
        public string Title
        {
            get => _title;
            set { Set(ref _title, value); }
        }

        #endregion

        #region Status

        private string _status = "Готово!";

        public string status
        {
            get => _status;
            set => Set(ref _status, value);
        }

        #endregion

        #region Команды

        #region CloseApplicationCommad

        public ICommand CloseApplicationCommad { get; }

        private void OnCloseApplicationCommadExecuted(object p) { Application.Current.Shutdown(); }

        private bool CanCloseApplicationCommadExecuted(object p) => true;

        #endregion

        #endregion

        public MainWindowViewModel()
        {
            #region

            CloseApplicationCommad = new LambdaCommand(OnCloseApplicationCommadExecuted, CanCloseApplicationCommadExecuted);

            #endregion

        }
    }
}
