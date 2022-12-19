﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgendaApi.Entities;

public class Contact
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; }
    public long CellPhoneNumber { get; set; }
    public long? TelephoneNumber { get; set; }
    public string? Description { get; set; } = String.Empty;
    
    public ContactsBook ContactsBook { get; set; }
    
    public int? ContactsBookId { get; set; }
}