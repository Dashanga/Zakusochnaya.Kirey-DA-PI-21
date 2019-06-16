using ZakusochnayaServiceDAL.BindingModel;
using System;
using System.Windows.Forms;
using ZakusochnayaServiceDAL.ViewModel;
using System.Collections.Generic;

namespace ZakusochnayaView
{
    public partial class FormSkladsLoad : Form
    {
        public FormSkladsLoad()
        {
            InitializeComponent();
        }
        private void FormSkladsLoad_Load(object sender, EventArgs e)
        {
            try
            {
                var dict = APIClient.GetRequest<List<SkladsLoadViewModel>>("api/Otchet/GetSkladsLoad");
                if (dict != null)
                {
                    dataGridView.Rows.Clear();
                    foreach (var elem in dict)
                    {
                        dataGridView.Rows.Add(new object[] { elem.SkladName, "", "" });
                        foreach (var listElem in elem.Elements)
                        {
                            dataGridView.Rows.Add(new object[] { "", listElem.Item1, listElem.Item2 });
                        }
                        dataGridView.Rows.Add(new object[] { "Итого", "", elem.FullCount });
                        dataGridView.Rows.Add(new object[] { });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            }
        }

        private void buttonSaveToExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "xls|*.xls|xlsx|*.xlsx"
            };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    APIClient.PostRequest< OtchetBindingModel, bool>("api/Otchet/SaveSkladsLoad", new OtchetBindingModel
                    {
                        FileName = sfd.FileName
                    });
                    MessageBox.Show("Выполнено", "Успех", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                }
            }
        }
    }
}
