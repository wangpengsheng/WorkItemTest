using System.Diagnostics;
using AsynchronousExecutor;
using Newtonsoft.Json;
using Presenter;

namespace Demo
{
    public partial class TaskFm : Krypton.Toolkit.KryptonForm
    {
        private List<ConfiguredPara>? parasList;
        private Executor<string> _executor;
        private Executor<string> _executor2;
        public bool isPause { get; set; }
        public bool isResume { get; set; }
        public bool isExit { get; set; }
        public TaskFm()
        {
            InitializeComponent();
            _executor = new Executor<string>(ExecutionMode.Serial);
            _executor2 = new Executor<string>(ExecutionMode.Parallel);
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                var st = new Stopwatch();
                st.Start();
                var result = _executor.StartExecute().Result;
                st.Stop();
                kryptonLabel1.Text = st.ElapsedMilliseconds.ToString();
                var dataModels = new List<DataGridModel>();
                foreach (var data in result)
                {
                    var subStrings = data.Split(',');
                    var model = new DataGridModel
                    {
                        Column0 = subStrings[0],
                        Column1 = subStrings[1],
                        Column2 = subStrings[2],
                        Column3 = subStrings[3],
                        Column4 = subStrings[4]
                    };
                    dataModels.Add(model);
                }
                kryptonDataGridView1.DataSource = dataModels;
                //
            });
        }

        private void TaskFm_Load(object sender, EventArgs e)
        {
            parasList = JsonConvert.DeserializeObject<List<ConfiguredPara>>(File.ReadAllText("configuredParas.json"));
            if (parasList != null)
                foreach (var my in parasList.Select(para => new MyClass(para)))
                {
                    _executor.AddTask(my);
                    _executor2.AddTask(my);
                }
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            while (true)
            {
                if (isPause)
                {
                    continue;
                }

                if (isExit)
                {
                    break;
                }

                var st = new Stopwatch();
                st.Start();
                var result = _executor.StartExecute().Result;
                st.Stop();
                kryptonLabel2.Text = st.ElapsedMilliseconds.ToString();
                var dataModels = new List<DataGridModel>();
                foreach (var data in result)
                {
                    var subStrings = data.Split(',');
                    var model = new DataGridModel
                    {
                        Column0 = subStrings[0],
                        Column1 = subStrings[1],
                        Column2 = subStrings[2],
                        Column3 = subStrings[3],
                        Column4 = subStrings[4]
                    };
                    dataModels.Add(model);
                }
                kryptonDataGridView2.DataSource = dataModels;
            }
        }

        private void kryptonCheckedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var index = kryptonCheckedListBox1.SelectedIndex;
            if (index == 0)
            {
                isPause = true;
                isResume = false;
            }
            else if (index == 1)
            {
                isResume = true;
                isPause = false;
            }
            else if (index == 2)
            {
                isExit = true;
                isPause = false;
            }
        }
    }
}