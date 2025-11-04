using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.Api.Core.Entities;

public class Session
{
    public required Guid Id { get; set; }
    
    public required Guid UserId { get; set; }
    
    public required string AccessToken { get; set; }
    
    public required string RefreshToken { get; set; }
    
    public DateTime AccessTokenExpiration { get; set; }
    
    public DateTime RefreshTokenExpiration { get; set; }
    
    public bool IsActive { get; set; } = true;
    
    // Device Information
    public string? DeviceType { get; set; }          // e.g., Mobile, Tablet, Desktop
    public string? DeviceName { get; set; }          // e.g., iPhone 13, Samsung Galaxy S21
    public string? OperatingSystem { get; set; }     // e.g., Windows 10, iOS 15, Android 12
    public string? Browser { get; set; }             // e.g., Chrome, Safari, Firefox
    
    // Location Information
    public string? IpAddress { get; set; }
    public string? Location { get; set; }            // Could be city, country, etc.
    
    // User Agent for additional device/browser information
    public string? UserAgent { get; set; }
    
    // Timestamps
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime? UpdatedAt { get; set; }
    public DateTime? LastActivityAt { get; set; }    // Last time the session was active
    
    public void Revoke()
    {
        IsActive = false;
        UpdatedAt = DateTime.UtcNow;
    }
    
    public bool IsExpired()
    {
        return DateTime.UtcNow > RefreshTokenExpiration;
    }

    public void UpdateActivity()
    {
        LastActivityAt = DateTime.UtcNow;
    }
}