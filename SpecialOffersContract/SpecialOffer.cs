namespace SpecialOffersContract
{
  using System.Collections.Generic;

  public class SpecialOffer
  {
    public string Name { get; set; }
    public string Description { get; set; }
    public IEnumerable<string> Keywords { get; set; }
    public int Id { get; private set; }

    public SpecialOffer(string name, string description, IEnumerable<string> keywords)
    {
      this.Name = name;
      this.Description = description;
      this.Keywords = keywords;
    }

    public SpecialOffer()
    {
      
    }

    public void AssingId(int id)
    {
      this.Id = id;
    }
  }
}