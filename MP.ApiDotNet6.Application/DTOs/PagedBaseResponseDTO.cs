using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MP.ApiDotNet6.Application.DTOs;

public class PagedBaseResponseDTO<T>
{
    public int TotalRegisters { get; private set; }
    public List<T> Data { get; private set; }

    // Passando o construtor msm tendo o private, quando for fazer a instancia (new)......
    // Fica sendo obrigatorio passar o tipo List<> e o int
    public PagedBaseResponseDTO(int totalRegisters, List<T> data)
    {
        TotalRegisters = totalRegisters;
        Data = data;
    }
}
