using ZakusochnayaServiceDAL.Interfaces;
using ZakusochnayaServiceDAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ZakusochnayaServiceDAL.ViewModel;

namespace ZakusochnayaView
{
    public partial class FormOutputElement : Form
    {
        public OutputElementViewModel Model {
            set { model = value; }
            get {
                return model;
            }
        }
        private OutputElementViewModel model;
        public FormOutputElement()
        {
            InitializeComponent();
        }
        private void FormOutputElement_Load(object sender, EventArgs e)
        {
            try
            {
                List<ElementViewModel> list = APIClient.GetRequest<List<ElementViewModel>>("api/Element/GetList");
                if (list != null)
                {
                    comboBoxComponent.DisplayMember = "ElementName";
                    comboBoxComponent.ValueMember = "Id";
                    comboBoxComponent.DataSource = list;
                    comboBoxComponent.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            }
            if (model != null)
            {
                comboBoxComponent.Enabled = false;
                comboBoxComponent.SelectedValue = model.ElementId;
                textBoxCount.Text = model.Number.ToString();
            }
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxComponent.SelectedValue == null)
            {
                MessageBox.Show("Выберите компонент", "Ошибка", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
                return;
            }
            try
            {
                if (model == null)
                {
                    model = new OutputElementViewModel
                    {
                        ElementId = Convert.ToInt32(comboBoxComponent.SelectedValue),
                        ElementName = comboBoxComponent.Text,
                        Number = Convert.ToInt32(textBoxCount.Text)
                    };
                  
                }
                else
                {
                    model.Number = Convert.ToInt32(textBoxCount.Text);
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

        private void buttonCancel_Click_1(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
