using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherData
{
    public interface iMeasurable
    {
        //prop
        //prop
        void Kallaste();
        void Varmaste();


        public class Inomhus : iMeasurable
        {
            public virtual void Kallaste()
            {
                //Placeholder
            }
            public virtual void Varmaste()
            {
                //Placeholder
            }

            public static void ConvertToBajs()
            {
                Helpers.ExtractDataFromFile();
            }
        }


        public class Utomhus
        {
            public virtual void Kallaste()
            {
                //Placeholder
            }
            public virtual void Varmaste()
            {
                //Placeholder
            }
        }


        //////FRÅN INTERFACE DEMO
        //////public interface IEatable
        //////{
        //////    bool Prepared { get; set; }
        //////    int Price { get; set; }

        //////    void Prepare();
        //////    void Bake();
        //////}

        //////public class Pizza
        //////{
        //////    public int Price { get; set; }
        //////    public bool Prepared { get; set; }

        //////    public static List<IEatable> CreatePizzaOrder()
        //////    {
        //////        List<IEatable> order = new List<IEatable>();
        //////        order.Add(new Margerita() { Price = 119 });
        //////        order.Add(new Calzone() { Price = 129 });
        //////        order.Add(new KebabPizza() { Price = 139 });
        //////        return order;
        //////    }
        //////    public virtual void Bake()
        //////    {
        //////        Console.Write("Grädda i ugn 10 minuter.. ");
        //////    }
        //////    public void SetAsPrepared()
        //////    {
        //////        Prepared = true;
        //////    }
        //////}

        //////public class Margerita : Pizza, IEatable
        //////{
        //////    public void Prepare()
        //////    {
        //////        Console.Write("Lägger på ost och tomat... ");
        //////        Console.Write("Massa annat");
        //////        SetAsPrepared();
        //////    }
        //////}
        
        ////// MAIN FRÅN INTERFACE DEMO
        //////internal class Program
        //////{
        //////    static void Main(string[] args)
        //////    {
        //////        List<IEatable> orders = Pizza.CreatePizzaOrder();

        //////        foreach (IEatable pizza in orders)
        //////        {
        //////            pizza.Prepare();

        //////            if (pizza.Prepared)
        //////            {
        //////                pizza.Bake();
        //////                Console.WriteLine("Din " + pizza.GetType().Name + " är färdig, priset är: " + pizza.Price);
        //////            }
        //////        }
        //////    }
        //////}
    }
}
