namespace Models
{
    public class User
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        //public string SmsCode { get; set; } - это думаю лишнее, смс код одноразовый же, в БД нужно только записать номер того кто зарегистрировался
    }
}
