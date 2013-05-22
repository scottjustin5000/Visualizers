using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using EfVisualizer;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            TestExpression();
            TestEfVisualizer();

        }
        static void  TestExpression()
        {
            var p1 = new Person
                {
                    City = "San Francisco",
                    Name = "Scott",
                    State = "CA"
                };
            var p2 = new Person
            {
                City = "Oakland",
                Name = "Scott",
                State = "CA"
            };

            var ppl = new List<Person> {p1, p2};

            var query = BuildEqFunc<Person>("City", "San Francisco");


            ExpressionTreeVisualizer.EtVisualizer.TestShowVisualizer(query);

            var compiled = (query as Expression<Func<Person, bool>>).Compile();

            var results = ppl.Where(compiled);
            if (results.Any())
            {
                Console.WriteLine(results.First().Name);
            }

        }
        private static void TestEfVisualizer()
        {
            var cts = new TestDbContext();

            var query = cts.Persons.Where(e => e.Name == "Scott");
            EfContextVisualizer.TestShowVisualizer(query);
            if (query.Any())
            {
                Console.WriteLine(query.First().Name);
            }

        }
        public static Expression BuildEqFunc<T>(string prop, object val)
        {
            var o = Expression.Parameter(typeof(T), "t");

            Expression<Func<T, bool>> expression = Expression.Lambda<Func<T, bool>>(Expression.Equal(Expression.PropertyOrField(o, prop), Expression.Constant(val)), o);

            return expression; //.Compile();

        }
    }
}
