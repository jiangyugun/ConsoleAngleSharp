namespace AngleSharpAPI.Entity
{
    /// <summary>
    /// 回傳結果-List
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DataResultModel<T>
    {
        /// <summary>
        /// 狀態碼
        /// </summary>
        public string StatusCode { get; set; } = "200";

        /// <summary>
        /// 錯誤訊息
        /// </summary>
        public string Msg { get; set; } = "失敗";

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Result { get; set; } = false;

        /// <summary>
        /// 資料
        /// </summary>
        public IEnumerable<T> DataList { get; set; } = Enumerable.Empty<T>();
    }
}
