using DataModel.DBEntity;
using DataModel.Share;
using Microsoft.AspNetCore.Mvc;
using ServiceInterface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ASPNETCoreWebAPIDevExtremeGantt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskManager _taskManager;

        public TaskController(ITaskManager taskManager)
        {
            _taskManager = taskManager;
        }

        /// <summary>
        /// 取得工作任務列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetList")]
        public ActionResult<List<task>> GetList()
        {
            try
            {
                List<task> dataList = _taskManager.GetTaskList();
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
        [HttpPost]
        [Route("Update")]
        public async Task<IActionResult> Update(task InputModel)
        {
            try
            {
                var result = await _taskManager.CreateOrUpdateTask(InputModel);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 取得單一工作
        /// </summary>
        /// <param name="InputModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetTask")]
        public ActionResult<task> GetTask(IntIdModel InputModel)
        {
            var result =  _taskManager.GetTask(InputModel.Id);
            return result;
        }

        /// <summary>
        /// 刪除工作
        /// </summary>
        /// <param name="InputModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DeleteTask")]
        public async Task<IActionResult> DeleteTask(task InputModel)
        {
            try
            {
                var result = await _taskManager.DeleteTask(InputModel.id);
                return NoContent();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 取得工作人員列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetWorkerList")]
        public ActionResult<List<worker>> GetWorkerList()
        {
            List<worker> dataList = _taskManager.GetWorkerList();
            return dataList;
        }

        /// <summary>
        /// 新增或更新指派工作
        /// </summary>
        /// <param name="InputModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateResourceAssignment")]
        public async Task<IActionResult> UpdateResourceAssignment(resourceassignment InputModel)
        {
            try
            {
                var result = await _taskManager.CreateOrUpdateResourceAssignment(InputModel);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 取得單一指派工作
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetResourceAssignment")]
        public ActionResult<resourceassignment> GetResourceAssignment(int id)
        {
            var data =  _taskManager.GetResourceAssignment(id);
            return data;
        }

        /// <summary>
        /// 取得指派工作列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetResourceAssignmentList")]
        public ActionResult<List<resourceassignment>> GetResourceAssignmentList()
        {
            List<resourceassignment> dataList = _taskManager.GetResourceAssignmentList();
            return dataList;
        }

        /// <summary>
        /// 取消指派工作任務
        /// </summary>
        /// <param name="InputModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DeleteResourceAssignment")]
        public async Task<IActionResult> DeleteResourceAssignment(resourceassignment InputModel)
        {
            try
            {
                var result = await _taskManager.DeleteResourceAssignment(InputModel.id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 取得工作相依(延伸)關係列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetDependencies")]
        public ActionResult<List<dependency>> GetDependencies()
        {
            List<dependency> dataList = _taskManager.GetDependencyList();
            return dataList;
        }

        /// <summary>
        /// 建立工作相依(延伸)關係
        /// </summary>
        /// <param name="InputModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateDependency")]
        public async Task<IActionResult> CreateDependency(dependency InputModel)
        {
            try
            {
                var result = await _taskManager.CreateDependency(InputModel);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 刪除工作相依(延伸)關係
        /// </summary>
        /// <param name="InputModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DeleteDependency")]
        public async Task<IActionResult> DeleteDependency(dependency InputModel)
        {
            try
            {
                var result = await _taskManager.DeleteDependency(InputModel.id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
