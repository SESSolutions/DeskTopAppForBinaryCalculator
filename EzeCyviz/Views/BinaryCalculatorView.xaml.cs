using EzeCyviz.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace EzeCyviz.Views
{
    /// <summary>
    /// Interaction logic for BinaryCalculatorView.xaml
    /// </summary>
    public partial class BinaryCalculatorView : UserControl
    {
        public BinaryCalculatorView()
        {
            InitializeComponent();
            DataContext = new BinaryCalculatorViewModel();
        }
    }
}
