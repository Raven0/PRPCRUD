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

        public static int getWriterId(string name)
        {
            return db.TableWriters.Where(x => x.WriterName.Equals(name)).Select(z => z.WriterID).FirstOrDefault();
        }
    }
}
