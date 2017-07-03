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
        linqDatabaseDataContext db = new linqDatabaseDataContext();

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
            int writerId = DBHelper.getWriterId(cbWriter.Text);
            int bookId = DBHelper.getBookId(cbBook.Text);
            var q = db.TableBDetails.Where(x => x.WriterID.Equals(writerId)).FirstOrDefault();
            if (q != null)
            {
                q.WriterID = writerId;
                q.BookID = bookId;
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
