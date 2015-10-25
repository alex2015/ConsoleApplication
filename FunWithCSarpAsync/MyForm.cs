using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FunWithCSarpAsync
{
    public partial class MyForm : Form
    {
        private readonly TaskScheduler _mSyncContextTaskScheduler;
        private CancellationTokenSource _mCts;

        public MyForm()
        {
            InitializeComponent();

            _mSyncContextTaskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
            Text = "Synchronization Context Task Scheduler Demo";
            Visible = true;
            Width = 600;
            Height = 100;
        }

        private void MyForm_Load(object sender, EventArgs e)
        {

        }

        private void MyForm_MouseClick(object sender, MouseEventArgs e)
        {
            if (_mCts != null)
            {
                // Операция начата, отменяем ее
                _mCts.Cancel();
                _mCts = null;
            }
            else
            {
                // Операция не начата, начинаем ее
                Text = "Operation running";
                _mCts = new CancellationTokenSource();
                // Задание использует планировщик по умолчанию
                // и выполняет поток из пула
                Task<Int32> t = Task.Run(() => Sum(_mCts.Token, 20000), _mCts.Token);
                // Эти задания используют планировщик контекста синхронизации
                // и выполняются в потоке графического интерфейса
                t.ContinueWith(task => 
                    Text = "Result: " + task.Result, 
                    CancellationToken.None, TaskContinuationOptions.OnlyOnRanToCompletion, _mSyncContextTaskScheduler);
                t.ContinueWith(task => 
                    Text = "Operation canceled", 
                    CancellationToken.None, TaskContinuationOptions.OnlyOnCanceled, _mSyncContextTaskScheduler);
                t.ContinueWith(task =>
                    Text = "Operation faulted", 
                    CancellationToken.None, TaskContinuationOptions.OnlyOnFaulted, _mSyncContextTaskScheduler);
            }

            //base.OnMouseClick(e);
        }

        private static Int32 Sum(CancellationToken ct, Int32 n)
        {
            Thread.Sleep(3000);
            Int32 sum = 0;
            for (; n > 0; n--)
            {
                ct.ThrowIfCancellationRequested();
                checked
                {
                    sum += n;
                } // при больших n появляется
                // исключение System.OverflowException
            }
            return sum;
        }
    }
}
