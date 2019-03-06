using ZakusochnayaServiceDAL.BindingModels;
using ZakusochnayaServiceDAL.Interfaces;
using ZakusochnayaServiceDAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;
using ZakusochnayaServiceDAL.ViewModel;
using ZakusochnayaServiceDAL.BindingModel;

namespace ZakusochnayaView
{
    public partial class FormPutOnSklad : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        private readonly ISkladService serviceS;
        private readonly IElementService serviceC;
        private readonly IMainService serviceM;
        public FormPutOnSklad(ISkladService serviceS, IElementService serviceC,
        IMainService serviceM)
        {
            InitializeComponent();
            this.serviceS = serviceS;
            this.serviceC = serviceC;
            this.serviceM = serviceM;
        }
        private void FormPutOnSklad_Load(object sender, EventArgs e)
        {
            try
            {
                List<ElementViewModel> listC = serviceC.GetList();
                if (listC != null)
                {
                    comboBoxComponent.DisplayMember = "ElementName";
                    comboBoxComponent.ValueMember = "Id";
                    comboBoxComponent.DataSource = listC;
                    comboBoxComponent.SelectedItem = null;
                }
                List<SkladViewModel> listS = serviceS.GetList();
                if (listS != null)
                {
                    comboBoxStock.DisplayMember = "SkladName";
                    comboBoxStock.ValueMember = "Id";
                    comboBoxStock.DataSource = listS;
                    comboBoxStock.SelectedItem = null;
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
            if (comboBoxStock.SelectedValue == null)
            {
                MessageBox.Show("Выберите склад", "Ошибка", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
                return;
            }
            try
            {
                serviceM.PutComponentOnStock(new SkladElementBindingModel
                {
                    ElementId = Convert.ToInt32(comboBoxComponent.SelectedValue),
                    SkladId = Convert.ToInt32(comboBoxStock.SelectedValue),
                    Number = Convert.ToInt32(textBoxCount.Text)
                });
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