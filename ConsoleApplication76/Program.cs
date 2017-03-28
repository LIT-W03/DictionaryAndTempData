using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication76
{
    class Program
    {
        static void Main(string[] args)
        {
            //hello
            //There were 0 A's
            //There were 0 B's
            //...
            //There were 1 E's
            //..
            //There were 2 L's
            //ToUpper
            string g = "Hello from your face";
            g = g.ToUpper();
            Console.WriteLine(g);
            foreach (char c in g)
            {
                
            }

            Dictionary<string, int> numbers = new Dictionary<string, int>();
            numbers.Add("One", 1);
            numbers.Add("Two", 2);
            numbers.Add("Three", 3);
            numbers.Add("Four", 4);
            numbers.Add("Five", 5);
            numbers.Add("Six", 6);
            numbers.Add("Seven", 7);
            numbers.Add("Eight", 8);
            numbers.Add("Nine", 9);
            numbers.Add("Ten", 10);

            //numbers["Ten"]++;

            //int result = numbers["Four"] + numbers["Ten"];

            //IEnumerable<string> x = numbers.Keys;
            IEnumerable<int> x = numbers.Values;

            foreach (KeyValuePair<string, int> kvp in numbers)
            {
                Console.WriteLine(kvp.Key + " - " + kvp.Value);
            }

            //List<Order> orders = new NorthwindDb().GetOrders();
            //#region ignore
            //int max = orders.Max(o => o.OrderId);
            //Random rnd = new Random();

            //for (int i = 1; i <= 5000000; i++)
            //{
            //    Order f = orders[rnd.Next(orders.Count)];
            //    Order o = new Order
            //    {
            //        OrderDate = f.OrderDate,
            //        ShipAddress = f.ShipAddress,
            //        ShipName = f.ShipName,
            //        OrderId = max + i
            //    };
            //    orders.Add(o);
            //}

            //orders = orders.OrderBy(o => Guid.NewGuid()).ToList();
            //#endregion
            //DoItWithLists(orders, 10671, 10702, 10743, 10748, 10781, 10817, 10880, 10858, 10956, 11002);
            //DoItWithDictionaries(orders, 10671, 10702, 10743, 10748, 10781, 10817, 10880, 10858, 10956, 11002);

            Console.ReadKey(true);
        }

        private static void DoItWithLists(List<Order> orders, params int[] orderIds)
        {
            Stopwatch watch = Stopwatch.StartNew();
            List<Order> result = new List<Order>();
            foreach (int id in orderIds)
            {
                foreach (Order o in orders)
                {
                    if (o.OrderId == id)
                    {
                        result.Add(o);
                        break;
                    }
                }
            }

            watch.Stop();
            Console.WriteLine($"List Version Took a total of {watch.ElapsedMilliseconds}");
        }

        private static void DoItWithDictionaries(List<Order> orders, params int[] orderIds)
        {
            Stopwatch watch = Stopwatch.StartNew();
            Dictionary<int, Order> ordersDictionary = new Dictionary<int, Order>();
            foreach (Order order in orders)
            {
                ordersDictionary.Add(order.OrderId, order);
            }
            List<Order> result = new List<Order>();
            foreach (int orderId in orderIds)
            {
                result.Add(ordersDictionary[orderId]);
            }
            watch.Stop();
            Console.WriteLine($"Dictionary Version Took a total of {watch.ElapsedMilliseconds}");
        }
    }

    public class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string ShipName { get; set; }
        public string ShipAddress { get; set; }
    }

    public class NorthwindDb
    {
        public List<Order> GetOrders()
        {
            using (SqlConnection con = new SqlConnection(Properties.Settings.Default.ConStr))
            using (SqlCommand cmd = con.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM Orders";
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                List<Order> orders = new List<Order>();
                while (reader.Read())
                {
                    Order o = new Order();
                    o.OrderId = (int)reader["OrderId"];
                    o.OrderDate = (DateTime)reader["OrderDate"];
                    o.ShipName = (string)reader["ShipName"];
                    o.ShipAddress = (string)reader["ShipAddress"];
                    orders.Add(o);
                }

                return orders;
            }
        }
    }
}
