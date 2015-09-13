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



        private async void btnShowMessage_Click(object sender, EventArgs e)
        {
            await MethodReturningVoidAsync();
            MessageBox.Show("Done!");
        }

        private Task MethodReturningVoidAsync()
        {
            return Task.Run(() =>
            {  /*  Выполнение  каких-то  действий...  */
                Thread.Sleep(4000);
            });
        }

        private async void btnMultiAwaits_Click(object sender, EventArgs e)
        {
            await Task.Run(() => { Thread.Sleep(2000); });

            MessageBox.Show("Done  with  first  task!");  //  завершена  первая  задача

            await Task.Run(() => { Thread.Sleep(2000); });

            MessageBox.Show("Done  with  second  task!");  //  завершена  вторая  задача

            await Task.Run(() => { Thread.Sleep(2000); });

            MessageBox.Show("Done  with  third  task!");  //  завершена  третья  задача

        }

        private void btnSum_Click(object sender, EventArgs e)
        {

        }
    }
}
