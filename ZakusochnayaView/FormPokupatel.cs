using System;
using System.Windows.Forms;
using Unity;
using ZakusochnayaServiceDAL;
using ZakusochnayaServiceDAL.ViewModel;
using ZakusochnayaServiceDAL.BindingModel;
//using Unity.Attributes;

namespace ZakusochnayaView
{
    public partial class FormPokupatel : Form
    {
            [Dependency]
            public new IUnityContainer Container { get; set; }
            public int Id { set { id = value; } }
            private readonly IPokupatelService service;
            private int? id;
            public FormPokupatel(IPokupatelService service)
            {
                InitializeComponent();
                this.service = service;
            }
            private void FormPokupatel_Load(object sender, EventArgs e)
            {
                if (id.HasValue)
                {
                    try
                    {
                        PokupatelViewModel view = service.GetElement(id.Value);
                        if (view != null)
                        {
                            textBoxFIO.Text = view.PokupatelFIO;
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
                if (string.IsNullOrEmpty(textBoxFIO.Text))
                {
                    MessageBox.Show("Заполните ФИО", "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                    return;
                }
                try
                {
                    if (id.HasValue)
                    {
                        service.UpdElement(new PokupatelBindingModel
                        {
                            Id = id.Value,
                            PokupatelFIO = textBoxFIO.Text
                        });
                    }
                    else
                    {
                        service.AddElement(new PokupatelBindingModel
                        {
                            PokupatelFIO = textBoxFIO.Text
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
