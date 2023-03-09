using Microsoft.EntityFrameworkCore.Storage;
using MP.ApiDotNet6.Infra.Data.Context;
using MP.ApiDotNet6.Infra.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MP.ApiDotNet6.Domain.Repositories;

public class UnityOfWork : IUnityOfWork
{
    // Conexão com banco de dados e a transação
    // readonly só permite dentro do construtor
    private readonly ApplicationDbContext _db;
    private IDbContextTransaction _transaction;


    public UnityOfWork(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task BeginTransaction()
    {
        _transaction = await _db.Database.BeginTransactionAsync();
    }

    public async Task Commit()
    {
        await _transaction.CommitAsync();
    }

    public async Task Rollback()
    {
        await _transaction.RollbackAsync();
    }

    // Checa se ele é diferente de nulo, caso n for da o Disable
    public void Dispose()
    {
        _transaction?.Dispose();
    }
}
