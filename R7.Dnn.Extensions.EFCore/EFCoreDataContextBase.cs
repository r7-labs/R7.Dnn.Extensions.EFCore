//
//  EFCoreDataContextBase.cs
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

using Microsoft.EntityFrameworkCore;
using R7.Dnn.Extensions.Data;
using R7.Dnn.Extensions.Models;

namespace R7.Dnn.Extensions.EFCore
{
    public abstract class EFCoreDataContextBase: DbContext, IDataContext
    {
        protected EFCoreDataContextBase ()
        {}

        protected EFCoreDataContextBase (DbContextOptions options): base (options)
        {}

        #region IDataContext implementation

        public virtual IDataSet<TEntity> GetDataSet<TEntity> () where TEntity : class
        {
            return new EFCoreDataSet<TEntity> (Set<TEntity> ());
        }

        public void WasModified<TEntity> (TEntity entity) where TEntity : class
        {
            Entry (entity).State = EntityState.Modified;
        }

        public void WasRemoved<TEntity> (TEntity entity) where TEntity : class
        {
            Entry (entity).State = EntityState.Deleted;
        }

        public ITransaction BeginTransaction ()
        {
            return new EFCoreTransaction (this);
        }

        #endregion
    }
}
