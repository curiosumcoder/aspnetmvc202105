using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA3
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new NorthwindEntities())
            {
                foreach (var p in db.Products)
                {
                    Console.WriteLine($"{p.ProductName}, {p.UnitPrice}");
                }
            }

            Console.WriteLine("READY!");
            Console.ReadLine();
        }

        private static void ADONET()
        {
            //SqlConnection conn = new SqlConnection("Data Source=.\\sqlexpress;Initial Catalog=Northwind; Integrated Security=SSPI;");

            //conn.Open();
            //conn.Close();

            using (var conn = new SqlConnection("Data Source=.\\sqlexpress;Initial Catalog=Northwind; Integrated Security=SSPI;"))
            {
                conn.Open();

                var comm = conn.CreateCommand();
                comm.CommandText = "select * from region";

                using (var dr = comm.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Console.WriteLine($"{dr.GetInt32(0)}, {dr.GetString(1)}");
                    }
                }
            }
        }
    }
}
