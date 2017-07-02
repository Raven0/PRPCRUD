using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Perpus.Helper;
using Perpus.Views;

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
                dgvMain.Rows.Add(DBHelper.getWriterNameId(a.WriterID), DBHelper.getBookTitleId(a.BookID));
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int WriterId = DBHelper.getWriterId(dgvMain.Rows[dgvMain.CurrentRow.Index].Cells[0].Value.ToString());
            int BookId = DBHelper.getBookId(dgvMain.Rows[dgvMain.CurrentRow.Index].Cells[1].Value.ToString());

            Update up = new Update(WriterId, BookId);
            up.ShowDialog();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = DBHelper.getWriterId(dgvMain.Rows[dgvMain.CurrentRow.Index].Cells[0].Value.ToString());
            var q = db.TableBDetails.Where(x => x.WriterID.Equals(id)).FirstOrDefault();
            if(q != null)
            {
                db.TableBDetails.DeleteOnSubmit(q);
                try
                {
                    db.SubmitChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            loadData();
        }
    }
}
