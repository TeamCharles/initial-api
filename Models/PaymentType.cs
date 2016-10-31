using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Bangazon.Models
{
  /**
   * Class: PaymentType
   * Purpose: Represents the PaymentType table in the database
   * Author: Garrett Vangilder
   */
  public class PaymentType
  {
    [Key]
    public int PaymentTypeId {get;set;}

    [Required]
    [DataType(DataType.Date)]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime DateCreated {get;set;}

    [Required]
    [StringLength(12)]
    public string Description { get; set; }

    [Required]
    [StringLength(20)]
    public string AccountNumber { get; set; }
    
    [Required]
    public int UserId {get;set;}
    public User User {get;set;}
  }
}
