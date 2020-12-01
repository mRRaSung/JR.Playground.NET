using System;

namespace DesignPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            飲料 drink = new 珍珠奶茶();
            drink = new 椰果(drink);
            drink = new 檸檬(drink);
            drink = new 粉條(drink);

            Console.WriteLine(drink.GetDescription());
            Console.WriteLine(drink.GetCost());
        }
    }
}
