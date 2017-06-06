namespace ECERP.Services.Suppliers
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Core;
    using Core.Domain.Suppliers;
    using Data.Abstract;

    public class SupplierService : ISupplierService
    {
        #region Fields
        private readonly IRepository _repository;
        #endregion

        #region Constructor
        public SupplierService(IRepository repository)
        {
            _repository = repository;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Gets suppliers
        /// </summary>
        /// <param name="filter">Filter</param>
        /// <param name="sortOrder">Sort Order</param>
        /// <param name="pageIndex">Page Index</param>
        /// <param name="pageSize">Page Size</param>
        /// <returns>Suppliers</returns>
        public virtual IPagedList<Supplier> GetSuppliers(
            Expression<Func<Supplier, bool>> filter = null,
            Func<IQueryable<Supplier>, IOrderedQueryable<Supplier>> sortOrder = null,
            int pageIndex = 0,
            int pageSize = int.MaxValue)
        {
            var skip = pageIndex * pageSize;
            var pagedSuppliers = _repository.Get(filter, sortOrder, skip, pageSize, c => c.City);
            var totalCount = _repository.GetCount(filter);
            return new PagedList<Supplier>(pagedSuppliers, pageIndex, pageSize, totalCount);
        }

        /// <summary>
        /// Gets a supplier
        /// </summary>
        /// <param name="id">Supplier Identifier</param>
        /// <returns>Supplier</returns>
        public virtual Supplier GetSupplierById(int id)
        {
            return _repository.GetById<Supplier>(id, c => c.City);
        }

        /// <summary>
        /// Gets a supplier
        /// </summary>
        /// <param name="name">Name</param>
        /// <returns>Supplier</returns>
        public virtual Supplier GetSupplierByName(string name)
        {
            return _repository.GetOne<Supplier>(s => s.Name.Equals(name));
        }

        /// <summary>
        /// Inserts a supplier
        /// </summary>
        /// <param name="supplier">Supplier</param>
        public void InsertSupplier(Supplier supplier)
        {
            _repository.Create(supplier);
            _repository.Save();
        }

        /// <summary>
        /// Updates a supplier
        /// </summary>
        /// <param name="supplier">Supplier</param>
        public void UpdateSupplier(Supplier supplier)
        {
            _repository.Update(supplier);
            _repository.Save();
        }
        #endregion
    }
}