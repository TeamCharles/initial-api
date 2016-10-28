using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bangazon.Models
{
  public class ProductSubType
  {
      [Key]
      public int ProductSubTypeId { get;set; }

      [Required]
      [DataType(DataType.Date)]
      [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
      public DateTime DateCreated {get;set;}

      [required]
      [StringLength(20)]
      public string Label { get; set; }

      [required]
      public int ProductTypeId { get; set; }
  }
}