namespace SpecialOffersMicroservice
{
  using System.Threading.Tasks;
  using Nancy;
  using Nancy.ModelBinding;
  using SpecialOffersContract;

  public class SpecialOffersModule : NancyModule
  {
    public SpecialOffersModule(ISpecialOffersRepository repo)
    {
      Post("/specialoffer", async _ =>
      {
        var newOffer = this.Bind<SpecialOffer>();
        await repo.Add(newOffer).ConfigureAwait(false);
        return this.Negotiate
          .WithStatusCode(HttpStatusCode.Created)
          .WithHeader("Location", $"/specialoffer/{newOffer.Id}");
      });
      
      Get("/specialoffer/{id:int}", async p =>
      {
        var offer = await repo.GetById((int) p.Id);
        if (offer == null)
          return HttpStatusCode.NotFound;
        return offer;
      });
    }
  }
}

