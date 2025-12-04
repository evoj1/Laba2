using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLogic;
using Ninject;

namespace Laba2
{
    public partial class FormStudents : Form
    {
        private readonly Logic logic;
        public FormStudents()
        {
            InitializeComponent();
            IKernel kernel = new StandardKernel(new SimpleConfigModule());
            logic = kernel.Get<Logic>();
            logic.DataChanged += UpdateUiFromLogic;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "appDbDataSet.Students". При необходимости она может быть перемещена или удалена.
            //this.studentsTableAdapter.Fill(this.appDbDataSet.Students);
            UpdateUiFromLogic();
            LoadGroups();
        }
        
        private void LoadGroups()
        {
            var groups = logic.GetAllGroups();
            comboBox1.DataSource = groups;
        }
            
        private void UpdateUiFromLogic()
        {
            dataGridView1.Rows.Clear();
            var students = logic.PrintAllStudentsWithGroupNames();
            //var students = logic.PrintAllStudentsWithGroupNamesDapper();
            foreach ( var student in students)
            {
                var parts = student.Split(new[] { "| "}, StringSplitOptions.None);
                dataGridView1.Rows.Add(
                    parts[0].Trim(),
                    parts[1].Trim(),
                    parts[2].Trim(),
                    parts[3].Trim()
                    );
            }
            UpdateChart();
            LoadGroups();
        }
        private void btnShowHistogram_Click(object sender, EventArgs e)
        {
            chart1.Visible = !chart1.Visible;
            if (chart1.Visible)
            {
                UpdateChart();
                btnShowHistogram.Text = "Скрыть гистограмму";
            }
            else
            {
                btnShowHistogram.Text = "Показать гистограмму";
            }
        }

        private void UpdateChart()
        {
            var hist = logic.GetSpecialtyHistogram();
            var series = chart1.Series[0];
            series.Points.Clear();
            foreach (var pair in hist)
                series.Points.AddXY(pair.Key, pair.Value);
        }

        private void btnAddStudent_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text.Trim();
            string specialty = textBox2.Text.Trim();
            try
            {
                var selectedGroup = comboBox1.SelectedItem.ToString().Split(' ');
                if (selectedGroup == null)
                    throw new Exception("Выберите группу");
                int groupId = int.Parse(selectedGroup[0]);
                logic.AddStudent(name, specialty, groupId);
                textBox1.Text = "";
                textBox2.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }
        private void btnRemoveStudent_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
                return;

            int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            if (logic.RemoveStudent(id))
            {
                UpdateUiFromLogic();
            }
            else
            {
                MessageBox.Show("Ошибка");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormGroups windowsFormGroups = new FormGroups();
            windowsFormGroups.ShowDialog();
            LoadGroups();
        }
    }
}
