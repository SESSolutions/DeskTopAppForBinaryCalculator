using EzeCyviz.Behaviors;
using EzeCyviz.Models;
using System.Windows.Input;

namespace EzeCyviz.ViewModels
{
    public class BinaryCalculatorViewModel : NotifyPropertyChanged
    {
        public BinaryCalculator BinaryCalculator { get; set; }
        public BinaryCalculatorViewModel()
        {
           BinaryCalculator = new BinaryCalculator(); 
        }

        private ICommand digitCommand;
        public ICommand DigitCommand
        {
            get
            {
                return digitCommand ?? (digitCommand = new RelayCommand<string>(DigitClicked));
                
            }
        }

        private ICommand operatorCommand;
        public ICommand OperatorCommand
        {
            get
            {
                return operatorCommand ?? (operatorCommand = new RelayCommand<string>(OperatorClicked));
            }
        }


        private ICommand clearCommand;
        public ICommand ClearCommand
        {
            get
            {
                return clearCommand ?? (clearCommand = new RelayCommand<string>(ClickedOnClear));
            }
        }


        private void OperatorClicked(string operation)
        {
           BinaryCalculator.ExecuteOperation(operation);
           RaisePropertyChanged("BinaryCalculator");
        }


        private void DigitClicked(string bit)
        {
            BinaryCalculator.GetBitIntoByteField(bit);
            RaisePropertyChanged("BinaryCalculator");
        }

        private void ClickedOnClear(string ClearOption)
        {
            BinaryCalculator.Clear(ClearOption);
            RaisePropertyChanged("BinaryCalculator");
        }
    }
}
