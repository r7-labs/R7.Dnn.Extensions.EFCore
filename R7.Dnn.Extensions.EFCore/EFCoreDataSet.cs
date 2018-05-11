//
//  EFCoreDataSet.cs
//
//  Author:
//       Roman M. Yagodin <roman.yagodin@gmail.com>
//
//  Copyright (c) 2018 Roman M. Yagodin
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System.Linq;
using Microsoft.EntityFrameworkCore;
using R7.Dnn.Extensions.Data;

namespace R7.Dnn.Extensions.EFCore
{
    public class EFCoreDataSet<TEntity>: DbSet<TEntity>, IDataSet<TEntity> where TEntity : class
    {
        protected DbSet<TEntity> Set;

        public EFCoreDataSet (DbSet<TEntity> set)
        {
            Set = set;
        }

        public IQueryable<TEntity> Query ()
        {
            return Set;
        }

        public virtual IQueryable<TEntity> FromSql (string sql, params object [] parameters)
        {
            return Set.FromSql (sql, parameters).AsNoTracking ();
        }

        void IDataSet<TEntity>.Add (TEntity entity)
        {
            Set.Add (entity);
        }

        void IDataSet<TEntity>.Attach (TEntity entity)
        {
            Set.Attach (entity);
        }

        void IDataSet<TEntity>.Remove (TEntity entity)
        {
            Set.Remove (entity);
        }

        public bool Exists (TEntity entity)
        {
            return Set.Local.Any (e => e == entity);
        }

        public TEntity Find<TKey> (TKey key)
        {
            return Set.Find (key);
        }
    }
}
