using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab7
{
    class Program
    {
        public static DataClasses1DataContext context = new DataClasses1DataContext();

        static void Main(string[] args)
        {
            fechaPedido();
            Console.Read();


        }

        // 1
        static void IntroToLING()
        {
            //1.Data Source
            int[] numbers = new int[7] { 0, 1, 2, 3, 4, 5, 6 };

            //2.Query creation
            var numQuery =
                from num in numbers
                where (num % 2) == 0
                select num;

            //3 Query execution
            foreach (int num in numQuery)
            {
                Console.Write("{0,1}", num);

            }
        }
        //11 Lambda
        static void paresconLabda()
        {
            int[] numbers = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var pareslamba = numbers.Where(x => x % 2 == 0).ToArray();
            foreach (var item in pareslamba)
            {
                Console.WriteLine(item.ToString());
            }


        }
        // 2
        static void DataSource()
        {
            var queryAllCustomers = from cust in context.clientes
                                    select cust;
            foreach (var item in queryAllCustomers)
            {
                Console.WriteLine(item.NombreCompañia);

            }
        }
        //22 Lambda
        static void DataSourceLambda()
        {
            var AllCustomersLambda = context.clientes.ToList();
            foreach (var item in AllCustomersLambda)
            {
                Console.WriteLine(item.NombreCompañia);
            }
        }


        // 3
        static void Filtering()
        {
            var queryLondonCustomers = from cust in context.clientes
                                       where cust.Ciudad == "Londres"
                                       select cust;
            foreach (var item in queryLondonCustomers)
            {
                Console.WriteLine(item.Ciudad);

            }
        }
        //33 Lambda

        static void FilterLambda()
        {
            var LondomCustomersLambda = context.clientes.Where(a => a.Ciudad == "Londres").ToList();
            foreach (var item in LondomCustomersLambda)
            {
                Console.WriteLine(item.Ciudad);

            }
        }
        // 4
        static void Ordering()
        {
            var queryLondonCustomer3 =
                from cust in context.clientes
                where cust.Ciudad == "London"
                orderby cust.NombreCompañia ascending
                select cust;
            foreach (var item in queryLondonCustomer3)
            {
                Console.WriteLine(item.NombreCompañia);

            }
        }
        //44 Lambda

        static void OrderLambda()
        {
            var LondomCustomer3Lambda = context.clientes.Where(a => a.Ciudad == "London").OrderBy(b => b.NombreCompañia).ToList();
            foreach (var item in LondomCustomer3Lambda)
            {
                Console.WriteLine(item.NombreCompañia);

            }

        }
        // 5
        static void Grouping()
        {
            var queryCustomerByCity =
                from cust in context.clientes
                group cust by cust.Ciudad;
            foreach (var customerGroup in queryCustomerByCity)
            {
                Console.WriteLine(customerGroup.Key);
                foreach (clientes customer in customerGroup)
                {
                    Console.WriteLine("    {0}", customer.NombreCompañia);

                }

            }
        }
        //55 Lambda

        static void Grouping1()
        {
            var CustomerByCityLambda = context.clientes.GroupBy(a => a.Ciudad).ToList();

            foreach (var item in CustomerByCityLambda)
            {
                Console.WriteLine(item.Key);
                foreach (clientes customers in item)
                {
                    Console.WriteLine("    {0}", customers.NombreCompañia);

                }

            }
        }
        // 6
        static void Grouping2()
        {
            var custQuery =
                from cust in context.clientes
                group cust by cust.Ciudad into custGroup
                where custGroup.Count() > 2
                orderby custGroup.Key
                select custGroup;

            foreach (var item in custQuery)
            {
                Console.WriteLine(item.Key);


            }
        }
        //66
        static void Groupon2Lamba()
        {
            var custLamba = context.clientes
                .GroupBy(a => a.Ciudad)
                .Where(a => a.Key.Count() > 2).OrderBy(b => b.Key);
            foreach (var item in custLamba)
            {
                Console.WriteLine(item.Key);
            }
        }
        // 7
        static void Joining()
        {
            var innerJoinQuery =
                from cust in context.clientes
                join dist in context.Pedidos on cust.idCliente equals dist.IdCliente
                select new { CustomerName = cust.NombreCompañia, DistributorName = dist.PaisDestinatario };

            foreach (var item in innerJoinQuery)
            {
                Console.WriteLine(item.CustomerName);

            }

        }
        //77 Lambda
        static void JoingLamdba()
        {
            var innerJoinLamba = context.clientes.Join(context.Pedidos, a => a.idCliente,
                b => b.IdCliente,
                (a, b) => new { a.NombreCompañia, b.PaisDestinatario });

            foreach (var item in innerJoinLamba)
            {
                Console.WriteLine($"{item.NombreCompañia} y destinatario : {item.PaisDestinatario} ");

            }
        }
        // Extras del profesor::)S

        static void fechaPedido()
        {
            DateTime data = new DateTime(1994, 08, 04);
            String fechadeepedido = "1994-08-04";
            DateTime oDate = Convert.ToDateTime(fechadeepedido);

            var queryuPedido = context.clientes.Join(context.Pedidos, a => a.idCliente,
                b => b.IdCliente,
                (a, b) => new { a.NombreCompañia, b.IdPedido });
            var queryPedido2 = context.Pedidos.Where(b => b.FechaPedido == oDate).Join(context.detallesdepedidos, a => a.IdPedido,
                b => b.cantidad,
                (a, b) => new { a.IdPedido, b.cantidad });

            foreach (var item in queryuPedido)
            {
                Console.WriteLine(item.NombreCompañia);

                foreach (var item2 in queryPedido2)
                {
                    Console.WriteLine($"{item2.cantidad} numeros:");

                }
            }
        }

    }
}





