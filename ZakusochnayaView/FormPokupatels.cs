﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ZakusochnayaServiceDAL;
using ZakusochnayaServiceDAL.BindingModel;
using ZakusochnayaServiceDAL.ViewModel;

namespace ZakusochnayaView
{
    public partial class FormPokupatels : Form
    {
        public FormPokupatels()
        {
            InitializeComponent();
        }
        private void FormPokupatels_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            try
            {
                List<PokupatelViewModel> list = APIClient.GetRequest<List<PokupatelViewModel>>("api/Pokupatel/GetList");
                if (list != null)
                {
                    dataGridView.DataSource = list;
                    dataGridView.Columns[0].Visible = false;
                    dataGridView.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            }
        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var form = new FormPokupatel();
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }
        private void buttonChange_Click_1(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                var form = new FormPokupatel();
                form.Id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }

        private void buttonDel_Click_1(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int id =
                    Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                    try
                    {
                        APIClient.PostRequest<PokupatelBindingModel, bool>("api/Pokupatel/DelElement", new PokupatelBindingModel
                        {
                            Id = id
                        });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    }
                    LoadData();
                }
            }
        }

        private void buttonUpdate_Click_1(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
