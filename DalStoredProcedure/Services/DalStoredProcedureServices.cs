using Bus_backUpData.Data;
using DalStoredProcedure.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DalStoredProcedure.Services
{
	public class DalStoredProcedureServices : IDalStoredProcedureServices
	{
		public DalStoredProcedureServices() { }

		public int ExecuteSqlRaw(string Connection, string Sql, List<SqlParameter> SqlParameters)
		{
			var result = 0;
			try
			{
				using (Context context = new Context(Connection))
				{
					result = context.Database.ExecuteSqlRaw(Sql, SqlParameters);
				}
			}
			catch (Exception ex)
			{
				result = -1;
			}
			return result;
		}
		public int ExecuteSqlRaw(string Connection, string Sql)
		{
			var result = 0;
			try
			{
				using (Context context = new Context(Connection))
				{
					result = context.Database.ExecuteSqlRaw(Sql);
				}
			}
			catch (Exception ex)
			{
				result = -1;
			}
			return result;
		}
		public List<string> SqlQueryRaw(string Connection, string Sql)
		{
			try
			{
				using (Context context = new Context(Connection))
				{
					var result = context.Database.SqlQueryRaw<string>(Sql);
					if (result == null) return new List<string>();
					return result.ToList();
				}
			}
			catch (Exception ex)
			{
				return new List<string>();
			}
		}

		public List<string> SqlQueryRaw(string Connection, string Sql, List<SqlParameter> SqlParameters)
		{
			try
			{
				using (Context context = new Context(Connection))
				{
					if (SqlParameters.Count == 1)
					{
						var resultFirst = context.Database.SqlQueryRaw<string>(Sql, SqlParameters.FirstOrDefault());
						return resultFirst.ToList();
					}
					var result = context.Database.SqlQueryRaw<string>(Sql, SqlParameters);
					return result.ToList();
				}
			}
			catch (Exception ex)
			{
				return new List<string>();
			}
		}

		public bool CheckConnection(string Connection)
		{
			try
			{
				using (Context context = new Context(Connection))
				{
					var result = context.Database.CanConnect();
					return result;
				}
			}
			catch (Exception ex)
			{
				return false;
			}
		}
	}
}
