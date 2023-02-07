using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using static WeatherData.Data.FileData;
using WeatherData.Data;
using static WeatherData.Program;

namespace WeatherData
{
    public interface iMeasurable
    {
        //prop
        //prop
        public string Location { get; set; }
        //void Kallaste();
        //void Varmaste();
        void AvgValues();
    }
    public class Data1
    {
        private const string filePath = "../../../Data/tempdata5-med fel.txt";
        public string Location { get; set; }

        public static List<iMeasurable> CreateData()
        {
            List<iMeasurable> data = new List<iMeasurable>();
            data.Add(new Inomhus() { Location = "Inne" });
            data.Add(new Utomhus() { Location = "Ute" });

            return data;
        }
        //public virtual void Kallaste()
        //{
        //    //Placeholder
        //}
        //public virtual void Varmaste()
        //{
        //    //Placeholder
        //}

        public virtual void AvgValues()
        {
            List<FileData> dataList = new List<FileData>();
            dataList = Helpers.ReadTextFile(filePath);
            int menuChoise = 0;
            DateTime inputDate = PromptUserForDate();
            //Console.WriteLine("1. Inne\n2. Ute");
            //menuChoise = Helpers.TryNumber(menuChoise, 1, 2);
            //Location = menuChoise == 1 ? "Inne" : "Ute";


            List<FileData> filteredData = dataList.Where(d => d.DateTime.Date == inputDate.Date && d.Location == Location).ToList();

            double dataAvgTemp = filteredData.Average(x => x.Temperature);
            double dataAvgResult = filteredData.Average(x => x.Humidity);

            if (Location == "Inne")
            {
                Console.WriteLine("Average temperature inside: " + Math.Round(dataAvgTemp, 2));
            }
            else if (Location == "Ute")
            {
                Console.WriteLine("Average temperature outside: " + Math.Round(dataAvgTemp, 2));
                Console.WriteLine("Average humidity: " + Math.Round(dataAvgResult, 2));
            }
            else
            {
                Console.WriteLine("Couldnt find any data from inside on that day.");
            }
        }
    }
    public class Inomhus : Data1, iMeasurable
    {

        public void AvgValues()
        {

        }
    }


    public class Utomhus : Data1, iMeasurable
    {
        //public virtual void Kallaste()
        //{
        //    //Placeholder
        //}
        //public virtual void Varmaste()
        //{
        //    //Placeholder
        //}
        public void AvgValues()
        {

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
