//
//  EFCoreDataContextTests.cs
//
//  Author:
//       Roman M. Yagodin <roman.yagodin@gmail.com>
//
//  Copyright (c) 2018 Roman M. Yagodin
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System.Linq;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using R7.Dnn.Extensions.EFCore.Tests;
using R7.Dnn.Extensions.EFCore.Tests.Models;
using Xunit;

namespace R7.Dnn.Extensions.EFCore
{
    public class EFCoreDataContextTests
    {
        [Fact]
        public void EFCoreDataContextTest ()
        {
            using (var connection = new SqliteConnection ("DataSource=:memory:")) {
                connection.Open ();

                var builder = new DbContextOptionsBuilder ();
                var options = builder.UseSqlite (connection).Options;

                var testEntity = new TestEntity { Key = 10, Value = "Sample Value" };

                using (var dc = new EFCoreTestDataContext (options)) {
                    dc.Database.EnsureCreated ();
                    dc.Set<TestEntity> ().Add (testEntity);
                    dc.SaveChanges ();
                }

                using (var dc = new EFCoreTestDataContext (options)) {
                    Assert.Equal (1, dc.Set<TestEntity> ().Count ());
                }
            }
        }
    }
}
