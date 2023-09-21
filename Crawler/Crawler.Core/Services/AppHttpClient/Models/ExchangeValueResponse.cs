﻿namespace Crawler.Core.Services.AppHttpClient.Models;

public class ExchangeValueResponse
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string EngName { get; set; }
    public string ISO_Char_Code { get; set; }
    public int Nominal { get; set; }
    public DateTime Date { get; set; }
    public decimal Value { get; set; }
}