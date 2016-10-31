using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Bangazon.Models
{
  /**
   * Class: LineItem
   * Purpose: Represents the LineItem table in the database
   * Author: Matt Hamil
   */
  public class LineItem
  {
    [Key]
    public int LineItemId {get;set;}

    [Required]
    [DataType(DataType.Date)]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime DateCreated { get; set; }

    [Required]
    public int OrderId { get; set; }
    public Order Order { get; set; }

    [Required]
    public int ProductId { get; set; }
    public Product Product { get; set; }
  }
}
