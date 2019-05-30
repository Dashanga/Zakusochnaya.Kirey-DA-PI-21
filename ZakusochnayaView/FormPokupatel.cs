using System;
using System.Windows.Forms;
using ZakusochnayaServiceDAL;
using ZakusochnayaServiceDAL.ViewModel;
using ZakusochnayaServiceDAL.BindingModel;
using System.Text.RegularExpressions;

namespace ZakusochnayaView
{
    public partial class FormPokupatel : Form
    {
            public int Id { set { id = value; } }
            private int? id;
            public FormPokupatel()
            {
                InitializeComponent();
            }
            private void FormPokupatel_Load(object sender, EventArgs e)
            {
                if (id.HasValue)
                {
                    try
                    {
                    PokupatelViewModel client =
APIClient.GetRequest<PokupatelViewModel>("api/Pokupatel/Get/" + id.Value);
                    textBoxFIO.Text = client.PokupatelFIO;
                    textBoxMail.Text = client.Mail;
                    dataGridView.DataSource = client.Messages;
                    dataGridView.Columns[0].Visible = false;
                    dataGridView.Columns[1].Visible = false;
                    dataGridView.Columns[4].AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.Fill;
                    }
                    catch (Exception ex)
                    {
                    while (ex.InnerException != null)
                    {
                        ex = ex.InnerException;
                    }
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
            string fio = textBoxFIO.Text;
            string mail = textBoxMail.Text;
            if (!string.IsNullOrEmpty(mail))
            {
                if (!Regex.IsMatch(mail, @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-
!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9az][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$"))
                {
                    MessageBox.Show("Неверный формат для электронной почты", "Ошибка",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            if (id.HasValue)
            {
                APIClient.PostRequest<PokupatelBindingModel,
               bool>("api/Element/UpdElement", new PokupatelBindingModel
               {
                   Id = id.Value,
                   PokupatelFIO = fio,
                   Mail = mail
               });
            }
            else
            {
                APIClient.PostRequest<PokupatelBindingModel,
               bool>("api/Element/AddElement", new PokupatelBindingModel
               {
                   PokupatelFIO = fio,
                   Mail = mail
               });
            }
            MessageBox.Show("Сохранение прошло успешно", "Сообщение",
           MessageBoxButtons.OK, MessageBoxIcon.Information);
            DialogResult = DialogResult.OK;
            Close();
        }
        private void buttonCancel_Click_1(object sender, EventArgs e)
        {
            Close();
        }
    }
}
