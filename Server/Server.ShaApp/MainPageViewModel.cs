using System;
using System.Linq;
using System.Windows.Input;

using Prism.Commands;
using Prism.Windows.Mvvm;

namespace Server.PiApp
{
    public class MainPageViewModel : ViewModelBase
    {
        public ICommand OnTestCommand { get; private set; }

        public MainPageViewModel()
        {
            this.OnTestCommand = new DelegateCommand(this.ExecuteOnTest);
        }

        private void ExecuteOnTest()
        {
            
            RemoteControl.RemoteControl.Instance.SwitchOn("11110", 1);

        }
    }
}
