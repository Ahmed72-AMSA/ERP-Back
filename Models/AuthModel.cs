using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace erp_back.Models;

public class Authentication{
[Key]
[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
public  int ID { get; set; }


[Required]
[StringLength(100)]
public required string companyName { get; set; }


[Required]
[EmailAddress]
public required string Email { get; set; }




public string? FilePath { get; set; } 

[NotMapped] 
public IFormFile? File { get; set; } 




[Required]
public required string Password { get; set; }


public string? role { get; set; } = "admin";






}