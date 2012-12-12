namespace FooBakery.Models {

    public class Order {
        public int Id { get; set; }
        public string User { get; set; }
        public string DeliveryAddress { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }        
    }

    public class OrderViewModel {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string DeliveryAddress { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
    }

    public class DealViewModel {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public string DeliveryAddress { get; set; }
    }
}