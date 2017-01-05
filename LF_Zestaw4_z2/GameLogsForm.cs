using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LF_Zestaw4_z2
{
    public partial class GameLogsForm : Form
    {
        public GameLogsForm()
        {
            InitializeComponent();
            boxLogs.Left = 0;
            boxLogs.Size = new Size(ClientSize.Width, ClientSize.Height - boxLogs.Top);
            BuildLogs();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (boxLogs != null)
                boxLogs.Size = new Size(ClientSize.Width, ClientSize.Height - boxLogs.Top);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            buttonRefresh.Click -= this.buttonRefresh_Click;
            buttonClear.Click -= this.buttonClear_Click;
        }

        private void BuildLogs()
        {
            int rozmiar = GraDwuosobowa.RozmiarLog;
            StringBuilder sb = new StringBuilder(50 * rozmiar);

            for (int i = rozmiar - 1; i >= 0; --i)
                sb.Append("(").Append((i + 1).ToString()).Append(")     ").AppendLine(GraDwuosobowa.Log(i));

            boxLogs.Text = sb.ToString();
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            BuildLogs();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            GraDwuosobowa.WyczyscLog();
            boxLogs.Text = string.Empty;
        }
    }
}
