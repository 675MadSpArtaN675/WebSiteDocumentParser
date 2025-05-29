using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace DatabaseWork.DataProcessors.StandartProcessors
{
    public class Finder
    {
        public static List<int> FindFreeNumbers<T>(DbSet<T> records, Expression<Func<T, int>> finder) where T : class
        {
            IQueryable<int> numbers = records.Select(finder);
            
            return FindFreeNumbersInArray(numbers);
        }

        private static List<int> FindFreeNumbersInArray(IQueryable<int> numbers)
        {
            List<int> free_numbers = new List<int>();

            int num = 0;
            int max = 0;
            bool first = true;
            foreach (var number in numbers)
            {
                if (first)
                {
                    num = number;
                    first = false;
                    continue;
                }

                int division = number - num;

                if (division > 1)
                {
                    for (int i = num + 1; i < division; i++)
                        free_numbers.Add(i);
                }

                if (max < number)
                {
                    max = number;
                }

                num = number;
            }

            free_numbers.Add(max + 1);

            return free_numbers;
        }

    }
}
