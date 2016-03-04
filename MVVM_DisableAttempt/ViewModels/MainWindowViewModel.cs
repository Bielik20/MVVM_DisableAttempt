using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVM_DisableAttempt.ViewModels
{
    class MainWindowViewModel : ViewModelBase
    {
        public ICommand ClickCommand { get; private set; }                  //It seems they have to be preperties
        public ICommand DisableCommand { get; private set; }
        public ICommand EnableCommand { get; private set; }

        public MainWindowViewModel()
        {
            ClickCommand = new RelayCommand(_ => ShowSomething());        //Przy kliknięciu uruchamia komendę ShowSomething() + to "_" albo "(s)" może być użyte zamiennie
            DisableCommand = new RelayCommand((s) => IsEnabled = false);    //Przekazania jako delegata instrukcji wyłączenia
            EnableCommand = new RelayCommand((s) => IsEnabled = true, (s) => IsDone());      //Jak wyżej + zdefiniowanie kiedy jest możliwe użycie
            Info = "StartUP";
            IsEnabled = true;
        }

        public void ShowSomething()
        {
            Info = "Kliknięcie wykonane";
            //IsEnabled = false;    //Inny sposób bez wykorzystania osobnej komendy
        }

        /// <summary>
        /// Provides information whether text in box is "DONE" or not.
        /// </summary>
        /// <returns></returns>
        public bool IsDone()
        {
            return DoneText == "DONE" ? true : false; 
        }

        /// <summary>
        /// Used for enabling and disabling thirdPanel
        /// Occurs whenever OnPropertyChanged("IsDoneP"); is invoked. Which is whenever "DoneText" property is changed (set).
        /// </summary>
        public bool IsDoneP
        {
            get { return IsDone(); }
        }

        private bool isEnabled;
        public bool IsEnabled
        {
            get
            {
                return isEnabled;
            }
            set
            {
                isEnabled = value;
                OnPropertyChanged("IsEnabled");     //Information used by Disable and Enable buttons
            }
        }

        private string info;
        public String Info
        {
            get
            {
                return info;
            }
            set
            {
                info = value;
                OnPropertyChanged("Info");          //Information that it effects IsDoneP property  ---  Necessary for it changes value (if readonly then unnecessary)
            }
        }

        private string doneText;
        public string DoneText
        {
            get
            {
                return doneText;
            }
            set
            {
                doneText = value;
                //OnPropertyChanged("DoneText");      //Information that it effects DoneText property  ---  Unnecessary for it doesn't affect anything (and it's changed by a user in UI)
                OnPropertyChanged("IsDoneP");       //Information that it effects IsDoneP property
            }
        }
        
    }
}
