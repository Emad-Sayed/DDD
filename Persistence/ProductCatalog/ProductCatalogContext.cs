using Domain.Common.Interfaces;
using Domain.ProductCatalog.AggregatesModel.BrandAggregate;
using Domain.ProductCatalog.AggregatesModel.ProductAggregate;
using Domain.ProductCatalog.AggregatesModel.ProductCategoryAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Data;
using System.Collections.Generic;

namespace Persistence.ProductCatalog
{
    public class ProductCatalogContext : DbContext, IUnitOfWork
    {

        private readonly IMediator _mediator;
        private IDbContextTransaction _currentTransaction;


        public ProductCatalogContext(DbContextOptions<ProductCatalogContext> options) : base(options) { }

        public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;
        public bool HasActiveTransaction => _currentTransaction != null;

        public ProductCatalogContext(DbContextOptions<ProductCatalogContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        }




        public DbSet<Product> Products { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Domain.ProductCatalog.AggregatesModel.ProductAggregate.Unit> Units { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brand>().HasData(new List<Brand>
            {
                new Brand("Brand 1"),
                new Brand("Brand 2"),
            });


            modelBuilder.Entity<ProductCategory>().HasData(new List<ProductCategory>
            {
                new ProductCategory("ProductCategory 1"),
                new ProductCategory("ProductCategory 2"),
            });

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductCatalogContext).Assembly, type => type.FullName.Contains("ProductCatalog"));
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            // Dispatch Domain Events collection. 
            // Choices:
            // A) Right BEFORE committing data (EF SaveChanges) into the DB will make a single transaction including  
            // side effects from the domain event handlers which are using the same DbContext with "InstancePerLifetimeScope" or "scoped" lifetime
            // B) Right AFTER committing data (EF SaveChanges) into the DB will make multiple transactions. 
            // You will need to handle eventual consistency and compensatory actions in case of failures in any of the Handlers. 
            await _mediator.DispatchProductDomainEventsAsync(this);

            // After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
            // performed through the DbContext will be committed
            var result = await base.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<bool> SaveEntitiesSeveralTransactionsAsync(CancellationToken cancellationToken = default(CancellationToken))
        {

            // After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
            // performed through the DbContext will be committed
            var result = await base.SaveChangesAsync(cancellationToken);

            // Dispatch Domain Events collection. 
            // Choices:
            // A) Right BEFORE committing data (EF SaveChanges) into the DB will make a single transaction including  
            // side effects from the domain event handlers which are using the same DbContext with "InstancePerLifetimeScope" or "scoped" lifetime
            // B) Right AFTER committing data (EF SaveChanges) into the DB will make multiple transactions. 
            // You will need to handle eventual consistency and compensatory actions in case of failures in any of the Handlers. 
            await _mediator.DispatchProductDomainEventsAsync(this);


            return true;
        }


        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if (_currentTransaction != null) return null;

            _currentTransaction = await Database.BeginTransactionAsync();

            return _currentTransaction;
        }

        public async Task CommitTransactionAsync(IDbContextTransaction transaction)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (transaction != _currentTransaction) throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

            try
            {
                await SaveChangesAsync();
                transaction.Commit();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }
    }
}
