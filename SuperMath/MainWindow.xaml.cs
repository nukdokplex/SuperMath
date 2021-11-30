using System;
using System.Collections.Generic;
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
using NDP.MathUtils;

namespace SuperMath
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            /*CommonFraction f1 = new CommonFraction(116, 71);
            f1.Minimize();
            MessageBox.Show($"{f1.Numerator}/{f1.Denominator}");
            MessageBox.Show($"{f1.GetRealValue()}");
            CommonFraction f2 = new CommonFraction(4, 3);
            CommonFraction f3 = f1 + f2;
            f3.Minimize();
            MessageBox.Show($"{f3.Numerator}/{f3.Denominator}");*/

            NDP.MathUtils.Matrix matrix = new NDP.MathUtils.Matrix(4, 4);
            

            MessageBox.Show(matrix.Determinator().Real().ToString());
        }
    }
}
