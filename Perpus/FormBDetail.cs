﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Perpus.Helper;

namespace Perpus
{
    public partial class FormBDetail : Form
    {

        linqDatabaseDataContext db = new linqDatabaseDataContext();
        public FormBDetail()
        {
            InitializeComponent();

            dgvMain.Columns.Add("", "Writer ID");
            dgvMain.Columns.Add("", "Book ID");

            cbWriter.DataSource = DBHelper.getWriterName();
            cbWriter.DropDownStyle = ComboBoxStyle.DropDownList;

            cbBook.DataSource = DBHelper.getBookTitle();
            cbBook.DropDownStyle = ComboBoxStyle.DropDownList;

            loadData();
        }

        private void loadData()
        {
            dgvMain.Rows.Clear();
            var q = db.TableBDetails.ToList();
            foreach (var a in q)
            {
                dgvMain.Rows.Add(a.WriterID, a.BookID);
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            TableBDetail detail = new TableBDetail();
            detail.WriterID = DBHelper.getWriterId(cbWriter.Text);
            detail.BookID = DBHelper.getBookId(cbBook.Text);
            db.TableBDetails.InsertOnSubmit(detail);

            try
            {
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                db.TableBDetails.DeleteOnSubmit(detail);
            }

            loadData();
        }
    }
}
