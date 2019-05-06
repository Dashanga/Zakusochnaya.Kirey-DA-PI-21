using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZakusochnayaServiceDAL.BindingModel;
using ZakusochnayaServiceDAL.ViewModel;

namespace ZakusochnayaView
{
    public partial class FormExecutor : Form
    {
        public int Id { set { id = value; } }
        private int? id;
        public FormExecutor()
        {
            InitializeComponent();
        }
        private void FormExecutor_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    ExecutorViewModel view = APIClient.GetRequest<ExecutorViewModel>("api/Executor/Get/" + id.Value);
                    if (view != null)
                    {
                        textBoxExecutor.Text = view.ExecutorFIO;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                }
            }
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxExecutor.Text))
            {
                MessageBox.Show("Заполните сотрудника", "Ошибка", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
                return;
            }
            try
            {
                if (id.HasValue)
                {
                    APIClient.PostRequest<ExecutorBindingModel, bool>("api/Executor/UpdExecutor", new ExecutorBindingModel
                    {
                        Id = id.Value,
                        ExecutorFIO = textBoxExecutor.Text
                    });
                }
                else
                {
                    APIClient.PostRequest<ExecutorBindingModel, bool>("api/Executor/AddExecutor", new ExecutorBindingModel
                    {
                        ExecutorFIO = textBoxExecutor.Text
                    });
                }
                MessageBox.Show("Сохранение прошло успешно", "Сообщение",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            }
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
