using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Thinktecture.Boardist.WebApi.DTOs
{
  [Serializable, XmlRoot("items")]
  public class BoardGameGeekApiResult
  {
    [XmlElement("item")]
    public BoardGame Game { get; set; }

    public class BoardGame
    {
      [XmlElement("image")]
      public string Image { get; set; }

      [XmlElement("link")]
      public List<Link> Link { get; set; }

      [XmlElement("maxplayers")]
      public AttributeValue MaxPlayers { get; set; }

      [XmlElement("maxplaytime")]
      public AttributeValue MaxPlayTime { get; set; }

      [XmlElement("minage")]
      public AttributeValue MinAge { get; set; }

      [XmlElement("minplayers")]
      public AttributeValue MinPlayers { get; set; }

      [XmlElement("minplaytime")]
      public AttributeValue MinPlayTime { get; set; }
    }

    public class Link
    {
      [XmlAttribute("id")]
      public int Id { get; set; }

      [XmlAttribute("type")]
      public string Type { get; set; }

      [XmlAttribute("value")]
      public string Value { get; set; }
    }

    public class AttributeValue
    {
      [XmlAttribute("value")]
      public int Value { get; set; }
      
      public static implicit operator int(AttributeValue value)
      {
        return value.Value;
      }
    }
  }
}
