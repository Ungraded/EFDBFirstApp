using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace EFDBFirstApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new PankkiEntitiesTrue())
            {
                // haetaan onko customerssissa ketään?
                var testi = context.customers.First<customers>();

                if (testi.Equals(null))
                {
                    var olio = new customers()
                    {
                        customer_number = 1,
                        customer_first_name = "Peppi",
                        customer_last_name = "Pitkätossu"
                    };
                    context.customers.Add(olio);
                    context.SaveChanges();
                }
                else
                {
                    foreach (customers iter in context.customers)
                    {
                        Console.WriteLine(iter.customer_first_name + " " + iter.customer_last_name);
                    }

                    var juttu =
                        from cust in context.customers
                        select cust.customer_first_name + " " + cust.customer_last_name;

                    foreach (var asia in juttu)
                    {
                        Console.WriteLine(asia);
                    }
                }

                // Muuta Peppi pitkätossun nimi
                // Pippa Lythyttossuksi
                // taulun arvon muutos
                var henkilö = context.customers
                    .FirstOrDefault<customers>(x => x.customer_first_name.Equals("Hessu"));

                //henkilö.customer_first_name = "HessuToo";
                //context.SaveChanges();
            }

            using (var context = new PankkiEntitiesTrue())
            {

                //
                // Leikitään accountilla
                //

                /*
                // 1) Luokaa 2 accounttia tietokantaan
                accounts insertAccountA = new accounts
                {
                    account_name = "Acc A",
                    account_type = 1,
                    credit_limit = 0,
                    account_saldo = 1000,
                };
                context.accounts.Add(insertAccountA);
                accounts insertAccountB = new accounts
                {
                    account_name = "Acc B",
                    account_type = 1,
                    credit_limit = 0,
                    account_saldo = 2000,
                };
                context.accounts.Add(insertAccountB);

                context.SaveChanges();
                */

                // 2) muuta toisen accountin saldoa
                var query = from acc in context.accounts
                            where acc.account_number == 2
                            select acc
                ;
                //query.First<accounts>().account_saldo = 10;
                //  tai:
                foreach (accounts acc in query)
                {
                    acc.account_saldo = 400;
                }

                context.SaveChanges();

                /*foreach (accounts acc in query)
                {
                    acc.account_saldo = 5000;
                }*/
                //context.SaveChanges();

                /*accounts changeBalanceAccount = new accounts
                {

                };*/

                // 3) tulosta accounttien sisältö

            }
            Console.ReadLine();
        }
    }
}
