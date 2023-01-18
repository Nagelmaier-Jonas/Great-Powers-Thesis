namespace Model.Configuration;

public static class AppSettings{
    public static string DBPort{ get; set; } = "26280";
    public static string RabbitPort{ get; set; } = "5672";
    
    public static string RabbitPort2{ get; set; } = "15672";
    public static string IpAddress{ get; set; } = "localhost";
}