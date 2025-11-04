using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.Api.Core.Entities;

public class RefreshTokenUser
{
    public required Guid UserId { get; set; }
    
    public required string RefreshToken { get; set; }
    
    public DateTime RefreshTokenExpiration { get; set; }
}