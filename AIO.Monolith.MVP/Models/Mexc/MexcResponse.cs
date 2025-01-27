namespace App1.Models.Mexc;

public class MexcResponse<T>
{
    public bool Success { get; set; }
    public int Code { get; set; }
    public T? Data { get; set; }
}