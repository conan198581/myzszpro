using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPZSZ.Service;

namespace RPZSZ.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                long i = ctx.Users.LongCount();
                Console.WriteLine(i);
            }
            Console.ReadKey();
        }
    }
}
