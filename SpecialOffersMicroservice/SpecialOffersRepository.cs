namespace SpecialOffersMicroservice
{
  using System.Collections.Generic;
  using System.Linq;
  using System.Threading.Tasks;
  using EasyNetQ;
  using SpecialOffersContract;

  public interface ISpecialOffersRepository
  {
    Task Add(SpecialOffer newOffer);
    Task<SpecialOffer> GetById(int id);
  }

  public class SpecialOffersRepository : ISpecialOffersRepository
  {
    private readonly IBus bus;
    private static List<SpecialOffer> Database = new List<SpecialOffer>();

    public SpecialOffersRepository(IBus bus)
    {
      this.bus = bus;
    }
    
    public Task Add(SpecialOffer newOffer)
    {
      newOffer.AssingId(Database.Count + 1);
      Database.Add(newOffer);
      return this.bus.PublishAsync(newOffer);
    }

    public Task<SpecialOffer> GetById(int id)
    {
      return Task.FromResult(Database.FirstOrDefault(o => o.Id == id));
    }
  }
}