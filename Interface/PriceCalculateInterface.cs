namespace Car_Rental_Management_System.Interface
{
    public interface PriceCalculateInterface
    {
        decimal CalculatePrice(decimal baseRate, int days);
    }
}
