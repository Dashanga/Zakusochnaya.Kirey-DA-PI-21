using System;
using System.Windows.Forms;
using ZakusochnayaServiceDAL;
using ZakusochnayaServiceDAL.BindingModel;
using ZakusochnayaServiceDAL.Interfaces;
using ZakusochnayaServiceDAL.ViewModel;

namespace ZakusochnayaView
{
    public partial class FormElement : Form
    {
        public int Id { set { id = value; } }
        private int? id;
        public FormElement()
        {
            InitializeComponent();
        }
        private void FormElement_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    ElementViewModel view = APIClient.GetRequest<ElementViewModel>("api/Element/Get/" + id.Value);
                    if (view != null)
                    {
                        textBoxComponent.Text = view.ElementName;
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
            if (string.IsNullOrEmpty(textBoxComponent.Text))
            {
                MessageBox.Show("Заполните компонент", "Ошибка", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
                return;
            }
            try
            {
                if (id.HasValue)
                {
                    APIClient.PostRequest<ElementBindingModel, bool>("api/Element/UpdElement", new ElementBindingModel
                    {
                        Id = id.Value,
                        ElementName = textBoxComponent.Text
                    });
                }
                else
                {
                    APIClient.PostRequest<ElementBindingModel, bool>("api/Element/AddElement", new ElementBindingModel
                    {
                        ElementName = textBoxComponent.Text
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
        private void buttonCancel_Click_1(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
