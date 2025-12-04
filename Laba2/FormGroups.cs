using BusinessLogic;
using Microsoft.IdentityModel.Tokens;
using Ninject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Laba2
{
    public partial class FormGroups : Form
    {
        private Logic logic;
        public FormGroups()
        {
            InitializeComponent();
            IKernel kernel = new StandardKernel(new SimpleConfigModule());
            logic = kernel.Get<Logic>();
            logic.DataChanged += UpdateUiFromLogic;
        }

        private void UpdateUiFromLogic()
        {
            dataGridView1.Rows.Clear();
            var groups = logic.GetAllGroups();
            //var students = logic.PrintAllStudentsWithGroupNamesDapper();
            foreach (var group in groups)
            {
                var parts = group.Split(new[] { " " }, StringSplitOptions.None);
                dataGridView1.Rows.Add(
                    parts[0].Trim(),
                    parts[1].Trim()
                    );
            }
        }

        private void btnAddSGroup_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text.Trim();
            try
            {
                if (!name.IsNullOrEmpty())
                {
                    logic.AddGroup(name);
                    textBox1.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void btnRemoveGroup_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
                return;

            int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            if (logic.RemoveGroup(id))
            {
                UpdateUiFromLogic();
            }
            else
            {
                MessageBox.Show("Ошибка");
            }
        }

        private void FormGroups_Load(object sender, EventArgs e)
        {
            UpdateUiFromLogic();
        }
    }
}
