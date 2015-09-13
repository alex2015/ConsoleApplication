using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FunWithCSarpAsync
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private async void btnCallMethod_Click(object sender, EventArgs e)
        {
            this.Text = await DoWork();
        }

        private Task<string> DoWork()
        {
            return Task.Run((Func<string>) MyMethod);
        }

        private string MyMethod()
        {
            Thread.Sleep(10000);
            return "Done with work!";
        }
    }
}
