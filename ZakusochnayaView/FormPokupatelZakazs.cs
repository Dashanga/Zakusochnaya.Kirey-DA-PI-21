using ZakusochnayaServiceDAL.BindingModel;
using Microsoft.Reporting.WinForms;
using System;
using System.Windows.Forms;
using ZakusochnayaServiceDAL.ViewModel;
using System.Collections.Generic;

namespace ZakusochnayaView
{
    public partial class FormPokupatelZakazs : Form
    {
        public FormPokupatelZakazs()
        {
            InitializeComponent();
        }
        private void buttonMake_Click(object sender, EventArgs e)
        {
            if (dateTimePickerFrom.Value.Date >= dateTimePickerTo.Value.Date)
            {
                MessageBox.Show("Дата начала должна быть меньше даты окончания",
                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                ReportParameter parameter = new ReportParameter("ReportParameterPeriod",
                "c " +
                dateTimePickerFrom.Value.ToShortDateString() +
                " по " +
                dateTimePickerTo.Value.ToShortDateString());
                reportViewer.LocalReport.SetParameters(parameter);
                List<PokupatelZakazsModel> response = APIClient.PostRequest<OtchetBindingModel,
                    List<PokupatelZakazsModel>>("api/Otchet/GetPokupatelZakazs", new OtchetBindingModel
                    {
                        DateFrom = dateTimePickerFrom.Value,
                        DateTo = dateTimePickerTo.Value
                    });
                ReportDataSource source = new ReportDataSource("DataSetZakazs", response);
                reportViewer.LocalReport.DataSources.Add(source);
                reportViewer.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            }
        }
        private void buttonToPdf_Click(object sender, EventArgs e)
        {
            if (dateTimePickerFrom.Value.Date >= dateTimePickerTo.Value.Date)
            {
                MessageBox.Show("Дата начала должна быть меньше даты окончания",
                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "pdf|*.pdf"
            };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    APIClient.PostRequest<OtchetBindingModel, bool>("api/Otchet/SavePokupatelZakazs", new OtchetBindingModel
                    {
                            FileName = sfd.FileName,
                            DateFrom = dateTimePickerFrom.Value,
                            DateTo = dateTimePickerTo.Value
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
