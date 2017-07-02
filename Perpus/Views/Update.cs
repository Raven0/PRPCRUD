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

namespace Perpus.Views
{
    public partial class Update : Form
    {
        public Update(int WriterId, int BookId)
        {
            InitializeComponent();

            cbWriter.DataSource = DBHelper.getWriterName();
            cbWriter.SelectedItem = DBHelper.getWriterNameId(WriterId);
            cbWriter.DropDownStyle = ComboBoxStyle.DropDownList;

            cbBook.DataSource = DBHelper.getBookTitle();
            cbBook.SelectedItem = DBHelper.getBookTitleId(BookId);
            cbBook.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            int id = DBHelper.getWriterId(dgvMain.Rows[dgvMain.CurrentRow.Index].Cells[0].Value.ToString());
            string title = dgvMain.Rows[i].Cells[1].Value.ToString();
            var q = db.TableWriters.Where(x => x.WriterID.Equals(id)).FirstOrDefault();
            if (q != null)
            {
                q.WriterName = title;
                try
                {
                    db.SubmitChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
