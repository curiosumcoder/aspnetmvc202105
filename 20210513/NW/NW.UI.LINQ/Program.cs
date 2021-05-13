using NW.Data;
using NW.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Dynamic.Core;
using System.Xml.Linq;
using System.Transactions;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace NW.UI.LINQ
{
    class Program
    {
        static void Main(string[] args)
        {
            string lectura = "";
            do
            {
                Console.WriteLine("Seleccione una opción:\n1. Basic\n2. CRUD\n3. LINQ To Entities\n4. Eager Loading\n5. Lazy Loading\n6. LINQ Dynamic\n7. LINQ To XML\n8. PLINQ\n9. Transactions\n10. Store Procedures\n11. Raw SQL\n12. Raw SQL Client (ADO.NET)\n");

                lectura = Console.ReadLine();

                if (!string.IsNullOrEmpty(lectura))
                {
                    int opcion = Convert.ToInt32(lectura);
                    Console.Clear();

                    switch (opcion)
                    {
                        case 1:
                            Console.WriteLine("Basic");
                            Basic();
                            break;
                        case 2:
                            Console.WriteLine("CRUD");
                            CRUD();
                            break;
                        case 3:
                            Console.WriteLine("LINQToEntities");
                            LINQToEntities();
                            break;
                        case 4:
                            Console.WriteLine("EagerLoading");
                            EagerLoading();
                            break;
                        case 5:
                            Console.WriteLine("LazyLoading");
                            LazyLoading();
                            break;
                        case 6:
                            Console.WriteLine("LINQDynamic");
                            LINQDynamic();
                            break;
                        case 7:
                            Console.WriteLine("LINQToXML");
                            LINQToXML();
                            break;
                        case 8:
                            Console.WriteLine("PLINQ");
                            PLINQ();
                            break;
                        case 9:
                            Console.WriteLine("Transactions");
                            Transactions();
                            break;
                        case 10:
                            Console.WriteLine("StoreProcedures");
                            StoreProcedures();
                            break;
                        case 11:
                            Console.WriteLine("RawSQL");
                            RawSQL();
                            break;
                        case 12:
                            Console.WriteLine("RawSQLClient");
                            RawSQLClient();
                            break;
                    }

                    Console.WriteLine("\n¡Listo!");
                    Console.ReadKey();
                    Console.Clear();
                }

            } while (!string.IsNullOrEmpty(lectura));
        }

        #region Demostraciones

        private static void Basic()
        {
            using (var db = new NorthwindContext())
            {
                // query
                var q0 = from p in db.Products
                         where p.ProductName.Contains("queso")
                         select p;

                var q1 = (from p in db.Products
                          where p.ProductName.Contains("queso")
                          select p).All(p => p.Discontinued);

                // métodos de extensión + expresiones lambda
                var query = db.Products.Where(p => p.ProductName.Contains("queso"));

                bool discontinued = db.Products.All(p => p.Discontinued);

                //foreach (var p in db.Products)
                foreach (var p in query)
                {
                    Console.WriteLine($"{p.ProductID} {p.ProductName}");
                }
            }
        }

        private static void CRUD()
        {
            using (var db = new NorthwindContext())
            {
                int lastId = db.Products.Max(p => p.ProductID);

                var np = new Product()
                {
                    ProductName = $"Demostración #{++lastId}",
                    Discontinued = false
                };

                db.Products.Add(np);
                db.SaveChanges();

                np.ProductName += " ACTUALIZADO";

                foreach (var e in db.ChangeTracker.Entries<Product>())
                {
                    Console.WriteLine($"{e.Property("ProductName").CurrentValue} {e.State}");
                }

                db.SaveChanges();

                db.Products.Remove(np);
                db.SaveChanges();

                foreach (var p in db.Products.Include("Category"))
                {
                    Console.WriteLine($"{p.ProductID}, {p.ProductName}, {p.UnitPrice}, {p.Category?.CategoryName ?? "Sin categoría"}");
                }

                Console.WriteLine($"Total de productos: {db.Products.Count()}");

                Console.ReadLine();
            }
        }

        static void LINQToEntities()
        {
            using (NorthwindContext db = new NorthwindContext())
            {
                // Filtro
                var q1 = from c in db.Customers
                         where c.Country == "Mexico"
                         select c;

                var q1x = db.Customers.
                    Where(c => c.Country == "Mexico");

                //var result = q1.Count();
                //var any = result.Any();

                foreach (var item in q1x)
                {
                    Console.WriteLine($"{item.CustomerID} {item.CompanyName} {item.ContactName} {item.Country}");
                }
            }

            // Proyecciones
            using (NorthwindContext db = new NorthwindContext())
            {
                var q2 = from c in db.Customers
                         select c.Country;
                var q2x = db.Customers.Select(c => c.Country);

                var q2y = from c in db.Customers
                          select new { c.CustomerID, c.ContactName };

                var q2z = db.Customers.Select(c =>
                    new { Id = c.CustomerID, c.ContactName });

                var q2w = db.Customers.Select(c =>
                    new Category() { CategoryName = c.ContactName });

                Console.Clear();
                foreach (var item in q2z)
                {
                    Console.WriteLine($"{item.Id}, {item.ContactName}");
                }
            }

            // SelectMany
            using (NorthwindContext db = new NorthwindContext())
            {
                var q4 = db.Customers.
                    Where(c => c.Country == "Mexico").
                    SelectMany(c => c.Orders);

                var q4x = db.Orders.
                    Where(o => o.Customer.Country == "Mexico");

                Console.Clear();
                foreach (var item in q4)
                {
                    Console.WriteLine($"{item.CustomerID}, {item.OrderID}");
                }
            }

            // Ordenamiento
            using (NorthwindContext db = new NorthwindContext())
            {
                var q5 = from c in db.Customers
                         where c.Orders.Count > 5
                         orderby c.Country descending
                         select c;

                var q5x = db.Customers.
                    Where(c => c.Orders.Count > 5).
                    OrderByDescending(c => c.Country);

                Console.Clear();
                foreach (var item in q5)
                {
                    Console.WriteLine($"{item.CompanyName}, {item.Country}");
                }

                var q6 = from c in db.Customers
                         orderby c.CompanyName, c.ContactTitle,
                         c.ContactName
                         select c;

                var q6x = db.Customers.OrderBy(c =>
                        new
                        {
                            c.CompanyName,
                            c.ContactTitle
                        }).
                    ThenBy(c => c.ContactName);

                Console.Clear();
                foreach (var item in q6)
                {
                    Console.WriteLine($"{item.CompanyName}, {item.Country}");
                }
            }

            // Agrupamiento
            using (NorthwindContext db = new NorthwindContext())
            {
                var q7 = from c in db.Customers
                         group c by c.Country into CustByCountry
                         select CustByCountry;

                var q7x = db.Customers.GroupBy(c => c.Country);

                Console.Clear();
                foreach (var item in q7)
                {
                    Console.WriteLine($"{item.Key}, {item.Count()}");

                    foreach (var c in item)
                    {
                        Console.WriteLine($"\t{c.ContactName}");
                    }
                }

                var q7y = from c in db.Customers
                          group c by new { c.Country, c.City } into CountryCity
                          where CountryCity.Count() > 1
                          select new
                          {
                              Country = CountryCity.Key.Country,
                              City = CountryCity.Key.City,
                              Count = CountryCity.Count(),
                              Items = CountryCity
                          };

                var q7y2 = db.Customers.GroupBy(c => new { c.Country, c.City }).
                    Where(g => g.Count() > 1).
                    Select(g => new
                    {
                        Country = g.Key.Country,
                        City = g.Key.City,
                        Count = g.Count(),
                        Items = g
                    });

                Console.Clear();
                foreach (var item in q7y)
                {
                    Console.WriteLine($"{item.Country}, {item.City}, {item.Count}");

                    foreach (var c in item.Items)
                    {
                        Console.WriteLine($"\t{c.ContactName}");
                    }
                }
            }

            // Join
            using (NorthwindContext db = new NorthwindContext())
            {
                var q8 = from c in db.Customers
                         join o in db.Orders on c.CustomerID
                         equals o.CustomerID
                         select new { c, o };

                //                new { c.CustomerID, c.Country }
                //equals new { o.CustomerID, Country =  o.ShipCountry }

                var q8x = db.Customers.Join(
                    db.Orders, c => c.CustomerID,
                    o => o.CustomerID,
                    (c, o) => new { c, o });

                Console.Clear();
                foreach (var item in q8)
                {
                    Console.WriteLine($"{item.c.CustomerID}, {item.o.OrderID}");
                }

                // Join agrupado
                var q8y = from c in db.Customers
                          join o in db.Orders on c.CustomerID
                          equals o.CustomerID into CustomerOrders
                          select new { c, Orders = CustomerOrders };
                //select CustomerOrders;

                foreach (var ordenes in q8y)
                {
                    //foreach (var orden in ordenes)
                    //{

                    //}
                }

                // Left Ourter Join
                var q8z = from c in db.Customers
                          join o in db.Orders on c.CustomerID
                          equals o.CustomerID into CustomerOrders
                          from detalle in CustomerOrders.DefaultIfEmpty()
                          select new
                          {
                              Customer = c,
                              Order = detalle
                          };

                foreach (var item in q8z)
                {
                    if (item.Order == null)
                    {
                        Console.WriteLine($"Customer {item.Customer.CustomerID} with NO orders!");
                    }
                }
            }

            // Conjuntos
            using (NorthwindContext db = new NorthwindContext())
            {
                var q9 = db.Customers.
                    Select(c => c.Country).Distinct();

                var q10 = db.Customers.Except(
                    db.Customers.Where(
                    c => c.Country == "Mexico")).
                    Select(c => c.Country).Distinct();

                Console.Clear();
                foreach (var item in q10)
                {
                    Console.WriteLine($"{item}");
                }
            }

            // Partición (paginación)
            using (NorthwindContext db = new NorthwindContext())
            {
                var q11 = db.Customers.
                    OrderBy(c => c.CustomerID).
                    Skip(10);
                // Tomar los primero 10 elementos
                var q12 = db.Customers.
                    OrderBy(c => c.CustomerID).
                    Take(10);
                // Segunda página de 10 elementos
                var q13 = db.Customers.
                    OrderBy(c => c.CustomerID).
                    Skip(10).Take(10);

                Console.Clear();
                foreach (var item in q13)
                {
                    Console.WriteLine($"{item.CustomerID}, {item.CompanyName}");
                }
            }

            // Modificación de consulta
            using (NorthwindContext db = new NorthwindContext())
            {
                var q14 = db.Customers.
                    Where(c => c.Orders.Count > 5);

                Console.Clear();
                Console.WriteLine(q14.Count());

                q14 = q14.Where(c => c.Country == "Mexico");
                Console.WriteLine(q14.Count());

                q14 = q14.OrderByDescending(c => c.ContactName);

                foreach (var item in q14)
                {
                    Console.WriteLine(item.ContactName);
                }
            }

            // Métodos útiles
            using (NorthwindContext db = new NorthwindContext())
            {
                var o1 = db.Customers.First();
                o1 = db.Customers.FirstOrDefault();
                o1 = db.Customers.Where(c => c.CustomerID == "ALFKI")
                    .Single();
                o1 = db.Customers.Where(c => c.CustomerID == "ALFKI").
                    SingleOrDefault();

                var o2 = db.Customers.All(c => c.Orders.Count > 5 &&
                        c.Country == "Mexico");
                o2 = db.Customers.
                    Any(c => c.Orders.Count > 5);

                var sum = db.OrderDetails.
                    Sum(od => od.Quantity * od.UnitPrice);
            }
        }

        static void EagerLoading()
        {
            // Eager Loading
            using (NorthwindContext db = new NorthwindContext())
            {
                // Proyección
                var customersOrders =
                    from c in db.Customers.
                        OrderBy(c => c.CustomerID).
                        Skip(10).Take(2)
                    select new
                    {
                        Cliente = c,
                        Ordenes = c.Orders
                    };

                foreach (var c in customersOrders)
                {
                    Console.WriteLine($"{c.Cliente.CustomerID}, {c.Cliente.ContactName}");
                    foreach (var o in c.Ordenes)
                    {
                        Console.WriteLine($"{o.OrderID}, {o.OrderDate}");
                    }
                }

                var customersOrders2 = from c in db.Customers.
                       Include("Orders").
                       Include("Orders.OrderDetails").
                       OrderBy(c => c.CustomerID).
                       Skip(10).Take(2)
                                       select c;

                var customersOrders2x = db.Customers.
                    Include("Orders").
                    Include("CustomerDemographics").Take(2);

                foreach (var c in customersOrders2)
                {
                    Console.WriteLine($"{c.CustomerID}, {c.ContactName}");
                    foreach (var o in c.Orders)
                    {
                        Console.WriteLine($"{o.OrderID}, {o.OrderDate}");

                        foreach (var od in o.OrderDetails)
                        {
                            Console.WriteLine($"{od.ProductID}, {od.Quantity}");
                        }
                    }
                }
            }
        }

        static void LazyLoading()
        {
            // Lazy Loading
            using (NorthwindContext db = new NorthwindContext())
            {
                //bool isLazy = true;
                //db.Configuration.LazyLoadingEnabled = isLazy; // default = true

                // .Include(c=>c.Orders). // Eager Loading
                var customers = db.Customers.
                    OrderBy(c => c.CustomerID);

                foreach (var c in customers.ToList())
                {
                    Console.WriteLine($"{c.CustomerID}, {c.ContactName}");

                    //if (!isLazy)
                    //{
                    //    db.Entry(c).Collection(o => o.Orders).Load();
                    //}

                    Console.WriteLine($"Número de Órdenes: {c.Orders.Count}");
                }

                foreach (var o in db.Orders)
                {
                    //if (!isLazy)
                    //{
                    //    db.Entry(o).Reference(c => c.Customer).Load();
                    //}

                    Console.WriteLine(o.Customer.ContactName);
                    Console.WriteLine($"{o.OrderID}, {o.OrderDate}");
                }
            }
        }

        /// <summary>
        /// https://dynamic-linq.net/
        /// Incluir el paquete https://www.nuget.org/packages/EntityFramework.DynamicLinq
        /// Hacer el using, using System.Linq.Dynamic.Core;
        /// </summary>
        static void LINQDynamic()
        {
            // Ordenamiento
            using (NorthwindContext db = new NorthwindContext())
            {
                string order = "Country DESC, City";

                var q1 = db.Customers.Where(c => c.Orders.Count > 5).OrderBy(order);

                Console.Clear();
                foreach (var item in q1)
                {
                    Console.WriteLine($"{item.CompanyName.PadRight(35)}, {item.Country}, {item.City}");
                }
            }
        }

        static void LINQToXML()
        {
            using (NorthwindContext db = new NorthwindContext())
            {
                List<Customer> customers = db.Customers.ToList();

                // LINQ to Objects
                var q1 = from c in customers
                         where c.Country == "Mexico"
                         select c;
                var q1x = customers.Where(c => c.Country == "Mexico");

                // LINQ to XML
                var docXML = new XElement("customers",
                    from c in customers
                    select new XElement("customer",
                        new XElement("id", c.CustomerID),
                        new XElement("companyName", c.CompanyName),
                        new XElement("contactName", c.ContactName))
                    );
                docXML.Save("customer.xml");

                var docXML2 = XElement.Load("customer.xml");
                var query = from c in docXML2.Descendants("customer")
                            where c.Element("companyName").Value.StartsWith("A", StringComparison.CurrentCulture)
                            select new Customer()
                            {
                                CustomerID = c.Element("id").Value,
                                CompanyName = c.Element("companyName").Value,
                                ContactName = c.Element("contactName").Value
                            };

                foreach (var item in query)
                {
                    Console.WriteLine(item.CompanyName);
                }
            }
        }

        static void PLINQ()
        {
            Console.WriteLine("Experimento PLINQ en proceso ...");

            // Parallel LINQ
            var nums = Enumerable.Range(1, 10000);

            var query = from n in nums.AsParallel()
                        where ToDo(n) == n
                        select ToDo(n);

            var sw = System.Diagnostics.Stopwatch.StartNew();

            var result = query.ToList();

            sw.Stop();

            Console.WriteLine($"Duración: {sw.ElapsedMilliseconds}");
        }

        static int ToDo(int n)
        {
            System.Threading.Thread.SpinWait(1000);
            return n;
        }

        static void Transactions()
        {
            using (var db = new NorthwindContext())
            {
                var tran = db.Database.BeginTransaction();
                try
                {
                    // Acciones
                    tran.Commit();
                }
                catch (Exception)
                {
                    tran.Rollback();
                    throw;
                }
            }

            // Agregar referencia a System.Transactions
            // Agregar: using System.Transactions;
            using (var ts = new TransactionScope())
            {
                using (var db = new NorthwindContext())
                {
                    // CRUD actions
                    db.SaveChanges();
                }
                ts.Complete();
            }
        }

        static void StoreProcedures()
        {
            using (var db = new NorthwindContext())
            {
                var inicio = new DateTime(1998, 1, 1);
                var fin = DateTime.Now;
                var sales = db.SalesByYear(inicio, fin);

                foreach (var s in sales)
                {
                    Console.WriteLine($"{s.OrderID}\t{s.ShippedDate?.ToShortDateString()}\t{s.Subtotal}\t{s.Year}");
                }
            }
        }

        static async void RawSQL()
        {
            using (var db = new NorthwindContext())
            {
                var quantity = 12;

                var pQuantity = new SqlParameter("quantity", quantity);
                int affected = db.Database.ExecuteSqlCommand("update [Order Details] set Quantity = @quantity where OrderID = 10248 and ProductID = 11", pQuantity);
                //db.Database.ExecuteSqlCommandAsync                

                var filter = 1;
                var pFilter = new SqlParameter("filter", filter);
                var qryStr = @"SELECT [ProductID],[ProductName],[SupplierID],[CategoryID]
                        ,[QuantityPerUnit],[UnitPrice],[UnitsInStock],[UnitsOnOrder],[ReorderLevel],[Discontinued]
                        FROM[dbo].[Products] WHERE [ProductID] = @filter";

                var products = db.Products.SqlQuery(qryStr, pFilter);
                foreach (var p in products)
                {
                    Console.WriteLine(p.ProductName);
                }

                var pFilter1 = new SqlParameter("filter", filter);
                var products1 = db.Database.SqlQuery<Product>(qryStr, pFilter1);
                foreach (var p in products1)
                {
                    Console.WriteLine(p.ProductName);
                }

                var pFilter2 = new SqlParameter("filter", filter);
                var products2 = db.Database.SqlQuery(typeof(Product), qryStr, pFilter2);
                foreach (Product p in await products2.ToListAsync())
                {
                    Console.WriteLine(p.ProductName);
                }
            }
        }

        /// <summary>
        /// Hacer referencia a: System.Configuration
        /// Agregar: using System.Configuration;
        /// Agregar: using Microsoft.Data.SqlClient;
        /// Agregar: using System.Data;
        /// </summary>
        static void RawSQLClient()
        {
            var connStr = "data source=.\\sqlexpress;initial catalog=Northwind;integrated security=True;MultipleActiveResultSets=True";
            var CustomerID = "VINET";

            using (var conn = new SqlConnection(connStr))
            {
                var comm = conn.CreateCommand();
                comm.CommandText = @"select o.*, 
                    (select sum(od.Quantity * od.UnitPrice)
                    from[Order Details] od
                    where od.OrderID = o.OrderID) OrderTotal
                from Orders o
                where o.CustomerID = @CustomerID";
                comm.Parameters.Add(new SqlParameter("CustomerID", CustomerID));

                conn.Open();

                using (var reader = comm.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var oId = reader.GetInt32(0);
                        var oTotal = Convert.ToDecimal(reader["OrderTotal"]);
                    }
                }

                var da = new SqlDataAdapter(comm);
                var dt = new DataTable();
                da.Fill(dt);

                // Se puede usar LINQ to DataSets
                var columns = dt.Columns;
                foreach (var dr in dt.AsEnumerable())
                {
                    var line = new StringBuilder();
                    foreach (DataColumn c in columns)
                    {
                        line.Append($"{dr.Field<int>("OrderID")}, ");
                    }
                    Console.WriteLine(line);
                }
            }
        }
        #endregion
    }
}
