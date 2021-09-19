using ProjectI.WebApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectI.WebApp.Core.Interfaces.IServices
{
    public interface IBaseServices<TEntity>
    {
        /// <summary>
        /// Lấy toàn bộ ds
        /// </summary>
        /// <returns></returns>
        ServiceResult GetAll();

        /// <summary>
        /// Lấy theo id
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        ServiceResult GetById(int entityId);

        /// <summary>
        /// Thêm
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        ServiceResult Add(TEntity entity);

        /// <summary>
        /// Sửa 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="entityId"></param>
        /// <returns></returns>
        ServiceResult Update(TEntity entity, int entityId);

        /// <summary>
        /// Xóa
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        ServiceResult Delete(int entityId);

        /// <summary>
        /// Lấy id mới
        /// </summary>
        /// <returns></returns>
        ServiceResult GetNewId();
    }
}
