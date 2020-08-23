using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
            updateCheckState((CheckBox)sender);
        }


        /// <summary>
        /// チェックボックスのクリックイベントから呼ばれる想定
        /// </summary>
        /// <param name="checkBox"></param>
        private void updateCheckState(CheckBox checkBox) {
            CheckTreeSource source = (CheckTreeSource)checkBox.DataContext;
            source.UpdateChildStatus();
            source.UpdateParentStatus();
        }

        /// <summary>
        /// TreeViewのイベントから呼ばれる想定
        /// </summary>
        /// <param name="source"></param>
        private void updateCheckState(CheckTreeSource source)
        {
            source.UpdateChildStatus();
            source.UpdateParentStatus();
        }

        /// <summary>
        /// チェックボックスのFocusを無効にしているのでTreeViewのボタン押下を見ることに
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBox_KeyUp(object sender, KeyEventArgs e) {
            if (e.Key != Key.Space) {
                return;
            }

            CheckTreeView view = (CheckTreeView)sender;
            if (view == null) {
                return;
            }
            CheckTreeSource item = (CheckTreeSource)view.SelectedItem;
            if (item == null)
            {
                return;
            }
            item.IsChecked = !item.IsChecked;
            updateCheckState(item);
        }

    }
}
