using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;


namespace Bangazon.Models
{
  public class ProductType
  {
    [Key]
    public int ProductTypeId {get;set;}

    [Required]
    [DataType(DataType.Date)]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime DateCreated {get;set;}

    [Required]
    [StringLength(20)]
    public string Label { get; set; }
    public int Quantity { get; set; }
  }
}