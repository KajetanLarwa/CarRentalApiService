namespace CarRentalApi.Domain.Entity
{
    public class ExportCar
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int ProductionYear { get; set; }
        public int Capacity { get; set; }
        public string Category { get; set; }
    }
}