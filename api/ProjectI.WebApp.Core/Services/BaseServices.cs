using ProjectI.WebApp.Core.Entities;
using ProjectI.WebApp.Core.Interfaces.IRepository;
using ProjectI.WebApp.Core.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectI.WebApp.Core.Services
{
    public class BaseServices<TEntity> : IBaseServices<TEntity>
    {

        #region Declare

        IBaseRepository<TEntity> _baseRepository;
        public ServiceResult _serviceResult;

        #endregion

        #region Constructorr

        public BaseServices(IBaseRepository<TEntity> baseRepository)
        {
            _baseRepository = baseRepository;
            _serviceResult = new ServiceResult();
        }

        #endregion

        /// <summary>
        /// Xử lí thêm mới
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ServiceResult Add(TEntity entity)
        {
            try
            {
                // xử lí nghiệp vụ thêm
                var isValidMode = Validate(entity, "add");

                switch (isValidMode)
                {
                    case 1:
                        _serviceResult.Data = new
                        {
                            devMsg = Resources.ResourceVN.Error_Dev_NullField,
                            userMsg = Resources.ResourceVN.Error_User_NullField,
                        };
                        _serviceResult.StatusCode = 400;
                        return _serviceResult;
                    case 2:
                        _serviceResult.Data = new
                        {
                            devMsg = Resources.ResourceVN.Error_Dev_DuplicateFiled,
                            userMsg = Resources.ResourceVN.Error_User_DuplicateField,
                        };
                        _serviceResult.StatusCode = 400;
                        return _serviceResult;
                    case 3:
                        _serviceResult.Data = new
                        {
                            devMsg = Resources.ResourceVN.Error_Dev_InvalidField,
                            userMsg = Resources.ResourceVN.Error_User_InvalidField,
                        };
                        _serviceResult.StatusCode = 400;
                        return _serviceResult;
                    default:
                        break;
                }

                // thêm dữ liệu vào db
                var rowAffect = _baseRepository.Add(entity);
                if (rowAffect > 0)
                {
                    _serviceResult.Data = rowAffect;
                    _serviceResult.StatusCode = 201;
                    return _serviceResult;
                }
                else
                {
                    _serviceResult.StatusCode = 204;
                    return _serviceResult;
                }
            }
            catch (Exception ex)
            {
                var errorObj = new
                {
                    devMsg = ex.Message,
                    userMsg = Resources.ResourceVN.Error_User,
                };
                _serviceResult.Data = errorObj;
                _serviceResult.StatusCode = 500;
                return _serviceResult;
            }
        }

        /// <summary>
        /// Xử lí xóa bản ghi theo id
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        public ServiceResult Delete(int entityId)
        {
            try
            {
                // xử lí nghiệp vụ xóa
                // xóa dữ liệu khỏi db
                var rowAffect = _baseRepository.Delete(entityId);
                if (rowAffect > 0)
                {
                    _serviceResult.Data = rowAffect;
                    _serviceResult.StatusCode = 201;
                    return _serviceResult;
                }
                else
                {
                    _serviceResult.StatusCode = 204;
                    return _serviceResult;
                }
            }
            catch (Exception ex)
            {
                var errorObj = new
                {
                    devMsg = ex.Message,
                    userMsg = Resources.ResourceVN.Error_User,
                };
                _serviceResult.Data = errorObj;
                _serviceResult.StatusCode = 500;
                return _serviceResult;
            }
        }

        /// <summary>
        /// Xử lí lấy toàn bộ ds
        /// </summary>
        /// <returns></returns>
        public ServiceResult GetAll()
        {
            try
            {
                // xử lí nghiệp vụ lấy dữ liệu
                // lấy tất cả dữ liệu từ db
                var entities = _baseRepository.GetAll();
                if (entities.Count() > 0)
                {
                    _serviceResult.Data = entities;
                    _serviceResult.StatusCode = 200;
                    return _serviceResult;
                }
                else
                {
                    _serviceResult.StatusCode = 204;
                    return _serviceResult;
                }
            }
            catch (Exception ex)
            {
                var errorObj = new
                {
                    devMsg = ex.Message,
                    userMsg = Resources.ResourceVN.Error_User,
                };
                _serviceResult.Data = errorObj;
                _serviceResult.StatusCode = 500;
                return _serviceResult;
            }
        }

        /// <summary>
        /// Xử lí lấy bản ghi theo id
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        public ServiceResult GetById(int entityId)
        {
            try
            {
                // xử lí nghiệp vụ lấy 1 dữ liệu
                // lấy bản ghi theo id
                var entity = _baseRepository.GetById(entityId);
                if (entity != null)
                {
                    _serviceResult.Data = entity;
                    _serviceResult.StatusCode = 200;
                    return _serviceResult;
                }
                else
                {
                    _serviceResult.StatusCode = 204;
                    return _serviceResult;
                }
            }
            catch (Exception ex)
            {
                var errorObj = new
                {
                    devMsg = ex.Message,
                    userMsg = Resources.ResourceVN.Error_User,
                };
                _serviceResult.Data = errorObj;
                _serviceResult.StatusCode = 500;
                return _serviceResult;
            }
        }

        /// <summary>
        /// Xử lí cập nhật bản ghi
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="entityId"></param>
        /// <returns></returns>
        public ServiceResult Update(TEntity entity, int entityId)
        {
            try
            {
                // xử lí nghiệp vụ sửa
                var isValidMode = Validate(entity, "update");

                switch (isValidMode)
                {
                    case 1:
                        _serviceResult.Data = new
                        {
                            devMsg = Resources.ResourceVN.Error_Dev_NullField,
                            userMsg = Resources.ResourceVN.Error_User_NullField,
                        };
                        _serviceResult.StatusCode = 400;
                        return _serviceResult;
                    case 2:
                        _serviceResult.Data = new
                        {
                            devMsg = Resources.ResourceVN.Error_Dev_DuplicateFiled,
                            userMsg = Resources.ResourceVN.Error_User_DuplicateField,
                        };
                        _serviceResult.StatusCode = 400;
                        return _serviceResult;
                    case 3:
                        _serviceResult.Data = new
                        {
                            devMsg = Resources.ResourceVN.Error_Dev_InvalidField,
                            userMsg = Resources.ResourceVN.Error_User_InvalidField,
                        };
                        _serviceResult.StatusCode = 400;
                        return _serviceResult;
                    default:
                        break;
                }

                // cập nhật dữ liệu vào db
                var rowAffect = _baseRepository.Update(entity, entityId);
                if (rowAffect > 0)
                {
                    _serviceResult.Data = rowAffect;
                    _serviceResult.StatusCode = 201;
                    return _serviceResult;
                }
                else
                {
                    _serviceResult.StatusCode = 204;
                    return _serviceResult;
                }
            }
            catch (Exception ex)
            {
                var errorObj = new
                {
                    devMsg = ex.Message,
                    userMsg = Resources.ResourceVN.Error_User,
                };
                _serviceResult.Data = errorObj;
                _serviceResult.StatusCode = 500;
                return _serviceResult;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="mode"></param>
        /// <returns>0: hợp lệ; 1:trống; 2: trùng; 3: sai định dạng</returns>
        private int Validate(TEntity entity, string mode)
        {
            var props = entity.GetType().GetProperties();

            foreach (var prop in props)
            {
                // kiểm tra trường bắt buộc nhập reqiued !!!! 1
                if (prop.IsDefined(typeof(Requied), false))
                {
                    var propValue = prop.GetValue(entity);
                    if (propValue == null)
                    {
                        return 1;
                    }
                    if(prop.GetType() == typeof(string) && (string)propValue == "")
                    {
                        return 1;
                    }
                    if(prop.GetType() == typeof(int) && (int)propValue == 0)
                    {
                        return 1;
                    }
                    // kiểm tra trường không cho phép trùng !!!! 2
                }
                if (prop.IsDefined(typeof(NotAllowDuplicate), false))
                {
                    var entityDuplicate = _baseRepository.GetByProp(prop.Name, prop.GetValue(entity));
                    if (mode == "add" && entityDuplicate != null)
                    {
                        return 2;
                    }
                    else if (mode == "update")
                    {
                        if (
                            !entityDuplicate.GetType().GetProperty($"{typeof(TEntity).Name}Id").GetValue(entityDuplicate).Equals(entity.GetType().GetProperty($"{typeof(TEntity).Name}Id").GetValue(entity))                       
                            )
                        {
                            return 2;
                        }
                    }
                    // kiểm tra email hợp lệ  !!!! 3
                }
                if (prop.Name == "Email")
                {
                    if ( (string)prop.GetValue(entity)!= "" &&  !Common.IsValidEmail((string)prop.GetValue(entity)))
                    {
                        return 3;
                    }
                }
            }
            return 0;
        }

        public ServiceResult GetNewId()
        {
            try
            {
                // xử lí nghiệp vụ lấy id mới
                // lấy id lớn nhất +1 từ db
                var newId = _baseRepository.GetNewId();
                _serviceResult.Data = newId;
                _serviceResult.StatusCode = 200;
                return _serviceResult;
            }
            catch (Exception ex)
            {
                var errorObj = new
                {
                    devMsg = ex.Message,
                    userMsg = Resources.ResourceVN.Error_User,
                };
                _serviceResult.Data = errorObj;
                _serviceResult.StatusCode = 500;
                return _serviceResult;
            }
        }
    }
}
