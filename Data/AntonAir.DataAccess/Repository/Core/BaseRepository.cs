using AntonAir.DataAccess.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace AntonAir.DataAccess.Repository.Core
{
	public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
	{
		protected DbContext Context { get; }
		protected DbSet<TEntity> Set { get; }

		public BaseRepository(DbContext context)
		{
			this.Context = context;
			this.Set = context.Set<TEntity>();
		}

		/// <inheritdoc />
		public TEntity Get(int id)
		{
			return this.Context.Set<TEntity>().Find(id);
		}

		/// <inheritdoc />
		public void Add(TEntity entity)
		{
			if (entity == null)
				throw new ArgumentNullException("entity");

			this.Set.Add(entity);
		}

		/// <inheritdoc />
		public void AddOrUpdate(TEntity entity)
		{
			if (entity == null)
				throw new ArgumentNullException("entity");

			this.Set.AddOrUpdate(entity);
		}

		/// <inheritdoc />
		public void Delete(TEntity entity)
		{
			if (entity == null)
				throw new ArgumentNullException("entity");

			if (this.Context.Entry(entity).State == EntityState.Detached)
			{
				this.Set.Attach(entity);
			}
			this.Set.Remove(entity);
		}

		/// <inheritdoc />
		public void AddRange(IEnumerable<TEntity> entities)
		{
			if (entities == null)
				throw new ArgumentNullException("entities");

			this.Set.AddRange(entities);
		}

		/// <inheritdoc />
		public void RemoveRange(IEnumerable<TEntity> entities)
		{
			if (entities == null)
				throw new ArgumentNullException("entities");

			foreach (var entity in entities)
			{
				if (this.Context.Entry(entity).State == EntityState.Detached)
				{
					this.Set.Attach(entity);
				}
			}

			this.Set.RemoveRange(entities);
		}

		public IQueryable<TEntity> Query(bool tracking = true)
		{
			return this.Set.AsQueryable();
		}
	}
}