using MP.ApiDotNet6.Domain.Validations;

namespace MP.ApiDotNet6.Domain.Entities;

// Essa classe não pode ser herdada, pq é Selada.
public sealed class Person
{
    // private em set para não permitir que sejam acessados fora da classe
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Document { get; private set; }
    public string Phone { get; private set; }

    // Uma Pessoa pode ter mais de uma Compra - EntityFrameWork
    public ICollection<Purchase> Purchases { get; set; }
    public ICollection<PersonImage> PersonImages { get; private set; }

    // Construtor para Adicionar Pessoa
    public Person(string document, string name, string phone)
    {
        Validation(document, name, phone);
        // Inicializando a Lista, se n ela começaria nulo
        Purchases = new List<Purchase>();
        PersonImages = new List<PersonImage>();
    }

    // Construtor para Editar Pessoa
    public Person(int id, string document, string name, string phone)
    {
        DomainValidationException.When(id < 0, "id inválido");
        Id = id;
        Validation(document, name, phone);
        // Inicializando a Lista, se n ela começaria nulo
        Purchases = new List<Purchase>();
        PersonImages = new List<PersonImage>();
    }

    // Método de validação privado
    private void Validation(string document, string name, string phone)
    {
        DomainValidationException.When(string.IsNullOrEmpty(name), "Nome deve ser informado!");
        DomainValidationException.When(string.IsNullOrEmpty(document), "Documento deve ser informado!");
        DomainValidationException.When(string.IsNullOrEmpty(phone), "Celular deve ser informado!");

        // Atribuindo os valores
        Name = name;
        Document = document;
        Phone = phone;
    }
}
