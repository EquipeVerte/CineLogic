namespace CineLogic.Controllers.Attributes
{
    public class AjaxError
    {
        public string Key { get; set; }
        public string[] Errors { get; set; }

        public AjaxError(string key, string[] errors)
        {
            Key = key;
            this.Errors = errors;
        }
    }
}