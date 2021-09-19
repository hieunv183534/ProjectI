using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectI.WebApp.Core.Interfaces.IRepository
{
    public interface IBaseRepository<TEntity>
    {
        /// <summary>
        /// Lấy toàn bộ ds
        /// </summary>
        /// <returns></returns>
        List<TEntity> GetAll();

        /// <summary>
        /// Lấy theo id
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        TEntity GetById(int entityId);

        /// <summary>
        /// Thêm
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Add(TEntity entity);

        /// <summary>
        /// Sửa 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="entityId"></param>
        /// <returns></returns>
        int Update(TEntity entity, int entityId);

        /// <summary>
        /// Xóa
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        int Delete(int entityId);

        /// <summary>
        /// Lấy theo một prop
        /// </summary>
        /// <param name="propName"></param>
        /// <param name="propValue"></param>
        /// <returns></returns>
        TEntity GetByProp(string propName, object propValue);

        /// <summary>
        /// lấy id mới
        /// </summary>
        /// <returns></returns>
        int GetNewId();
    }
}
