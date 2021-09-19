using Dapper;
using ProjectI.WebApp.Core.Interfaces.IRepository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectI.WebApp.Infrastructure.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>
    {

        #region Declare

        public IDbConnection dbConnection;
        string _tableName;

        #endregion

        #region Constructor

        public BaseRepository()
        {
            dbConnection = DatabaseConnection.DbConnection;
            _tableName = typeof(TEntity).Name;
        }

        #endregion

        /// <summary>
        /// Thêm mới vào db
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Add(TEntity entity)
        {
            /// complete sql String
            var colNames = string.Empty;
            var colParams = string.Empty;
            // đọc từng property
            var properties = entity.GetType().GetProperties();

            DynamicParameters parameters = new DynamicParameters();
            // duyệt từng property

            foreach (var prop in properties)
            {
                // lấy tên prop
                var propName = prop.Name;

                // lấy value prop
                var propValue = prop.GetValue(entity);
                //// lấy kiểu prop
                //var propType = prop.PropertyType;

                parameters.Add($"@{propName}", propValue);
                colNames += $"{propName},";
                colParams += $"@{propName},";
            }
            colNames = colNames.Remove(colNames.Length - 1, 1);
            colParams = colParams.Remove(colParams.Length - 1, 1);
            var sql = $"insert into {_tableName}({colNames}) values( {colParams} ) ";
            var rowAffects = dbConnection.Execute(sql, param: parameters);
            return rowAffects;
        }

        /// <summary>
        /// Xóa theo id
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        public int Delete(int entityId)
        {
            var sql = $"delete from {_tableName} where {_tableName}Id = '{entityId}'";
            var rowAffects = dbConnection.Execute(sql);
            return rowAffects;
        }

        /// <summary>
        /// Lấy toànn bộ ds
        /// </summary>
        /// <returns></returns>
        public List<TEntity> GetAll()
        {
            var sql = $"select * from {_tableName}";
            var entities = dbConnection.Query<TEntity>(sql);
            return (List<TEntity>)entities;
        }

        /// <summary>
        /// Lấy theo id
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        public TEntity GetById(int entityId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add($"@{_tableName}Id", entityId);
            var sql = $"select * from {_tableName} where {_tableName}Id= @{_tableName}Id";
            var entity = dbConnection.QueryFirstOrDefault<TEntity>(sql, param: parameters);
            return entity;
        }

        /// <summary>
        /// Lấy theo một thuộc tính
        /// </summary>
        /// <param name="propName"></param>
        /// <param name="propValue"></param>
        /// <returns></returns>
        public TEntity GetByProp(string propName, object propValue)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add($"@{propName}", propValue.ToString());
            var sql = $"select * from {_tableName} where {propName}= @{propName} ";
            var entity = dbConnection.QueryFirstOrDefault<TEntity>(sql, param: parameters);
            return entity;
        }

        public int GetNewId()
        {
            var sql = $"select top 1 * from {_tableName} order by {_tableName}Id desc";
            var entity = dbConnection.QueryFirstOrDefault<TEntity>(sql);
            int newId = (int)entity.GetType().GetProperty($"{_tableName}Id").GetValue(entity) + 1;
            return newId;
        }

        /// <summary>
        /// Cập nhật bản ghi
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="entityId"></param>
        /// <returns></returns>
        public int Update(TEntity entity, int entityId)
        {
            /// complete sql String
            var cols = string.Empty;
            // đọc từng property
            var properties = entity.GetType().GetProperties();
            DynamicParameters parameters = new DynamicParameters();
            // duyệt từng property

            foreach (var prop in properties)
            {
                // lấy tên prop
                var propName = prop.Name;
                // lấy value prop
                var propValue = prop.GetValue(entity);
                // lấy kiểu prop
                var propType = prop.PropertyType;

                parameters.Add($"@{propName}", propValue);
                cols += $" { propName } = @{propName},";
            }
            cols = cols.Remove(cols.Length - 1, 1);
            var sql = $"update {_tableName} set {cols} where {_tableName}Id = '{entityId}'";
            var rowAffects = dbConnection.Execute(sql, param: parameters);
            return rowAffects;
        }
    }
}
