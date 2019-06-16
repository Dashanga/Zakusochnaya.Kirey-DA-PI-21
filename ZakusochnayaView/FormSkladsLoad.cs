using ZakusochnayaServiceDAL.BindingModel;
using ZakusochnayaServiceDAL.Interfaces;
using System;
using System.Windows.Forms;
using Unity;

namespace ZakusochnayaView
{
    public partial class FormSkladsLoad : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        private readonly IOtchetService service;
        public FormSkladsLoad(IOtchetService service)
        {
            InitializeComponent();
            this.service = service;
        }
        private void FormSkladsLoad_Load(object sender, EventArgs e)
        {
            try
            {
                var dict = service.GetStocksLoad();
                if (dict != null)
                {
                    dataGridView.Rows.Clear();
                    foreach (var elem in dict)
                    {
                        dataGridView.Rows.Add(new object[] { elem.SkladName, "", "" });
                        foreach (var listElem in elem.Elements)
                        {
                            dataGridView.Rows.Add(new object[] { "", listElem.Item1,
listElem.Item2 });
                        }
                        dataGridView.Rows.Add(new object[] { "Итого", "", elem.FullCount
});
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
                    service.SaveStocksLoad(new OtchetBindingModel
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
