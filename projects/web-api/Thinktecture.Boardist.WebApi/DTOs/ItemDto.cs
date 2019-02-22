namespace Thinktecture.Boardist.WebApi.DTOs
{
  public abstract class ItemDto : SyncableDto
  {
    public string Name { get; set; }
    public int? BoardGameGeekId { get; set; }
  }
}
