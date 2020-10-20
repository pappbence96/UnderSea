namespace StrategyGame.Model.Entities
{
    public interface IApplicationUser
    {
        public int CountryId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}