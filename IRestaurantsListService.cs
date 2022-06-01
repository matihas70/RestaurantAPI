namespace RestaurantAPI
{
    public interface IRestaurantsListService
    {
        public string Get();
        public string Post(Address address, Restaurant restaurant);
    }
}
