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
        //objek database dari model linqdatabase
        linqDatabaseDataContext db = new linqDatabaseDataContext();

        public FormBook()
        {
            InitializeComponent();

            dgvMain.Columns.Add("", "ID");
            dgvMain.Columns.Add("", "TITLE");

            //load data langsung
            loadData();
        }

        private void loadData()
        {
            dgvMain.Rows.Clear();
            var q = db.TableBooks.ToList();
            foreach(var a in q)
            {
                dgvMain.Rows.Add(a.BookID, a.BookTitle); //masukin data a ke row
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            //optional ini buat validasi ada spasi di depan
            if (string.IsNullOrWhiteSpace(tbTitle.Text))
            {
                MessageBox.Show("Pastikan TextBox Terisi");
                return; //untuk membalikkan kondisi ke awal sebelum validasi
            }
            TableBook book = new TableBook();
            book.BookTitle = tbTitle.Text.Trim(); //value dari judul buku diambil dari textbox
            db.TableBooks.InsertOnSubmit(book); //di insert ke tabel
            
            try
            {
                db.SubmitChanges(); //menghitung objek yg diubah, dan mengeksekusi perintah untuk database
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                db.TableBooks.DeleteOnSubmit(book); //pending delete
            }
            
            loadData();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            for(int i = 0; i < dgvMain.Rows.Count; i++ )
            {
                int id = Int32.Parse(dgvMain.Rows[i].Cells[0].Value.ToString());
                string title = dgvMain.Rows[i].Cells[1].Value.ToString();

                var q = db.TableBooks.Where(x => x.BookID.Equals(id)).FirstOrDefault();
                if(q != null)
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

            loadData();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = Int32.Parse(dgvMain.Rows[dgvMain.CurrentRow.Index].Cells[0].Value.ToString());

            var q = db.TableBooks.Where(x => x.BookID.Equals(id)).FirstOrDefault();
            if(q != null)
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

            loadData();
        }
    }
}
