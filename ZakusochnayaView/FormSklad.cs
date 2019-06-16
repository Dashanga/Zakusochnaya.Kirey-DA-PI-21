using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ZakusochnayaServiceDAL.BindingModel;
using ZakusochnayaServiceDAL.ViewModel;

namespace ZakusochnayaView
{
    public partial class FormSklad : Form
    {
        public int Id { set { id = value; } }
        private int? id;

        private List<SkladElementViewModel> productComponents;
        public FormSklad()
        {
            InitializeComponent();
        }
        private void FormSklad_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    SkladViewModel view = APIClient.GetRequest<SkladViewModel>("api/Sklad/Get/" + id.Value);
                    if (view != null)
                    {
                        textBoxName.Text = view.SkladName;
                        productComponents = view.SkladElements;
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                }
            }
            else
            {
                productComponents = new List<SkladElementViewModel>();
            }
        }

        private void LoadData()
        {
            try
            {
                if (productComponents != null)
                {
                    dataGridView.DataSource = null;
                    dataGridView.DataSource = productComponents;
                    dataGridView.Columns[0].Visible = false;
                    dataGridView.Columns[1].Visible = false;
                    dataGridView.Columns[2].Visible = false;
                    dataGridView.Columns[3].AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            }
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
                return;
            }
            try
            {
                if (id.HasValue)
                {
                    APIClient.PostRequest<SkladBindingModel, bool>("api/Sklad/UpdElement", new SkladBindingModel
                    {
                        Id = id.Value,
                        SkladName = textBoxName.Text
                    });
                }
                else
                {
                    APIClient.PostRequest<SkladBindingModel, bool>("api/Sklad/AddElement", new SkladBindingModel
                    {
                        SkladName = textBoxName.Text
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
