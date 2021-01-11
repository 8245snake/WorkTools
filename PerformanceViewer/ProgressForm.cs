using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PerformanceViewer
{
    public partial class ProgressForm : Form
    {

        private int _Max;
        private int _CurrentProgress;

        public ProgressForm()
        {
            InitializeComponent();
            lblProgress.Text = "";
        }

        public void InvokeSetMaxProgress(int max)
        {
            progressBar1.Invoke(new Action(()=> {
                progressBar1.Maximum = max;
                _Max = max;
            }));
        }

        public void InvokeSetProgressValue(int progress)
        {
            progressBar1.Invoke(new Action(() => {
                progressBar1.Value = progress;
                _CurrentProgress = progress;
            }));

            lblProgress.Invoke(new Action(() => {
                lblProgress.Text = $"{_CurrentProgress}/{_Max}";
            }));
        }

        private void ProgressForm_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible) {
                lblProgress.Text = "";
                progressBar1.Value = 0;
            }
        }
    }
}
