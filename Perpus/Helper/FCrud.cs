using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Perpus.Helper
{
    class FCrud
    {
        linqDatabaseDataContext db = new linqDatabaseDataContext();

        TextBox tb;
        DataGridView dgv;

        public void create()
        {
            if (string.IsNullOrWhiteSpace(tb.Text))
            {
                MessageBox.Show("Pastikan TextBox Terisi");
                return;
            }
            TableBook book = new TableBook();
            book.BookTitle = tb.Text.Trim();
            db.TableBooks.InsertOnSubmit(book);

            try
            {
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                db.TableBooks.DeleteOnSubmit(book);
            }
        }

        public void read()
        {
            dgv.Rows.Clear();
            var q = db.TableBooks.ToList();
            foreach (var a in q)
            {
                dgv.Rows.Add(a.BookID, a.BookTitle); //masukin data a ke row
            }
        }

        public void update()
        {
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                int id = Int32.Parse(dgv.Rows[i].Cells[0].Value.ToString());
                string title = dgv.Rows[i].Cells[1].Value.ToString();

                var q = db.TableBooks.Where(x => x.BookID.Equals(id)).FirstOrDefault();
                if (q != null)
                {
                    q.BookTitle = title;
                    try
                    {
                        db.SubmitChanges();
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
        }

        public void delete()
        {
            int id = Int32.Parse(dgv.Rows[dgv.CurrentRow.Index].Cells[0].Value.ToString());

            var q = db.TableBooks.Where(x => x.BookID.Equals(id)).FirstOrDefault();
            if (q != null)
            {
                db.TableBooks.DeleteOnSubmit(q);
                try
                {
                    db.SubmitChanges();
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}
