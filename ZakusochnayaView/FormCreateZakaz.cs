﻿using ZakusochnayaServiceDAL.BindingModels;
using ZakusochnayaServiceDAL.ViewModels;
using System;
using System.Windows.Forms;
using ZakusochnayaServiceDAL.ViewModel;
using System.Collections.Generic;

namespace ZakusochnayaView
{
    public partial class FormCreateZakaz : Form
    {
         public FormCreateZakaz()
        {
            InitializeComponent();
        }
        private void FormCreateZakaz_Load(object sender, EventArgs e)
        {
            try
            {
                List<PokupatelViewModel> listC = APIClient.GetRequest<List<PokupatelViewModel>>("api/Pokupatel/GetList");
                if (listC != null)
                {
                    comboBoxClient.DisplayMember = "PokupatelFIO";
                    comboBoxClient.ValueMember = "Id";
                    comboBoxClient.DataSource = listC;
                    comboBoxClient.SelectedItem = null;
                }
                List<OutputViewModel> listP = APIClient.GetRequest<List<OutputViewModel>>("api/Output/GetList");
                if (listP != null)
                {
                    comboBoxProduct.DisplayMember = "OutputName";
                    comboBoxProduct.ValueMember = "Id";
                    comboBoxProduct.DataSource = listP;
                    comboBoxProduct.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            }
        }
        private void CalcSum()
        {
            if (comboBoxProduct.SelectedValue != null &&
            !string.IsNullOrEmpty(textBoxCount.Text))
            {
                try
                {
                    int id = Convert.ToInt32(comboBoxProduct.SelectedValue);
                    OutputViewModel product = APIClient.GetRequest<OutputViewModel>("api/Output/Get/" + id);
                    int count = Convert.ToInt32(textBoxCount.Text);
                    textBoxSum.Text = (count * product.Cost).ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                }
            }
        }
        private void textBoxCount_TextChanged(object sender, EventArgs e)
        {
            CalcSum();
        }
        private void comboBoxProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalcSum();
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxClient.SelectedValue == null)
            {
                MessageBox.Show("Выберите клиента", "Ошибка", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
                return;
            }
            if (comboBoxProduct.SelectedValue == null)
            {
                MessageBox.Show("Выберите изделие", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                APIClient.PostRequest<ZakazBindingModel, bool>("api/Main/CreateOrder", new ZakazBindingModel
                {
                    PokupatelId = Convert.ToInt32(comboBoxClient.SelectedValue),
                    OutputId = Convert.ToInt32(comboBoxProduct.SelectedValue),
                    Number = Convert.ToInt32(textBoxCount.Text),
                    Summa = Convert.ToDecimal(textBoxSum.Text)
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

        private void buttonCancel_Click_1(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
