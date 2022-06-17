using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace CPSDevExerciseWeb.Models
{
    [DataContract]
    [Index(new string[] { "CustomerName" })]
    [XmlRoot(ElementName = "Order")]
    public class Order
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        [XmlAttribute(AttributeName = "CustomerName")]
        public string CustomerName { get; set; }
        
        [Required, EmailAddress, MaxLength(200)]
        [XmlAttribute(AttributeName = "CustomerEmail")]
        public string CustomerEmail { get; set; }

        [XmlAttribute(AttributeName = "Quantity")]
        public long Quantity { get; set; }

        [XmlAttribute(AttributeName = "Size")]
        public decimal Size { get; set; }

        [XmlAttribute(AttributeName = "DateRequired")]
        public DateTime DateRequired { get; set; }

        [XmlAttribute(AttributeName = "Notes")]
        public string Notes { get; set; }
    }
}
