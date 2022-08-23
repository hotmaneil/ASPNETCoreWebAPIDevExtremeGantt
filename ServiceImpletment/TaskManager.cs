using DataModel.DBEntity;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using Dapper;
using ServiceInterface;
using DataModel.Share;
using System.Threading.Tasks;
using System.Net;

namespace ServiceImpletment
{
    public class TaskManager : ITaskManager
    {
        internal readonly string ConnectionString;
        internal readonly MySqlConnection connection;

        public TaskManager(IConfiguration configuration)
        {
            ConnectionString = configuration.GetSection("ConnectionStrings")["MySQL"];
            connection = new MySqlConnection(ConnectionString);
            SimpleCRUD.SetDialect(SimpleCRUD.Dialect.MySQL);
        }

        #region Task

        /// <summary>
        /// 取得工作任務列表
        /// </summary>
        /// <returns></returns>
        public List<task> GetTaskList()
        {
            List<task> dataList = new List<task>();

            try
            {
                var query = connection.GetList<task>();
                if (query != null)
                    dataList = query.AsList();

                return dataList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 新增或更新工作任務
        /// </summary>
        /// <param name="InputModel"></param>
        /// <returns></returns>
        public async Task<VerityResult> CreateOrUpdateTask(task InputModel)
        {
            VerityResult result = new VerityResult();

            try
            {
                var query = connection.Get<task>(InputModel.id);
                if (query == null)
                {
                    var newResult = connection.Insert<task>(InputModel);
                    result.Message = "新增工作成功!";

                    if (newResult.HasValue)
                        result.Payload = newResult.Value;

                    result.StatusCode = HttpStatusCode.OK;
                }
                else
                {
                    query.parentId = InputModel.parentId;
                    query.title = InputModel.title;
                    query.start = InputModel.start;
                    query.end = InputModel.end;
                    query.progress = InputModel.progress;
                    query.count = InputModel.count;

                    connection.Update<task>(query);
                    result.Message = "更新工作成功!";
                    result.Payload = InputModel.id;
                    result.StatusCode = HttpStatusCode.OK;
                }
            }
            catch (Exception ex)
            {
                result.StatusCode = HttpStatusCode.InternalServerError;
                throw ex;
            }
            return await Task.Run(() => result);
        }

        /// <summary>
        /// 取得單一工作
        /// </summary>
        public task GetTask(int id)
        {
            task data = new task();
            var query = connection.Get<task>(id);
            if (query != null)
                data = query;

            return data;
        }

        /// <summary>
        /// 刪除工作
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<VerityResult> DeleteTask(int Id)
        {
            VerityResult result = new VerityResult();

            try
            {
                var query = connection.Get<task>(Id);
                if (query == null)
                {
                    result.Message = "No exist task";
                    result.StatusCode = HttpStatusCode.NotFound;
                }
                else
                {
                    //先檢查有無已指派的工作人員
                    var findAssign = connection.GetList<resourceassignment>(new { taskId = Id });
                    if(findAssign!=null)
                    {
                        foreach (var delItem in findAssign.AsList())
                        {
                            connection.Delete(delItem);
                        }
                    }

                    //再刪除工作
                    connection.Delete(query);
                    result.Message = "Delete task success!";
                    result.StatusCode = HttpStatusCode.OK;
                }
            }
            catch (Exception ex)
            {
                result.StatusCode = HttpStatusCode.InternalServerError;
                throw ex;
            }
            return await Task.Run(() => result);
        }
        #endregion

        #region worker
        /// <summary>
        /// 取得工作人員列表
        /// </summary>
        /// <returns></returns>
        public List<worker> GetWorkerList()
        {
            List<worker> dataList = new List<worker>();

            try
            {
                var query = connection.GetList<worker>();
                if (query != null)
                    dataList = query.AsList();

                foreach (var item in dataList)
                {
                    item.text = item.name;
                }
                return dataList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region resourceassignment

        /// <summary>
        /// 新增或更新指派工作
        /// </summary>
        /// <param name="InputModel"></param>
        /// <returns></returns>
        public async Task<VerityResult> CreateOrUpdateResourceAssignment(resourceassignment InputModel)
        {
            VerityResult result = new VerityResult();

            try
            {
                var query = connection.Get<resourceassignment>(InputModel.id);
                if (query == null)
                {
                    var newResult = connection.Insert<resourceassignment>(InputModel);
                    result.Message = "指派新工作任務成功!";

                    if (newResult.HasValue)
                        result.Payload = newResult.Value;

                    result.StatusCode = HttpStatusCode.OK;
                }
                else
                {
                    query.taskId = InputModel.taskId;
                    query.resourceId = InputModel.resourceId;

                    connection.Update<resourceassignment>(query);
                    result.Message = "改派工作任務成功!";
                    result.Payload = InputModel.id;
                    result.StatusCode = HttpStatusCode.OK;
                }
            }
            catch (Exception ex)
            {
                result.StatusCode = HttpStatusCode.InternalServerError;
                throw ex;
            }
            return await Task.Run(() => result);
        }

        /// <summary>
        /// 取得單一指派工作
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public resourceassignment GetResourceAssignment(int id)
        {
            resourceassignment data = new resourceassignment();
            var query = connection.Get<resourceassignment>(id);
            if (query != null)
                data = query;

            return data;
        }

        /// <summary>
        /// 取得指派工作列表
        /// </summary>
        /// <returns></returns>
        public List<resourceassignment> GetResourceAssignmentList()
        {
            List<resourceassignment> dataList = new List<resourceassignment>();

            try
            {
                var query = connection.GetList<resourceassignment>();
                if (query != null)
                    dataList = query.AsList();

                return dataList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 刪除指派工作
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<VerityResult> DeleteResourceAssignment(int Id)
        {
            VerityResult result = new VerityResult();

            try
            {
                var query = connection.Get<resourceassignment>(Id);
                if (query == null)
                {
                    result.Message = "No exist resourceAssignment";
                    result.StatusCode = HttpStatusCode.NotFound;
                }
                else
                {
                    connection.Delete(query);
                    result.Message = "取消指派工作任務成功!";
                    result.StatusCode = HttpStatusCode.OK;
                }
            }
            catch (Exception ex)
            {
                result.StatusCode = HttpStatusCode.InternalServerError;
                throw ex;
            }
            return await Task.Run(() => result);
        }
        #endregion

        #region 工作相依(延伸)關係

        /// <summary>
        /// 取得工作相依(延伸)關係列表
        /// </summary>
        /// <returns></returns>
        public List<dependency> GetDependencyList()
        {
            List<dependency> dataList = new List<dependency>();

            try
            {
                var query = connection.GetList<dependency>();
                if (query != null)
                    dataList = query.AsList();

                return dataList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 建立工作相依(延伸)關係
        /// </summary>
        /// <param name="InputModel"></param>
        /// <returns></returns>
        public async Task<VerityResult> CreateDependency(dependency InputModel)
        {
            VerityResult result = new VerityResult();

            try
            {
                var query = connection.Get<dependency>(InputModel.id);
                if (query == null)
                {
                    var newResult = connection.Insert<dependency>(InputModel);
                    result.Message = "建立工作相依(延伸)關係成功!";

                    if (newResult.HasValue)
                        result.Payload = newResult.Value;

                    result.StatusCode = HttpStatusCode.OK;
                }
            }
            catch (Exception ex)
            {
                result.StatusCode = HttpStatusCode.InternalServerError;
                throw ex;
            }
            return await Task.Run(() => result);
        }

        /// <summary>
        /// 刪除工作相依(延伸)關係
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<VerityResult> DeleteDependency(int Id)
        {
            VerityResult result = new VerityResult();

            try
            {
                var query = connection.Get<dependency>(Id);
                if (query == null)
                {
                    result.Message = "工作相依(延伸)關係不存在";
                    result.StatusCode = HttpStatusCode.NotFound;
                }
                else
                {
                    connection.Delete(query);
                    result.Message = "刪除工作相依(延伸)關係成功！";
                    result.StatusCode = HttpStatusCode.OK;
                }
            }
            catch (Exception ex)
            {
                result.StatusCode = HttpStatusCode.InternalServerError;
                throw ex;
            }
            return await Task.Run(() => result);
        }
        #endregion
    }
}
