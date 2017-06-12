namespace NotificationMicroservice
{
  using System;
  using Nancy;
  using Nancy.ModelBinding;

  public class Notification
  {
    public string Type { get; set; }
    public string Name { get; set; }
  }

  public class NotificationModule : NancyModule
  {
    public NotificationModule() : base("/notifications")
    {
      Post("/", _ =>
      {
        var notification = this.Bind<Notification>();
        Console.WriteLine($"Sending {notification.Type} to {notification.Name}");
        return 200;
      });
    }
  }
}