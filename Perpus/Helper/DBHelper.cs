using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perpus.Helper
{
    class DBHelper
    {
        public static linqDatabaseDataContext db = new linqDatabaseDataContext();

        public static List<string> getWriterName()
        {
            return db.TableWriters.Select(x => x.WriterName).ToList();
        }

        public static List<string> getBookTitle()
        {
            return db.TableBooks.Select(x => x.BookTitle).ToList();
        }

        public static int getWriterId(string name)
        {
            return db.TableWriters.Where(x => x.WriterName.Equals(name)).Select(z => z.WriterID).FirstOrDefault();
        }

        public static int getBookId(string name)
        {
            return db.TableBooks.Where(x => x.BookTitle.Equals(name)).Select(z => z.BookID).FirstOrDefault();
        }
    }
}
