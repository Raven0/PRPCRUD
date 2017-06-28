using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Perpus
{
    public partial class FormBook : Form
    {
        linqDatabaseDataContext db = new linqDatabaseDataContext();
        public FormBook()
        {
            InitializeComponent();

            dgvMain.Columns.Add("", "ID");
            dgvMain.Columns.Add("", "TITLE");

            loadData();
        }

        private void loadData()
        {
            dgvMain.Rows.Clear();
            var q = db.TableBooks.ToList();
            foreach(var a in q)
            {
                dgvMain.Rows.Add(a.BookID, a.BookTitle);
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbTitle.Text))
            {
                MessageBox.Show("Pastikan TextBox Terisi");
                return;
            }
            TableBook book = new TableBook();
            book.BookTitle = tbTitle.Text.Trim();
            db.TableBooks.InsertOnSubmit(book);
            
            try
            {
                db.SubmitChanges();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                db.TableBooks.DeleteOnSubmit(book);
            }
            
            loadData();
        }
    }
}
