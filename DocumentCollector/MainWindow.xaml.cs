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

namespace DocumentCollector
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<CheckTreeSource> TreeRoot { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            TreeRoot = new ObservableCollection<CheckTreeSource>();
            var item1 = new CheckTreeSource() { Text = "Item1", IsExpanded = true, IsChecked = false };
            var item11 = new CheckTreeSource() { Text = "Item1-1", IsExpanded = true, IsChecked = false };
            var item12 = new CheckTreeSource() { Text = "Item1-2", IsExpanded = true, IsChecked = false };
            var item2 = new CheckTreeSource() { Text = "Item2", IsExpanded = false, IsChecked = false };
            var item21 = new CheckTreeSource() { Text = "Item2-1", IsExpanded = true, IsChecked = false };
            TreeRoot.Add(item1);
            TreeRoot.Add(item2);
            item1.Add(item11);
            item1.Add(item12);
            item2.Add(item21);

            DataContext = this;
        }
    }
}
