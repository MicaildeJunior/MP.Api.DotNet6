﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MP.ApiDotNet6.Application.DTOs;

public class PurchaseDTO
{
    // Atributos para trafegar os dados
    public int Id { get; set; }
    public string CodeErp { get; set; }
    public string Document { get; set; }
    public string? ProductName { get; set; }
    public decimal? Price { get; set; }
}
