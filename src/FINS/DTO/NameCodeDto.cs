namespace FINS.DTO
{
    public class NameCodeDto<T> : BaseDto<T>
    {
        public string Name { get; set; }

        public string Code { get; set; }
    }
}
