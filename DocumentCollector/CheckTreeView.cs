using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DocumentCollector
{
    public partial class CheckTreeView : TreeView
    {
        public CheckTreeView()
        {
            InitializeComponent();
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            var checkBox = (CheckBox)sender;
            var source = (CheckTreeSource)checkBox.DataContext;

            source.UpdateChildStatus();
            source.UpdateParentStatus();
        }
    }
}
