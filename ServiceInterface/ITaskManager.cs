using DataModel.DBEntity;
using DataModel.Share;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceInterface
{
    public interface ITaskManager
    {
        #region Task
        /// <summary>
        /// 取得工作任務列表
        /// </summary>
        /// <returns></returns>
        List<task> GetTaskList();

        /// <summary>
        /// 新增或更新工作任務
        /// </summary>
        /// <param name="InputModel"></param>
        /// <returns></returns>
        Task<VerityResult> CreateOrUpdateTask(task InputModel);

        /// <summary>
        /// 取得單一工作
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        task GetTask(int id);

        /// <summary>
        /// 刪除工作
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<VerityResult> DeleteTask(int Id);
        #endregion

        /// <summary>
        /// 取得工作人員列表
        /// </summary>
        /// <returns></returns>
        List<worker> GetWorkerList();

        #region resourceassignment
        /// <summary>
        /// 新增或更新指派工作
        /// </summary>
        /// <param name="InputModel"></param>
        /// <returns></returns>
        Task<VerityResult> CreateOrUpdateResourceAssignment(resourceassignment InputModel);

        /// <summary>
        /// 取得單一指派工作
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        resourceassignment GetResourceAssignment(int id);

        /// <summary>
        /// 取得指派工作列表
        /// </summary>
        /// <returns></returns>
        List<resourceassignment> GetResourceAssignmentList();

        /// <summary>
        /// 刪除指派工作
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<VerityResult> DeleteResourceAssignment(int Id);
        #endregion

        #region 工作相依(延伸)關係

        /// <summary>
        /// 取得工作相依(延伸)關係列表
        /// </summary>
        /// <returns></returns>
        List<dependency> GetDependencyList();

        /// <summary>
        /// 建立工作相依(延伸)關係
        /// </summary>
        /// <param name="InputModel"></param>
        /// <returns></returns>
        Task<VerityResult> CreateDependency(dependency InputModel);

        /// <summary>
        /// 刪除工作相依(延伸)關係
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<VerityResult> DeleteDependency(int Id);
        #endregion
    }
}
