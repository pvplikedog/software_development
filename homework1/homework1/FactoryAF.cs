namespace homework1;

public class FactoryAF
{
    public List<Customer> Customers { get; private set; }
    
    public List<Car> Cars { get; init; }

    public FactoryAF(List<Customer> customers)
    {
        Customers = customers;
        Cars = [];
    }

    public void SaleCar()
    {
        foreach (Customer customer in Customers)
        {
            customer.Car ??= Cars.LastOrDefault();

            if (customer.Car == null)
            {
                // Means no cars left.
                break;
            }
            
            Cars.RemoveAt(Cars.Count - 1);
        }
        
        Customers = Customers.Where(customer => customer.Car != null).ToList();
        Cars.Clear();
    }

    public void AddCar()
    {
        var car = new Car { Number = Cars.Count + 1 };
        Cars.Add(car);
    }
}