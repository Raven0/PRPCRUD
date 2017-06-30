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

namespace Perpus
{
    public partial class FormWriter : Form
    {
        linqDatabaseDataContext db = new linqDatabaseDataContext();

        public FormWriter()
        {
            InitializeComponent();

            dgvMain.Columns.Add("", "ID");
            dgvMain.Columns.Add("", "NAME");

            loadData();
        }

        private void loadData()
        {
            dgvMain.Rows.Clear();
            var q = db.TableWriters.ToList();
            foreach( var a in q)
            {
                dgvMain.Rows.Add(a.WriterID, a.WriterName);
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            TableWriter writer = new TableWriter();
            writer.WriterName = tbName.Text.Trim();
            db.TableWriters.InsertOnSubmit(writer);

            try
            {
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                db.TableWriters.DeleteOnSubmit(writer);
            }

            loadData();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            for(int i = 0; i < dgvMain.Rows.Count; i++)
            {
                int id = Int32.Parse(dgvMain.Rows[i].Cells[0].Value.ToString());
                string title = dgvMain.Rows[i].Cells[1].Value.ToString();
                var q = db.TableWriters.Where(x => x.WriterID.Equals(id)).FirstOrDefault();
                if(q != null)
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = Int32.Parse(dgvMain.Rows[dgvMain.CurrentRow.Index].Cells[0].Value.ToString());
            var q = db.TableWriters.Where(x => x.WriterID.Equals(id)).FirstOrDefault();
            if(q != null)
            {
                db.TableWriters.DeleteOnSubmit(q);
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
