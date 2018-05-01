using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Diagnostics;


namespace ImageServiceGUI.ViewModel
{
    class SettingsViewModel
    {
        public ICommand RemoveCommand { get; private set; }

        public SettingsViewModel()
        {
           // this.RemoveCommand = new DelegateCommand<object>(this.OnRemove,this.CanRemove);
        }
        private void OnRemove(object obj)
        {

        }
        private bool CanRemove(object obj)
        {
            return true;
        }
       
    }
    
}
