using System;
using System.Net;

namespace DataModel.Share
{
    /// <summary>
    /// 驗證結果
    /// </summary>
    public class VerityResult
	{
		/// <summary>
		/// 狀態代碼
		/// 參閱HttpStatusCode 列舉 
		/// https://docs.microsoft.com/zh-tw/dotnet/api/system.net.httpstatuscode?view=netframework-4.8
		/// 或是
		/// 自訂HttpStatusCode(CustomHttpStatusCode)
		/// </summary>
		public HttpStatusCode StatusCode { get; set; }

		/// <summary>
		/// 訊息
		/// </summary>
		public string Message { get; set; }

		/// <summary>
		/// 資料結果
		/// </summary>
		public Object Payload { get; set; }

		/// <summary>
		/// Server回覆時間
		/// </summary>
		public DateTime ResponseTime { get; set; }

		public VerityResult()
		{
			ResponseTime = DateTime.Now;
		}
	}
}
