using MP.ApiDotNet6.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace MP.ApiDotNet6.Domain.Entities;

// Essa classe não pode ser herdada, pq é Selada.
public sealed class Product
{
    // private em set para não permitir que sejam acessados fora da classe
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string CodeErp { get; private set; }
    public decimal Price { get; private set; }

    // Esse mesmo Produto pode estar em outras Compras e em várias Pessoas diferentes - EntityFrameWork
    public ICollection<Purchase> Purchases { get; set; }

    // Construtor para Adicionar Produto
    public Product(string name, string codeErp, decimal price)
    {
        Validation(name, codeErp, price);
    }

    // Construtor para Editar Produto
    public Product(int id, string name, string codeErp, decimal price)
    {
        DomainValidationException.When(id < 0, "id do produto deve ser informado");
        Id = id;
        Validation(name, codeErp, price);
        // Inicializando a Lista, se n ela começaria nulo
        Purchases = new List<Purchase>();
    }

    // Método de validação privado
    private void Validation(string name, string codeErp, decimal price)
    {
        DomainValidationException.When(string.IsNullOrEmpty(name), "Nome deve ser informado!");
        DomainValidationException.When(string.IsNullOrEmpty(codeErp), "Codigo erp deve ser informado!");
        DomainValidationException.When(price < 0, "Preço deve ser informado!");

        Name = name;
        CodeErp = codeErp;
        Price = price;
    }
}
