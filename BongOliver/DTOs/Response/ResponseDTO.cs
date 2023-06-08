namespace BongOliver.DTOs.Response
{
    public class ResponseDTO
    {
        public int code { get; set; } = 200;
        public string message { get; set; } = "Success";
        public int total { get; set; } = 0;
        public Object data { get; set; } = null;
    }
}
